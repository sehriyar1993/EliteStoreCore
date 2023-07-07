using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Microsoft.AspNetCore.Mvc;

namespace EliteStoreCore.ViewComponents
{
    public class NletterMailSend:ViewComponent
    {
        NewsLettermanager nm = new NewsLettermanager(new EFNewsLetterRepository());
        public IViewComponentResult Invoke()
        {
            var values = nm.TGetList();
            return View(values);
        }
    }
}
