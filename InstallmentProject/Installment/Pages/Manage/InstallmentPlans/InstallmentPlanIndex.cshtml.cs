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
            
                       

                
            

        }
        
    }
}
