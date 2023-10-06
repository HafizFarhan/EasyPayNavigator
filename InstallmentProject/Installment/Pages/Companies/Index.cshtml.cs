using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Companies
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<Company> Companies { get; set; }
        public IndexModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Companies = await _unitOfWork.Repository.GetQueryable<Company>().Where(m => m.Id > 0).ToListAsync();

                return Page();
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                // Load the existing product from the database by its ID
                var existingCompany = await _unitOfWork.Repository.GetByIdAsync<Company>(asNoTracking: false, id: id);

                if (existingCompany == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }

                // Remove the product from the repository
                _unitOfWork.Repository.HardDelete(existingCompany);

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/Companies/Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }

    }
}
