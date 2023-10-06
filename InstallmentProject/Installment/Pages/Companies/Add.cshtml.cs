using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.Companies
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Company company { get; set; }
        // Add a property for the uploaded file
        [BindProperty]
        public IFormFile LogoImageFile { get; set; }
        public AddModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            // Initialize the NewProduct object or perform any other setup as needed.
            company = new Company();
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
                if (LogoImageFile != null && LogoImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await LogoImageFile.CopyToAsync(memoryStream);
                        company.LogoImage = memoryStream.ToArray();
                    }
                }

                // Add the new company to the repository
                _unitOfWork.Repository.Add(company);

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/Companies/Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }
    }
}
