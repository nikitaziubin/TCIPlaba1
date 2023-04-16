using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TCIPlaba1.NewFolder;
using TCIPlaba1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;

namespace TCIPlaba1.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<Users> _userManager;
		private readonly SignInManager<Users> _signInManager;
		private string curEmail;
		private string curPassword;


		public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				Users user = new Users { Email = model.Email, UserName = model.Email, Year = model.Year };

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Participants");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = "/")
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					// перевіряємо, чи належить URL додатку
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						curEmail = model.Email;
						curPassword = model.Password;
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Participants");
					}
				}
				else
				{
					ModelState.AddModelError("", "Неправильний логін чи (та) пароль");
				}
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			// видаляємо аутентифікаційні куки
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Participants");
		}

        [HttpGet]
        public IActionResult Profile(ProfileViewModel model)
        {
			//model.Email = curEmail;
			//model.Password = curPassword;
			//var user = await _userManager.FindByIdAsync();  FindByIdAsync(Users.Identity.GetUserId());
			
			
            var s = _userManager.Users.FirstOrDefault(s => s.Email == User.Identity.Name);
			model.Email = s.Email;
			model.Year = s.Year;
            //ViewData["User"] = new SelectList(s);
            //ViewData["User"] = s;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model, int t)
        {



            if (ModelState.IsValid)
            {
                Users user = new Users { Email = model.Email, UserName = model.Email, Year = model.Year };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Participants");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

    }
}
