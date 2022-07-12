using System;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.Master_Page
{
    public partial class AdminMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDataSource datasidemenu = new XmlDataSource
            {
                DataFile = "~/Data/SideMenu.xml",
                XPath = "/menu/*"
            };

            NavBarAltMenu.DataSource = datasidemenu;
            NavBarAltMenu.DataBind();
        
        }
    }
}