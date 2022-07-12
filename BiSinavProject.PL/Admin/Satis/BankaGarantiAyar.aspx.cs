using BiSinavProject.PL.Data;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.Program.Satis
{
    public partial class BankaGarantiAyar : System.Web.UI.Page
    {

        #region Properties

        private const string PageUrl = "BankaGarantiAyar.aspx";
        private const string PageHeader = "Garanti Bankası Ayarları";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitials();
            }
        }

        protected void ButtonGuncelle_Click(object sender, EventArgs e)
        {
            #region validation

            #endregion

            using (var service = new DBEntities())
            {
                BankaGaranti data = service.BankaGarantis.Single();

                data.StoreKey = TextBoxStoreKey.Text;
                data.ProvisionPassword = TextBoxProvisionPassword.Text;
                data.CompanyName = TextBoxCompanyName.Text;
                data.TerminalId = TextBoxTerminalId.Text;
                data.TerminalUserId = TextBoxTerminalUserId.Text;
                data.TerminalMerchantId = TextBoxTerminalMerchantId.Text;
                data.CustomerIpAddress = TextBoxCustomerIpAddress.Text;
                data.CustomerEmailAddress = TextBoxCustomerEmailAddress.Text;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;

                data.GuncelleKisiKey = userkey;
                data.GuncelleTarih = DateTime.Now;

                service.SaveChanges();
            }

            Response.Redirect(PageUrl);
        }

        protected void ButtonIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl);
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            LabelBaslik.Text = PageHeader;

            DataLoad();
        }

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                BankaGaranti data = service.BankaGarantis.Single();

                TextBoxStoreKey.Text = data.StoreKey;
                TextBoxProvisionPassword.Text = data.ProvisionPassword;
                TextBoxCompanyName.Text = data.CompanyName;
                TextBoxTerminalId.Text = data.TerminalId;
                TextBoxTerminalUserId.Text = data.TerminalUserId;
                TextBoxTerminalMerchantId.Text = data.TerminalMerchantId;
                TextBoxCustomerIpAddress.Text = data.CustomerIpAddress;
                TextBoxCustomerEmailAddress.Text = data.CustomerEmailAddress;
            }
        }

        #endregion


    }
}