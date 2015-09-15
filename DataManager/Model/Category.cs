using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public ICollection<CategoryType> CategoryTypes { get; set; }
        public ICollection<Good> Goods { get; set; }
        public Category() { 
            Goods = new List<Good>();
            CategoryTypes = new List<CategoryType>(); 
        }

    }
}
