using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KingsConsulting.Pages
{
    public class AccountModel : PageModel
    {
        public bool IsUserActive { get; set; } = false;

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public void OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            FirstName = HttpContext.Session.GetString("FirstName");
            LastName = HttpContext.Session.GetString("LastName");

            if (Email != null && Email != "")
            {
                IsUserActive = true;
            }

            System.Diagnostics.Debug.WriteLine("Yoooo");
        }

        public IActionResult OnPostSubmit()
        {
            HttpContext.Session.Clear();

            System.Diagnostics.Debug.WriteLine("Hello from here");

            return Redirect("/Login");
        }
    }
}
