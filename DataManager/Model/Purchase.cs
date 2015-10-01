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
    public class Purchase
    {
        
        public int Id { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime)]
        public DateTime Date { get; set; }
        public int? Amount { get; set; }
        public int PurchasePricePerItem { get; set; }

        public int? GoodId { get; set; }
        public Good Good { get; set; }
    }
}
