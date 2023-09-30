using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.InstallmentPayments
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<InstallmentPayment> payments { get; set; }
        public IndexModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync(int InstallmentPlanId)
        {
            try
            {
                payments = await _unitOfWork.Repository.GetQueryable<InstallmentPayment>().Where(m => m.InstallmentPlanId == InstallmentPlanId).ToListAsync();
                // Check if the payments list is null or empty
                if (payments == null || !payments.Any())
                {
                    // Handle the case where there are no payments
                    // You can set payments to an empty list or perform other actions.
                    payments = new List<InstallmentPayment>(); // Set to an empty list
                }
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
                var existingpayment = await _unitOfWork.Repository.GetByIdAsync<InstallmentPayment>(asNoTracking: false, id: id);

                if (existingpayment == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }

                // Remove the product from the repository
                _unitOfWork.Repository.HardDelete(existingpayment);

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
