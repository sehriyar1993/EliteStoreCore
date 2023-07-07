using Elite.BiznessLayer.Abstract;
using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EliteStoreCore.Controllers
{
	public class AboutController : Controller
	{
        private readonly IAboytServices _aboytServices;
		AboutManager aboutManager = new AboutManager(new EFAboutRepository());

        public AboutController(IAboytServices aboytServices)
        {
            _aboytServices = aboytServices;
        }
        [Authorize(Roles = "Admin")]

        public IActionResult AdminIndex()
        {
            var values = aboutManager.TGetList();
            return View(values);
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Add(About bag)
        {
            bag.Status = true;
            aboutManager.TAdd(bag);
            return RedirectToAction("AdminIndex");
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            var values = aboutManager.TGetByID(id);
            aboutManager.TDelete(values);
            return RedirectToAction("AdminIndex");

        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Update(int id)
        {
            var values = aboutManager.TGetByID(id);
            return View(values);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Update(About bag)
        {
            aboutManager.TUpdate(bag);
            return RedirectToAction("AdminIndex");
        }
        public IActionResult ChangeToTrue(int id)
        {
            aboutManager.TChangeToTrue(id);
            
            return RedirectToAction("AdminIndex");
        }
        [Authorize(Roles = "Admin")]

        public IActionResult ChangeToFalse(int id)
        {
            _aboytServices.TChangeToFalse(id);
            return RedirectToAction("AdminIndex");
        }

        [AllowAnonymous]

		public IActionResult Index()
		{
			Context c = new Context();
			var values = c.Abouts.Where(x=>x.Status==true).FirstOrDefault();
			return View(values);
		}
	}
}
