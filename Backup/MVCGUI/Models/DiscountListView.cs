using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataManager.Model;

namespace MVCGUI.Models
{
    public class DiscountListView
    {
        public IEnumerable<Discount> Items { get; set; }
        public PagingInfo paginginfo { get; set; }
    }
}