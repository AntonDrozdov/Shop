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
    }
}
