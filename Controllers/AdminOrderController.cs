using Elite.BiznessLayer.Abstract;
using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using X.PagedList;

namespace EliteStoreCore.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminOrderController : Controller
	{
		OrderManager orderManager = new OrderManager(new EFOrderRepository());
		AppUserManager userManager = new AppUserManager(new EFAppUserRepository());

		public IActionResult Index(int page = 1)
		{

			var values = orderManager.TGetListAppuser().OrderByDescending(x => x.OrderId).ToPagedList(page, 9);
			return View(values);
		}


		public IActionResult ChangeToTrue(int id)
		{
			orderManager.TChangeToTrue(id);
			return RedirectToAction("Index");
		}


		public IActionResult ChangeToFalse(int id)
		{
			orderManager.TChangeToFalse(id);
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int id)
		{
			var values = orderManager.TGetByID(id);
			orderManager.TDelete(values);
			return RedirectToAction("Index");
		}
	}
}
