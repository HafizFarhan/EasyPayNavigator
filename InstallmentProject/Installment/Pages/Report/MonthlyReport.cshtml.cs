using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Installment.Pages.Report
{
    public class MonthlyReportModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public List<dynamic> clients { get; set; }
        [BindProperty]
        public DateTime? fromDate { get; set; }

        [BindProperty]
        public DateTime? toDate { get; set; }
        public MonthlyReportModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public dynamic MonthlyReportData { get; set; }
        public List<DateTime> PaymentDates { get; set; }
        [BindProperty]
        public DateTime? selectedDate { get; set; }
        public List<InstallmentPayment> payments { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Get distinct payment dates from the payments table
                PaymentDates = _unitOfWork.Repository.GetQueryable<InstallmentPayment>()
                    .Select(payment => payment.Date.Date)
                    .Distinct()
                    .ToList();
                return Page();
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }
        public IActionResult OnPost()
        {
            // Repopulate PaymentDates even in OnPost
            PaymentDates = _unitOfWork.Repository.GetQueryable<InstallmentPayment>()
                .Select(payment => payment.Date.Date)
                .Distinct()
                .ToList();

            if (fromDate.HasValue && toDate.HasValue)
            {
                MonthlyReportData = _unitOfWork.Repository.GetQueryable<InstallmentPlan>()
                    .Join(
                        _unitOfWork.Repository.GetQueryable<InstallmentPayment>(),
                        plan => plan.Id,
                        payment => payment.InstallmentPlanId,
                        (plan, payment) => new
                        {
                            ProductName = plan.ProductName,
                            ClientName = plan.ClientName,
                            NoOfInstallments = plan.NoOfInstallments,
                            InstallmentAmount = plan.InstallmentAmount,
                            TotalPaid = payment.RecentAmount,
                            Status = payment.Status,
                            Date = payment.Date
                        })
                    .Where(data =>
                        data.Date.Date >= fromDate.Value.Date &&
                        data.Date.Date <= toDate.Value.Date)
                    .ToList();
            }

            // Handle the case where no date range is selected
            return Page();
        }


    }
}
