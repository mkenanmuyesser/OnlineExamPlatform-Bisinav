using BiSinavProject.PL.Class.Helper;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Kullanici
{
    public partial class Hatirlatma : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSifreGonder_Click(object sender, EventArgs e)
        {
            string adres = TextBoxEposta.Text;
            var users = Membership.FindUsersByEmail(adres);
            if (users.Count == 0)
            {
                LabelSonuc.Text = "Kullanıcı hesabı bulunamadı.";
            }
            else
            {
                var user = users.OfType<MembershipUser>().First();
                string resetPassword = user.ResetPassword();

                string mesaj = "Kullanıcı adınız : " + user.UserName + "<br />" + "Yenilenen şifreniz : " + resetPassword;

                if (MailHelper.MailGonder(adres, mesaj, "Bilgi"))
                {
                    LabelSonuc.Text = "Şifreniz e-posta kutunuza gönderilmiştir.";
                }
                else
                {
                    LabelSonuc.Text = "Şifre gönderilirken bir hata oluştu. Lütfen tekrar deneyiniz.";
                }
            }
        }

        #endregion

        #region Methods


        #endregion
    }
}