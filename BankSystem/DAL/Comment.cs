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
    
    public partial class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserProfileId { get; set; }
        public bool IsInternal { get; set; }
        public int RequestId { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}