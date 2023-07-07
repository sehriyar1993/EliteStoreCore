using Elite.BiznessLayer.Abstract;
using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EliteStoreCore.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminPruductController : Controller
	{
		ProductManager productManager = new ProductManager(new EFProductRepository());
		CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
		ColorManager cm = new ColorManager(new EFColorRepository());
		StillManager sm = new StillManager(new EFStillRepository());
		MaterialManager mm = new MaterialManager(new EFMaterialRepository());

		public async Task<IActionResult> Search(string searchString, int page = 1)
		{

			if (searchString != null)
				searchString = searchString.ToLower();
			ViewData["CurrentFilter"] = searchString;
			var values = from x in productManager.TGetList().OrderByDescending(x => x.ProductId) select x;
			if (!string.IsNullOrEmpty(searchString))
			{
				values = values.Where(y => y.ProductName.ToLower().Contains(searchString));
			}
			return View(values.ToPagedList(page, 12));
			
		}

		public IActionResult Index(int page = 1)
		{
			var values = productManager.TGetList();
			return View(values.ToPagedList(page, 12));
		}
		public IActionResult ListBycategory(int id, int page = 1)
		{
			var values = productManager.TGetListByFilter(x => x.CategoryId == id);
			return View(values.ToPagedList(page, 12));
		}
		[HttpGet]
		public IActionResult AddProduct()
		{
		
			List<SelectListItem> values1 = (from x in categoryManager.TGetList()
											select new SelectListItem
											{
												Text = x.CategoryName,
												Value = x.CategoryId.ToString()
											}).ToList();
			ViewBag.v1 = values1;


            List<SelectListItem> still = (from x in sm.TGetList()
                                            select new SelectListItem
                                            {
                                                Text = x.StillName,
                                                Value = x.StillId.ToString()
                                            }).ToList();
            ViewBag.still = still;


            List<SelectListItem> material = (from x in mm.TGetList()
                                            select new SelectListItem
                                            {
                                                Text = x.MaterialName,
                                                Value = x.MaterialId.ToString()
                                            }).ToList();
            ViewBag.material = material;


            List<SelectListItem> color = (from x in cm.TGetList()
                                            select new SelectListItem
                                            {
                                                Text = x.ColorName,
                                                Value = x.ColorId.ToString()
                                            }).ToList();
            ViewBag.color = color;

            return View();
		}
		[HttpPost]
		public IActionResult AddProduct(Product p)
		{
			p.ProductStatus = true;
			productManager.TAdd(p);
			return RedirectToAction("Index");
		}
		public IActionResult DeleteProduct(int id)
		{
			var values = productManager.TGetByID(id);
			productManager.TDelete(values);
			return RedirectToAction("Index");
		}
		[HttpGet]

		public IActionResult UpdateProduct(int id)
		{
			List<SelectListItem> values1 = (from x in categoryManager.TGetList()
											select new SelectListItem
											{
												Text = x.CategoryName,
												Value = x.CategoryId.ToString()
											}).ToList();
			ViewBag.v1 = values1;
            List<SelectListItem> still = (from x in sm.TGetList()
                                          select new SelectListItem
                                          {
                                              Text = x.StillName,
                                              Value = x.StillId.ToString()
                                          }).ToList();
            ViewBag.still = still;


            List<SelectListItem> material = (from x in mm.TGetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.MaterialName,
                                                 Value = x.MaterialId.ToString()
                                             }).ToList();
            ViewBag.material = material;


            List<SelectListItem> color = (from x in cm.TGetList()
                                          select new SelectListItem
                                          {
                                              Text = x.ColorName,
                                              Value = x.ColorId.ToString()
                                          }).ToList();
            ViewBag.color = color;


            var values = productManager.TGetByID(id);
			return View(values);
		}
		[HttpPost]

		public IActionResult UpdateProduct(Product p)
		{
			productManager.TUpdate(p);
			return RedirectToAction("Index");
		}
        public IActionResult GetProductsByCategoryId(int id, int page = 1)
        {
            var values = productManager.TGetListByFilter(x => x.CategoryId == id);


            return View(values.Where(m => m.ProductGender == "Kişi").ToPagedList(page, 12));

        }
        public IActionResult GetProductsByCategoryIdWoman(int id , int page = 1)
        {
            var values = productManager.TGetListByFilter(x => x.CategoryId == id);


            return View(values.Where(m => m.ProductGender == "Qadın").ToPagedList(page, 12));

        }
        public IActionResult ProductSıngle(int id)
        {

            var values = productManager.TGetProductWithID(id);
            return View(values);
        }

		public async Task<IActionResult> PruductListMan(int page = 1)
		{
			
			var values = productManager.TGetProductWithCategory();
			return View(values.Where(m => m.ProductGender == "Kişi").ToPagedList(page, 12));
		}
		public async Task<IActionResult> PruductListWoman(int page = 1)
		{
			var values = productManager.TGetProductWithCategory();
			return View(values.Where(m => m.ProductGender == "Qadın").ToPagedList(page, 12));
		}
		public IActionResult Color(int page = 1)
		{
			var values = cm.TGetList();
			return View(values.ToPagedList(page, 12));
		}
		[HttpGet]
		public IActionResult ColorAdd()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ColorAdd(Color p)
		{


			p.Status = true;
			cm.TAdd(p);

			return RedirectToAction("Color");


		}

		public IActionResult ChangeToTrueColor(int id)
		{
			Context context = new Context();
            var values = context.Colors.Find(id);
            values.Status = true;
            context.Update(values);
            context.SaveChanges();
            return RedirectToAction("Color");
		}

		public IActionResult ChangeToFalseColor(int id)
		{
            Context context = new Context();
            var values = context.Colors.Find(id);
            values.Status = false;
            context.Update(values);
            context.SaveChanges();
            return RedirectToAction("Color");
		}

		[HttpGet]
		public IActionResult ColorUpdate(int id)
		{
			var values = cm.TGetByID(id);

			return View(values);
		}
		[HttpPost]
		public IActionResult ColorUpdate(Color p)
		{

			cm.TUpdate(p);

			return RedirectToAction("Color");

		}
        public IActionResult Still(int page = 1)
        {
            var values = sm.TGetList();
            return View(values.ToPagedList(page, 12));
        }
        [HttpGet]
        public IActionResult StillAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StillAdd(Still p)
        {


            p.Status = true;
            sm.TAdd(p);

            return RedirectToAction("Still");


        }

        public IActionResult ChangeToTrueStill(int id)
        {
            Context context = new Context();
            var values = context.Stills.Find(id);
            values.Status = true;
            context.Update(values);
            context.SaveChanges();
            return RedirectToAction("Still");
        }

        public IActionResult ChangeToFalseStill(int id)
        {
            Context context = new Context();
            var values = context.Stills.Find(id);
            values.Status = false;
            context.Update(values);
            context.SaveChanges();
            return RedirectToAction("Still");
        }

        [HttpGet]
        public IActionResult StillUpdate(int id)
        {
            var values = sm.TGetByID(id);

            return View(values);
        }
        [HttpPost]
        public IActionResult StillUpdate(Still p)
        {

            sm.TUpdate(p);

            return RedirectToAction("Still");

        }

        public IActionResult Material(int page = 1)
        {
            var values = mm.TGetList();
            return View(values.ToPagedList(page, 12));
        }
        [HttpGet]
        public IActionResult MaterialAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MaterialAdd(Material p)
        {


            p.Status = true;
            mm.TAdd(p);

            return RedirectToAction("Material");


        }

        public IActionResult ChangeToTrueMaterial(int id)
        {
            Context context = new Context();
            var values = context.Materials.Find(id);
            values.Status = true;
            context.Update(values);
            context.SaveChanges();
            return RedirectToAction("Material");
        }

        public IActionResult ChangeToFalseMaterial(int id)
        {
            Context context = new Context();
            var values = context.Materials.Find(id);
            values.Status = false;
            context.Update(values);
            context.SaveChanges();
            return RedirectToAction("Material");
        }

        [HttpGet]
        public IActionResult MaterialUpdate(int id)
        {
            var values = mm.TGetByID(id);

            return View(values);
        }
        [HttpPost]
        public IActionResult MaterialUpdate(Material p)
        {

            mm.TUpdate(p);

            return RedirectToAction("Material");

        }

    }
}
