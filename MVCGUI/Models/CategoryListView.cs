using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataManager.Model;

namespace MVCGUI.Models
{
    public class CategoryListView
    {
        public int? cattype { get; set; }
        public int? parentcat { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public PagingInfo paginginfo { get; set; }
    }
}