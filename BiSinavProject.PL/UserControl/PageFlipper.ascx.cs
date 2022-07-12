using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.UserControl
{
    public partial class PageFlipper : System.Web.UI.UserControl
    {
        #region Properties

        public int Key { get; set; }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitials();
            }
        }

        protected void ButtonCikis_Click(object sender, EventArgs e)
        {
            if (Page.AppRelativeVirtualPath.Contains("Oku"))
            {
                Response.Redirect("/EDergi.aspx");
            }
            else
            {
                Response.Redirect("/Admin/Yayin/EDergiTanimIslemleri.aspx");
            }
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            if (Key != 0)
            {
                DataLoad();
            }
        }

        private void DataLoad()
        {
            var data = Sayfalar();

            using (var service = new DBEntities())
            {
                var eDergi = service.EDergis.
                                     AsNoTracking().
                                     Single(p => p.EDergiKey == Key);

                LabelBaslik.Text = eDergi.Adi;
            }
        }

        public string[] Sayfalar()
        {
            string[] eDergiSayfaList = null;
            using (var service = new DBEntities())
            {
                eDergiSayfaList = service.EDergiSayfas.
                                          AsNoTracking().
                                          Where(p => p.AktifMi &&
                                          p.EDergiKey == Key).
                                          ToList().
                                          OrderBy(p => p.Sira).
                                          Select(p => p.Resim).
                                          ToArray();
            }

            return eDergiSayfaList;
        }

        #endregion       

    }
}