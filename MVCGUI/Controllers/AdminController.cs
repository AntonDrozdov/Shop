using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataManager.Abstract;
using DataManager.Model;

namespace MVCGUI.Controllers
{
    public class AdminController : Controller
    {
        private IRepository repository;
        public AdminController(IRepository _repository) {
            this.repository = _repository;
        }

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






        public ActionResult GoodsList()
        {
            return View(repository.Goods.ToList());
        }

    }
}
