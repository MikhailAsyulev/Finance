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
    
    public partial class Deposit
    {
        public Deposit()
        {
            this.Transactions = new HashSet<Transaction>();
            this.Clients = new HashSet<Client>();
        }
    
        public int Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public decimal Balance { get; set; }
        public int DepositTypeId { get; set; }
    
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
