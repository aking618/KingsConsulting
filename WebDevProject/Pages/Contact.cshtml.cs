using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevProject.Pages
{
    public class ContactModel : PageModel
    {

        [BindProperty]
        public string EmailAddress { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            Console.WriteLine(EmailAddress);
        }
    }
}
