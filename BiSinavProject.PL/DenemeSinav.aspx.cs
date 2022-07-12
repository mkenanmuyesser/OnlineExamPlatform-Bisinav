using DevExpress.Web.ASPxEditors;
using BiSinavProject.PL.Class.Business;
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
    public partial class DenemeSinav : System.Web.UI.Page
    {
        #region Properties

        private int Key
        {
            get
            {
                string key = Request.QueryString["P"];
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
            int denemeKey = Convert.ToInt32((sender as LinkButton).CommandArgument);

            using (var service = new DBEntities())
            {
                var deneme = service.Denemes.
                                     AsNoTracking().
                                     Single(p => p.DenemeKey == denemeKey);

                if (deneme.DemoMu)
                {
                    
                }
                else if (deneme.HediyeMi && GirisDogrula())
                {
                    
                }
                else if (deneme.HediyeMi && !GirisDogrula())
                {
                    Response.Redirect("~/DenemeSinav.aspx", true);
                    return;
                }
                //satın alınmışmı
                else if (SatinAlDogrula(deneme.DenemeKey))
                {
                   
                }
                else
                {
                    Response.Redirect("~/DenemeSinav.aspx", true);
                    return;
                }
            }

            SessionHelper.DenemeData = null;
            SessionHelper.DenemeData = new DenemeType
            {
                AktifDenemeKey = denemeKey,
                SinavMode = SinavModeEnum.SinavBasla
            };
            SessionHelper.DenemeData = SessionHelper.DenemeData;

            Response.Redirect("/Deneme/Sinav.aspx", true);
        }

        protected void RepeaterDenemeler_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Deneme deneme = e.Item.DataItem as Deneme;
            LinkButton linkButtonGiris = e.Item.FindControl("LinkButtonGiris") as LinkButton;
            LinkButton linkButtonAltGiris = e.Item.FindControl("LinkButtonAltGiris") as LinkButton;
            Label labelTanim = e.Item.FindControl("LabelTanim") as Label;

            if (deneme.DemoMu)
            {
                labelTanim.Text = "Hediyedir";
            }
            else if (deneme.HediyeMi && GirisDogrula())
            {
                labelTanim.Text = "Hediyedir";
            }
            else if (deneme.HediyeMi && !GirisDogrula())
            {
                linkButtonGiris.Attributes.Add("onclick", "alert('Lütfen üye olunuz.'); return false;");
                linkButtonAltGiris.Attributes.Add("onclick", "alert('Lütfen üye olunuz.'); return false;");
                labelTanim.Text = "Hediyedir";
            }
            //satın alınmışmı
            else if (SatinAlDogrula(deneme.DenemeKey))
            {
                labelTanim.Text = "Sınava Başla";

                //daha önce giriş yapılan denemeler var ise değişik göster kısmı yapılacak
                if (DenemeDogrula(deneme.DenemeKey))
                {
                    //eğer daha önceki denemeye girilmiş ve bitirilmemiş ise soru sor. İsterse öncekinden devam edebilecek
                    linkButtonGiris.Attributes.Add("onclick", "return confirm('Daha önce bu deneme çözülmüş. Devam etmek istiyor musunuz?')");
                    linkButtonAltGiris.Attributes.Add("onclick", "return confirm('Daha önce bu deneme çözülmüş. Devam etmek istiyor musunuz?')");
                }
            }
            else
            {
                linkButtonGiris.Attributes.Add("onclick", "alert('Ürün hesabınızda bulunmamaktadır. Satın almanız gerekmektedir.'); return false;");
                labelTanim.Text = "Satın Al &nbsp;&nbsp;&nbsp;&nbsp;" + string.Format("{0:0.00}", deneme.Fiyat) + " &#8378;";
                linkButtonAltGiris.Attributes.Add("href", deneme.Link);
                linkButtonAltGiris.Attributes.Add("target", "_blank");
             
            }
        }

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                RepeaterAlan.DataSource = null;
                var iller = service.Alans.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderBy(p => new
                                                     {
                                                         p.Sira
                                                     }).
                                                     ToList();
                iller.Insert(0, new Alan
                {
                    AlanKey = 0,
                    Adi = "Tümü",
                });

                RepeaterAlan.DataSource = iller.
                                            Select(p => new
                                            {
                                                Key = p.AlanKey,
                                                p.Adi,
                                                Secim = p.AlanKey == Key
                                            }).
                                            ToList();
                RepeaterAlan.DataBind();

                RepeaterDenemeler.DataSource = null;
                RepeaterDenemeler.DataSource = service.Denemes.
                                                       AsNoTracking().
                                                       Where(p => p.AktifMi &&
                                                                  (Key == 0 || p.AlanKey == Key)).
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                RepeaterDenemeler.DataBind();
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

        private bool SatinAlDogrula(int denemeKey)
        {
            bool dogrulama = false;
            var membershipUser = Membership.GetUser(true);
            if (membershipUser != null)
            {
                Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                using (var service = new DBEntities())
                {
                    dogrulama = service.ServisDenemes.
                                        AsNoTracking().
                                        Include("Servi").
                                        Include("Servi.Dagitims").
                                        Any(p => p.DenemeKey == denemeKey &&
                                                 p.Servi.Dagitims.Any(x => x.UserId == userId && x.KullanildiMi));
                }
            }

            return dogrulama;
        }

        private bool DenemeDogrula(int denemeKey)
        {
            bool dogrulama = false;
            var membershipUser = Membership.GetUser(true);
            if (membershipUser != null)
            {
                Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                using (var service = new DBEntities())
                {

                    dogrulama = service.DenemeKullanicis.
                                        Any(p => p.DenemeKey == denemeKey &&
                                                 p.UserId == userId);
                }
            }

            return dogrulama;
        }

        #endregion

    }
}