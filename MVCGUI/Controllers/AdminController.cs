using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataManager.Abstract;
using DataManager.Model;
using MVCGUI.Models;
using System.Web.Helpers;


namespace MVCGUI.Controllers
{
    public partial class AdminController : Controller
    {
        private IRepository repository;
        private int PageSize = 10;

        public AdminController(IRepository _repository) {
            this.repository = _repository;
        }
     
        //CATEGORIES
        public ActionResult CategoriesList(int page = 1, int? cattype = null, int? parentcat = null)
        {

            ViewBag.ParentCategories = new SelectList(repository.PureCategories().ToList(), "Id", "Title");//категории для формирования DropListDown
            ViewBag.CategoryTypes = new SelectList(repository.CategoryTypes.ToList(), "Id", "Title");
            List<Category> cats = new List<Category>();
            cats = repository.Categories(page, cattype, parentcat).ToList();//категории страницы
            List<Category> totalcats = new List<Category>();
            totalcats = repository.Categories(cattype, parentcat).ToList();//всего категорий в выбранной родительской
            //формируем модель для отображения
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
        //public ActionResult CreateCategory(HttpPostedFileBase Image, Category category, int[] selected, int[] selected2)
        public ActionResult CreateCategory(string Title, string Description, int[] selected, int[] selected2,HttpPostedFileBase Image)
        {
            Category newcategory = new Category();
            newcategory.Title = Title;
            newcategory.Description = Description;
            repository.CreateCategory(newcategory, selected, selected2, Image);
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
        public ActionResult EditCategory(int Id, string Title, string Description, int[] selected, int[] selected2, HttpPostedFileBase Image)
        {
            Category newcategory = new Category() { Id=Id,Title=Title,Description = Description};
            repository.SaveEditedCategory(newcategory, selected, selected2, Image);
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
        public FileContentResult GetCategoryImage(int Id)
        {
            Category item = repository.FindCategory(Id);
            if (item != null)
            {
               return File(item.Image, item.ImageMimeType);
            }
            else
            {
                return null;
            }
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
        
        //GOODS
        public ActionResult GoodsList(int page = 1, int? parentcat = null)
        {
            ViewBag.ParentCategories = new SelectList(repository.PureCategories().ToList(), "Id", "Title");//категории для формирования DropListDown
            List<Good> goods = new List<Good>();
            goods = repository.Goods(page, parentcat).ToList();//все элементы на странице
            List<Good> totalgoods = new List<Good>();
            totalgoods = repository.Goods(parentcat).ToList();//всего элементов в категории
            //формируем модель для отображения
            GoodListView model = new GoodListView()
            {
                parentcat = parentcat,
                Goods = goods,
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalgoods.Count()
                }
            };

            return View(model);
        }
        [HttpGet]
        public ActionResult CreateGood()
        {
            ViewBag.Categories = repository.Categories().ToList();
            //здесь надо получить последнюю запись в таблице изображений
            // вот способ хороший - db.Таблица.OrderByDescending(x => x.ПервичныйКлюч).FirstOrDefault()
            ViewBag.ImageStartNumber = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(0);
            return PartialView("PartialCreateGood");
            //return View("CreateGood");
        }
        [HttpPost]
        public ActionResult CreateGood(string Title, string Description, int Amount, int[] checkbselected, int[] radioselected, HttpPostedFileBase[] newfiles)
        {
            Good newgood = new Good() { Title = Title, Description = Description, Amount = Amount };
            
            repository.CreateGood(newgood, checkbselected, radioselected, newfiles);
            // перенаправляем на главную страницу*/
            return RedirectToAction("GoodsList");
        }
        [HttpGet]
        public ActionResult EditGood(int? id)
        {
            if (id == null) return HttpNotFound();
            
            Good good = repository.FindFullGood(id);
            
            if (good != null)
            {
                //категории
                ViewBag.Categories = repository.Categories().ToList();
                //здесь надо получить последнюю запись в таблице изображений
                Image lastimage = repository.GetLastImage();
                int startnumberofnewfiles = lastimage.Id + 1;
                ViewBag.ImageStartNumber = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(startnumberofnewfiles);
                return PartialView("PartialEditGood",good);
            }
            return RedirectToAction("GoodsList");
        }
        [HttpPost]
        public ActionResult EditGood(int Id, string Title, string Description, int Amount,
                                    int[] checkbselected, int[] radioselected, int[] imageids, HttpPostedFileBase[] newfiles)
        {
            Good newgood = new Good() {Id = Id, Title = Title, Description = Description, Amount = Amount };

            //здесь надо получить последнюю запись в таблице изображений
            Image lastimage = repository.GetLastImage();
            int startnumberofnewfiles = lastimage.Id + 1;
            ViewBag.ImageStartNumber = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(startnumberofnewfiles);
 
            repository.SaveEditedGood(newgood, checkbselected, radioselected, imageids, newfiles, startnumberofnewfiles);

            return RedirectToAction("GoodsList");
        }
        [HttpGet]
        public ActionResult DeleteGood(int? id)
        {
            if (id == null) return HttpNotFound();
            
            Good good = repository.FindGood(id);

            if (good == null) return HttpNotFound();
            
            return PartialView("PartialDeleteGood", good);
        }
        [HttpPost, ActionName("DeleteGood")]
        public ActionResult DeleteGoodConfirmed(int? id)
        {
            if (id == null) return HttpNotFound();
            
            Good good = repository.FindGood(id);

            if (good == null) return HttpNotFound();
            
            repository.DeleteGood(good);
            
            return RedirectToAction("GoodsList");
        }

        
        //DISCOUNTS
        public ActionResult DiscountsList(int page = 1)
        {
            ViewBag.ParentCategories = new SelectList(repository.PureCategories().ToList(), "Id", "Title");//категории для формирования DropListDown
            List<Discount> items = new List<Discount>();
            items = repository.Discounts(page).ToList();//все элементы на странице
            List<Discount> totalitems = new List<Discount>();
            totalitems = repository.Discounts().ToList();//всего элементов в категории
            //формируем модель для отображения
            DiscountListView model = new DiscountListView()
            {
                Items = items,
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalitems.Count()
                }
            };

            return View(model);
        }
        [HttpGet]
        public ActionResult CreateDiscount()
        {
            ViewBag.Goods = repository.PureGoods().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateDiscount(Discount item, int[] selected)
        {
            repository.CreateDiscount(item, selected);
            return RedirectToAction("DiscountsList");
        }
        [HttpGet]
        public ActionResult EditDiscount(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Discount item = repository.FindDiscount(id);
            if (item != null)
            {
                ViewBag.Goods = repository.PureGoods().ToList();
                return View(item);
            }
            return RedirectToAction("DiscountsList");
        }
        [HttpPost]
        public ActionResult EditDiscount(Discount item, int[] selected)
        {
            repository.SaveEditedDiscount(item, selected);
            return RedirectToAction("DiscountsList");
        }
        [HttpGet]
        public ActionResult DeleteDiscount(int? id)
        {
            if (id == null) return HttpNotFound();
            
            Discount item = repository.FindDiscount(id);
            if (item == null) return HttpNotFound();
            
            return View(item);
        }
        [HttpPost, ActionName("DeleteDiscount")]
        public ActionResult DeleteDiscountConfirmed(int? id)
        {
            if (id == null) return HttpNotFound();

            Discount item = repository.FindDiscount(id);
            if (item == null) return HttpNotFound();

            repository.DeleteDiscount(item);
            return RedirectToAction("DiscountsList");
        }
        
        //PURCHASES
        public ActionResult PurchasesList(int page = 1, int? good = null)
        {
            ViewBag.Goods = new SelectList(repository.PureGoods().ToList(), "Id", "Title");//категории для формирования DropListDown
            List<Purchase> itemsperpage = new List<Purchase>();
            itemsperpage = repository.Purchases(page, good).ToList();//все элементы на странице
            List<Purchase> totalitems = new List<Purchase>();
            totalitems = repository.Purchases(good).ToList();//всего элементов в категории
            //формируем модель для отображения

            PurchaseListView model = new PurchaseListView()
            {
                good = good,
                Purchases = itemsperpage,
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalitems.Count()
                }
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult CreatePurchase()
        {
            // Формируем список для передачи в представление
            ViewBag.Goods = new SelectList(repository.PureGoods().ToList(), "Id", "Title");//категории для формирования DropListDown
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
                ViewBag.Goods = new SelectList(repository.PureGoods().ToList(), "Id", "Title");//категории для формирования DropListDown
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

        //SALES
        public ActionResult SalesList(int page = 1, int? good = null)
        {
            ViewBag.Goods = new SelectList(repository.PureGoods().ToList(), "Id", "Title");//категории для формирования DropListDown
            List<Sale> itemsperpage = new List<Sale>();
            itemsperpage = repository.Sales(page, good).ToList();//все элементы на странице
            List<Sale> totalitems = new List<Sale>();
            totalitems = repository.Sales(good).ToList();//всего элементов в категории
            //формируем модель для отображения

            SaleListView model = new SaleListView()
            {
                good = good,
                Sales = itemsperpage,
                paginginfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalitems.Count()
                }
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateSale()
        {
            // Формируем список для передачи в представление
            ViewBag.Goods = new SelectList(repository.PureGoods().ToList(), "Id", "Title");//категории для формирования DropListDown
            return View();
        }
        [HttpPost]
        public ActionResult CreateSale(Sale sale)
        {
            sale.Date = DateTime.Now;
            repository.CreateSale(sale);
            // перенаправляем на главную страницу
            return RedirectToAction("SalesList");
        }
        [HttpGet]
        public ActionResult EditSale(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд футболиста
            Sale item = repository.FindSale(id);
            if (item != null)
            {
                // Создаем список  для передачи в представление
                ViewBag.Goods = new SelectList(repository.PureGoods().ToList(), "Id", "Title");//категории для формирования DropListDown
                return View(item);
            }
            return RedirectToAction("SalesList");
        }
        [HttpPost]
        public ActionResult EditSale(Sale item)
        {
            item.Date = DateTime.Now;
            repository.SaveEditedSale(item);
            return RedirectToAction("SalesList");
        }
        [HttpGet]
        public ActionResult DeleteSale(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Sale item = repository.FindSale(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        [HttpPost, ActionName("DeleteSale")]
        public ActionResult DeleteSaleConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Sale item = repository.FindSale(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            repository.DeleteSale(item);
            return RedirectToAction("SalesList");
        }

        //IMAGES
        public FileContentResult GetImage(int Id)
        {
            Image item = repository.FindImage(Id);
            if (item != null)
            {
                return File(item.ImageContent, item.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}
