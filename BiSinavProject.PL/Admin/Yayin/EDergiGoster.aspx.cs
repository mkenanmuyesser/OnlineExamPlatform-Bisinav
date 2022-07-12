using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.UserControl
{
    public partial class EDergiGoster : System.Web.UI.Page
    {
        #region Properties

        private int Key
        {
            get
            {
                string key = Request.QueryString["Key"];
                int keysonuc;
                int.TryParse(key, out keysonuc);
                return keysonuc;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitials();
            }           
        }

        protected void ButtonGeriDon_Click(object sender, EventArgs e)
        {
            Response.Redirect("EDergiTanimIslemleri.aspx");
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            if (Key == 0)
            {
                Response.Redirect("EDergiTanimIslemleri.aspx", true);
                return;
            }
            else
            {
                DataLoad();
            }
        }

        private void DataLoad()
        {
            if (!IsPostBack)
            {
                PageFlipperControl.Key = Key;
            }
        }

        #endregion
     
    }
}