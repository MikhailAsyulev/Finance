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
        }
    
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RequestType { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
