﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace DataManager.Model
{
    public class CategoryType
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(3000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
