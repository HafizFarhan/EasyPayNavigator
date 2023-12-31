using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Installment.Pages.Clients
{
    public class AddModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Client client { get; set; }
        public List<SelectListItem> CompanyItems { set; get; }
        public AddModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
                // Initialize the NewProduct object or perform any other setup as needed.
                client = new Client();
            CompanyItems = _unitOfWork.Repository.GetQueryable<Company>().Select(a => new SelectListItem { Value = a.CompanyName, Text = a.CompanyName }).ToList();

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
                var selectedCompany = _unitOfWork.Repository.GetQueryable<Company>().FirstOrDefault(m => m.CompanyName == client.CompanyName);

                client.CompanyId = selectedCompany.Id;

                // Add the new product to the repository
                _unitOfWork.Repository.Add(client);

                // Save changes to the database
                await _unitOfWork.Repository.CompleteAsync();

                // Redirect to a success page or another appropriate action.
                return RedirectToPage("/Clients/Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                return RedirectToPage("/Error");
            }
        }
    }
}
