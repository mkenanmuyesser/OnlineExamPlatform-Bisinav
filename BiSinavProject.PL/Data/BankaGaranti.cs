//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BiSinavProject.PL.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BankaGaranti
    {
        public int BankaGarantiKey { get; set; }
        public string StoreKey { get; set; }
        public string ProvisionPassword { get; set; }
        public string CompanyName { get; set; }
        public string TerminalId { get; set; }
        public string TerminalUserId { get; set; }
        public string TerminalMerchantId { get; set; }
        public string CustomerIpAddress { get; set; }
        public string CustomerEmailAddress { get; set; }
        public Nullable<System.Guid> GuncelleKisiKey { get; set; }
        public Nullable<System.DateTime> GuncelleTarih { get; set; }
    }
}
