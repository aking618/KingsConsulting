using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KingsConsulting.Model;

namespace KingsConsulting.Pages
{
    public class ContactModel : PageModel
    {

        [BindProperty]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Please select an option.")]
        public string CategoryOption { get; set; } = "1";

        [BindProperty]
        [Required(ErrorMessage = "Please select an option.")]
        public string BudgetOption { get; set; } = "1";

        [BindProperty]
        [Display(Name = "Message")]
        [Required(ErrorMessage = "Please enter a message.")]
        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPostCancel()
        {
            ModelState.Clear();
            Name = string.Empty;
            Email = string.Empty;
            Message = string.Empty;
            CategoryOption = "1";
            BudgetOption = "1";

            return Page();
        }

        public IActionResult OnPostSubmit()
        {
            if (ModelState.IsValid)
            {
                return Page();
            }

            TempData[Constants.AlertSuccess] = "Thank you for your message. We will get back to you shortly.";
            return Page();
        }
    }
}
