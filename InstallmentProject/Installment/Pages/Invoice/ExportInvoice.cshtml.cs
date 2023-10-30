using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages
{
    public class ExportInvoiceModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExportInvoiceModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Company> Companies { get; set; }
        public InstallmentPlan plans { get; set; }
        public InstallmentPlan installmentPlans { get; set; }
        public List<InstallmentPayment> InstallmentPayments { get; set; }

        public async Task<IActionResult> OnGetAsync(int companyId, int planId)
        {
            Companies = await _unitOfWork.Repository
               .GetQueryable<Company>()
               .Where(company => company.Id == companyId)
               .ToListAsync();
            // Get installment plans for the specified companyId
            plans = await _unitOfWork.Repository
               .GetQueryable<InstallmentPlan>()
               .FirstOrDefaultAsync(plan => plan.CompanyId == companyId && plan.Id == planId);

            // Get installment payments for the same companyId
            InstallmentPayments = await _unitOfWork.Repository
                .GetQueryable<InstallmentPayment>()
                .Where(payment => payment.InstallmentPlanId == planId)
                .ToListAsync();
            // Calculate the total installments and total amount paid
            installmentPlans = await _unitOfWork.Repository
        .GetQueryable<InstallmentPlan>()
        .Where(plan => plan.CompanyId == companyId && plan.Id == planId)
        .GroupJoin(
            _unitOfWork.Repository.GetQueryable<InstallmentPayment>(),
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
            })
        .FirstOrDefaultAsync();
            // You can use 'plans' and 'payments' to generate the invoice

            return Page();
        }
    }
}
