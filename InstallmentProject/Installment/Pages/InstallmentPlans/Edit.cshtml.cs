using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.InstallmentPlans
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

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

                // Apply changes to the existing product
                existingPlan.ProductName = plan.ProductName;
                existingPlan.ClientName = plan.ClientName;
                existingPlan.TotalPrice = plan.TotalPrice;
                existingPlan.AdvancePayment = plan.AdvancePayment;
                existingPlan.CompanyId = plan.CompanyId;

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
