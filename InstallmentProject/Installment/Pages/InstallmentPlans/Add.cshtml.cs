using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.InstallmentPlans
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public  InstallmentPlan plan { get; set; }
        
        public List<SelectListItem> ProductItems { set; get; }
        public List<SelectListItem> ClientItems { set; get; }

        public AddModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync()
        {

            ProductItems = _unitOfWork.Repository.GetQueryable<Product>().Select(p => new SelectListItem { Value = p.Name, Text = p.Name }).ToList();
            ClientItems = _unitOfWork.Repository.GetQueryable<Client>().Select(c => new SelectListItem { Value = c.Name, Text = c.Name }).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        // Log or print ModelState errors for debugging
                        Console.WriteLine($"Model error for {key}: {error.ErrorMessage}");
                    }
                }
            }

            try
            {
                var selectedProduct = _unitOfWork.Repository.GetQueryable<Product>().FirstOrDefault(m => m.Name == plan.ProductName);
                var selectedClient = _unitOfWork.Repository.GetQueryable<Client>().FirstOrDefault(m => m.Name == plan.ClientName);
                if (selectedProduct != null && selectedClient != null)
                {
                    plan.ProductId = selectedProduct.Id; 
                    plan.ClientId = selectedClient.Id;
                    plan.CompanyId = Global.Instance.GetCompanyId();    
                }
                else
                {
                    // Handle the case where the selected product name doesn't exist
                    ModelState.AddModelError("plan.ProductName", "Selected product not found.");
                    return Page(); // Return to the page to display the error
                }
                // Add the new product to the repository
                _unitOfWork.Repository.Add(plan);

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/InstallmentPlans/Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }

    }
}
