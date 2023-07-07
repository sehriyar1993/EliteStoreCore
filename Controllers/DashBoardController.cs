using Elite.DataAccsessLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace EliteStoreCore.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DashBoardController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            ViewBag.g = c.Orders.Where(x=>x.Status== "Təsdiq Gözləyir").Count();
            ViewBag.t = c.Orders.Where(x=>x.Status== "Təsdiqləndi").Count();
            ViewBag.m = c.Orders.Where(x=>x.Status== "Müddəti Bitmiş").Count();
            return View();
        }
    }
}
