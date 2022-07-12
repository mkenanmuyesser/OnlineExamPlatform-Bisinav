using System.Linq;
using System;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System.Collections.Generic;

namespace BiSinavProject.PL.Program.Satis
{
    public partial class SatisRaporlari : System.Web.UI.Page
    {
        #region Properties

        private const string PageHeader = "Satış Raporları";

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

        protected void ButtonAra_Click(object sender, EventArgs e)
        {
            Ara();
        }

        protected void ButtonTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            LabelBaslik.Text = PageHeader;

            DateEditBaslangicZamani.Value = DateTime.Now.AddDays(-1).Date;
            DateEditBitisZamani.Value = DateTime.Now.Date.AddHours(23).AddMinutes(59);
        }

        private void Ara()
        {
            using (var service = new DBEntities())
            {
                DateTime baslangicZamani = Convert.ToDateTime(DateEditBaslangicZamani.Value);
                DateTime bitisZamani = Convert.ToDateTime(DateEditBitisZamani.Value);

                GridViewArama.DataSource = null;
                var sonuc = service.Siparis.
                                    AsNoTracking().
                                    Include("SiparisDenemes").
                                    Include("SiparisEDergis").
                                    Include("aspnet_Users").
                                    Include("SiparisDurumTip").
                                    Where(p => baslangicZamani <= p.Tarih &&
                                               bitisZamani >= p.Tarih).
                                    OrderByDescending(p => p.Tarih).
                                    ToList().
                                    Select(p => new
                                    {
                                        p.Tarih,                                        
                                        KullaniciAdi = p.aspnet_Users.UserName,
                                        SiparisDurum = p.SiparisDurumTip.SiparisDurumTipAdi,
                                        Urunler = UrunListesi(p.SiparisKey),
                                    }).
                                    ToList();

                GridViewArama.DataSource = sonuc;
                PageHelper.SessionData = sonuc;
                GridViewArama.DataBind();
            }
        }

        private string UrunListesi(int siparisKey)
        {
            string urunListesi = "";

            using (var service = new DBEntities())
            {
                var denemeler = service.SiparisDenemes.
                                        AsNoTracking().
                                        Include("Deneme").
                                        Where(p => p.SiparisKey == siparisKey).
                                        ToList();

                foreach (var deneme in denemeler)
                {
                    urunListesi +="Deneme : "+ deneme.Deneme.Adi + "</br>";
                }

                var edergiler = service.SiparisEDergis.
                                        AsNoTracking().
                                        Include("EDergi").
                                        Where(p => p.SiparisKey == siparisKey).
                                        ToList();

                foreach (var edergi in edergiler)
                {
                    urunListesi += "E-Dergi &nbsp;: " + edergi.EDergi.Adi + "</br>";
                }
            }
            return urunListesi;
        }

        #endregion

    }
}