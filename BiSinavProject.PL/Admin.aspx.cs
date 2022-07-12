using System;
using System.Web.Security;
using BiSinavProject.PL.Class.Helper;

namespace BiSinavProject.PL
{
    public partial class Admin : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ButtonGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = TextBoxKullaniciAdi.Text;
            string sifre = TextBoxSifre.Text;


            if (Membership.ValidateUser(kullaniciAdi, sifre))
            {
                if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                {
                    FormsAuthentication.SetAuthCookie(kullaniciAdi, false);
                    Response.Redirect("Admin/AnaMenu.aspx");
                }
                else
                    FormsAuthentication.RedirectFromLoginPage(kullaniciAdi, false);
            }
            else
            {
                PageHelper.MessageBox(this, "Kullanýcý adý veya þifreniz yanlýþ!");
            }
        }

        protected void ButtonAnasayfa_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Default.aspx");

        }

        #endregion

        #region Methods

        #endregion

    }
}