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
    
    public partial class DenemeKullaniciSoru
    {
        public int DenemeKullaniciSoruKey { get; set; }
        public int DenemeKullaniciKey { get; set; }
        public int SoruKey { get; set; }
        public string CevapSik { get; set; }
        public System.DateTime IslemZamani { get; set; }
        public Nullable<System.Guid> KayitKisiKey { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<System.Guid> GuncelleKisiKey { get; set; }
        public Nullable<System.DateTime> GuncelleTarih { get; set; }
        public bool AktifMi { get; set; }
    
        public virtual DenemeKullanici DenemeKullanici { get; set; }
        public virtual Soru Soru { get; set; }
    }
}
