using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Sinav
{
    public partial class AlanTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "AlanTanimIslemleri.aspx";
        private const string PageHeader = "Alan Tanım İşlemleri";

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
                Alan data;
                if (Key == 0)
                {
                    data = new Alan();
                }
                else
                {
                    data = service.Alans.SingleOrDefault(p => p.AlanKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Adi = TextBoxAlanAdi.Text;
                data.Sira = Convert.ToInt32(SpinEditAlanSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Alans.Add(data);
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

        protected void GridViewAlanTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Alan deleteddata = service.Alans.Single(p => p.AlanKey == deletedkey);
                service.Alans.Remove(deleteddata);

                try
                {
                    if (Key != 0)
                    {
                        GridViewAlanTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewAlanTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewAlanTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewAlanTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewAlanTanim.GetRowValues(index, new string[] { "AlanKey" }));
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
                GridViewAlanTanim.DataSource = null;
                GridViewAlanTanim.DataSource = service.Alans.
                                                       AsNoTracking().
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewAlanTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Alan data = service.Alans.
                                        AsNoTracking().
                                        Single(p => p.AlanKey == Key);

                    TextBoxAlanAdi.Text = data.Adi;
                    SpinEditAlanSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        #endregion

    }
}