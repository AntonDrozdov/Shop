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
    public class CFRepository: IRepository
    {
        private CFContext dbcontex = new CFContext();
        public IQueryable<Good> Goods {
            get { return dbcontex.Goods; } 
        }
        public IQueryable<Purchase> Purchases {
            get { return dbcontex.Purchases.Include(p => p.Good); } 
        }
        public void CreatePurchase(Purchase purchase){
            dbcontex.Purchases.Add(purchase);
            dbcontex.SaveChanges();
        }
        public Purchase FindPurchase(int? id) {
            return dbcontex.Purchases.Find(id);
        }
        public void SaveEditedPurchase(Purchase purchase) {
            dbcontex.Entry(purchase).State = EntityState.Modified;
            dbcontex.SaveChanges();
        }
        public void DeletePurchase(Purchase purchase)
        {
            dbcontex.Purchases.Remove(purchase);
            dbcontex.SaveChanges();
        }
    }
}
