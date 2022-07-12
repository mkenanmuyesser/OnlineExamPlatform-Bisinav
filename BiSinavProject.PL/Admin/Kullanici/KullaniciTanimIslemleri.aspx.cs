using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Kullanici
{
    public partial class KullaniciTanimIslemleri : System.Web.UI.Page
    {
        #region Properties

        private Guid Key
        {
            get
            {
                string key = Request.QueryString["Key"];
                Guid keysonuc;
                Guid.TryParse(key, out keysonuc);
                return keysonuc;
            }
        }

        private const string PageUrl = "KullaniciTanimIslemleri.aspx";
        private const string PageHeader = "Kullanıcı Tanım İşlemleri";

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

            string kullaniciAdi = TextBoxKullaniciAdi.Text;
            string sifre = TextBoxSifre.Text;
            bool aktifmi = CheckBoxAktif.Checked;

            #region validation

            #endregion

            if (Key == Guid.Empty)
            {
                MembershipCreateStatus durum;
                Membership.CreateUser(kullaniciAdi, sifre, kullaniciAdi, null, null, aktifmi, out durum);


                if (durum != MembershipCreateStatus.Success)
                {
                    PageHelper.MessageBox(this, "Kullanıcı oluşturulamadı!");
                    return;
                }

                Roles.AddUserToRole(kullaniciAdi, "Admin");
            }
            else
            {
                MembershipUser kullanici = Membership.GetUser(Key);
                if (kullanici == null)
                {
                    Response.Redirect(PageUrl);
                }
                else
                {
                    try
                    {
                        kullanici.ChangePassword(kullanici.ResetPassword(), sifre);
                        kullanici.IsApproved = aktifmi;
                        kullanici.Email = kullanici.UserName;

                        Membership.UpdateUser(kullanici);
                    }
                    catch(Exception ex)
                    {
                        PageHelper.MessageBox(this, "Kullanıcı güncelleme işlemi başarısız!");
                        return;
                    }
                }
            }

            Response.Redirect(PageUrl);
        }

        protected void ButtonIptalTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl);
        }

        protected void GridViewYoneticiKullaniciIslem_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid deletedkey = Guid.Parse(e.Keys[0].ToString());

            try
            {
                if (deletedkey == Guid.Empty)
                {
                    GridViewYoneticiKullaniciIslem.JSProperties["cpErrorMessage"] = true;
                }
                else
                {
                    MembershipUser kullanici = Membership.GetUser(deletedkey);
                    if (!Membership.DeleteUser(kullanici.UserName))
                    {
                        GridViewYoneticiKullaniciIslem.JSProperties["cpErrorMessage"] = false;
                    }
                }
            }
            catch
            {
                GridViewYoneticiKullaniciIslem.JSProperties["cpErrorMessage"] = true;
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewYoneticiKullaniciIslem_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            Guid key = Guid.Parse(GridViewYoneticiKullaniciIslem.GetRowValues(index, new string[] { "UserId" }).ToString());
            ASPxGridView.RedirectOnCallback(string.Format("{0}?Key={1}", PageUrl, key));
        }

        protected void GridViewProgramKullaniciIslem_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            Guid deletedkey = Guid.Parse(e.Keys[0].ToString());

            try
            {
                if (deletedkey == Guid.Empty)
                {
                    GridViewProgramKullaniciIslem.JSProperties["cpErrorMessage"] = true;
                }
                else
                {
                    MembershipUser kullanici = Membership.GetUser(deletedkey);
                    if (!Membership.DeleteUser(kullanici.UserName))
                    {
                        GridViewProgramKullaniciIslem.JSProperties["cpErrorMessage"] = false;
                    }
                }
            }
            catch
            {
                GridViewProgramKullaniciIslem.JSProperties["cpErrorMessage"] = true;
            }

            DataLoad();
            e.Cancel = true;
        }
     
        #endregion

        #region Methods

        private void SetInitials()
        {
            if (Key == Guid.Empty)
            {
                TextBoxKullaniciAdi.Enabled = true;
                ButtonKaydet.Visible = true;
                ButtonTemizle.Visible = true;
                ButtonGuncelle.Visible = false;
                ButtonIptal.Visible = false;
            }
            else
            {
                TextBoxKullaniciAdi.Enabled = false;
                ButtonKaydet.Visible = false;
                ButtonTemizle.Visible = false;
                ButtonGuncelle.Visible = true;
                ButtonIptal.Visible = true;
            }

            MembershipUser kullanici = Membership.GetUser(true);
            if (kullanici != null && Roles.IsUserInRole("SuperAdmin"))
            {
                GridViewYoneticiKullaniciIslem.Columns[5].Visible = true;
            }
            else
            {
                GridViewYoneticiKullaniciIslem.Columns[5].Visible = false;
            }

            LabelBaslik.Text = PageHeader;
        }

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                GridViewYoneticiKullaniciIslem.DataSource = null;
                GridViewYoneticiKullaniciIslem.DataSource = service.aspnet_Users.
                                                                    AsNoTracking().
                                                                    Include("aspnet_Membership").
                                                                    Include("aspnet_Roles").
                                                                    Where(p => p.aspnet_Roles.Any(x => x.RoleName != "SuperAdmin") &&
                                                                               p.aspnet_Roles.Any(x => x.RoleName == "Admin")).
                                                                    OrderBy(p => p.UserName).ToList();
                GridViewYoneticiKullaniciIslem.DataBind();


                GridViewProgramKullaniciIslem.DataSource = null;
                GridViewProgramKullaniciIslem.DataSource = service.aspnet_Users.
                                                                    AsNoTracking().
                                                                    Include("aspnet_Membership").
                                                                    Include("aspnet_Roles").
                                                                    Where(p =>
                                                                    !p.aspnet_Roles.Any()
                                                                               //p.aspnet_Roles ==null
                                                                               //p.aspnet_Roles.Any(x => x.RoleName == "Program") ||
                                                                               //p.aspnet_Roles.Any(x => x.RoleName == "Guest")
                                                                               ).
                                                                    OrderBy(p => p.UserName).ToList();
                GridViewProgramKullaniciIslem.DataBind();


                if (Key != Guid.Empty && !IsPostBack)
                {
                    MembershipUser kullanici = Membership.GetUser(Key);
                    if (kullanici == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                    else
                    {
                        TextBoxKullaniciAdi.Text = kullanici.UserName;
                        TextBoxSifre.Text =string.Empty;
                        CheckBoxAktif.Checked = kullanici.IsApproved;
                    }
                }
            }
        }

        #endregion

    }
}