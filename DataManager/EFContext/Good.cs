//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataManager.EFContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Good
    {
        public Good()
        {
            this.Category = new HashSet<Category>();
            this.Purchase = new HashSet<Purchase>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
    
        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
