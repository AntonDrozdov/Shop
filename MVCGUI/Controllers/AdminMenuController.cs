using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCGUI.Controllers
{
    public class AdminMenuController : Controller
    {

        public PartialViewResult AdminMenu(string SelectedSection= "Goods")
        {
            List<string> itemstoshow = new List<string> { "Список позиций", "Закупки", "Продажи", "Категории", "Типы категорий", "Скидки" };
            List<string> items = new List<string> { "Goods", "Purchases", "Sales", "Categories", "CategoryTypes","Discounts" };
            ViewBag.Items = items;
            ViewBag.SelectedSection = SelectedSection;
            return PartialView();
        }

    }
}
