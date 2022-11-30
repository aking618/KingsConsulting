using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KingsConsulting.Model;

namespace KingsConsulting.Pages
{
    public class AccountModel : PageModel
    {
        public bool IsUserActive { get; set; } = false;

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Formats the phone number to a standard format.
        /// </summary>
        public string FormatPhoneNumber(string phone)
        {
            Regex regex = new Regex(@"[^\d]");
            phone = regex.Replace(phone, "");
            string format = "###-###-####";
            phone = Convert.ToInt64(phone).ToString(format);
            return phone;
        }

        public void OnGet()
        {
            // Retrieve the user's information from session.
            Email = HttpContext.Session.GetString("Email");
            FirstName = HttpContext.Session.GetString("FirstName");
            LastName = HttpContext.Session.GetString("LastName");
            PhoneNumber = HttpContext.Session.GetString("PhoneNumber");

            // Determine if the user is active.
            if (Email != null && Email != "")
            {
                IsUserActive = true;
            }
        }

        public IActionResult OnPostSubmit()
        {
            // Clear the user's session.
            HttpContext.Session.Clear();

            // Setup a message to display to the user.
            TempData[Constants.AlertSuccess] = Email == string.Empty ? null : "Logout Successful!";

            return Redirect("/Login");
        }
    }
}
