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
    
    public partial class SiparisDurumTip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SiparisDurumTip()
        {
            this.Siparis = new HashSet<Sipari>();
        }
    
        public int SiparisDurumTipNo { get; set; }
        public string SiparisDurumTipAdi { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sipari> Siparis { get; set; }
    }
}