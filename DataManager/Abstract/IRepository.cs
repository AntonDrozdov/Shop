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
        void CreateGood(Good good, int[] selectedcategories);
        Good FindGood(int? id);
        void SaveEditedGood(Good good, int[] selectedcategories);
        void DeleteGood(Good good);
        
        //PURCHASES
        IQueryable<Purchase> Purchases { get; }
        void CreatePurchase(Purchase purchase);
        Purchase FindPurchase(int? id);
        void SaveEditedPurchase(Purchase purchase);
        void DeletePurchase(Purchase purchase);
        
        //CATEGORIES
        IQueryable<Category> Categories();
        IQueryable<Category> PureCategories();
        IQueryable<Category> Categories(int? cattype, int? parentcat);
        IQueryable<Category> Categories(int page, int? cattype, int? parentcat);
        void CreateCategory(Category category, int[] selected, int[] selected2);
        Category FindCategory(int? id);
        void SaveEditedCategory(Category category, int[] selected, int[] selected2);
        void DeleteCategory(Category category);

        //CATEGORYTypes
        IQueryable<CategoryType> CategoryTypes { get; }
        void CreateCategoryType(CategoryType categorytype);
        CategoryType FindCategoryType(int? id);
        void SaveEditedCategoryType(CategoryType categorytype);
        void DeleteCategoryType(CategoryType categorytype);


    }
}
