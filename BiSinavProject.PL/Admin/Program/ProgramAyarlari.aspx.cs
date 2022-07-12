using BiSinavProject.PL.Data;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.Program.Program
{
    public partial class ProgramAyarlari : System.Web.UI.Page
    {

        #region Properties

        private const string PageUrl = "ProgramAyarlari.aspx";
        private const string PageHeader = "Program Ayarları";
        private const string UploadDirectory = "/Uploads/Program/";

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
                Ayar data = service.Ayars.Single();

                data.MailHost = TextBoxMailHost.Text;
                data.MailPort = Convert.ToInt32(SpinEditMailPort.Value);
                data.MailKullaniciAdi = TextBoxMailKullaniciAdi.Text;
                data.MailSifre = TextBoxMailSifre.Text;

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

        protected void ButtonYedekle_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            string dbName = connectionString.Split(';')[1].Split('=')[1];
            SqlConnection cnn = new SqlConnection(connectionString);

            string backupDirectory = Server.MapPath(UploadDirectory);
            string command = "Backup Database "+ dbName + " to disk = '" + backupDirectory + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".bak'";
            SqlCommand cmd = new SqlCommand(command, cnn);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect(PageUrl);
        }

        protected void LinkButtonIndir_Click(object sender, EventArgs e)
        {
            string dosyaAdi = (sender as LinkButton).CommandArgument;
            FileInfo dosya = new FileInfo(Server.MapPath(UploadDirectory + dosyaAdi));

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + dosyaAdi);
            Response.AddHeader("Content-Length", dosya.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.WriteFile(dosya.FullName);
            Response.End();
        }

        protected void LinkButtonSil_Click(object sender, EventArgs e)
        {
            string dosyaAdi = (sender as LinkButton).CommandArgument;
            FileInfo dosya = new FileInfo(Server.MapPath(UploadDirectory + dosyaAdi));
            if (dosya != null)
            {
                dosya.Delete();
            }

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
                Ayar data = service.Ayars.Single();

                TextBoxMailHost.Text = data.MailHost;
                SpinEditMailPort.Value = data.MailPort;
                TextBoxMailKullaniciAdi.Text = data.MailKullaniciAdi;
                TextBoxMailSifre.Text = data.MailSifre;

                string backupDirectory = Server.MapPath(UploadDirectory);
                DirectoryInfo directoryInfo = new DirectoryInfo(backupDirectory);

                RepeaterAlinanYedekler.DataSource = directoryInfo.GetFiles("*.bak");
                RepeaterAlinanYedekler.DataBind();
            }
        }

        #endregion

       
    }
}