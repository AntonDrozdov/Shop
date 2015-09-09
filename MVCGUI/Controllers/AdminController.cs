using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataManager.Abstract;

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

    }
}
