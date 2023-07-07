using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using X.PagedList;

namespace EliteStoreCore.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminCategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());

        public IActionResult Index(int page=1)
        {
            var values = categoryManager.TGetList();
            return View(values.ToPagedList(page, 12));
        }
        [HttpGet]
        public IActionResult CateqoriyaAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CateqoriyaAdd(Category p)
        {
           
           
                p.Status = true;
            categoryManager.TAdd(p);

                return RedirectToAction("Index");
            
         
        }

        public IActionResult ChangeToTrue(int id)
        {
            categoryManager.TChangeToTrue(id);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeToFalse(int id)
        {
            categoryManager.TChangeToFalse(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CateqoryUpdate(int id)
        {
            var values = categoryManager.TGetByID(id);

            return View(values);
        }
        [HttpPost]
        public IActionResult CateqoryUpdate(Category p)
        {
          
                categoryManager.TUpdate(p);

                return RedirectToAction("Index");
            
        }
    }
}
