using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Haber
{
    public partial class IlanTanimIslemleri : System.Web.UI.Page
    {

        #region Properties

        private int Key
        {
            get
            {
                string key = Request.QueryString["Key"];
                int keysonuc;
                int.TryParse(key, out keysonuc);
                return keysonuc;
            }
        }

        private const string PageUrl = "IlanTanimIslemleri.aspx";
        private const string PageHeader = "Ilan Tanım İşlemleri";
        const string UploadDirectory = "/Uploads/Program/";
        string DosyaUrl = null;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitials();
            }

            DataLoad();
        }

        protected void ButtonKaydetGuncelle_Click(object sender, EventArgs e)
        {
            #region validation

            #endregion

            using (var service = new DBEntities())
            {
                Ilan data;
                if (Key == 0)
                {
                    data = new Ilan();
                }
                else
                {
                    data = service.Ilans.SingleOrDefault(p => p.IlanKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Aciklama = TextBoxAciklama.Text;
                data.Tarih= Convert.ToDateTime(DateEditTarih.Value);
                data.Link = TextBoxLink.Text;
                data.Sira = Convert.ToInt32(SpinEditSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Ilans.Add(data);
                }
                else
                {                    
                    data.GuncelleKisiKey = userkey;
                    data.GuncelleTarih = DateTime.Now;
                }

                service.SaveChanges();
            }

            Response.Redirect(PageUrl);
        }

        protected void ButtonIptalTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl);
        }

        protected void GridViewTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Ilan deleteddata = service.Ilans.Single(p => p.IlanKey == deletedkey);
                service.Ilans.Remove(deleteddata);

                try
                {
                    if (Key != 0)
                    {
                        GridViewTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewTanim.GetRowValues(index, new string[] { "IlanKey" }));
            ASPxGridView.RedirectOnCallback(string.Format("{0}?Key={1}", PageUrl, key));
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            if (Key == 0)
            {
                ButtonKaydet.Visible = true;
                ButtonTemizle.Visible = true;
                ButtonGuncelle.Visible = false;
                ButtonIptal.Visible = false;
            }
            else
            {
                ButtonKaydet.Visible = false;
                ButtonTemizle.Visible = false;
                ButtonGuncelle.Visible = true;
                ButtonIptal.Visible = true;
            }

            LabelBaslik.Text = PageHeader;
        }

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                GridViewTanim.DataSource = null;
                GridViewTanim.DataSource = service.Ilans.
                                                   AsNoTracking().
                                                   OrderBy(p => p.Sira).
                                                   ToList();
                GridViewTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Ilan data = service.Ilans.
                                       AsNoTracking().
                                       Single(p => p.IlanKey == Key);

                    TextBoxAciklama.Text = data.Aciklama;
                    DateEditTarih.Value= data.Tarih ;
                    TextBoxLink.Text = data.Link;
                    SpinEditSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        #endregion

    }
}