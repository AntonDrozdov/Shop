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
        public DbSet<Discount> Discounts{ get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Image> Images { get; set; }

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
            modelBuilder.Entity<Category>().HasMany(c => c.ChildCategories)
                .WithMany(s => s.ParentCategories)
                .Map(t => t.MapLeftKey("ParentCategoryId")
                .MapRightKey("ChildCategoryId")
                .ToTable("ParentCategories"));
            modelBuilder.Entity<Discount>().HasMany(c => c.Goods)
                .WithMany(i=>i.Discounts)
                .Map(t => t.MapLeftKey("DiscountId")
                .MapRightKey("GoodId")
                .ToTable("GoodDiscounts"));
            modelBuilder.Entity<Good>().HasMany(c => c.Images)
                .WithMany(i => i.Goods)
                .Map(t => t.MapLeftKey("GoodId")
                .MapRightKey("ImageId")
                .ToTable("GoodImages"));

        }
        
    }
}
