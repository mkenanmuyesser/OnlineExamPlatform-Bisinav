using System;
using System.Web.Security;

namespace BiSinavProject.PL.Kullanici
{
    public partial class Detay : System.Web.UI.Page
    {
        #region Properties

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Membership.GetUser(true)==null)
            {
                Response.Redirect("/Default.aspx");
            }
        }      

        #endregion

        #region Methods


        #endregion
    }
}