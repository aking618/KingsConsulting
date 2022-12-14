using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using KingsConsulting.Entities;
using KingsConsulting.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KingsConsulting.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string StatusMessage { get; set; } = string.Empty;

        public UserInfo NewUserInfo { get; set; }

        private readonly IConfiguration configuration;

        public RegisterModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// Clears the form and model.
        /// </summary>
        public IActionResult OnPostCancel()
        {
            ModelState.Clear();
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            PhoneNumber = string.Empty;
            StatusMessage = string.Empty;

            return Page();
        }

        public IActionResult OnPostSubmit()
        {
            // check if model state is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // establish connection to database
            var strConn = configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConn = new(strConn))
            {
                // select stored procedure
                SqlDataAdapter sqlDataValidator = new SqlDataAdapter("spCreateUser", sqlConn);
                sqlDataValidator.SelectCommand.CommandType = CommandType.StoredProcedure;

                // add parameters
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@firstName", FirstName);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@lastName", LastName);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@email", Email);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@passcode", Password);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@phone", PhoneNumber);

                try
                {
                    DataSet dsUserRecord = new DataSet();

                    // open connection and fill dataset
                    sqlDataValidator.Fill(dsUserRecord);

                    // check if dataset is empty
                    if (dsUserRecord.Tables[0].Rows.Count == 0)
                    {
                        StatusMessage = "Invalid registration, please try again";
                        return Page();
                    }

                    // clear form and model
                    FirstName = string.Empty;
                    LastName = string.Empty;
                    Email = string.Empty;
                    PhoneNumber = string.Empty;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;
                    ModelState.Clear();

                    TempData[Constants.AlertSuccess] = "<strong>Account Creation Successful!</strong> Please login now!";

                    return Redirect("/Login");

                }
                catch (Exception e)
                {
                    StatusMessage = e.Message;
                    TempData[Constants.AlertWarning] = "Unable to create account.";
                    return Page();
                }
            };
        }

    }
}
