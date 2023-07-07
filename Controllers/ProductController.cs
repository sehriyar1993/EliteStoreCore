using Elite.BiznessLayer.Abstract;
using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using EliteStoreCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EliteStoreCore.Controllers
{
	[AllowAnonymous]

	public class ProductController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductRepository());
		OrderManager orderManager = new OrderManager(new EFOrderRepository());
		private readonly UserManager<AppUser> _userManager;

		public ProductController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IActionResult> Order( int page = 1)
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				var values = orderManager.TGetListAppuserId(value.Id);
				return View(values.ToPagedList(page, 12));
			}
			else
			{
				return View();
			}
		}
		public IActionResult OderDelete(int id)
		{
			var values = orderManager.TGetByID(id);
			orderManager.TDelete(values);
			return RedirectToAction("Order");
		}
		public async Task<IActionResult> Index( int page = 1)
        {
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userID = value.Id;
			}
			else
			{
			}
			
			var values = productManager.TGetProductWithCategory();
            return View(values.Where(m => m.ProductGender == "Kişi").ToPagedList(page, 12));
        }
		public async Task<IActionResult> IndexWoman(int page = 1)
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userID = value.Id;
			}
			else
			{
			}
			var values = productManager.TGetProductWithCategory();
			return View(values.Where(m => m.ProductGender == "Qadın").ToPagedList(page, 12));
		}
	
		public async Task<IActionResult> SearchBy(string searchString, int page = 1)
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userID = value.Id;
			}
			else
			{
			}
		
			if (searchString != null)
				searchString = searchString.ToLower();
			ViewData["CurrentFilter"] = searchString;
			var values = from x in productManager.TGetList().OrderByDescending(x => x.ProductId) select x;
			if (!string.IsNullOrEmpty(searchString))
			{
				values = values.Where(y => y.ProductName.ToLower().Contains(searchString) || y.ProductGender.ToLower().Contains(searchString)); 
			}
			return View(values.ToPagedList(page, 12));
		
		}
		[HttpGet]
		public PartialViewResult BuyProduct()
		{

			return PartialView();
		}

		[HttpPost]
		public IActionResult BuyProduct(Order p)
		{
			p.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
			p.Status = "Təsdiq Gözləyir";
			orderManager.TAdd(p);
			
			return RedirectToAction("Index", "Home");
		}
		public async Task<IActionResult> ProductSıngle(int id)
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userID = value.Id;
			}
			else
			{
			}
			var values = productManager.TGetProductWithID(id);
			return View(values);
		}
		public async Task<IActionResult> GetProductsByCategoryId(int id)
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userID = value.Id;
			}
			else
			{
			}
			var values = productManager.TGetListByFilter(x=>x.CategoryId==id);


			return View(values.Where(m => m.ProductGender == "Kişi"));
			
		}
		public async Task<IActionResult> GetProductsByCategoryIdWoman(int id)
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.userID = value.Id;
			}
			else
			{
			}
			var values = productManager.TGetListByFilter(x => x.CategoryId == id);


			return View(values.Where(m => m.ProductGender == "Qadın"));

		}
	}
}
