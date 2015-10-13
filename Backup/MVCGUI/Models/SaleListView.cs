using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataManager.Model;

namespace MVCGUI.Models
{
    public class SaleListView
    {
        public int? good { get; set; }

        public IEnumerable<Sale> Sales { get; set; }
        public PagingInfo paginginfo { get; set; }
    }
}

