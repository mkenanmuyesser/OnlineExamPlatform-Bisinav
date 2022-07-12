using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace BiSinavProject.PL
{
    public partial class Kurs : System.Web.UI.Page
    {
        #region Properties

        private int Key
        {
            get
            {
                string key = Request.QueryString["K"];
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

        #endregion

        #region Methods

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                RepeaterIl.DataSource = null;
                var iller = service.Ils.
                                                     AsNoTracking().
                                                     Where(p => p.AktifMi).
                                                     OrderBy(p => new
                                                     {
                                                         p.Sira
                                                     }).
                                                     ToList();
                iller.Insert(0, new Il
                {
                    IlKey = 0,
                    Adi = "Tümü",
                });

                RepeaterIl.DataSource = iller.
                                            Select(p => new
                                            {
                                                Key = p.IlKey,
                                                p.Adi,
                                                Secim = p.IlKey == Key
                                            }).
                                            ToList();
                RepeaterIl.DataBind();

                RepeaterKurs.DataSource = null;
                RepeaterKurs.DataSource = service.Kurs.
                                                   AsNoTracking().
                                                   Include("IlKurs").
                                                   Where(p => p.AktifMi &&
                                                              (Key == 0 || p.IlKurs.Where(x=>x.AktifMi).Select(x => x.IlKey).Contains(Key))).
                                                OrderBy(p => new
                                                {
                                                    p.Sira,
                                                }).
                                                ToList();
                RepeaterKurs.DataBind();
            }
        }

        #endregion
    }
}