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
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Client = new HashSet<Client>();
            this.Comment = new HashSet<Comment>();
            this.Employee = new HashSet<Employee>();
            this.Request = new HashSet<Request>();
            this.Roles = new HashSet<Roles>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserLastName { get; set; }
        public Nullable<System.DateTime> UserBirthDate { get; set; }
        public string UserPassportSerialNumber { get; set; }
    
        public virtual ICollection<Client> Client { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}
