using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.InstallmentPlans
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<InstallmentPlan> plans { get; set; }
        public IndexModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                //plans = await _unitOfWork.Repository.GetQueryable<InstallmentPlan>().Where(m => m.CompanyId == 1).ToListAsync();
                plans = await _unitOfWork.Repository.GetQueryable<InstallmentPlan>().Where(m => m.CompanyId == 1).
                    GroupJoin(_unitOfWork.Repository.GetQueryable<InstallmentPayment>(),
                    plan => plan.Id,
                    payment => payment.InstallmentPlanId,
                    (plan, payments) => new InstallmentPlan
                    {
                        // Include other properties from InstallmentPlan here
                        Id = plan.Id,
                        ProductName = plan.ProductName,
                        ClientName = plan.ClientName,
                        TotalPrice = plan.TotalPrice,
                        AdvancePayment = plan.AdvancePayment,
                        CompanyId = plan.CompanyId,

                        // Calculate the total installments and total amount paid
                        NoInstallments = payments.Count(),
                        TotalPaid = payments.Sum(payment => payment.RecentAmount) + plan.AdvancePayment,
                        RemainingAmount = plan.TotalPrice - (payments.Sum(payment => payment.RecentAmount) + plan.AdvancePayment)

                    }).ToListAsync();

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
                var existingProduct = await _unitOfWork.Repository.GetByIdAsync<InstallmentPlan>(asNoTracking: false, id: id);

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
