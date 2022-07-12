using DevExpress.Web.ASPxEditors;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Security;

namespace BiSinavProject.PL.Class.Business
{
    public class KodBs
    {
        public static bool KodOlustur(int servisKey, int limit)
        {
            bool durum = false;
            MembershipUser user = Membership.GetUser(true);
            Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;

            using (var service = new DBEntities())
            {
                using (var dbContextTransaction = service.Database.BeginTransaction())
                {
                    try
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < limit; i++)
                        {
                            Dagitim dagitim = new Dagitim
                            {
                                ServisKey = servisKey,
                                UserId = null,
                                Kod = CodeHelper.GenerateNewCode(rnd, 10),

                                KayitKisiKey = userkey,
                                KayitTarih = DateTime.Now,
                                GuncelleKisiKey = userkey,
                                GuncelleTarih = DateTime.Now,

                                KullanildiMi = false,
                                AktifMi = true,
                            };
                            service.Dagitims.Add(dagitim);
                        }

                        service.SaveChanges();

                        dbContextTransaction.Commit();
                        durum = true;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            return durum;
        }

        public static int KullanilanKodSayisi(int servisKey)
        {
            int kullanilan = 0;

            using (var service = new DBEntities())
            {

                kullanilan = service.Servis.
                                     AsNoTracking().
                                     Include("Dagitims").
                                     Where(p => p.ServisKey == servisKey).
                                     Count(p => p.Dagitims.Any(x => x.KullanildiMi));
            }

            return kullanilan;
        }

        public static int UretilenKodSayisi(int servisKey)
        {
            int kullanilan = 0;

            using (var service = new DBEntities())
            {

                kullanilan = service.Dagitims.
                                     AsNoTracking().
                                     Count(p => p.ServisKey == servisKey);
            }

            return kullanilan;
        }

        public static ServisKodDurumTip KodAktifEt(string kod)
        {
            ServisKodDurumTip servisKodDurumTip = ServisKodDurumTip.Basarisiz;

            var membershipUser = Membership.GetUser(true);
            if (membershipUser != null)
            {
                Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                using (var service = new DBEntities())
                {
                    //koda bağlı servis aktif durumdamı kontrol et
                    var dagitim = service.Dagitims.
                                          Include("Servi").
                                          SingleOrDefault(p =>
                                                             p.Servi.AktifMi &&
                                                             p.Servi.Baslangic <= DateTime.Now &&
                                                             p.Servi.Bitis >= DateTime.Now &&
                                                             p.Kod == kod);
                    if (dagitim == null)
                    {
                        servisKodDurumTip = ServisKodDurumTip.ServisPasif;
                    }
                    else if (dagitim.KullanildiMi || !dagitim.AktifMi)
                    {
                        servisKodDurumTip = ServisKodDurumTip.KodKullanildi;
                    }
                    else
                    {
                        dagitim.UserId = userId;
                        dagitim.KullanildiMi = true;

                        dagitim.GuncelleKisiKey = userId;
                        dagitim.GuncelleTarih = DateTime.Now;
                        dagitim.AktifMi = false;

                        service.SaveChanges();

                        servisKodDurumTip = ServisKodDurumTip.Basarili;
                    }
                }
            }

            return servisKodDurumTip;
        }

        public static List<int> KeyConverter(SelectedItemCollection items)
        {
            List<int> collection = new List<int>();

            foreach (ListEditItem item in items)
            {
                int value = Convert.ToInt32(item.Value);
                if (value == 0)
                {
                    continue;
                }

                collection.Add(value);
            }

            return collection;
        }

        public static string StringConverter(IEnumerable<string> items)
        {
            string collection = "";

            foreach (string item in items)
            {
                collection += item + ",";
            }

            if (collection.Length > 0)
            {
                collection = collection.Remove(collection.Length - 1, 1);
            }

            return collection;
        }
    }
}