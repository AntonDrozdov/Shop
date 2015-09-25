using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

using DataManager.EFContext.CFContext;
using DataManager.Model;
using DataManager.Abstract;

namespace DataManager.Concrete
{
    public partial class CFRepository : IRepository
    {
        public IQueryable<Sale> Sales() {
            return dbcontex.Sales.Include(i => i.Good); 
        }
        public IQueryable<Sale> Sales(int? good) {
            return dbcontex.Sales.Include(i => i.Good)
                .Where(i => good == null || i.GoodId == good);
        }
        public IQueryable<Sale> Sales(int page, int? good)
        {
            return dbcontex.Sales.Include(i => i.Good)
                .Where(i => good == null || i.GoodId == good)
                .OrderByDescending(i => i.Date)
                .Skip((page - 1) * PageSize)
                .Take(PageSize); ; 
        }
        public void CreateSale(Sale item) {
            dbcontex.Sales.Add(item);
            dbcontex.SaveChanges();
        }
        public Sale FindSale(int? id) {
            if (dbcontex.Sales.Find(id) != null)
            {
                IQueryable<Sale> items = dbcontex.Sales.Include(i => i.Good).Where(i => i.Id == id).Select(i => i);
                return items.First();
            }
            else
            {
                return null;
            }
        }
        public void SaveEditedSale(Sale item) {
            dbcontex.Entry(item).State = EntityState.Modified;
            dbcontex.SaveChanges();
        }
        public void DeleteSale(Sale item) {
            dbcontex.Sales.Remove(item);
            dbcontex.SaveChanges();
        }
    }
}

