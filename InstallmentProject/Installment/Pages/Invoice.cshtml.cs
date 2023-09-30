using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Installment.Pages
{
    public class InvoiceModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<InstallmentPlan> plans { get; set; }
        public List<InstallmentPayment> InstallmentPayments { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
                plans = await _unitOfWork.Repository
    .GetQueryable<InstallmentPlan>()
    .Where(m => m.CompanyId == 1)
    .GroupJoin(
    _unitOfWork.Repository.GetQueryable<InstallmentPayment>(),
    plan => plan.Id,
    payment => payment.InstallmentPlanId,
    (plan, payments) => new
    {
        Plan = plan,
        Payments = payments.ToList()
    })
    .Select(group => new InstallmentPlan
    {
    Id = group.Plan.Id,
    ProductName = group.Plan.ProductName,
    ClientName = group.Plan.ClientName,
    TotalPrice = group.Plan.TotalPrice,
    AdvancePayment = group.Plan.AdvancePayment,
    CompanyId = group.Plan.CompanyId,
    NoInstallments = group.Payments.Count(),
    TotalPaid = group.Payments.Sum(payment => payment.RecentAmount) + group.Plan.AdvancePayment,
    RemainingAmount = group.Plan.TotalPrice - (group.Payments.Sum(payment => payment.RecentAmount) + group.Plan.AdvancePayment)
    })
    .ToListAsync();
            return Page();
        }
    }
}
