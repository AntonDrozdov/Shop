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
        //GOODS
        IQueryable<Good> Goods { get; }
        void CreateGood(Good good);
        Good FindGood(int? id);
        void SaveEditedGood(Good good);
        void DeleteGood(Good good);
        
        //PURCHASES
        IQueryable<Purchase> Purchases { get; }
        void CreatePurchase(Purchase purchase);
        Purchase FindPurchase(int? id);
        void SaveEditedPurchase(Purchase purchase);
        void DeletePurchase(Purchase purchase);
        
    }
}
