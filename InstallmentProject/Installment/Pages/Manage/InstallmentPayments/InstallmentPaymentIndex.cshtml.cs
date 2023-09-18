using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Manage.InstallmentPayments
{
    public class InstallmentPaymentIndexModel : PageModel
    {
        private readonly InstallmentDbContext _context;
        public InstallmentPaymentIndexModel(InstallmentDbContext context)
        {
            _context = context;
        }
        public IList<InstallmentPayment> InstallmentPayments { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            InstallmentPayments = await _context.InstallmentPayment
        .Include(payment => payment.InstallmentPlan)
        .ToListAsync();
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            // Find the product with the given ID
            var payment = _context.InstallmentPayment.Find(id);

            if (payment == null)
            {
                return NotFound(); // Product not found
            }

            // Remove the product from the database
            _context.InstallmentPayment.Remove(payment);
            _context.SaveChanges();

            return RedirectToPage("./InstallmentPaymentIndex");
        }

    }
}
