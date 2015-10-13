using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Data.Entity;

using DataManager.EFContext.CFContext;
using DataManager.Model;
using DataManager.Abstract;

namespace DataManager.Concrete
{
    public partial class CFRepository : IRepository
    {
        public void SaveImage(Image item){
            dbcontex.Images.Add(item);
            dbcontex.SaveChanges();
        }
        public Image FindImage(int? Id)
        {
            return dbcontex.Images.Find(Id);
        }
    }
}
