using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL
{
    public partial class Yayin : System.Web.UI.Page
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

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                RepeaterKategori.DataSource = null;
                var kategoriler = service.Kategoris.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderBy(p => new
                                                     {
                                                         p.Sira
                                                     }).
                                                     ToList();
                kategoriler.Insert(0, new Kategori
                {
                    KategoriKey = 0,
                    Adi = "Tümü",
                });

                RepeaterKategori.DataSource = kategoriler.
                                            Select(p => new
                                            {
                                                Key = p.KategoriKey,
                                                p.Adi,
                                                Secim = p.KategoriKey == Key
                                            }).
                                            ToList();
                RepeaterKategori.DataBind();

                RepeaterKitap.DataSource = null;
                RepeaterKitap.DataSource = service.Kitaps.
                                                   AsNoTracking().
                                                   Include("KategoriKitaps").
                                                   Where(p => p.AktifMi &&
                                                              (Key == 0 || p.KategoriKitaps.Where(x => x.AktifMi).Select(x => x.KategoriKey).Contains(Key))).
                                                   OrderBy(p => new
                                                   {
                                                       p.Sira,
                                                   }).
                                                   ToList();
                RepeaterKitap.DataBind();
            }
        }

        #endregion
    }
}