using DevExpress.Web.ASPxEditors;
using BiSinavProject.PL.Class.CustomType;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL
{
    public partial class E_Dergi : System.Web.UI.Page
    {
        #region Properties

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
            int edergiKey = Convert.ToInt32((sender as LinkButton).CommandArgument);

            using (var service = new DBEntities())
            {
                var eDergi = service.EDergis.
                                     AsNoTracking().
                                     Single(p => p.EDergiKey == edergiKey);

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
            }

            Response.Redirect("/Dergi/Oku.aspx?K=" + PageHelper.Encrypt(edergiKey.ToString()));
        }

        protected void RepeaterEDergiler_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            EDergi eDergi = e.Item.DataItem as EDergi;
            LinkButton linkButtonGiris = e.Item.FindControl("LinkButtonGiris") as LinkButton;
            LinkButton linkButtonAltGiris = e.Item.FindControl("LinkButtonAltGiris") as LinkButton;
            Label labelTanim = e.Item.FindControl("LabelTanim") as Label;

            if (eDergi.DemoMu)
            {
                labelTanim.Text = "Hediyedir";
            }
            else if (eDergi.HediyeMi && GirisDogrula())
            {
                labelTanim.Text = "Hediyedir";
            }
            else if (eDergi.HediyeMi && !GirisDogrula())
            {
                linkButtonGiris.Attributes.Add("onclick", "alert('Lütfen üye olunuz.'); return false;");
                linkButtonAltGiris.Attributes.Add("onclick", "alert('Lütfen üye olunuz.'); return false;");
                labelTanim.Text = "Hediyedir";
            }
            //satın alınmışmı
            else if (SatinAlDogrula(eDergi.EDergiKey))
            {
                labelTanim.Text = "Tıkla Oku";
            }
            else
            {
                linkButtonGiris.Attributes.Add("onclick", "alert('Ürün hesabınızda bulunmamaktadır. Satın almanız gerekmektedir.'); return false;");
                labelTanim.Text = "Satın Al &nbsp;&nbsp;&nbsp;&nbsp;" + string.Format("{0:0.00}", eDergi.Fiyat) + " &#8378;";
                linkButtonAltGiris.Attributes.Add("href", eDergi.Link);
                linkButtonAltGiris.Attributes.Add("target", "_blank");
            }
        }

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                RepeaterEDergiler.DataSource = null;
                RepeaterEDergiler.DataSource = service.EDergis.
                                                       AsNoTracking().
                                                       Where(p => p.AktifMi).
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                RepeaterEDergiler.DataBind();
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