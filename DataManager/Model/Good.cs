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
    public class Good
    {

        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int? Amount { get; set; }
        public byte[] Image { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public ICollection<Discount> Discounts { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Good() { 
            Categories = new List<Category>();
            Discounts = new List<Discount>();
        }

    }
}
