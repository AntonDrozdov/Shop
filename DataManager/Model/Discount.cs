using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DataManager.Model
{
    public class Discount
    {
        public int Id { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
        public DateTime BeginDate { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
        public DateTime EndDate { get; set; }
        public int DiscountRate { get; set; }

        public ICollection<Good> Goods { get; set; }
        public Discount() { Goods = new List<Good>();}

    }
}
