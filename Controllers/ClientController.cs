using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using X.PagedList;

namespace EliteStoreCore.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ClientController : Controller
    {
        AppUserManager manager = new AppUserManager(new EFAppUserRepository());
        private readonly UserManager<AppUser> _userManager;

        Context c = new Context();

        public ClientController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string searchString, int page = 1)
        {
            if (searchString != null)
                searchString = searchString.ToLower();
            ViewData["CurrentFilter"] = searchString;
            var values = from x in manager.TGetList().OrderByDescending(x => x.UserName) select x;
            if (!string.IsNullOrEmpty(searchString))
            {
                values = values.Where(y => y.Name.ToLower().Contains(searchString) || y.Surname.ToLower().Contains(searchString));
            }
            //var valuse = manager.TGetList();
        
            return View(values.ToPagedList(page,15));
        }
        public IActionResult Delete(int id)
        {
            var values = manager.TGetByID(id);
            manager.TDelete(values);
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var values = manager.TGetByID(id);
            return View(values);

        }
    }
}
