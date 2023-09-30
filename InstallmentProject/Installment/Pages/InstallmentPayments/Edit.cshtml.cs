using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.InstallmentPayments
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public InstallmentPayment payments { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Load the existing product from the database by its ID
            payments = await _unitOfWork.Repository.GetByIdAsync<InstallmentPayment>(asNoTracking: false, id: id);

            if (payments == null)
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
                var existingPayment = await _unitOfWork.Repository.GetByIdAsync<InstallmentPayment>(asNoTracking: false, id: id);

                if (existingPayment == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }

                // Apply changes to the existing product
                existingPayment.RecentAmount = payments.RecentAmount;
                existingPayment.Date = payments.Date;
                existingPayment.Status = payments.Status;

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/InstallmentPayments/Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }
    }
}
