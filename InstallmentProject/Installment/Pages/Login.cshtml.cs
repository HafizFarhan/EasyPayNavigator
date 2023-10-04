using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Installment.Pages
{
	public class LoginModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;
		[BindProperty]
		public User user { get; set; }
		public LoginModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public void OnGet()
		{
			user = new User();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			try
			{
				// Search for the user in the
				var dbUser = _unitOfWork.Repository.GetQueryable<User>().FirstOrDefault(
					u => u.Email == user.Email && u.Password == user.Password);

				if (dbUser == null)
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return Page();
				}

				// Assign static values to the username and company ID
				dbUser.Username = "staticUsername"; // replace with your static value
				dbUser.CompanyId = 1234; // replace with your static value

				return LocalRedirect("~/");
			}
			catch (Exception ex)
			{
				return RedirectToPage("/Login");
			}
		}
	}

}
