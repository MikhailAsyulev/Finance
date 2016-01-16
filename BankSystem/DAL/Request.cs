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
    
    public partial class Request
    {
        public Request()
        {
            this.Comment = new HashSet<Comment>();
            this.Credit = new HashSet<Credit>();
            this.Deposit = new HashSet<Deposit>();
        }
    
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int Amount { get; set; }
        public int State { get; set; }
        public int Type { get; set; }
        public Nullable<int> CreditTypeId { get; set; }
        public Nullable<int> DepositTypeId { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> AssignedOperatorId { get; set; }
        public Nullable<int> AssignedSecurityWorkerId { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Credit> Credit { get; set; }
        public virtual CreditType CreditType { get; set; }
        public virtual ICollection<Deposit> Deposit { get; set; }
        public virtual DepositType DepositType { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        public virtual UserProfile UserProfile2 { get; set; }
    }
}
