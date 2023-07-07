using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Microsoft.AspNetCore.Mvc;

namespace EliteStoreCore.ViewComponents
{
	public class ProductFour : ViewComponent
	{
		ProductManager productManager = new ProductManager(new EFProductRepository());
		public IViewComponentResult Invoke()
		{
			var values = productManager.TGetList();
			return View(values);
		}
	}
	}
