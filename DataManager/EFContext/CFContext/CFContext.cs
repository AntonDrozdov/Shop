using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using DataManager.Model;

namespace DataManager.EFContext.CFContext
{
    public class CFContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}
