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
    
    public partial class CreditPayment
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public decimal MainAmount { get; set; }
        public int Type { get; set; }
        public System.DateTime Date { get; set; }
        public decimal PercentsAmount { get; set; }
    
        public virtual Credit Credit { get; set; }
    }
}
