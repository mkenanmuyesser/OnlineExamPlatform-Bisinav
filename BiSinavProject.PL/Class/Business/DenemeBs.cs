using BiSinavProject.PL.Data;
using BiSinavProject.PL.Class.CustomType;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace BiSinavProject.PL.Class.Business
{
    public class DenemeBs
    {
        public static void SoruOlustur(int denemeKey, SinavDurumEnum sinavDurumEnum)
        {
            SessionHelper.DenemeData = null;

            var membershipUser = Membership.GetUser(true);

            if (membershipUser == null)
            {
                using (var service = new DBEntities())
                {
                    var sorular = new List<SoruType>();

                    var aktifDeneme = service.Denemes.
                                              AsNoTracking().
                                              Single(p => p.DenemeKey == denemeKey);

                    service.Sorus.
                           AsNoTracking().
                           Include("Deneme").
                           Include("Der").
                           Include("DenemeKullaniciSorus").
                           Where(p => p.AktifMi &&
                                      p.DenemeKey != null &&
                                      p.DenemeKey.Value == denemeKey).
                            OrderBy(p =>
                            new
                            {
                                DenemeSira = p.Deneme.Sira,
                                Sira = p.Sira,
                            }).
                            ToList().
                            ForEach
                            (p =>
                             sorular.Add
                                 (
                                     new SoruType
                                     {
                                         DersKey = p.DersKey,
                                         DersAdi = p.Der.Adi,
                                         SoruKey = p.SoruKey,
                                         SoruNo = sorular.Count(x => x.DersKey == p.DersKey) + 1,
                                         Soru = p.Soru1,
                                         SoruDogruSik = p.DogruSik,
                                         SoruSikSayisi = p.SikSayisi,
                                         SoruAciklama = p.Aciklama,
                                         SoruIsaretlenenSik =  null,
                                         SoruGoruntulendiMi = false,
                                     }
                                 )
                            );

                    if (sorular.Count() == 0)
                    {
                        Page page = HttpContext.Current.Handler as Page;
                        if (page != null)
                        {
                            PageHelper.MessageBox(page, "Sınava ait soru girilmemiştir.");
                        }
                        return;
                    }
                    else
                    {

                        DenemeType data = new DenemeType();

                            //1den başlat
                            data = new DenemeType
                            {
                                DenemeKullaniciKey = -1,
                                AktifDenemeKey = denemeKey,
                                AktifDersKey = sorular.First().DersKey,
                                AktifDenemeAdi = aktifDeneme.Adi,
                                Sorular = sorular,
                                AktifSoruNo = 1,
                                SinavMode = SinavModeEnum.SinavAktif,
                                BaslangicZamani = DateTime.Now,
                                IslemZamani = DateTime.Now,
                                GecenSure = 0,
                                ToplamDakika = aktifDeneme.Sure,
                            };                    

                        SessionHelper.DenemeData = data;
                    }
                }
            }
            else
            {
                Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                using (var service = new DBEntities())
                {
                    var deneme = service.Denemes.
                                       AsNoTracking().
                                       Single(p => p.DenemeKey == denemeKey);


                    DenemeKullanici aktifDenemeKullanici = null;

                    var aktifDenemeKullanicilar = service.DenemeKullanicis.
                                                         Where(p => p.AktifMi &&
                                                                    p.DenemeKey == denemeKey &&
                                                                    p.UserId == userId);

                    switch (sinavDurumEnum)
                    {
                        case SinavDurumEnum.SinavYeni:
                            //eski sınavların hepsini kapat 

                            foreach (var aktifDenemeKullaniciItem in aktifDenemeKullanicilar)
                            {
                                aktifDenemeKullaniciItem.AktifMi = false;
                            }

                            //yeni bir sınav aç ve veritabanına kaydet
                            aktifDenemeKullanici = new DenemeKullanici
                            {
                                DenemeKey = denemeKey,
                                UserId = userId,
                                BaslangicZamani = DateTime.Now,
                                BitisZamani = DateTime.Now,
                                GecenSure = 0,

                                KayitKisiKey = userId,
                                KayitTarih = DateTime.Now,
                                GuncelleKisiKey = userId,
                                GuncelleTarih = DateTime.Now,
                                AktifMi = true,
                            };
                            service.DenemeKullanicis.Add(aktifDenemeKullanici);

                            break;
                        case SinavDurumEnum.SinavDevam:

                            if (aktifDenemeKullanicilar.Count() > 1)
                            {
                                Page page = HttpContext.Current.Handler as Page;
                                if (page != null)
                                {
                                    PageHelper.MessageBox(page, "Birden fazla aktif sınavınız bulunmaktadır. Lütfen yeni bir sınav başlatınız.");
                                }
                                return;
                            }

                            aktifDenemeKullanici = aktifDenemeKullanicilar.Single();

                            break;
                    }

                    service.SaveChanges();

                    var sorular = new List<SoruType>();

                    var aktifDeneme = service.Denemes.
                                              AsNoTracking().
                                              Single(p => p.DenemeKey == denemeKey);

                    service.Sorus.
                           AsNoTracking().
                           Include("Deneme").
                           Include("Der").
                           Include("DenemeKullaniciSorus").
                           Where(p => p.AktifMi &&
                                      p.DenemeKey != null &&
                                      p.DenemeKey.Value == denemeKey).
                            OrderBy(p =>
                            new
                            {
                                DenemeSira = p.Deneme.Sira,
                                Sira = p.Sira,
                            }).
                            ToList().
                            ForEach
                            (p =>
                             sorular.Add
                                 (
                                     new SoruType
                                     {
                                         DersKey = p.DersKey,
                                         DersAdi = p.Der.Adi,
                                         SoruKey = p.SoruKey,
                                         SoruNo = sorular.Count(x => x.DersKey == p.DersKey) + 1,
                                         Soru = p.Soru1,
                                         SoruDogruSik = p.DogruSik,
                                         SoruSikSayisi = p.SikSayisi,
                                         SoruAciklama = p.Aciklama,
                                         SoruIsaretlenenSik = EskiSoruDogrula(p, aktifDenemeKullanici.DenemeKullaniciKey) != null ?
                                         EskiSoruDogrula(p, aktifDenemeKullanici.DenemeKullaniciKey).CevapSik :
                                         null,
                                         SoruGoruntulendiMi = EskiSoruDogrula(p, aktifDenemeKullanici.DenemeKullaniciKey) != null ?
                                         true :
                                         false,
                                     }
                                 )
                            );

                    if (sorular.Count() == 0)
                    {
                        Page page = HttpContext.Current.Handler as Page;
                        if (page != null)
                        {
                            PageHelper.MessageBox(page, "Sınava ait soru girilmemiştir.");
                        }
                        return;
                    }
                    else
                    {
                        //daha önce kalınmış bir soru var mı?
                        DenemeType data = new DenemeType();
                        if (aktifDenemeKullanici.AktifSoruKey == null)
                        {
                            //1den başlat
                            data = new DenemeType
                            {
                                DenemeKullaniciKey = aktifDenemeKullanici.DenemeKullaniciKey,
                                AktifDenemeKey = denemeKey,
                                AktifDersKey = sorular.First().DersKey,
                                AktifDenemeAdi = aktifDeneme.Adi,
                                Sorular = sorular,
                                AktifSoruNo = 1,
                                SinavMode = SinavModeEnum.SinavAktif,
                                BaslangicZamani = aktifDenemeKullanici.BaslangicZamani,
                                IslemZamani = DateTime.Now,
                                GecenSure = 0,
                                ToplamDakika = aktifDeneme.Sure,
                            };
                            aktifDenemeKullanici.AktifSoruKey = sorular.First().SoruKey;
                        }
                        else
                        {
                            //son kalınan soruyu bul
                            var aktifSoru = sorular.Single(p => p.SoruKey == aktifDenemeKullanici.AktifSoruKey);

                            data = new DenemeType
                            {
                                DenemeKullaniciKey = aktifDenemeKullanici.DenemeKullaniciKey,
                                AktifDenemeKey = denemeKey,
                                AktifDersKey = aktifSoru.DersKey,
                                AktifDenemeAdi = aktifDeneme.Adi,
                                Sorular = sorular,
                                AktifSoruNo = aktifSoru.SoruNo,
                                SinavMode = SinavModeEnum.SinavAktif,
                                BaslangicZamani = DateTime.Now,
                                IslemZamani = DateTime.Now,
                                GecenSure = aktifDenemeKullanici.GecenSure,
                                ToplamDakika = aktifDeneme.Sure,
                            };
                        }


                        SessionHelper.DenemeData = data;

                        service.SaveChanges();
                    }
                }
            }
        }

        private static DenemeKullaniciSoru EskiSoruDogrula(Soru soru, int denemeKullaniciKey)
        {
            return soru.DenemeKullaniciSorus.SingleOrDefault(x => x.DenemeKullaniciKey == denemeKullaniciKey);
        }
    }
}