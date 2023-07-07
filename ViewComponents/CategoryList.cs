using Elite.BiznessLayer.Abstract;
using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using EliteStoreCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EliteStoreCore.ViewComponents
{
	public class CategoryList:ViewComponent
	{
		
		CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
		public IViewComponentResult Invoke()
		{
			var values = categoryManager.TGetList();
			return View(values);
			
		}
	}
}
