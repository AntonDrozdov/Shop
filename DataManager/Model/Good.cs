using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace DataManager.Model
{
    public class Good
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
    }
}
