using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Microsoft.AspNetCore.Mvc;

namespace EliteStoreCore.ViewComponents
{
	public class NavbarCategoryListWoman : ViewComponent
	{
		CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
		public IViewComponentResult Invoke()
		{
			var values = categoryManager.TGetList();
			return View(values);

		}
	}
}
