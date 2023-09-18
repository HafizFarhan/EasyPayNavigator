using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Manage.InstallmentPayments
{
    public class EditModel : PageModel
    {
        private readonly InstallmentDbContext _context;

        public EditModel(InstallmentDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstallmentPayment Payment { get; set; }
        // Add a property for InstallmentPlans
        public List<InstallmentPlan> InstallmentPlans { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Payment = await _context.InstallmentPayment
                 .Include(p => p.InstallmentPlan)
                 .FirstOrDefaultAsync(m => m.PaymentID == id);

            if (Payment == null)
            {
                return NotFound();
            }
            // Load InstallmentPlans here
            InstallmentPlans = await _context.InstallmentPlan.ToListAsync();

            return Page();
        }

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

            _context.Attach(Payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(Payment.PaymentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./InstallmentPaymentIndex");
        }

        private bool PaymentExists(int id)
        {
            return _context.InstallmentPayment.Any(e => e.PaymentID == id);
        }
    }
}
