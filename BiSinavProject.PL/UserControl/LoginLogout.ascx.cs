using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.UserControl
{
    public partial class LoginLogout : System.Web.UI.UserControl
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink link = LoginViewGirisCikis.FindControl("HyperLinkFacebookGiris") as HyperLink;
            if (link != null)
            {
                link.NavigateUrl = "https://www.facebook.com/v2.6/dialog/oauth/?client_id=" +
                ConfigurationManager.AppSettings["FacebookAppId"] +
                "&redirect_uri=" +
                //"http://localhost:14690" +
                "http://www.bisinav.net" +
                "/Kullanici/GirisYonlendir.aspx&response_type=code&state=1&scope=email";               
            }
          
        }

        #endregion

        #region Methods

        #endregion
    }
}