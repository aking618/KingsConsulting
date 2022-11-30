﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KingsConsulting.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Message { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Retrieve the user's information from session.
            var userId = HttpContext.Session.GetString("UserId");
            var email = HttpContext.Session.GetString("Email");
            var firstName = HttpContext.Session.GetString("FirstName");
            var lastName = HttpContext.Session.GetString("LastName");

            // Determine if the user is active.
            if (email != null && email != "")
            {
                Message = "Welcome " + firstName + ", to <br/>";
            }
        }
    }
}