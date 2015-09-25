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
        public IQueryable<Good> Goods() 
        {
            return dbcontex.Goods.Include(g => g.Categories);
        }
        public  IQueryable<Good> PureGoods()
        {
            return dbcontex.Goods;
        }
        public IQueryable<Good> Goods(int? parentcat)
        {
            return dbcontex.Goods.Include(g => g.Categories)
                .Where(g => parentcat == null || g.Categories.FirstOrDefault(c => c.Id == parentcat) != null);
        }
        public IQueryable<Good> Goods(int page, int? parentcat)
        {
            return dbcontex.Goods.Include(g => g.Categories)
                .Where(g => parentcat == null || g.Categories.FirstOrDefault(c => c.Id == parentcat) != null)
                .OrderBy(c => c.Title)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
        }
        public void CreateGood(Good good, int[] selected)
        {
            Good newgood = good;

            newgood.Categories.Clear();
            if (selected != null)
            {
                foreach (Category c in Categories().Where(cat => selected.Contains(cat.Id)))
                {
                    newgood.Categories.Add(c);
                }
            }
            dbcontex.Goods.Add(newgood);
            dbcontex.SaveChanges();
        }
        public Good FindGood(int? id)
        {
            if (dbcontex.Goods.Find(id) != null)
            {
                IQueryable<Good> goods = dbcontex.Goods.Where(p => p.Id == id).Select(p => p).Include(p=>p.Categories);
                return goods.First();
            }
            else
            {
                return null;
            }

        }
        public void SaveEditedGood(Good good, int[] selected)
        {
            Good newgood = FindGood(good.Id);
            
            newgood.Title = good.Title;
            newgood.Description = good.Description;
            newgood.Amount = good.Amount;
            
            newgood.Categories.Clear();
            if (selected != null)
            {
                foreach (Category c in Categories().Where(cat => selected.Contains(cat.Id)))
                {
                    newgood.Categories.Add(c);
                }
            }

            dbcontex.Entry(newgood).State = EntityState.Modified;
            dbcontex.SaveChanges();
        }
        public void DeleteGood(Good good)
        {
            dbcontex.Goods.Remove(good);
            dbcontex.SaveChanges();
        }
    }
}
