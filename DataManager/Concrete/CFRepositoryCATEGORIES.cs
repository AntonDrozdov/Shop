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
        public IQueryable<Category> Categories (){
            return  dbcontex.Categories.Include(c => c.CategoryTypes).Include(c => c.ParentCategories);
        }
    
        public IQueryable<Category> PureCategories(){
            return  dbcontex.Categories;
        }
        public IQueryable<Category> Categories(int? cattype, int? parentcat)//без page чтобы знать общее количество в категории
        {
            return dbcontex.Categories
                .Where(c => cattype == null || c.CategoryTypes.FirstOrDefault(ctype => ctype.Id == cattype) != null)
                .Where(c => parentcat == null || c.ParentCategories.FirstOrDefault(pc => pc.Id == parentcat) != null);
        }
        public IQueryable<Category> Categories(int page, int? cattype, int? parentcat)
        {

            return dbcontex.Categories
                .Include(c => c.CategoryTypes)
                .Include(c => c.ParentCategories)
                .Where(c => cattype == null || c.CategoryTypes.FirstOrDefault(ctype => ctype.Id == cattype) != null)
                .Where(c => parentcat == null || c.ParentCategories.FirstOrDefault(pc => pc.Id == parentcat) != null)
                .OrderBy(c=>c.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
        }
            
        public void CreateCategory(Category category, int[] selected, int[] selected2)
        {
            Category newcategory = category;
            newcategory.CategoryTypes.Clear();
            if (selected != null)
            {
                foreach (CategoryType item in CategoryTypes.Where(item => selected.Contains(item.Id)))
                {
                    newcategory.CategoryTypes.Add(item);
                }
            }
            newcategory.ParentCategories.Clear();
            if (selected2 != null)
            {
                foreach (Category item in Categories().Where(item => selected2.Contains(item.Id)))
                {
                    newcategory.ParentCategories.Add(item);
                }
            }

            dbcontex.Categories.Add(newcategory);
            dbcontex.SaveChanges();
        }
        public void SaveEditedCategory(Category category, int[] selected, int[] selected2)
        {
            Category newcategory = FindCategory(category.Id);

            newcategory.Title = category.Title;
            newcategory.Description = category.Description;

            newcategory.CategoryTypes.Clear();
            if (selected != null)
            {
                foreach (CategoryType item in CategoryTypes.Where(item => selected.Contains(item.Id)))
                {
                    newcategory.CategoryTypes.Add(item);
                }
            }
            newcategory.ParentCategories.Clear();
            if (selected2 != null)
            {
                foreach (Category item in Categories().Where(item => selected2.Contains(item.Id)))
                {
                    newcategory.ParentCategories.Add(item);
                }
            }

            dbcontex.Entry(newcategory).State = EntityState.Modified; 
            dbcontex.SaveChanges();
        }
        public Category FindCategory(int? id)
        {
            if (dbcontex.Categories.Find(id) != null)
            {
                Category category = dbcontex.Categories.Where(c => c.Id == id).Include(c=>c.CategoryTypes).Include(c=>c.ParentCategories).Include(c=>c.ChildCategories).Select(c => c).First();
                return category;
            }
            else return null;
        }
        public void DeleteCategory(Category category)
        {
            dbcontex.Categories.Remove(category);
            dbcontex.SaveChanges();
        }
    }
}
