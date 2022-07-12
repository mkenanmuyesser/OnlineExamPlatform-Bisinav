using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace BiSinavProject.PL.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BiSinavWcfService
    {
        [OperationContract]
        public string SifreUret(string servisKod)
        {
            string kod = null;

            using (var service = new DBEntities())
            {
                //servis aktif durumdamı kontrol et
                int servisTip = Convert.ToInt32(KodServisTip.ServisDagitim);
                Servi servis = service.Servis.
                                      AsNoTracking().
                                      SingleOrDefault(p =>
                                                         p.AktifMi &&
                                                         p.ServisTipKey == servisTip &&
                                                         p.Baslangic <= DateTime.Now &&
                                                         p.Bitis >= DateTime.Now &&
                                                         p.Kod != null &&
                                                         p.Kod == servisKod);
                if (servis != null)
                {
                    //limiti aşan kod varmı?
                    List<Dagitim> dagitimlar = service.Dagitims.
                                                       AsNoTracking().
                                                       Where(p => p.ServisKey == servis.ServisKey).
                                                       ToList();

                    if (servis.Limit > dagitimlar.Count())
                    {
                        //bütün kontrolleri geçti. şimdi servis adına bir kod üretip veritabanına yazıcaz

                        Random rnd = new Random();
                        kod = CodeHelper.GenerateNewCode(rnd, 1);

                        Dagitim dagitim = new Dagitim
                        {
                            ServisKey = servis.ServisKey,
                            UserId = null,
                            Kod = kod,

                            KayitKisiKey = Guid.Empty,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = Guid.Empty,
                            GuncelleTarih = DateTime.Now,

                            KullanildiMi = false,
                            AktifMi = true,
                        };
                        service.Dagitims.Add(dagitim);

                        service.SaveChanges();
                    }
                }
            }

            return kod;
        }
    }
}
