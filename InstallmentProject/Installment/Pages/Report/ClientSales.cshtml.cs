using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages.Report
{
    public class ClientSalesModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<InstallmentPlan> plans { get; set; }
        public ClientSalesModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync ()
        {
            try
            {
                //plans = await _unitOfWork.Repository.GetQueryable<InstallmentPlan>().Where(m => m.CompanyId == 1).ToListAsync();
                plans = await _unitOfWork.Repository.GetQueryable<InstallmentPlan>().Where(m => m.Id > 0).
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
                        SalePrice = plan.SalePrice,
                        ProductQty = plan.ProductQty,
                        Status = plan.Status,
                        AdvancePayment = plan.AdvancePayment,
                        CompanyId = plan.CompanyId,

                        // Calculate the total installments and total amount paid
                        NoInstallments = payments.Count(),
                        TotalPaid = payments.Sum(payment => payment.RecentAmount) + plan.AdvancePayment,
                        RemainingAmount = plan.SalePrice - (payments.Sum(payment => payment.RecentAmount) + plan.AdvancePayment)

                    }).ToListAsync();

                return Page();
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }
    }
}
