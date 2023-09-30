using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Installment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<Product> Products { get; set; }
       
        public IndexModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                 Products = await _unitOfWork.Repository.GetQueryable<Product>().Where(m => m.CompanyId == 1).ToListAsync();
                
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
                var existingProduct = await _unitOfWork.Repository.GetByIdAsync<Product>(asNoTracking: false, id: id);

                if (existingProduct == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }

                // Remove the product from the repository
                _unitOfWork.Repository.HardDelete(existingProduct);

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/Products/Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }



    }   
}
