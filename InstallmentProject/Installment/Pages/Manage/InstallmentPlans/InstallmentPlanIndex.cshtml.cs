using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Manage.InstallmentPlans
{
    public class InstallmentPlanIndexModel : PageModel
    {
        private readonly InstallmentDbContext _context;
        public InstallmentPlanIndexModel(InstallmentDbContext context)
        {
            _context = context;

        }
        public List<InstallmentPlan> InstallmentPlans { get; set; }

        public void OnGet()
        {
            try
            {
                var installmentPlansWithDetails = _context.InstallmentPlan
                    .Join(
                        _context.Products,
                        installmentPlan => installmentPlan.ProductId,
                        product => product.ProductId,
                        (installmentPlan, product) => new { InstallmentPlan = installmentPlan, Product = product }
                    )
                    .Join(
                        _context.Clients,
                        combined => combined.InstallmentPlan.ClientId,
                        client => client.ClientId,
                        (combined, client) => new
                        {
                            InstallmentPlan = combined.InstallmentPlan,
                            ProductName = combined.Product.Name,
                            ClientName = client.Name,
                            // Map other properties as needed
                        }
                    )
                    .ToList();

                // Check if any data was retrieved
                if (installmentPlansWithDetails.Any())
                {
                    // Update the InstallmentPlan instances with the related data
                    foreach (var combinedData in installmentPlansWithDetails)
                    {
                        combinedData.InstallmentPlan.ProductName = combinedData.ProductName;
                        combinedData.InstallmentPlan.ClientName = combinedData.ClientName;
                    }

                    InstallmentPlans = installmentPlansWithDetails.Select(data => data.InstallmentPlan).ToList();
                }
                else
                {
                    // Handle the case where no data was found
                    // You can set an appropriate message or take other actions.
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, e.g., log the error.
            }

        }
        
    }
}
