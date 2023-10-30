using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.InstallmentPlans
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<SelectListItem> ProductList { set; get; }
        public List<SelectListItem> ClientItems { set; get; }
        public List<SelectListItem> CompanyItems { set; get; }
        public EditModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public InstallmentPlan plan { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Load the existing product from the database by its ID
            plan = await _unitOfWork.Repository.GetByIdAsync<InstallmentPlan>(asNoTracking: false, id: id);

            if (plan == null)
            {
                // Handle the case where the product doesn't exist
                return RedirectToPage("/Error");
            }
            ProductList = _unitOfWork.Repository.GetQueryable<Product>().Select(p => new SelectListItem { Value = p.Name, Text = p.Name }).ToList();
            ClientItems = _unitOfWork.Repository.GetQueryable<Client>().Select(c => new SelectListItem { Value = c.Name, Text = c.Name }).ToList();
            CompanyItems = _unitOfWork.Repository.GetQueryable<Company>().Select(a => new SelectListItem { Value = a.CompanyName, Text = a.CompanyName }).ToList();

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
                var existingPlan = await _unitOfWork.Repository.GetByIdAsync<InstallmentPlan>(asNoTracking: false, id: id);

                if (existingPlan == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }
                var selectedProduct = _unitOfWork.Repository.GetQueryable<Product>().FirstOrDefault(m => m.Name == plan.ProductName);
                var selectedClient = _unitOfWork.Repository.GetQueryable<Client>().FirstOrDefault(m => m.Name == plan.ClientName);
                var selectedCompany = _unitOfWork.Repository.GetQueryable<Company>().FirstOrDefault(m => m.CompanyName == plan.CompanyName);

                // Apply changes to the existing product
                
                existingPlan.ProductName = selectedProduct.Name;
                existingPlan.ClientName = selectedClient.Name;
                existingPlan.TotalPrice = plan.TotalPrice;
                existingPlan.SalePrice = plan.SalePrice;
                existingPlan.ProductQty = plan.ProductQty;
                existingPlan.Status = plan.Status;
                existingPlan.Date = plan.Date;
                existingPlan.NoOfInstallments = plan.NoOfInstallments;
                existingPlan.InstallmentAmount = plan.InstallmentAmount;
                existingPlan.AdvancePayment = plan.AdvancePayment;
                existingPlan.CompanyId = selectedCompany.Id;
                existingPlan.CompanyName = selectedCompany.CompanyName;


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
