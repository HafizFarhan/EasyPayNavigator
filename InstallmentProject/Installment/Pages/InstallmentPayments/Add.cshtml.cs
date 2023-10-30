using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.InstallmentPayments
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddModel> _logger;

        [BindProperty]
        public InstallmentPayment payments { get; set; }
        [BindProperty(SupportsGet = true)]
        public int InstallmentPlanId { get; set; }
        public AddModel(IUnitOfWork unitOfWork, ILogger<AddModel> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public IActionResult OnGet(int InstallmentPlanId)
        {
            this.InstallmentPlanId = InstallmentPlanId;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return to the form with validation errors.
                return Page();
            }

            try
            {
                
                payments.InstallmentPlanId = InstallmentPlanId;
                // Add the new product to the repository
                _unitOfWork.Repository.Add(payments);

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/InstallmentPayments/Index");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, "An error occurred while processing the payment.");

                // Handle errors
                return RedirectToPage("/Error");
            }
        }
    }
}
