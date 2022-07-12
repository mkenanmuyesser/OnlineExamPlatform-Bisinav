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
    public partial class Oku : System.Web.UI.Page
    {
        #region Properties

        private int Key
        {
            get
            {
                if (Request.QueryString["K"] == null)
                {
                    return 0;
                }
                else
                {
                    string key = PageHelper.Decrypt(Request.QueryString["K"]);
                    int keysonuc;
                    int.TryParse(key, out keysonuc);
                    return keysonuc;
                }
            }
        }

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

        protected void ButtonCikis_Click(object sender, EventArgs e)
        {
            Response.Redirect("/EDergi.aspx");
        }

        #endregion

        #region Methods

        public void SetInitials()
        {
            if (Key == 0)
            {
                Response.Redirect("~/EDergi.aspx");
                return;
            }

        }

        public void DataLoad()
        {
            using (var service = new DBEntities())
            {
                var eDergi = service.EDergis.
                                     AsNoTracking().
                                     Single(p => p.EDergiKey == Key);


                if (eDergi.DemoMu)
                {

                }
                else if (eDergi.HediyeMi && GirisDogrula())
                {

                }
                else if (eDergi.HediyeMi && !GirisDogrula())
                {
                    Response.Redirect("~/EDergi.aspx", true);
                    return;
                }
                //satın alınmışmı
                else if (SatinAlDogrula(eDergi.EDergiKey))
                {

                }
                else
                {
                    Response.Redirect("~/EDergi.aspx", true);
                    return;
                }

                PageFlipperControl.Key = Key;
            }
        }

        private bool GirisDogrula()
        {
            bool dogrulama = false;
            if (Membership.GetUser(true) == null)
            {
                dogrulama = false;
            }
            else
            {
                dogrulama = true;
            }
            return dogrulama;
        }

        private bool SatinAlDogrula(int edergiKey)
        {
            bool dogrulama = false;
            var membershipUser = Membership.GetUser(true);
            if (membershipUser != null)
            {
                Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                using (var service = new DBEntities())
                {
                    dogrulama = service.ServisEDergis.
                                        AsNoTracking().
                                        Include("Servi").
                                        Include("Servi.Dagitims").
                                        Any(p => p.EDergiKey == edergiKey &&
                                                 p.Servi.Dagitims.Any(x => x.UserId == userId && x.KullanildiMi));
                }
            }

            return dogrulama;
        }

        #endregion

    }
}