using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace BiSinavProject.PL.Class.Helper
{
    public class PageHelper
    {
        public static object SessionData
        {
            get { return HttpContext.Current.Session["SessionData"]; }
            set { HttpContext.Current.Session["SessionData"] = value; }
        }

        public static void MessageBox(Page _page, string _message)
        {
            string script = "<script type=\"text/javascript\">alert('" + _message + "');</script>";
            _page.ClientScript.RegisterClientScriptBlock(_page.GetType(), "alert", script);
        }

        public static DateTime ConvertDate(DateTime pTarih)
        {
            return pTarih.Date;
        }

        public static string ConvertTime(DateTime pTarih)
        {
            return pTarih.ToShortTimeString();
        }

        public static bool TcKimlikDogrulama(string pTcKimlikNo)
        {
            int toplam = 0;
            for (int i = 0; i < pTcKimlikNo.Length - 1; i++)
            {
                toplam += Convert.ToInt32(pTcKimlikNo[i].ToString());
            }

            int sonhane = Convert.ToInt32(pTcKimlikNo[10].ToString());
            if ((toplam % 10) == sonhane)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return HttpUtility.UrlEncode(clearText);
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}