using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using EliteStoreCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EliteStoreCore.Controllers
{
	[AllowAnonymous]

	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly UserManager<AppUser> _userManager;

		
        ProductManager sliderManager = new ProductManager(new EFProductRepository());

		public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
		{
			_logger = logger;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
        {
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userID = value.Id;
			}
			else
			{
			}
			var values = sliderManager.TGetList();
            return View(values.OrderByDescending(x=>x.ProductId).Take(8));
        }
        public IActionResult Layout()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
		public IActionResult Error404(int code)
		{
			return View();
		}
	}
}
