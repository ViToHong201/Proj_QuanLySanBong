//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Management_Football_Pitch
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        public string id_invoice { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<System.DateTime> invoice_date { get; set; }
        public Nullable<System.DateTime> pay_day { get; set; }
        public string id_deposit_voice { get; set; }
    
        public virtual Deposit_Invoice Deposit_Invoice { get; set; }
    }
}
