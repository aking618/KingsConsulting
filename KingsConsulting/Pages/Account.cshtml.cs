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
            Email = HttpContext.Session.GetString("Email");
            FirstName = HttpContext.Session.GetString("FirstName");
            LastName = HttpContext.Session.GetString("LastName");
            PhoneNumber = HttpContext.Session.GetString("PhoneNumber");

            if (Email != null && Email != "")
            {
                IsUserActive = true;
            }

            System.Diagnostics.Debug.WriteLine("Yoooo");
        }

        public IActionResult OnPostSubmit()
        {
            HttpContext.Session.Clear();

            TempData[Constants.AlertSuccess] = Email == string.Empty ? null : "Logout Successful!";

            return Redirect("/Login");
        }
    }
}
