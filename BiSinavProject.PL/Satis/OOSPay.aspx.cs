using System;
using System.Text;
using System.Web.UI;
using System.Linq;
using System.Security.Cryptography;
using BiSinavProject.PL.Data;

namespace BiSinavProject.PL.Satis
{
    public partial class OOSPay : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitials();
            }           
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            
            if (!IsPostBack)
            {
                using (var service = new DBEntities())
                {
                    BankaGaranti bankaGaranti = service.BankaGarantis.SingleOrDefault();

                    string strMode = "PROD";
                    string strApiVersion = "v0.01";
                    string strTerminalProvUserID = "PROVOOS";
                    string strType = "sales";
                    string strAmount = "100"; // slem Tutarı
                    string strCurrencyCode = "949";
                    string strInstallmentCount = ""; //Taksit Sayısı. Bos gönderilirse taksit yapılmaz
                    string strTerminalUserID = bankaGaranti.TerminalUserId; //"XXXXXX";
                    string strOrderID = "deneme";
                    string strCustomeripaddress = bankaGaranti.CustomerIpAddress;//"127.0.0.1";
                    string strCustomeremailaddress = bankaGaranti.CustomerEmailAddress; //"info@tradesis.com";
                    string strTerminalID = bankaGaranti.TerminalId; //"XXXXXXXX";
                    string _strTerminalID = bankaGaranti.TerminalId; //"0XXXXXXXX"; //'Basına 0 eklenerek 9 digite tamamlanmalıdır.
                    string strTerminalMerchantID = bankaGaranti.TerminalMerchantId; //"XXXXXX"; //Üye syeri Numarası
                    string strStoreKey = bankaGaranti.StoreKey; //"XXXXXX"; //3D Secure sifreniz
                    string strProvisionPassword = bankaGaranti.ProvisionPassword;//"XXXXXX"; //Terminal UserID sifresi
                    string strSuccessURL = "http://www.bisinav.net/Satis/OOSPayResults.aspx"; //"https://<sunucu_adresi>/OOSPayResults.aspx";
                    string strErrorURL = "http://www.bisinav.net/Satis/OOSPayResults.aspx";//"https://<sunucu_adresi>/OOSPayResults.aspx";
                    string strCompanyName = bankaGaranti.CompanyName;//"TradeSiS";
                    string strlang = "tr";
                    string strtimestamp = DateTime.Now.ToString();//"XXXXXX";
                    string SecurityData = GetSHA1(strProvisionPassword + _strTerminalID).ToUpper();
                    string HashData = GetSHA1(strTerminalID + strOrderID + strAmount + strSuccessURL + strErrorURL +
                    strType + strInstallmentCount + strStoreKey + SecurityData).ToUpper();
                    mode.Value = strMode;
                    apiversion.Value = strApiVersion;
                    terminalprovuserid.Value = strTerminalProvUserID;
                    terminaluserid.Value = strTerminalUserID;
                    terminalmerchantid.Value = strTerminalMerchantID;
                    txntype.Value = strType;
                    txnamount.Value = strAmount;
                    txncurrencycode.Value = strCurrencyCode;
                    txninstallmentcount.Value = strInstallmentCount;
                    customeremailaddress.Value = strCustomeremailaddress;
                    customeripaddress.Value = strCustomeripaddress;
                    orderid.Value = strOrderID;
                    terminalid.Value = strTerminalID;
                    successurl.Value = strSuccessURL;
                    errorurl.Value = strErrorURL;
                    companyname.Value = strCompanyName;
                    lang.Value = strlang;
                    secure3dhash.Value = HashData;
                    txntimestamp.Value = strtimestamp;
                }
            }
        }

        public string GetSHA1(string SHA1Data)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string HashedPassword = SHA1Data;
            byte[] hashbytes = Encoding.GetEncoding("ISO-8859-9").GetBytes(HashedPassword);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            return GetHexaDecimal(inputbytes);
        }

        public string GetHexaDecimal(byte[] bytes)
        {
            StringBuilder s = new StringBuilder();
            int length = bytes.Length;
            for (int n = 0; n <= length - 1; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }

        #endregion

    }
}