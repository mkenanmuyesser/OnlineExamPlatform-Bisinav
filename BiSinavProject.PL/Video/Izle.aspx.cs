using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.Video
{
    public partial class Izle : System.Web.UI.Page
    {
        #region Properties

        private int Key
        {
            get
            {
                if (Request.QueryString["K"] == null)
                {
                    return 0;
                }
                else
                {
                    string key = PageHelper.Decrypt(Request.QueryString["K"]);
                    int keysonuc;
                    int.TryParse(key, out keysonuc);
                    return keysonuc;
                }
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            SetInitials();

            if (!IsPostBack)
            {
                DataLoad();
            }
        }     

        #endregion

        #region Methods

        public void SetInitials()
        {
            if (Key == 0)
            {
                Response.Redirect("~/VideoDers.aspx");
                return;
            }

        }

        public void DataLoad()
        {
            using (var service = new DBEntities())
            {
                var kayit = service.Kayits.
                                     AsNoTracking().
                                     Single(p => p.KayitKey == Key);

                if (kayit==null)
                {
                    Response.Redirect("~/VideoDers.aspx");
                    return;
                }
                else
                {
                    LiteralBaslik.Text = kayit.Baslik;
                    LiteralAciklama.Text = kayit.Aciklama;
                    LiteralVideo.Text = kayit.EmbedCode;
                }
            }
        }       

        #endregion
    }
}