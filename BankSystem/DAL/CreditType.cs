//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CreditType
    {
        public CreditType()
        {
            this.Credit = new HashSet<Credit>();
            this.Request = new HashSet<Request>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public double Percent { get; set; }
        public double OverduePercent { get; set; }
        public long ReturnTerm { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string CurrencyShort { get; set; }
    
        public virtual ICollection<Credit> Credit { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }
}
