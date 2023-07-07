using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EliteStoreCore.ViewComponents
{
    public class AppuserAdminNAv:ViewComponent
    {
        Context c = new Context();

        public IViewComponentResult Invoke()
        {
            var name = User.Identity.Name;
            ViewBag.Surname = c.Users.Where(x => x.UserName == name).Select(y => y.Surname).FirstOrDefault();
            ViewBag.name = c.Users.Where(x => x.UserName == name).Select(y => y.Name).FirstOrDefault();
            var usermail = c.Users.Where(x => x.UserName == name).Select(y => y.Email).FirstOrDefault();
            ViewBag.ImageUrlAdmin = c.Users.Where(x => x.UserName == name).Select(y => y.ImageUrl).FirstOrDefault();
            ViewBag.EmailAdmin = usermail;
            return View();
        }
    }
}
