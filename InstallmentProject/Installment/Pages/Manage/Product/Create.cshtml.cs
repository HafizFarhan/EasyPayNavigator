using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.Manage.Product
{
    public class CreateModel : PageModel
    {
        private readonly InstallmentDbContext _context;
        public CreateModel(InstallmentDbContext context)
        {
            _context = context;
            Product = new Products();
        }
        [BindProperty]
        public Products Product { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ProductIndex");
        }
        public void OnGet()
        {
        }
    }
}

