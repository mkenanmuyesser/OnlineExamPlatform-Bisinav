using System.Collections.Generic;
using System.Linq;
using System;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using BiSinavProject.PL.Class.Business;

namespace BiSinavProject.PL.Program.KodPaylasim
{
    public partial class KodDagitimAramaIslemleri : System.Web.UI.Page
    {
        #region Properties

        private const string PageHeader = "Kod Dağıtım Arama İşlemleri";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageHelper.SessionData = null;
                SetInitials();
            }
            else
            {
                GridViewArama.DataSource = PageHelper.SessionData;
                GridViewArama.DataBind();
            }
        }

        protected void ButtonRapor_Click(object sender, EventArgs e)
        {
            GridViewArama.DataSource = PageHelper.SessionData;
            GridViewArama.DataBind();

            GridViewExporterArama.WriteXlsxToResponse("BiSınav.net Kod");                
        }

        protected void ButtonAra_Click(object sender, EventArgs e)
        {
            Ara();
        }

        protected void ButtonTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ComboBoxSirketAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServisYukle();
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            LabelBaslik.Text = PageHeader;
            DataLoad();
        }

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                List<Sirket> listSirket = service.Sirkets.
                                                  AsNoTracking().
                                                  OrderBy(p => p.Sira).
                                                  ToList();
                listSirket.Insert(0, new Sirket
                {
                    SirketKey = 0,
                    Adi = "Tümü"
                });

                ComboBoxSirketAdi.DataSource = listSirket;
                ComboBoxSirketAdi.DataBind();

                List<ServisTip> listServisTip = service.ServisTips.
                                                AsNoTracking().
                                                OrderBy(p => p.Sira).
                                                ToList();
                listServisTip.Insert(0, new ServisTip
                {
                    ServisTipKey = 0,
                    ServisTipAdi = "Tümü"
                });

                ComboBoxServisTip.DataSource = listServisTip;
                ComboBoxServisTip.DataBind();
                ComboBoxServisTip.SelectedIndex = 0;

                List<Deneme> listDeneme = service.Denemes.
                                                      AsNoTracking().
                                                      OrderBy(p => p.Sira).
                                                      ToList();
                listDeneme.Insert(0, new Deneme
                {
                    DenemeKey = 0,
                    Adi = "Tümü"
                });

                ComboBoxDenemeler.DataSource = listDeneme;
                ComboBoxDenemeler.DataBind();
                ComboBoxDenemeler.SelectedIndex = 0;

                List<EDergi> listEDergi = service.EDergis.
                                                 AsNoTracking().
                                                 OrderBy(p => p.Sira).
                                                 ToList();
                listEDergi.Insert(0, new EDergi
                {
                    EDergiKey = 0,
                    Adi = "Tümü"
                });

                ComboBoxEDergiler.DataSource = listEDergi;
                ComboBoxEDergiler.DataBind();
                ComboBoxEDergiler.SelectedIndex = 0;

                if (listSirket.Any())
                {
                    ComboBoxSirketAdi.SelectedIndex = 0;
                    ServisYukle();
                }
            }
        }

        private void Ara()
        {
            using (var service = new DBEntities())
            {
                int pSirketKey = Convert.ToInt32(ComboBoxSirketAdi.SelectedItem.Value);
                int pServisKey = Convert.ToInt32(ComboBoxServis.SelectedItem.Value);
                int pServisTipKey = Convert.ToInt32(ComboBoxServisTip.SelectedItem.Value);
                int pDenemeKey = Convert.ToInt32(ComboBoxDenemeler.SelectedItem.Value);
                int pEDergiKey = Convert.ToInt32(ComboBoxEDergiler.SelectedItem.Value);
                string pKod = TextBoxKod.Text;
                byte pKullanimDurum = Convert.ToByte(RadioButtonListKullanimDurum.SelectedItem.Value);
                byte pAktif = Convert.ToByte(RadioButtonListAktif.SelectedItem.Value);

                GridViewArama.DataSource = null;
                var sonuc = service.Dagitims.
                                    AsNoTracking().
                                    Include("Servi").
                                    Include("Servi.Sirket").
                                    Include("aspnet_Users").
                                    Include("Servi.Sirket").
                                    Include("Servi.ServisTip").
                                    Include("Servi.ServisDenemes").
                                    Include("Servi.ServisDenemes.Deneme").
                                    Include("Servi.ServisEDergis").
                                      Include("Servi.ServisEDergis.EDergi").
                                    Where(p =>
                                         (pSirketKey == 0 || p.Servi.SirketKey == pSirketKey) &&
                                         (pServisKey == 0 || p.ServisKey == pServisKey) &&
                                         (pServisTipKey == 0 || p.Servi.ServisTipKey == pServisTipKey) &&
                                         (string.IsNullOrEmpty(pKod) || p.Kod == pKod) &&
                                         (pKullanimDurum == 0 || (p.KullanildiMi == (pKullanimDurum == 1 ? true : false))) &&
                                         (pAktif == 0 || (p.AktifMi == (pAktif == 1 ? true : false)))).
                                         ToList().
                                    Select(p => new
                                    {
                                        ServisAdi = p.Servi.Aciklama,
                                        SirketAdi = p.Servi.Sirket.Adi + " - " + (string.IsNullOrEmpty(p.Servi.Kod) ? "?" : p.Servi.Kod),
                                        ServisTip = p.Servi.ServisTip.ServisTipAdi,
                                        Denemeler = KodBs.StringConverter(p.Servi.ServisDenemes.Select(x => x.Deneme.Adi)),
                                        EDergiler = KodBs.StringConverter(p.Servi.ServisEDergis.Select(x => x.EDergi.Adi)),
                                        Kod = p.Kod == null ? "-" : p.Kod,
                                        KullaniciAdi = p.aspnet_Users == null ? "" : p.aspnet_Users.UserName,
                                        KullanildiMi = p.KullanildiMi,
                                        AktifMi = p.AktifMi,
                                    }).
                                    ToList();
                //OrderBy(p => p.Der.Alan.Adi).
                //OrderBy(p => p.Der.Adi).

                #region gruplama

                GridViewArama.GroupBy(GridViewArama.Columns["ServisAdi"]);

                #endregion

                GridViewArama.DataSource = sonuc;
                PageHelper.SessionData = sonuc;
                GridViewArama.DataBind();

                LabelSonuc.Text = string.Format("Sonuç sayısı : {0}", sonuc.Count());
            }
        }

        private void ServisYukle()
        {
            using (var service = new DBEntities())
            {
                int sirketKey = Convert.ToInt32(ComboBoxSirketAdi.SelectedItem.Value);
                List<Servi> listServis = service.Servis.
                                            AsNoTracking().
                                            Where(p => sirketKey == 0 || p.SirketKey == sirketKey).
                                            OrderBy(p => p.Sira).
                                            ToList();
                listServis.Insert(0, new Servi { ServisKey = 0, Aciklama = "Tümü" });
                ComboBoxServis.DataSource = listServis;
                ComboBoxServis.DataBind();
                ComboBoxServis.SelectedIndex = 0;
            }
        }

        #endregion

    }
}