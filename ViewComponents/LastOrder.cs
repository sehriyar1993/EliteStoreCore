using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Microsoft.AspNetCore.Mvc;

namespace EliteStoreCore.ViewComponents
{
    public class LastOrder : ViewComponent
    {
        OrderManager order = new OrderManager(new EFOrderRepository());
        public IViewComponentResult Invoke()
        {
            var values = order.TGetListAppuser();
            return View(values);
        }
    }
}
