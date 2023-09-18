using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Installment.Pages.Manage.InstallmentPayments
{
    public class CreateModel : PageModel
    {
        private readonly InstallmentDbContext _context;

        public CreateModel(InstallmentDbContext context)
        {
            _context = context;
            // Initialize Payment.InstallmentPlan
            Payment = new InstallmentPayment
            {
                InstallmentPlan = new InstallmentPlan() // Initialize the InstallmentPlan property
            };
            InstallmentPlans = new List<InstallmentPlan>
            {
            new InstallmentPlan {  AdvancePayment = 2000,TotalPrice=2000000 },
            new InstallmentPlan {  AdvancePayment = 2000,TotalPrice=2000000 },
            
            
            };

        }
        [BindProperty]
        public InstallmentPayment Payment { get; set; }
        public List<InstallmentPlan> InstallmentPlans { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // You can log or debug the error here
                        // For example, you can use the built-in ILogger
                        
                    }
                }
                return Page();
            }
            // Ensure that InstallmentPlanId is set correctly based on the selected value in the dropdown
            Payment.InstallmentPlan = InstallmentPlans.FirstOrDefault(plan => plan.InstallmentPlanId == Payment.InstallmentPlan.InstallmentPlanId); 
            _context.InstallmentPayment.Add(Payment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./InstallmentPaymentIndex");
        }
        public void OnGet()
        {
        }
    }
}
