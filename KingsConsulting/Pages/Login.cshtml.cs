using System.ComponentModel.DataAnnotations;
using KingsConsulting.Models;
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
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 20 characters.")]
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

        public IActionResult OnPostSubmit()
        {
            // check if model state is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var strConn = configuration.GetConnectionString("DefaultConnection");

            using SqlConnection sqlConn = new(strConn);
            SqlCommand selectUserCommand = new SqlCommand("spSelectUserInfo", sqlConn);
            selectUserCommand.CommandType = CommandType.StoredProcedure;

            selectUserCommand.Parameters.AddWithValue("@Email", Email);
            selectUserCommand.Parameters.AddWithValue("@Password", Password);

            try
            {
                sqlConn.Open();
                SqlDataReader reader = selectUserCommand.ExecuteReader();
                var result = -1;
                if (reader.Read())
                    result = int.Parse(reader["userId"].ToString());

                if (result <= 0)
                {
                    Message = "The user credentials provided do not match any current users";
                    return Page();
                }

                TempData["userId"] = result;
                return RedirectToPage("Index");

            }
            catch (Exception e)
            {
                Message = e.Message;
                return Page();
            }

        }
    }
}
