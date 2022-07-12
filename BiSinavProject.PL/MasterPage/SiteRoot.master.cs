using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL.Master_Page
{
    public partial class SiteRoot : System.Web.UI.MasterPage
    {
        #region Properties

        const string UploadDirectory = "/Uploads/Program/";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Action = Request.RawUrl;

            if (!IsPostBack)
            {
                DataLoad();
            }
        }

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                var reklamlar = service.Tanitims.
                                        AsNoTracking().
                                        Where(p => p.AktifMi).
                                        ToList();

                var reklamBaslikBolgesi = reklamlar.FirstOrDefault(p => p.BolgeTipKey == Convert.ToInt32(ReklamBolgeTip.BaslikBolgesi));
                if (reklamBaslikBolgesi == null)
                {
                     HyperLinkHyperLinkBaslikBolgesi.ImageUrl = "../Content/Site/Images/no-banner-728x90.jpg";
                }
                else
                {                   
                    if (!string.IsNullOrEmpty(reklamBaslikBolgesi.Link))
                    {
                        HyperLinkHyperLinkBaslikBolgesi.Target = "_blank";
                        HyperLinkHyperLinkBaslikBolgesi.NavigateUrl = reklamBaslikBolgesi.Link;
                    }

                    HyperLinkHyperLinkBaslikBolgesi.ImageUrl = UploadDirectory + reklamBaslikBolgesi.Resim;
                }                
            }
        }

        #endregion
    }
}