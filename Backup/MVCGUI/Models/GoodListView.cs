using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataManager.Model;

namespace MVCGUI.Models
{
    public class GoodListView
    {
        public int? parentcat { get; set; }

        public IEnumerable<Good> Goods { get; set; }
        public PagingInfo paginginfo { get; set; }
    }
}