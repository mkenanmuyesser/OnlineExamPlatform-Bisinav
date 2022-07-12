using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace BiSinavProject.PL.Class.Helper
{
    public class MailHelper
    {
        public static bool MailGonder(string adres,string mesaj,string takmaAd)
        {
            try
            {
                Ayar ayar = null;
                using (var service = new DBEntities())
                {
                    ayar = service.Ayars.AsNoTracking().SingleOrDefault();
                }

                SmtpClient smtpClient = new SmtpClient(ayar.MailHost, ayar.MailPort.Value);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(ayar.MailKullaniciAdi, ayar.MailSifre);
                //smtpClient.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.Body = mesaj;
                mail.Subject = takmaAd;
                mail.From = new MailAddress(ayar.MailKullaniciAdi, takmaAd);
                mail.To.Add(new MailAddress(adres));

                smtpClient.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}