using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.Manage.Client
{
    public class CreateModel : PageModel
    {
        private readonly InstallmentDbContext _context;
        public CreateModel(InstallmentDbContext context)
        {
            _context = context;
            Client = new Clients();
        }
        [BindProperty]
        public Clients Client { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Clients.Add(Client);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ClientIndex");
        }
        public void OnGet()
        {
        }
    }
}
