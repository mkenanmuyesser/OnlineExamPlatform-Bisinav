using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL
{
    public partial class Default : System.Web.UI.Page
    {
        #region Properties

        const string UploadDirectory = "/Uploads/Program/";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataLoad();
            }
        }

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                RepeaterIcerik.DataSource = null;
                RepeaterIcerik.DataSource = service.Iceriks.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderByDescending(p => new
                                                     {
                                                         p.Tarih,
                                                         p.Sira
                                                     }).
                                                     ToList();
                RepeaterIcerik.DataBind();

                int counter = 1;
                var mansetData = service.Mansets.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderBy(p => p.Sira).
                                                     ToList().
                                                     Select(p => new
                                                     {
                                                         Sira = counter++,
                                                         p.Baslik,
                                                         p.Aciklama,
                                                         p.Link,
                                                         p.Resim,
                                                     }).
                                                     ToList().
                                                     Take(4).
                                                     ToList();

                RepeaterMansetSol.DataSource = null;
                RepeaterMansetSol.DataSource = mansetData;
                RepeaterMansetSol.DataBind();

                RepeaterMansetSag.DataSource = null;
                RepeaterMansetSag.DataSource = mansetData;
                RepeaterMansetSag.DataBind();


                RepeaterVideoDers.DataSource = null;
                RepeaterVideoDers.DataSource = service.KayitKategoris.
                                                AsNoTracking().
                                                Include("Kayits").
                                                Where(p => p.AktifMi).
                                                OrderBy(p => new
                                                {
                                                    p.Sira,
                                                }).
                                                ToList().
                                                Select(p => new
                                                {
                                                    Key = p.KayitKategoriKey,
                                                    p.Adi,
                                                    KayitSayi = p.Kayits.Count(x => x.AktifMi)
                                                }).
                                                ToList();
                RepeaterVideoDers.DataBind();


                RepeaterIl.DataSource = null;
                RepeaterIl.DataSource = service.Ils.
                                                AsNoTracking().
                                                Include("IlKurs").
                                                Where(p => p.AktifMi).
                                                OrderBy(p => new
                                                {
                                                    p.Sira,
                                                }).
                                                ToList().
                                                Select(p => new
                                                {
                                                    Key = p.IlKey,
                                                    p.Adi,
                                                    KursSayi = p.IlKurs.Count(x => x.AktifMi)
                                                }).
                                                ToList();
                RepeaterIl.DataBind();

                RepeaterKategori.DataSource = null;
                RepeaterKategori.DataSource = service.Kategoris.
                                                AsNoTracking().
                                                Include("KategoriKitaps").
                                                Where(p => p.AktifMi).
                                                OrderBy(p => new
                                                {
                                                    p.Sira,
                                                }).
                                                ToList().
                                                Select(p => new
                                                {
                                                    Key = p.KategoriKey,
                                                    p.Adi,
                                                    KitapSayi = p.KategoriKitaps.Count(x => x.AktifMi)
                                                }).
                                                ToList();
                RepeaterKategori.DataBind();

                RepeaterDeneme.DataSource = null;
                RepeaterDeneme.DataSource = service.Denemes.
                                                AsNoTracking().
                                                Where(p => p.AktifMi &&
                                                           p.YeniMi).
                                                OrderBy(p => new
                                                {
                                                    p.Sira,
                                                }).
                                                ToList().
                                                Select(p => new
                                                {
                                                    Key = p.DenemeKey,
                                                    p.Adi,
                                                }).
                                                ToList();
                RepeaterDeneme.DataBind();

                RepeaterEDergi.DataSource = null;
                RepeaterEDergi.DataSource = service.EDergis.
                                                AsNoTracking().
                                                Where(p => p.AktifMi &&
                                                           p.YeniMi).
                                                OrderBy(p => new
                                                {
                                                    p.Sira,
                                                }).
                                                ToList().
                                                Select(p => new
                                                {
                                                    Key = p.EDergiKey,
                                                    p.Adi,
                                                }).
                                                ToList();
                RepeaterEDergi.DataBind();

                var reklamlar = service.Tanitims.
                                       AsNoTracking().
                                       Where(p => p.AktifMi).
                                       ToList();

                var reklamOrtaBolge = reklamlar.FirstOrDefault(p => p.BolgeTipKey == Convert.ToInt32(ReklamBolgeTip.OrtaBolge));
                if (reklamOrtaBolge == null)
                {
                    HyperLinkHyperLinkOrtaAltBolge.ImageUrl = "../Content/Site/Images/no-banner-728x90.jpg";
                }
                else
                {
                    if (!string.IsNullOrEmpty(reklamOrtaBolge.Link))
                    {
                        HyperLinkHyperLinkOrtaAltBolge.Target = "_blank";
                        HyperLinkHyperLinkOrtaAltBolge.NavigateUrl = reklamOrtaBolge.Link;
                    }

                    HyperLinkHyperLinkOrtaAltBolge.ImageUrl = UploadDirectory + reklamOrtaBolge.Resim;
                }
            }
        }

        #endregion

    }
}