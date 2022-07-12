using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL
{
    public partial class VideoDers : System.Web.UI.Page
    {
        #region Properties

        private int Key
        {
            get
            {
                string key = Request.QueryString["K"];
                int keysonuc;
                int.TryParse(key, out keysonuc);
                return keysonuc;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataLoad();
            }
        }

        protected void LinkButtonGiris_Click(object sender, EventArgs e)
        {
            int kayitKey = Convert.ToInt32((sender as LinkButton).CommandArgument);

            Response.Redirect("/Video/Izle.aspx?K=" + PageHelper.Encrypt(kayitKey.ToString()));
        }

        protected void RepeaterKayitlar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Kayit kayit = e.Item.DataItem as Kayit;
            LinkButton linkButtonGiris = e.Item.FindControl("LinkButtonGiris") as LinkButton;
            LinkButton linkButtonAltGiris = e.Item.FindControl("LinkButtonAltGiris") as LinkButton;
            Label labelTanim = e.Item.FindControl("LabelTanim") as Label;

            labelTanim.Text = "Tıkla İzle";
        }

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                RepeaterKayitKategori.DataSource = null;
                var kayitKategoriler = service.KayitKategoris.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderBy(p => new
                                                     {
                                                         p.Sira
                                                     }).
                                                     ToList();
                kayitKategoriler.Insert(0, new KayitKategori
                {
                    KayitKategoriKey = 0,
                    Adi = "Tümü",
                });

                RepeaterKayitKategori.DataSource = kayitKategoriler.
                                            Select(p => new
                                            {
                                                Key = p.KayitKategoriKey,
                                                p.Adi,
                                                Secim = p.KayitKategoriKey == Key
                                            }).
                                            ToList();
                RepeaterKayitKategori.DataBind();

                RepeaterKayitlar.DataSource = null;
                RepeaterKayitlar.DataSource = service.Kayits.
                                                   AsNoTracking().
                                                   Include("KayitKategori").
                                                   Where(p => p.AktifMi &&
                                                              (Key == 0 || p.KayitKategoriKey == Key)).
                                                OrderBy(p => new
                                                {
                                                    p.Sira,
                                                }).
                                                ToList();
                RepeaterKayitlar.DataBind();
            }
        }

        #endregion
    }
}