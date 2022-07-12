using BiSinavProject.PL.Data;
using DevExpress.Web.ASPxGridView;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Akademi
{
    public partial class IlTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "IlTanimIslemleri.aspx";
        private const string PageHeader = "İl Tanım İşlemleri";

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
                Il data;
                if (Key == 0)
                {
                    data = new Il();
                }
                else
                {
                    data = service.Ils.SingleOrDefault(p => p.IlKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Adi = TextBoxIlAdi.Text;
                data.Sira = Convert.ToInt32(SpinEditAlanSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Ils.Add(data);
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

        protected void GridViewIlTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Il deleteddata = service.Ils.Single(p => p.IlKey == deletedkey);
                service.Ils.Remove(deleteddata);

                try
                {
                    if (Key != 0)
                    {
                        GridViewIlTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewIlTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewIlTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewIlTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewIlTanim.GetRowValues(index, new string[] { "IlKey" }));
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
                GridViewIlTanim.DataSource = null;
                GridViewIlTanim.DataSource = service.Ils.
                                                       AsNoTracking().
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewIlTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Il data = service.Ils.
                                        AsNoTracking().
                                        Single(p => p.IlKey == Key);

                    TextBoxIlAdi.Text = data.Adi;
                    SpinEditAlanSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        #endregion

    }
}