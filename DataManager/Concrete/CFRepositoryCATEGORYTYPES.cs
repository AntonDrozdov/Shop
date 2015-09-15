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
        public IQueryable<CategoryType> CategoryTypes
        {
            get { return dbcontex.CategoryTypes; }
        }
        public void CreateCategoryType(CategoryType categorytype)
        {
            CategoryType newcategory = categorytype;

            dbcontex.CategoryTypes.Add(categorytype);
            dbcontex.SaveChanges();
        }
        public CategoryType FindCategoryType(int? id)
        {
            if (dbcontex.CategoryTypes.Find(id) != null)
            {
                CategoryType categorytype = dbcontex.CategoryTypes.Where(c => c.Id == id).Select(c => c).First();
                return categorytype;
            }
            else return null;
        }
        public void SaveEditedCategoryType(CategoryType categorytype)
        {
            CategoryType newcategorytype = FindCategoryType(categorytype.Id);

            newcategorytype.Title = categorytype.Title;
            newcategorytype.Description = categorytype.Description;


            dbcontex.Entry(newcategorytype).State = EntityState.Modified;
            dbcontex.SaveChanges();
        }
        public void DeleteCategoryType(CategoryType categorytype)
        {
            dbcontex.CategoryTypes.Remove(categorytype);
            dbcontex.SaveChanges();
        }
    }
}
