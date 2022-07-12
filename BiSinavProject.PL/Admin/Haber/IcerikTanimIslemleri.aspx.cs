using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Haber
{
    public partial class IcerikTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "IcerikTanimIslemleri.aspx";
        private const string PageHeader = "İçerik Tanım İşlemleri";

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
                Icerik data;
                if (Key == 0)
                {
                    data = new Icerik();
                }
                else
                {
                    data = service.Iceriks.SingleOrDefault(p => p.IcerikKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Aciklama = TextBoxAciklama.Text;
                data.Tarih = Convert.ToDateTime(DateEditTarih.Value);
                data.Link = TextBoxLink.Text;
                data.Sira = Convert.ToInt32(SpinEditSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Iceriks.Add(data);
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
                Icerik deleteddata = service.Iceriks.Single(p => p.IcerikKey == deletedkey);
                service.Iceriks.Remove(deleteddata);

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
            int key = Convert.ToInt32(GridViewTanim.GetRowValues(index, new string[] { "IcerikKey" }));
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
                GridViewTanim.DataSource = service.Iceriks.
                                                   AsNoTracking().
                                                   OrderBy(p => p.Sira).
                                                   ToList();
                GridViewTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Icerik data = service.Iceriks.
                                       AsNoTracking().
                                       Single(p => p.IcerikKey == Key);

                    TextBoxAciklama.Text = data.Aciklama;
                    DateEditTarih.Value = data.Tarih;
                    TextBoxLink.Text = data.Link;
                    SpinEditSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        #endregion

    }
}