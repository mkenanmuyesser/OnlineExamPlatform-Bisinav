using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Linq;

namespace BiSinavProject.PL
{
    public partial class Iletisim : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonMesajGonder_Click(object sender, EventArgs e)
        {
            string adSoyad = TextBoxAdSoyad.Text;
            string ePosta = TextBoxEposta.Text;
            string baslik = TextBoxBaslik.Text;
            string mesaj = baslik + " " + adSoyad + " " + ePosta + " " + TextBoxMesaj.Text;

            Ayar ayar = null;
            using (var service = new DBEntities())
            {
                ayar = service.Ayars.AsNoTracking().SingleOrDefault();
            }

            if (MailHelper.MailGonder(ayar.MailKullaniciAdi, mesaj, "Bilgi"))
            {
                LabelSonuc.Text = "Mesajnız gönderilmiştir.";

                Temizle();
            }
            else
            {
                LabelSonuc.Text = "Mesajnız gönderilirken bir hata oluştu. Lütfen tekrar deneyiniz.";
            }
        }

        #endregion

        #region Methods

        private void Temizle()
        {
            TextBoxAdSoyad.Text = "";
            TextBoxEposta.Text = "";
            TextBoxBaslik.Text = "";
            TextBoxMesaj.Text = "";
        }

        #endregion

    }
}