using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Model
{
    public class Purchase
    {
        
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? Amount { get; set; }
        public int PurchasePricePerItem { get; set; }

        public int? GoodId { get; set; }
        public Good Good { get; set; }
    }
}
