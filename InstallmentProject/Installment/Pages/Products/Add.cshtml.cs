using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Installment.Pages.Products
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<SelectListItem> CompanyItems { set; get; }
        [BindProperty]
        public Product product { get; set; }

        public AddModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            // Initialize the NewProduct object or perform any other setup as needed.
            product = new Product();
            CompanyItems = _unitOfWork.Repository.GetQueryable<Company>().Select(a => new SelectListItem { Value = a.CompanyName, Text = a.CompanyName }).ToList();
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
                var selectedCompany = _unitOfWork.Repository.GetQueryable<Company>().FirstOrDefault(m => m.CompanyName == product.CompanyName);

                product.CompanyId = selectedCompany.Id;

                // Add the new product to the repository
                _unitOfWork.Repository.Add(product);

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
