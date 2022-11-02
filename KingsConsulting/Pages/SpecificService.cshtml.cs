using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using KingsConsulting.Model;

namespace KingsConsulting.Pages
{
    public class SpecificServiceModel : PageModel
    {
        [BindProperty]
        public ServiceDetails serviceDetails { get; set; }

        public void OnGet(int postId)
        {
            serviceDetails = new ServiceDetails();

            switch (postId)
            {
                case 0:
                    
                    serviceDetails.Title = "General Consulting";

                    serviceDetails.categories = new Category[2];
                    serviceDetails.categories[0] = new Category("Web Consult", "Need advice / help with a current web project?", 123);
                    serviceDetails.categories[1] = new Category("Mobile Consult", "Need advice / help with a current mobile project?", 456);

                    serviceDetails.imageUrls = new string[2];
                    for (int i = 0; i < 2; i++)
                    {
                        serviceDetails.imageUrls[i] = "https://picsum.photos/500?random=" + i + 1;
                    }
                    break;
                case 1:
                    serviceDetails.Title = "Web Development";

                    serviceDetails.categories = new Category[3];
                    serviceDetails.categories[0] = new Category("Mockup", "Need advice / help with a current web project?", 123);
                    serviceDetails.categories[1] = new Category("Home Page", "Need advice / help with a current mobile project?", 456);
                    serviceDetails.categories[2] = new Category("X - Pages", "Need advice / help with a current mobile project?", 789);

                    serviceDetails.imageUrls = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        serviceDetails.imageUrls[i] = "https://picsum.photos/500?random=" + i + 1;
                    }
                    break;
                case 2:
                    serviceDetails.Title = "Mobile Development";

                    serviceDetails.categories = new Category[3];
                    serviceDetails.categories[0] = new Category("Mockup", "Need advice / help with a current web project?", 123);
                    serviceDetails.categories[1] = new Category("Home Screen", "Need advice / help with a current mobile project?", 456);
                    serviceDetails.categories[2] = new Category("X - Screen", "Need advice / help with a current mobile project?", 789);

                    serviceDetails.imageUrls = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        serviceDetails.imageUrls[i] = "https://picsum.photos/500?random=" + i + 1;
                    }
                    break;
            }
        }
    }
}
