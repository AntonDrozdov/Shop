using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Data.Entity;

using DataManager.EFContext.CFContext;
using DataManager.Model;
using DataManager.Abstract;

namespace DataManager.Concrete
{
    public partial class CFRepository : IRepository
    {
        public IQueryable<Discount> Discounts() {
            return dbcontex.Discounts
                .Include(i=>i.Goods)
                .OrderByDescending(i => i.EndDate);
        }
        public IQueryable<Discount> Discounts(int page) { 
            return dbcontex.Discounts
                .Include(i=>i.Goods)
                .OrderByDescending(i=>i.EndDate)
                .Skip((page-1)*PageSize)
                .Take(PageSize); }
        public void CreateDiscount(Discount item, int[] selected) {
            Discount newitem = item;

            newitem.Goods.Clear();
            if (selected != null)
            {
                foreach (Good g in Goods().Where(i => selected.Contains(i.Id)))
                {
                    newitem.Goods.Add(g);
                }
            }
            dbcontex.Discounts.Add(newitem);
            dbcontex.SaveChanges();
        }
        public Discount FindDiscount(int? id) {

            if (dbcontex.Discounts.Find(id) != null)
            {
                return dbcontex.Discounts.Where(p => p.Id == id).Select(p => p).Include(p => p.Goods).First();
                
            }
            else
            {
                return null;
            }
        }

        public void SaveEditedDiscount(Discount item, int[] selected) 
        {
            Discount newitem = FindDiscount(item.Id);

            newitem.Goods.Clear();
            if (selected != null)
            {
                foreach (Good g in Goods().Where(i => selected.Contains(i.Id)))
                {
                    newitem.Goods.Add(g);
                }
            }

            dbcontex.Entry(newitem).State = EntityState.Modified;
            dbcontex.SaveChanges();
        }
        public void DeleteDiscount(Discount item) {
            dbcontex.Discounts.Remove(item);
            dbcontex.SaveChanges();
        }
    }
}
