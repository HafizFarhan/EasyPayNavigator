using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Manage.Product
{
    public class ProductIndexModel : PageModel
    {
        private readonly InstallmentDbContext _context;
        public ProductIndexModel(InstallmentDbContext context)
        {
            _context = context;

        }
        public IList<Products> Products { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            // Find the product with the given ID
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound(); // Product not found
            }

            // Remove the product from the database
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToPage("./ProductIndex");
        }

    }
}
