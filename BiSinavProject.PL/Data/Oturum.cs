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
    
    public partial class Oturum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oturum()
        {
            this.OturumDenemes = new HashSet<OturumDeneme>();
        }
    
        public int OturumKey { get; set; }
        public string Adi { get; set; }
        public System.DateTime Baslangic { get; set; }
        public System.DateTime Bitis { get; set; }
        public Nullable<System.Guid> KayitKisiKey { get; set; }
        public Nullable<System.DateTime> KayitTarih { get; set; }
        public Nullable<System.Guid> GuncelleKisiKey { get; set; }
        public Nullable<System.DateTime> GuncelleTarih { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OturumDeneme> OturumDenemes { get; set; }
    }
}
