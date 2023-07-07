using Elite.EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using EliteStoreCore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EliteStoreCore.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProfileController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public ProfileController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			UserEditViewModel userEditViewModel = new UserEditViewModel();
			userEditViewModel.name = values.Name;
			userEditViewModel.surname = values.Surname;
			userEditViewModel.phonenumber = values.PhoneNumber;
			userEditViewModel.mail = values.Email;
			userEditViewModel.city = values.City;
			userEditViewModel.adress = values.Adress;
			userEditViewModel.about = values.About;
			userEditViewModel.gender = values.Gender;
			userEditViewModel.imageurl = values.ImageUrl;
			userEditViewModel.username = values.UserName;
			return View(userEditViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserEditViewModel p)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (p.Image != null)
			{
				var resource = Directory.GetCurrentDirectory();
				var extension = Path.GetExtension(p.Image.FileName);
				var imagename = Guid.NewGuid() + extension;
				var savelocation = resource + "/wwwroot/userimages/" + imagename;
				var stream = new FileStream(savelocation, FileMode.Create);
				await p.Image.CopyToAsync(stream);
				user.ImageUrl = imagename;
			}
			user.Adress = p.adress;
			user.Gender = p.gender;
			user.City = p.city;
			user.About = p.about;
			user.Name = p.name;
			user.Surname = p.surname;
			user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, p.password);
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction("SignIn", "Login");
			}
			return View();
		}
	}
}
