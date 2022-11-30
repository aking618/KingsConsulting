using System.ComponentModel.DataAnnotations;
using KingsConsulting.Entities;
using KingsConsulting.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace KingsConsulting.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public UserInfo MyUserInfo { get; set; }

        private readonly IConfiguration configuration;

        public LoginModel(IConfiguration configuration)
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
            Email = string.Empty;
            Password = string.Empty;

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
                // open connection and select stored procedure
                SqlDataAdapter sqlDataValidator = new SqlDataAdapter("spValidateUser", sqlConn);
                sqlDataValidator.SelectCommand.CommandType = CommandType.StoredProcedure;

                // add parameters to stored procedure
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@email", Email);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@passcode", Password);

                try
                {
                    DataSet dsUserRecord = new DataSet();

                    // fill dataset with results from stored procedure
                    sqlDataValidator.Fill(dsUserRecord);

                    // check if dataset is empty
                    if (dsUserRecord.Tables[0].Rows.Count == 0)
                    {
                        Message = "Invalid login, please try again";
                        return Page();
                    }

                    // get user information from dataset
                    MyUserInfo = new UserInfo();
                    MyUserInfo.UserId = Convert.ToInt32(dsUserRecord.Tables[0].Rows[0]["userId"]);
                    MyUserInfo.Email = dsUserRecord.Tables[0].Rows[0]["email"].ToString();
                    MyUserInfo.FirstName = dsUserRecord.Tables[0].Rows[0]["firstName"].ToString();
                    MyUserInfo.LastName = dsUserRecord.Tables[0].Rows[0]["lastName"].ToString();
                    MyUserInfo.PhoneNumber = dsUserRecord.Tables[0].Rows[0]["phoneNumber"].ToString();

                    // set session variables
                    HttpContext.Session.SetString("UserId", MyUserInfo.UserId.ToString());
                    HttpContext.Session.SetString("Email", MyUserInfo.Email!);
                    HttpContext.Session.SetString("FirstName", MyUserInfo.FirstName!);
                    HttpContext.Session.SetString("LastName", MyUserInfo.LastName!);
                    HttpContext.Session.SetString("PhoneNumber", MyUserInfo.PhoneNumber!);

                    // reset model state
                    Email = string.Empty;
                    Password = string.Empty;
                    ModelState.Clear();

                    TempData[Constants.AlertSuccess] = "Login Successful!";

                    return Redirect("/Index");

                }
                catch(Exception e)
                {
                    Message = e.Message;
                    TempData[Constants.AlertWarning] = "Unable to login.";
                    return Page();
                }
            };
        }
    }
}
