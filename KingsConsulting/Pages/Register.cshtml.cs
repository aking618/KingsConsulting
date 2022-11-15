using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using KingsConsulting.Entities;
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
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 20 characters.")]
        public string Password { get; set; } = string.Empty;

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

        public IActionResult OnPostCancel()
        {
            ModelState.Clear();
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            StatusMessage = string.Empty;

            return Page();
        }

        public IActionResult OnPostSubmit()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var strConn = configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConn = new(strConn))
            {
                SqlDataAdapter sqlDataValidator = new SqlDataAdapter("spCreateUser", sqlConn);
                sqlDataValidator.SelectCommand.CommandType = CommandType.StoredProcedure;

                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@firstName", FirstName);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@lastName", LastName);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@email", Email);
                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@passcode", Password);

                try
                {
                    DataSet dsUserRecord = new DataSet();

                    sqlDataValidator.Fill(dsUserRecord);

                    if (dsUserRecord.Tables[0].Rows.Count == 0)
                    {
                        StatusMessage = "Invalid registration, please try again";
                        return Page();
                    }

                    NewUserInfo = new UserInfo();
                    NewUserInfo.UserId = Convert.ToInt32(dsUserRecord.Tables[0].Rows[0]["userId"]);
                    NewUserInfo.Email = dsUserRecord.Tables[0].Rows[0]["email"].ToString();
                    NewUserInfo.FirstName = dsUserRecord.Tables[0].Rows[0]["firstName"].ToString();
                    NewUserInfo.LastName = dsUserRecord.Tables[0].Rows[0]["lastName"].ToString();

                    HttpContext.Session.SetString("UserId", NewUserInfo.UserId.ToString());
                    HttpContext.Session.SetString("Email", NewUserInfo.Email!);
                    HttpContext.Session.SetString("FirstName", NewUserInfo.FirstName!);
                    HttpContext.Session.SetString("LastName", NewUserInfo.LastName!);

                    FirstName = string.Empty;
                    LastName = string.Empty;
                    Email = string.Empty;
                    Password = string.Empty;
                    ModelState.Clear();

                    return Redirect("/Index");

                }
                catch (Exception e)
                {
                    StatusMessage = e.Message;
                    return Page();
                }
            };
        }

    }
}
