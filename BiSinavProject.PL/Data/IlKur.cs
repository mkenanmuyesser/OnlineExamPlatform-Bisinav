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
    
    public partial class IlKur
    {
        public int IlKursKey { get; set; }
        public int IlKey { get; set; }
        public int KursKey { get; set; }
        public Nullable<System.Guid> KayitKisiKey { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<System.Guid> GuncelleKisiKey { get; set; }
        public Nullable<System.DateTime> GuncelleTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    
        public virtual Il Il { get; set; }
        public virtual Kur Kur { get; set; }
    }
}
