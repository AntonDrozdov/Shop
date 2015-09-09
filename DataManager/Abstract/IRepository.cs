using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataManager.Model;


namespace DataManager.Abstract
{
    public interface IRepository
    {
        IQueryable<Good> Goods { get; }
        IQueryable<Purchase> Purchases { get; }
    }
}
