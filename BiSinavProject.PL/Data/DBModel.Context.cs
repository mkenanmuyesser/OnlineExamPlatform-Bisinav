﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Il> Ils { get; set; }
        public virtual DbSet<IlKur> IlKurs { get; set; }
        public virtual DbSet<Kur> Kurs { get; set; }
        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Icerik> Iceriks { get; set; }
        public virtual DbSet<Ilan> Ilans { get; set; }
        public virtual DbSet<Manset> Mansets { get; set; }
        public virtual DbSet<Dagitim> Dagitims { get; set; }
        public virtual DbSet<Servi> Servis { get; set; }
        public virtual DbSet<ServisDeneme> ServisDenemes { get; set; }
        public virtual DbSet<ServisEDergi> ServisEDergis { get; set; }
        public virtual DbSet<ServisTip> ServisTips { get; set; }
        public virtual DbSet<Sirket> Sirkets { get; set; }
        public virtual DbSet<Ayar> Ayars { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<BolgeTip> BolgeTips { get; set; }
        public virtual DbSet<Tanitim> Tanitims { get; set; }
        public virtual DbSet<BankaGaranti> BankaGarantis { get; set; }
        public virtual DbSet<Sipari> Siparis { get; set; }
        public virtual DbSet<SiparisDeneme> SiparisDenemes { get; set; }
        public virtual DbSet<SiparisDurumTip> SiparisDurumTips { get; set; }
        public virtual DbSet<SiparisEDergi> SiparisEDergis { get; set; }
        public virtual DbSet<Alan> Alans { get; set; }
        public virtual DbSet<Deneme> Denemes { get; set; }
        public virtual DbSet<DenemeKullanici> DenemeKullanicis { get; set; }
        public virtual DbSet<DenemeKullaniciSoru> DenemeKullaniciSorus { get; set; }
        public virtual DbSet<Der> Ders { get; set; }
        public virtual DbSet<Oturum> Oturums { get; set; }
        public virtual DbSet<OturumDeneme> OturumDenemes { get; set; }
        public virtual DbSet<SonucDetay> SonucDetays { get; set; }
        public virtual DbSet<Soru> Sorus { get; set; }
        public virtual DbSet<Kayit> Kayits { get; set; }
        public virtual DbSet<KayitKategori> KayitKategoris { get; set; }
        public virtual DbSet<EDergi> EDergis { get; set; }
        public virtual DbSet<EDergiSayfa> EDergiSayfas { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<KategoriKitap> KategoriKitaps { get; set; }
        public virtual DbSet<Kitap> Kitaps { get; set; }
    }
}
