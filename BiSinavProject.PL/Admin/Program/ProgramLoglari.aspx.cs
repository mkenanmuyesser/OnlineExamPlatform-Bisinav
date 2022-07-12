using System.Linq;
using System;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;

namespace BiSinavProject.PL.Program.Program
{
    public partial class ProgramLoglari : System.Web.UI.Page
    {
        #region Properties

        private const string PageHeader = "Program Logları";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageHelper.SessionData = null;
                SetInitials();
            }
            else
            {
                GridViewProgramLoglari.DataSource = PageHelper.SessionData;
                GridViewProgramLoglari.DataBind();
            }
        }

        protected void ButtonAra_Click(object sender, EventArgs e)
        {
            Ara();
        }

        protected void ButtonTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void GridViewProgramLoglari_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Log deleteddata = service.Logs.Single(p => p.LogKey == deletedkey);
                service.Logs.Remove(deleteddata);

                try
                {
                    service.SaveChanges();
                    GridViewProgramLoglari.JSProperties["cpErrorMessage"] = false;
                }
                catch
                {
                    GridViewProgramLoglari.JSProperties["cpErrorMessage"] = true;
                }
            }

            Ara();
            e.Cancel = true;
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            LabelBaslik.Text = PageHeader;

            DateEditBaslangicZamani.Value = DateTime.Now.AddDays(-1).Date;
            DateEditBitisZamani.Value = DateTime.Now.Date.AddHours(23).AddMinutes(59);
        }

        private void Ara()
        {
            using (var service = new DBEntities())
            {
                DateTime baslangicZamani = Convert.ToDateTime(DateEditBaslangicZamani.Value);
                DateTime bitisZamani = Convert.ToDateTime(DateEditBitisZamani.Value);

                GridViewProgramLoglari.DataSource = null;
                var sonuc = service.Logs.
                                    AsNoTracking().
                                    Where(p => baslangicZamani <= p.Tarih &&
                                               bitisZamani >= p.Tarih).
                                    OrderByDescending(p => p.Tarih).
                                    ToList();

                GridViewProgramLoglari.DataSource = sonuc;
                PageHelper.SessionData = sonuc;
                GridViewProgramLoglari.DataBind();
            }
        }

        #endregion

    }
}