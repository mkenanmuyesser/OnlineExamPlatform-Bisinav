using BiSinavProject.PL.Class.CustomType;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiSinavProject.PL.Class.Business
{
    public class BesteBesBs
    {
        public static void SoruOlustur(int dersKey)
        {
            SessionHelper.BesteBesData = null;

            using (var service = new DBEntities())
            {
                var sorular = new List<SoruType>();
                int soruNo = 1;
                var ders = service.Ders.
                                   AsNoTracking().
                                   Single(p => p.DersKey == dersKey);

                service.Sorus.
                        AsNoTracking().
                        Where(p =>
                             p.AktifMi &&
                             p.DersKey == dersKey &&
                             (p.SadeceDeneme == false || p.SadeceDeneme == null)
                             ).
                         OrderBy(p => Guid.NewGuid()).
                         Take(5).
                         ToList().
                         ForEach
                         (p =>
                          sorular.Add
                              (
                                  new SoruType
                                  {
                                      SoruKey = p.SoruKey,
                                      SoruNo = (soruNo++),
                                      Soru = p.Soru1,
                                      SoruDogruSik = p.DogruSik,
                                      SoruSikSayisi = p.SikSayisi,
                                      SoruAciklama = p.Aciklama,
                                      SoruIsaretlenenSik = null,
                                      SoruGoruntulendiMi = false,
                                  }
                              )
                         );
                BesteBesType data = new BesteBesType
                {
                    AktifDersKey = dersKey,
                    AktifDersAdi = ders.Adi,
                    Sorular = sorular,
                    AktifSoruNo = 1,
                    SinavMode = SinavModeEnum.SinavBasla,
                    BaslangicZamani = DateTime.Now,
                };

                SessionHelper.BesteBesData = data;
            }

        }
    }
}