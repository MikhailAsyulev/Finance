﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FinanceEntities : DbContext
    {
        public FinanceEntities()
            : base("name=FinanceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Credit> Credit { get; set; }
        public virtual DbSet<CreditPayment> CreditPayment { get; set; }
        public virtual DbSet<CreditType> CreditType { get; set; }
        public virtual DbSet<Deposit> Deposit { get; set; }
        public virtual DbSet<DepositPayment> DepositPayment { get; set; }
        public virtual DbSet<DepositType> DepositType { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }
        public virtual DbSet<GlobalDate> GlobalDate { get; set; }
    }
}
