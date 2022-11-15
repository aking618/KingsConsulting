using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

using KingsConsulting.Model;

namespace KingsConsulting.Pages
{
    public class SpecificServiceModel : PageModel
    {

        private readonly IConfiguration configuration;

        public SpecificServiceModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [BindProperty]
        public ServiceDetails serviceDetails { get; set; }

        public void OnGet(int postId)
        {

            var strConn = configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection sqlConn = new(strConn))
            {
                SqlDataAdapter sqlDataValidator = new SqlDataAdapter("spGetSpecificServiceByCategory", sqlConn);
                sqlDataValidator.SelectCommand.CommandType = CommandType.StoredProcedure;

                sqlDataValidator.SelectCommand.Parameters.AddWithValue("@serviceCategoryID", postId);

                try
                {
                    DataSet dsUserRecord = new DataSet();

                    sqlDataValidator.Fill(dsUserRecord);

                    var table = dsUserRecord.Tables[0];
                    serviceDetails = new ServiceDetails();
                    serviceDetails.categories = new Category[table.Rows.Count];
                    serviceDetails.imageUrls = new string[table.Rows.Count];

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var row = table.Rows[i];
                        
                        serviceDetails.Title = row["ServiceCategoryName"].ToString();
                        serviceDetails.categories[i] = new Category(
                                row["serviceName"].ToString(),
                                row["serviceDescription"].ToString(),
                                Convert.ToInt32(row["servicePrice"])
                            );
                        serviceDetails.imageUrls[i] = "https://picsum.photos/500?random=" + i + 1;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
