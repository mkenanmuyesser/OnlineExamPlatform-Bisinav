using BiSinavProject.PL.Class.Business;
using BiSinavProject.PL.Class.CustomType;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL
{
    public partial class BesteBesSinav : System.Web.UI.Page
    {
        #region Properties

        private int Key
        {
            get
            {
                string key = Request.QueryString["P"];
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
                DataLoad();
            }
        }

        protected void LinkButtonGiris_Click(object sender, EventArgs e)
        {
            int dersKey = Convert.ToInt32((sender as LinkButton).CommandArgument);

            BesteBesBs.SoruOlustur(dersKey);

            Response.Redirect("/BesteBes/Sinav.aspx");
        }

        #endregion

        #region Methods

        private void DataLoad()
        {          
            using (var service = new DBEntities())
            {
                RepeaterAlan.DataSource = null;
                var iller = service.Alans.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderBy(p => new
                                                     {
                                                         p.Sira
                                                     }).
                                                     ToList();
                iller.Insert(0, new Alan
                {
                    AlanKey = 0,
                    Adi = "Tümü",
                });

                RepeaterAlan.DataSource = iller.
                                            Select(p => new
                                            {
                                                Key = p.AlanKey,
                                                p.Adi,
                                                Secim = p.AlanKey == Key
                                            }).
                                            ToList();
                RepeaterAlan.DataBind();


                RepeaterDersler.DataSource = null;
                RepeaterDersler.DataSource = service.Ders.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi &&
                                                               (Key == 0 || p.AlanKey == Key)).
                                                     OrderBy(p => p.Sira).
                                                     ToList();
                RepeaterDersler.DataBind();
            }
        }

        #endregion
        
    }
}