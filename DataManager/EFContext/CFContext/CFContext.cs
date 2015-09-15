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
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(c => c.Goods)
                .WithMany(s => s.Categories)
                .Map(t => t.MapLeftKey("CategoryId")
                .MapRightKey("GoodId")
                .ToTable("CategoryGood"));
            modelBuilder.Entity<CategoryType>().HasMany(c => c.Categories)
                    .WithMany(s => s.CategoryTypes)
                    .Map(t => t.MapLeftKey("CategoryTypeId")
                    .MapRightKey("CategoryId")
                    .ToTable("CategoryCategoryType"));
            

        }
        
    }
}
