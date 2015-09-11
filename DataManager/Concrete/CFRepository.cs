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
    public partial class CFRepository: IRepository
    {
        private CFContext dbcontex = new CFContext();
        private bool disposing = false;

        public virtual void Dispose(bool disposing) {
            if (!this.disposing)
            {
                if (disposing) 
                {
                    this.dbcontex.Dispose();
                }
            }
            this.disposing = true;

        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
