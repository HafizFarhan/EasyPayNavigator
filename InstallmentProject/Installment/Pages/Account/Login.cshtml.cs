using EasyRepository.EFCore.Generic;
using Installment.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Installment.Pages
{
	public class LoginModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor; // Inject the IHttpContextAccessor
        [BindProperty]
		public User user { get; set; }
       
        public LoginModel(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor; // Inject the IHttpContextAccessor
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
                // Search for the user in the database and include the Company navigation property
                var dbUser = _unitOfWork.Repository
                    .GetQueryable<User>()
                    .Include(u => u.Company) // Include the Company navigation property
                    .FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);


                if (dbUser == null )
				    {
					    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					    return Page();
				    }
						var identity = new ClaimsIdentity(new[] {
						new Claim(ClaimTypes.Name, dbUser.Username),
						new Claim(ClaimTypes.Email, dbUser.Email),
                        new Claim(ClaimTypes.Role, dbUser.Role.ToString())
						// Add more claims if needed
					}, "custom");

				   var principal = new ClaimsPrincipal(identity);

					// Sign in the user
				   //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				 // You can also store the user's role in TempData for later use
				  TempData["UserRole"] = dbUser.Role;

                if (TempData["UserRole"].ToString() == "Admin")
                {
                    // Redirect to the admin panel if the user is an admin
                    return RedirectToPage("/AdminPanel");
                }
                else
                {
                    // Redirect to the home page for other users
                    return RedirectToPage("/Home");
                }

            }
			catch (Exception ex)
			{
				return RedirectToPage("/");
			}
		}
	}

}
