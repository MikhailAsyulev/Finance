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
    
    public partial class Employee
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
    }
}