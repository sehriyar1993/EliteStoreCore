using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Microsoft.AspNetCore.Mvc;

namespace EliteStoreCore.ViewComponents
{
    public class ContactUs : ViewComponent
    {
        ContactManager contactManager = new ContactManager(new EFContactRepository());
        public IViewComponentResult Invoke()
        {
            var values = contactManager.TGetList();
            return View(values);
        }
    }
}
