using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TCIPlaba1.Models;
using TCIPlaba1.NewFolder;

namespace LibraryWebApplication.Controllers
{
	[Authorize(Roles = "admin")]
	public class RolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<Users> _userManager;

		public RolesController(RoleManager<IdentityRole> roleManager, UserManager<Users> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public IActionResult Index() => View(_roleManager.Roles.ToList());

		public IActionResult UserList() => View(_userManager.Users.ToList());

		public async Task<IActionResult> Edit(string userId)
		{
			// получаем пользователя
			Users? user = await _userManager.FindByIdAsync(userId );
			if (user != null)
			{
				// получаем список ролей пользователя
				var userRoles = await _userManager.GetRolesAsync(user);
				// получаем все роли
				var allRoles = _roleManager.Roles.ToList();
				// формируем модель представления
				ChangeRoleViewModel model = new ChangeRoleViewModel
				{
					UserId = user.Id,
					UserEmail = user.Email,
					UserRoles = userRoles,
					AllRoles = allRoles
				};
				return View(model);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string userId, List<string> roles)
		{
			// получаем пользователя
			Users? user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				// получаем список ролей пользователя
				var userRoles = await _userManager.GetRolesAsync(user);
				// получаем все роли
				var allRoles = _roleManager.Roles.ToList();
				// получаем список ролей, которые были добавлены
				var addedRoles = roles.Except(userRoles);
				// получаем список ролей, которые были удалены
				var removedRoles = userRoles.Except(roles);

				await _userManager.AddToRolesAsync(user, addedRoles);
				await _userManager.RemoveFromRolesAsync(user, removedRoles);

				return RedirectToAction("UserList");
			}
			return NotFound();
		}
	}
}
