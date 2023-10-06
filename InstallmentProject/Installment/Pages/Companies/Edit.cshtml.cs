using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Installment.Pages.Companies
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Company company { get; set; }
        [BindProperty]
        public IFormFile LogoImageFile { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Load the existing product from the database by its ID
            company = await _unitOfWork.Repository.GetByIdAsync<Company>(asNoTracking: false, id: id);

            if (company == null)
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
                // Load the existing company from the database by its ID
                var existingCompany = await _unitOfWork.Repository.GetByIdAsync<Company>(asNoTracking: false, id: id);

                if (existingCompany == null)
                {
                    // Handle the case where the company doesn't exist
                    return RedirectToPage("/Error");
                }

                // Apply changes to the existing company
                existingCompany.CompanyName = company.CompanyName;
                existingCompany.Email = company.Email;
                existingCompany.Phone = company.Phone;
                existingCompany.Address = company.Address;

                if (LogoImageFile != null && LogoImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await LogoImageFile.CopyToAsync(memoryStream);
                        existingCompany.LogoImage = memoryStream.ToArray(); // Assign the byte array directly
                    }
                }

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
