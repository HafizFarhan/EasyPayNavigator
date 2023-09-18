using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Manage.Client
{
    public class ClientIndexModel : PageModel
    {
        private readonly InstallmentDbContext _context;
        public ClientIndexModel(InstallmentDbContext context)
        {
            _context = context;

        }
        public IList<Clients> Clients { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Clients = await _context.Clients.ToListAsync();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            // Find the product with the given ID
            var client = _context.Clients.Find(id);

            if (client == null)
            {
                return NotFound(); // Product not found
            }

            // Remove the product from the database
            _context.Clients.Remove(client);
            _context.SaveChanges();

            return RedirectToPage("./ClientIndex");
        }
    }
}
