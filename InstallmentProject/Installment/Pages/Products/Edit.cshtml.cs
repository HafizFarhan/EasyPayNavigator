using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public EditModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Product product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
            {
                // Load the existing product from the database by its ID
                product = await _unitOfWork.Repository.GetByIdAsync<Product>(asNoTracking: false, id: id);

                if (product == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }

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
                var existingProduct = await _unitOfWork.Repository.GetByIdAsync<Product>(asNoTracking: false, id: id);

                if (existingProduct == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }

                // Apply changes to the existing product
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;

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
