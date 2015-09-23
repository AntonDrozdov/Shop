using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataManager.Abstract;
using DataManager.Model;
using MVCGUI.Models;

namespace MVCGUI.Controllers
{
    public partial class AdminController : Controller
    {
        private IRepository repository;
        private int PageSize = 5;

        public AdminController(IRepository _repository) {
            this.repository = _repository;
        }
        //PURCHASES
        public ActionResult PurchasesList()
        {
            return View(repository.Purchases.ToList());
        }
        [HttpGet]
        public ActionResult CreatePurchase()
        {
            // Формируем список команд для передачи в представление
            SelectList list = new SelectList(repository.Goods, "Id", "Title");
            ViewBag.Goods = list;
            return View();
        }
        [HttpPost]
        public ActionResult CreatePurchase(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            repository.CreatePurchase(purchase);
            // перенаправляем на главную страницу
            return RedirectToAction("PurchasesList");
        }
        [HttpGet]
        public ActionResult EditPurchase(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд футболиста
            Purchase purchase = repository.FindPurchase(id);
            if (purchase != null)
            {
                // Создаем список команд для передачи в представление
                SelectList list = new SelectList(repository.Goods, "Id", "Title", purchase.GoodId);
                ViewBag.Goods = list;
                return View(purchase);
            }
            return RedirectToAction("PurchasesList");
        }
        [HttpPost]
        public ActionResult EditPurchase(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            repository.SaveEditedPurchase(purchase);
            return RedirectToAction("PurchasesList");
        }
        [HttpGet]
        public ActionResult DeletePurchase(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Purchase purchase = repository.FindPurchase(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }
        [HttpPost, ActionName("DeletePurchase")]
        public ActionResult DeletePurchaseConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Purchase purchase = repository.FindPurchase(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }

            repository.DeletePurchase(purchase);
            return RedirectToAction("PurchasesList");
        }

        //GOODS
        public ActionResult GoodsList()
        {
            IQueryable<Good> list = repository.Goods;
            return View(repository.Goods.ToList());
        }
        [HttpGet]
        public ActionResult CreateGood()
        {
            ViewBag.Categories = repository.Categories().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateGood(Good good, int[] selected)
        {
            repository.CreateGood(good, selected);
            // перенаправляем на главную страницу
            return RedirectToAction("GoodsList");
        }
        [HttpGet]
        public ActionResult EditGood(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
             Good good = repository.FindGood(id);
            if (good != null)
            {
                ViewBag.Categories = repository.Categories().ToList();
                return View(good);
            }
            return RedirectToAction("PurchasesList");
        }
        [HttpPost]
        public ActionResult EditGood(Good good, int[] selected)
        {
            repository.SaveEditedGood(good, selected);
            return RedirectToAction("GoodsList");
        }
        [HttpGet]
        public ActionResult DeleteGood(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Good good = repository.FindGood(id);
            if (good == null)
            {
                return HttpNotFound();
            }
            return View(good);
        }
        [HttpPost, ActionName("DeleteGood")]
        public ActionResult DeleteGoodConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Good good = repository.FindGood(id);
            if (good == null)
            {
                return HttpNotFound();
            }

            repository.DeleteGood(good);
            return RedirectToAction("GoodsList");
        }

        //CATEGORIES
        public ActionResult CategoriesList(int page =1 ,int? cattype=null, int? parentcat=null)
        {
            ViewBag.ParentCategories = new SelectList(repository.Categories().ToList(), "Id", "Title");
            ViewBag.CategoryTypes = new SelectList(repository.CategoryTypes.ToList(), "Id", "Title");
            List<Category> cats = new List<Category>();
            cats = repository.Categories(page, cattype, parentcat).ToList();
            List<Category> totalcats = new List<Category>();
            totalcats = repository.Categories(cattype, parentcat).ToList();

            CategoryListView model = new CategoryListView
            {
                cattype = cattype,
                parentcat = parentcat,
                Categories = cats,
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalcats.Count()
                }
            };
              
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            ViewBag.Categories = repository.Categories().ToList();
            ViewBag.CategoryTypes = repository.CategoryTypes.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category, int[] selected, int[] selected2)
        {
            repository.CreateCategory(category, selected, selected2);
            // перенаправляем на главную страницу
            return RedirectToAction("CategoriesList");
        }
        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category category = repository.FindCategory(id);
            if (category != null)
            {
                ViewBag.Categories = repository.Categories().ToList();
                ViewBag.CategoryTypes = repository.CategoryTypes.ToList();
                return View(category);
            }
            return RedirectToAction("CategoriesList");
        }
        [HttpPost]
        public ActionResult EditCategory(Category category, int[] selected, int[] selected2)
        {
            repository.SaveEditedCategory(category, selected, selected2);
            return RedirectToAction("CategoriesList");
        }
        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category category = repository.FindCategory(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        public ActionResult DeleteCategoryConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
           Category category = repository.FindCategory(id);
           if (category == null)
            {
                return HttpNotFound();
            }

           repository.DeleteCategory(category);
            return RedirectToAction("CategoriesList");
        }
   
        //CATEGORIESTYPES
        public ActionResult CategoryTypesList()
        {
            return View(repository.CategoryTypes.ToList());
        }
        [HttpGet]
        public ActionResult CreateCategoryType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategoryType(CategoryType categorytype)
        {
            repository.CreateCategoryType(categorytype);
            // перенаправляем на главную страницу
            return RedirectToAction("CategoryTypesList");
        }
        [HttpGet]
        public ActionResult EditCategoryType(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            CategoryType categorytype = repository.FindCategoryType(id);
            if (categorytype != null)
            {
                return View(categorytype);
            }
            return RedirectToAction("CategoryTypesList");
        }
        [HttpPost]
        public ActionResult EditCategoryType(CategoryType categorytype)
        {
            repository.SaveEditedCategoryType(categorytype);
            return RedirectToAction("CategoryTypesList");
        }
        [HttpGet]
        public ActionResult DeleteCategoryType(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            CategoryType categorytype = repository.FindCategoryType(id);
            if (categorytype == null)
            {
                return HttpNotFound();
            }
            return View(categorytype);
        }
        [HttpPost, ActionName("DeleteCategoryType")]
        public ActionResult DeleteCategoryTypeConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            CategoryType categorytype = repository.FindCategoryType(id);
            if (categorytype == null)
            {
                return HttpNotFound();
            }

            repository.DeleteCategoryType(categorytype);
            return RedirectToAction("CategoryTypesList");
        }
    }
}
