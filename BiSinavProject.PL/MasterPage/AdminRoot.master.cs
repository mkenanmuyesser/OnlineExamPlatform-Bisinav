using System;

namespace BiSinavProject.PL.Master_Page
{
    public partial class AdminRoot : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            //LabelFirma.Text = DateTime.Now.Year + Server.HtmlDecode(" &copy; ");
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");
        }
    }
}