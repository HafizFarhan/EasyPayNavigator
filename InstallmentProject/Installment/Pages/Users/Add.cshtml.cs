using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Users
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public User users { get; set; }
        public List<Company> Companies { get; set; }
        public AddModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task OnGetAsync()
        {
            // Initialize the NewProduct object or perform any other setup as needed.
            users = new User
            {
                Username = string.Empty,
                Password = string.Empty
            };
            Companies = await _unitOfWork.Repository.GetQueryable<Company>().Where(m => m.Id > 0).ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return to the form with validation errors.
                return Page();
            }

            try
            {
                // Add the new product to the repository
                _unitOfWork.Repository.Add(users);

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/Users/Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }
    }
}
