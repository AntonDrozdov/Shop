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
    public class Good
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0,1000)]
        public int? Amount { get; set; }

        public ICollection<Image> Images { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Good() {
            Amount = 0;
            Images = new List<Image>();
            Categories = new List<Category>();
            Discounts = new List<Discount>();
        }

    }
}
