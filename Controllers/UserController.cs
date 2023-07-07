using Elite.EntityLayer.Concreate;
using EliteStoreCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using System.Linq;

namespace EliteStoreCore.Controllers
{
	[AllowAnonymous]
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		AppUserManager user = new AppUserManager(new EFAppUserRepository());
		public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public PartialViewResult SignUp()
		{
		
            return PartialView();

		}
		[HttpPost]
		public async Task<IActionResult> SignUp(UserRegisterViewModel p)
		{
			AppUser appUser = new AppUser()
			{
				Name = p.Name,
				Surname = p.Surname,
				Email = p.Mail,
				UserName = p.Username,
				PhoneNumber = p.PhoneNumber,
				City = p.City,
				Adress = p.Adress,
				Gender = p.Gender,
				About = p.About,
				ImageUrl = p.ImageUrl
			};
			if (p.Password == p.ConfirmPassword)
			{
				var result = await _userManager.CreateAsync(appUser, p.Password);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home", new { data_target = "#myModal" });
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			return View(p);
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(UserSignInViewModel p)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
		{
			var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Mail);
			string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var passwordResetTokenLink = Url.Action("ResetPassword", "User", new
			{
				userId = user.Id,
				token = passwordResetToken
			}, HttpContext.Request.Scheme);

			MimeMessage mimeMessage = new MimeMessage();

			MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "ficihaciyev@gmail.com");

			mimeMessage.From.Add(mailboxAddressFrom);

			MailboxAddress mailboxAddressTo = new MailboxAddress("User", forgetPasswordViewModel.Mail);
			mimeMessage.To.Add(mailboxAddressTo);

			var bodyBuilder = new BodyBuilder();
			bodyBuilder.TextBody = passwordResetTokenLink;
			mimeMessage.Body = bodyBuilder.ToMessageBody();

			mimeMessage.Subject = "Şifrə Dəyişmə Tələbi";

            SmtpClient client = new SmtpClient();
			client.Connect("smtp.gmail.com", 587, false);
			client.Authenticate("ficihaciyev@gmail.com", "qcjrvjxbbuvckfcv\r\n");
			client.Send(mimeMessage);
			client.Disconnect(true);

			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string userid, string token)
		{
			TempData["userid"] = userid;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
		{
			var userid = TempData["userid"];
			var token = TempData["token"];
			if (userid == null || token == null)
			{
				//hata mesajı
			}
			var user = await _userManager.FindByIdAsync(userid.ToString());
			var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {

            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserImage userEditViewModel = new AppUserImage();
            userEditViewModel.Id = values.Id;
            userEditViewModel.username = values.UserName;
            userEditViewModel.name = values.Name;
            userEditViewModel.surname = values.Surname;
            userEditViewModel.phonenumber = values.PhoneNumber;
            userEditViewModel.about = values.About;
            userEditViewModel.mail = values.Email;
            userEditViewModel.city = values.City;
            userEditViewModel.adress = values.Adress;
            return View(userEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(AppUserImage p)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (p.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "wwwroot/WriterImage" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await p.Image.CopyToAsync(stream);
                user.ImageUrl = imagename;
            }
            user.ImageUrl = p.imageurl;
            user.Name = p.name;
            user.Id = p.Id;
            user.Surname = p.surname;
            user.Email = p.mail;
            user.PhoneNumber = p.phonenumber;
            user.UserName = p.username;
            user.About = p.about;
			user.Adress = p.adress;
			user.City = p.city;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, p.password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
    }
}
