using BiSinavProject.PL.Class.Business;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.MasterPage
{
    public partial class SiteMain : System.Web.UI.MasterPage
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

        protected void ButtonAktivasyon_Click(object sender, EventArgs e)
        {
            string kod = TextBoxAktivasyon.Text.Trim();
            if (Membership.GetUser(true) == null)
            {
                PageHelper.MessageBox(Page, "Lütfen kullanıcı girişi yapınız.");
                TextBoxAktivasyon.Text = "";
                return;
            }
            else if (string.IsNullOrEmpty(kod))
            {
                PageHelper.MessageBox(Page, "Lütfen kod giriniz.");
                return;
            }
            else
            {
                //kod kullanılabilir durumdamı?
                switch (KodBs.KodAktifEt(kod))
                {
                    default:
                    case ServisKodDurumTip.Basarisiz:
                        PageHelper.MessageBox(Page, "İşlem başarısızdır.");
                        break;
                    case ServisKodDurumTip.ServisPasif:
                        PageHelper.MessageBox(Page, "Girdiğiniz kod yanlış veya süresi geçmiş bir kod olabilir.");
                        break;
                    case ServisKodDurumTip.KodKullanildi:
                        PageHelper.MessageBox(Page, "Girdiğiniz kod daha önce kullanılmıştır.");
                        break;
                    case ServisKodDurumTip.Basarili:                      
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("alert('İşlem başarılıdır. Girdiğiniz koda ait ürünler hesabınıza eklenmiştir.');");
                        Response.Write("document.location.href='"+Request.Url.AbsoluteUri+"';");
                        Response.Write("</script>");
                        break;
                }
            }
        }

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                RepeaterIlan.DataSource = null;
                RepeaterIlan.DataSource = service.Ilans.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderByDescending(p => new
                                                     {
                                                         p.Tarih,
                                                         p.Sira
                                                     }).
                                                     ToList().
                                                     Select(p => new
                                                     {
                                                         Tarih = p.Tarih.ToString("dd.MMM.yyyy"),
                                                         p.Link,
                                                         p.Aciklama,
                                                     }).
                                                     ToList();
                RepeaterIlan.DataBind();

                var reklamlar = service.Tanitims.
                                       AsNoTracking().
                                       Where(p => p.AktifMi).
                                       ToList();

                var reklamSagUstBolge = reklamlar.FirstOrDefault(p => p.BolgeTipKey == Convert.ToInt32(ReklamBolgeTip.SagUstBolge));
                if (reklamSagUstBolge == null)
                {
                    HyperLinkHyperLinkSagUstBolge.ImageUrl = "../Content/Site/Images/no-banner-300x250.jpg";
                }
                else
                {
                    if (!string.IsNullOrEmpty(reklamSagUstBolge.Link))
                    {
                        HyperLinkHyperLinkSagUstBolge.Target = "_blank";
                        HyperLinkHyperLinkSagUstBolge.NavigateUrl = reklamSagUstBolge.Link;
                    }

                    HyperLinkHyperLinkSagUstBolge.ImageUrl = UploadDirectory + reklamSagUstBolge.Resim;
                }

                var reklamSagAltBolge = reklamlar.FirstOrDefault(p => p.BolgeTipKey == Convert.ToInt32(ReklamBolgeTip.SagAltBolge));
                if (reklamSagAltBolge == null)
                {
                    HyperLinkSagAltBolge.ImageUrl = "../Content/Site/Images/no-banner-300x250.jpg";
                }
                else
                {
                    if (!string.IsNullOrEmpty(reklamSagAltBolge.Link))
                    {
                        HyperLinkSagAltBolge.Target = "_blank";
                        HyperLinkSagAltBolge.NavigateUrl = reklamSagAltBolge.Link;
                    }

                    HyperLinkSagAltBolge.ImageUrl = UploadDirectory + reklamSagAltBolge.Resim;
                }

            }
        }

        #endregion       
    }
}