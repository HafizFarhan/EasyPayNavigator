using EasyRepository.EFCore.Abstractions;
using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Installment.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<SelectListItem> CompanyItems { set; get; }
        public EditModel(IRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Client client { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            CompanyItems = _unitOfWork.Repository.GetQueryable<Company>().Select(a => new SelectListItem { Value = a.CompanyName, Text = a.CompanyName }).ToList();

            // Load the existing client from the database by its ID
            client = await _unitOfWork.Repository.GetByIdAsync<Client>(asNoTracking: false, id: id);

            if (client == null)
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
                // Load the existing product from the database by its ID
                var existingClient = await _unitOfWork.Repository.GetByIdAsync<Client>(asNoTracking: false, id: id);

                if (existingClient == null)
                {
                    // Handle the case where the product doesn't exist
                    return RedirectToPage("/Error");
                }
                var selectedCompany = _unitOfWork.Repository.GetQueryable<Company>().FirstOrDefault(m => m.CompanyName == client.CompanyName);

                // Apply changes to the existing product
                existingClient.Name = client.Name;
                existingClient.Email = client.Email;
                existingClient.Phone = client.Phone;
                existingClient.CompanyId = selectedCompany.Id;
                existingClient.CompanyName = selectedCompany.CompanyName;

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
