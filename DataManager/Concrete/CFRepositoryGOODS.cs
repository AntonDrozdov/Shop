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
        public IQueryable<Good> Goods
        {
            get { return dbcontex.Goods; }
        }
        public void CreateGood(Good good)
        {
            dbcontex.Goods.Add(good);
            dbcontex.SaveChanges();
        }
        public Good FindGood(int? id)
        {
            if (dbcontex.Goods.Find(id) != null)
            {
                IQueryable<Good> goods = dbcontex.Goods.Where(p => p.Id == id).Select(p => p);
                return goods.First();
            }
            else
            {
                return null;
            }

        }
        public void SaveEditedGood(Good good)
        {
            dbcontex.Entry(good).State = EntityState.Modified;
            dbcontex.SaveChanges();
        }
        public void DeleteGood(Good good)
        {
            dbcontex.Goods.Remove(good);
            dbcontex.SaveChanges();
        }
    }
}
