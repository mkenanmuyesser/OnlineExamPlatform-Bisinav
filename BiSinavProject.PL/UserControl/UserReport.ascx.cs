using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.UserControl
{
    public partial class UserReport : System.Web.UI.UserControl
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            SetInitials();

            if (!IsPostBack)
            {
                DataLoad();
            }
        }

        protected void ButtonSifreDegistir_Click(object sender, EventArgs e)
        {
            string yeniSifre = TextBoxYeniSifre.Text;
            string yeniSifreTekrar = TextBoxYeniSifreTekrar.Text;

            var membershipUser = Membership.GetUser(true);
            if (yeniSifre != yeniSifreTekrar)
            {
                PageHelper.MessageBox(this.Page, "Yeni şifreler eşleşmiyor, lütfen yeni şifrenizi doğru giriniz.");

            }
            else
            {
                membershipUser.ChangePassword(membershipUser.ResetPassword(), yeniSifre);
                PageHelper.MessageBox(this.Page, "Şifreniz başarıyla değiştirilmiştir.");
            }

            TextBoxYeniSifre.Text = "";
            TextBoxYeniSifreTekrar.Text = "";
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            if (Membership.GetUser(true) == null)
            {
                Response.Redirect("/Default.aspx");
            }
        }

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                var membershipUser = Membership.GetUser(true);
                Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                #region Urunler

                var serviceDagitim = service.Dagitims.
                                             AsNoTracking().
                                             Include("Servi").
                                             Where(p =>
                                             p.UserId == userId &&
                                             p.Servi.AktifMi).
                                             ToList().
                                             OrderBy(p => p.Sira).
                                             ToList();

                var denemeler1 = service.ServisDenemes.
                                        AsNoTracking().
                                        Include("Deneme").
                                        ToList().
                                        Where(p => serviceDagitim.Any(x => x.ServisKey == p.ServisKey)).
                                        Select(p =>
                                        new
                                        {
                                            p.Deneme.Resim,
                                            p.Deneme.Adi,
                                        }).
                                        Distinct().
                                        ToList();

                var denemeler2 = service.Denemes.
                                        AsNoTracking().
                                        ToList().
                                        Where(p => p.DemoMu || p.HediyeMi).
                                        Select(p =>
                                        new
                                        {
                                            p.Resim,
                                            p.Adi,
                                        }).
                                        Distinct().
                                        ToList();

                var denemelerSonuc = denemeler1.Union(denemeler2).Distinct();


                var edergiler1 = service.ServisEDergis.
                                       AsNoTracking().
                                       Include("EDergi").
                                       ToList().
                                       Where(p => serviceDagitim.Any(x => x.ServisKey == p.ServisKey)).
                                        Select(p =>
                                        new
                                        {
                                            p.EDergi.Resim,
                                            p.EDergi.Adi,
                                        }).
                                       Distinct().
                                       ToList();

                var edergiler2 = service.EDergis.
                                        AsNoTracking().
                                        ToList().
                                        Where(p => p.DemoMu || p.HediyeMi).
                                        Select(p =>
                                        new
                                        {
                                            p.Resim,
                                            p.Adi,
                                        }).
                                        Distinct().
                                        ToList();

                var edergilerSonuc = edergiler1.Union(edergiler2).Distinct();


                RepeaterDenemeler.DataSource = null;
                RepeaterDenemeler.DataSource = denemelerSonuc;
                RepeaterDenemeler.DataBind();

                RepeaterEDergiler.DataSource = null;
                RepeaterEDergiler.DataSource = edergilerSonuc;
                RepeaterEDergiler.DataBind();

                #endregion

                #region BesteBes

                int siraBesteBes = 1;
                var besteBesData = service.SonucDetays.
                                        AsNoTracking().
                                        Include("Der").
                                        Include("DenemeKullanici").
                                        Include("DenemeKullanici.Deneme").
                                        Include("DenemeKullanici.Deneme.Alan").
                                        Where(p => p.UserId == userId &&
                                        p.DenemeKullaniciKey == null).
                                        ToList().
                                       Select(p => new
                                       {
                                           Sira = siraBesteBes++,
                                           Tarih = p.KayitTarih.Value.ToString("dd.MM.yyyy"),
                                           DersAdi = p.Der.Adi,
                                           Dogru = p.DogruSayisi,
                                           Yanlis = p.YanlisSayisi,
                                           Bos = p.BosSayisi,
                                       }).
                                       ToList();
                if (besteBesData.Any())
                {
                    if (besteBesData.Any())
                    {
                        decimal toplamBesteBesDogru = besteBesData.Sum(p => p.Dogru);
                        decimal toplamBesteBesYanlis = besteBesData.Sum(p => p.Yanlis);
                        decimal toplamBesteBesBos = besteBesData.Sum(p => p.Bos);
                        decimal toplamBesteBesSoru = toplamBesteBesDogru + toplamBesteBesYanlis + toplamBesteBesBos;

                        decimal oranBesteBesDogru = toplamBesteBesSoru == 0 ? 0 : (toplamBesteBesDogru / toplamBesteBesSoru) * 100;
                        decimal oranBesteBesYanlis = toplamBesteBesSoru == 0 ? 0 : (toplamBesteBesYanlis / toplamBesteBesSoru) * 100;
                        decimal oranBesteBesBos = 100 - oranBesteBesDogru - oranBesteBesYanlis;

                        LabelBesteBesToplamSinavSayisi.Text = besteBesData.Count().ToString();
                        LabelBesteBesToplamGirisSayisi.Text = besteBesData.Count().ToString();
                        LabelBesteBesOrtalamaBasari.Text = "-";
                        LabelBesteBesOrtalamaDogruSayisi.Text = oranBesteBesDogru.ToString("n2");
                        LabelBesteBesOrtalamaYanlisSayisi.Text = oranBesteBesYanlis.ToString("n2");
                        LabelBesteBesOrtalamaBosSayisi.Text = oranBesteBesBos.ToString("n2");

                        RepeaterBesteBes.DataSource = null;
                        RepeaterBesteBes.DataSource = besteBesData;
                        RepeaterBesteBes.DataBind();
                    }
                }

                #endregion

                #region Deneme

                int siraDeneme = 1;
                var denemeData = service.DenemeKullanicis.
                        AsNoTracking().
                        Include("Deneme").
                        Include("Deneme.Alan").
                        Include("SonucDetays").
                        Where(p => p.UserId == userId).
                        ToList().
                        Select(p => new
                        {
                            p.DenemeKey,
                            p.DenemeKullaniciKey,
                            Sira = siraDeneme++,
                            Tarih = p.BitisZamani.ToString("dd.MM.yyyy"),
                            AlanAdi = p.Deneme.Alan.Adi,
                            ToplamSure = p.Deneme.Sure,
                            KullanilanSure = Convert.ToInt32(Math.Ceiling(p.GecenSure / 60m)),
                            Dogru = p.SonucDetays.Sum(x => x.DogruSayisi),
                            Yanlis = p.SonucDetays.Sum(x => x.YanlisSayisi),
                            Bos = p.SonucDetays.Sum(x => x.BosSayisi),
                            Aktif = p.AktifMi
                        }).
                        ToList();

                if (denemeData.Any())
                {
                    decimal toplamDenemeDogru = denemeData.Sum(p => p.Dogru);
                    decimal toplamDenemeYanlis = denemeData.Sum(p => p.Yanlis);
                    decimal toplamDenemeBos = denemeData.Sum(p => p.Bos);
                    decimal toplamDenemeSoru = toplamDenemeDogru + toplamDenemeYanlis + toplamDenemeBos;

                    decimal oranDenemeDogru = toplamDenemeSoru == 0 ? 0 : (toplamDenemeDogru / toplamDenemeSoru) * 100;
                    decimal oranDenemeYanlis = toplamDenemeSoru == 0 ? 0 : (toplamDenemeYanlis / toplamDenemeSoru) * 100;
                    decimal oranDenemeBos = 100 - oranDenemeDogru - oranDenemeYanlis;

                    LabelDenemeToplamSinavSayisi.Text = denemeData.Select(p => new { p.DenemeKey }).Distinct().Count().ToString();
                    LabelDenemeToplamGirisSayisi.Text = denemeData.Select(p => new { p.DenemeKullaniciKey }).Distinct().Count().ToString();
                    LabelDenemeOrtalamaBasari.Text = "-";
                    LabelDenemeOrtalamaDogruSayisi.Text = oranDenemeDogru.ToString("n2");
                    LabelDenemeOrtalamaYanlisSayisi.Text = oranDenemeYanlis.ToString("n2");
                    LabelDenemeOrtalamaBosSayisi.Text = oranDenemeBos.ToString("n2");

                    RepeaterDeneme.DataSource = null;
                    RepeaterDeneme.DataSource = denemeData;
                    RepeaterDeneme.DataBind();
                }

                #endregion
            }
        }

        #endregion

    }
}