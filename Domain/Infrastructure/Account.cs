//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public long Id { get; set; }
        public System.DateTime RegisteredOn { get; set; }
        public System.DateTime AccountExpirationDate { get; set; }
        public Nullable<long> TenderTypeIdForEmail { get; set; }
    
        public virtual TenderType TenderType { get; set; }
        public virtual User User { get; set; }
    }
}
