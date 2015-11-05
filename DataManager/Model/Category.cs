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
    public class Category
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }
       
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
       
        [Required]
        [StringLength(3000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
       
        public byte[] Image { get; set; }
       
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }


        public ICollection<Category> ParentCategories { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<CategoryType> CategoryTypes { get; set; }
        public ICollection<Good> Goods { get; set; }
        public Category() {
            Image = new byte[0];
            Goods = new List<Good>();
            CategoryTypes = new List<CategoryType>();
            ParentCategories = new List<Category>();
            ChildCategories = new List<Category>();
        }

    }
}
