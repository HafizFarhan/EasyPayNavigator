using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public User user { get; set; }
        public List<Company> CompanyList { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Load the existing product from the database by its ID
            user = await _unitOfWork.Repository.GetByIdAsync<User>(asNoTracking: false, id: id);
            

            if (user == null)
            {
                // Handle the case where the product doesn't exist
                return RedirectToPage("/Error");
            }
            
            
                CompanyList = await _unitOfWork.Repository.GetQueryable<Company>().Where(m => m.Id > 0).ToListAsync();
            
            return Page();
        }
        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {

            if (!ModelState.IsValid)
            {

                return Page();
            }

            try
            {
                // Load the existing product from the database by its ID
                var existingUser = await _unitOfWork.Repository.GetByIdAsync<User>(asNoTracking: false, id: id);

                if (existingUser == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }

                // Apply changes to the existing product
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
                existingUser.CompanyId = user.CompanyId;

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
