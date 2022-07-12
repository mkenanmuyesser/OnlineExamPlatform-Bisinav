using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.ASPxGridView;
using System;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;

namespace BiSinavProject.PL.Program.Sinav
{
    public partial class SoruAramaIslemleri : System.Web.UI.Page
    {
        #region Properties

        private const string PageHeader = "Soru Arama İşlemleri";

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
                GridViewSorular.DataSource = PageHelper.SessionData;
                GridViewSorular.DataBind();
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

        protected void ComboBoxAlanAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DersDenemeYukle();
        }

        protected void GridViewSorular_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Soru deleteddata = service.Sorus.Single(p => p.SoruKey == deletedkey);
                service.Sorus.Remove(deleteddata);

                try
                {
                    service.SaveChanges();
                    GridViewSorular.JSProperties["cpErrorMessage"] = false;
                }
                catch
                {
                    GridViewSorular.JSProperties["cpErrorMessage"] = true;
                }
            }

            Ara();
            e.Cancel = true;
        }

        protected void GridViewSorular_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewSorular.GetRowValues(index, new string[] { "SoruKey" }));
            ASPxGridView.RedirectOnCallback(string.Format("SoruTanimIslemleri.aspx?Key={0}", key));
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            LabelBaslik.Text = PageHeader;
            DataLoad();
        }

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                List<Alan> listBESTEBES_ALAN = service.Alans.
                                                                AsNoTracking().
                                                                OrderBy(p => p.Sira).
                                                                ToList();
                listBESTEBES_ALAN.Insert(0, new Alan
                {
                    AlanKey = 0,
                    Adi = "Tümü"
                });

                ComboBoxAlanAdi.DataSource = listBESTEBES_ALAN;
                ComboBoxAlanAdi.DataBind();

                if (listBESTEBES_ALAN.Any())
                {
                    ComboBoxAlanAdi.SelectedIndex = 0;
                    DersDenemeYukle();
                }
            }
        }

        private void Ara()
        {
            using (var service = new DBEntities())
            {
                int pAlanKey = Convert.ToInt32(ComboBoxAlanAdi.SelectedItem.Value);
                int pDersKey = Convert.ToInt32(ComboBoxDersAdi.SelectedItem.Value);
                int pDenemeKey = Convert.ToInt32(ComboBoxDenemeAdi.SelectedItem.Value);
                string pSoru = TextBoxSoru.Text;
                byte pAktif = Convert.ToByte(RadioButtonListAktif.SelectedItem.Value);

                GridViewSorular.DataSource = null;
                var sonuc = service.Sorus.
                                    AsNoTracking().
                                    Include("Der").
                                    Include("Der.Alan").
                                    OrderBy(p => p.Der.Alan.Adi).
                                    OrderBy(p => p.Der.Adi).
                                    Where(p =>
                                         (pAlanKey == 0 || p.Der.AlanKey == pAlanKey) &&
                                         (pDersKey == 0 || p.DersKey == pDersKey) &&
                                         (pDenemeKey == 0 || p.DenemeKey == pDenemeKey) &&
                                          p.Soru1.Contains(pSoru) &&
                                         (pAktif == 0 || (p.AktifMi == (pAktif == 1 ? true : false)))).
                                    Select(p => new
                                    {
                                        SoruKey = p.SoruKey,
                                        AlanAdi = p.Der.Alan.Adi,
                                        DersAdi = p.Der.Adi,
                                        SoruNo = p.SoruKey,
                                        Soru = p.Soru1.Length > 300 ? p.Soru1.Substring(0, 300) + "..." : p.Soru1,
                                        AktifMi = p.AktifMi,
                                    }).
                                    ToList();
             
                #region gruplama

                GridViewSorular.GroupBy(GridViewSorular.Columns["DersAdi"]);

                #endregion

                GridViewSorular.DataSource = sonuc;
                PageHelper.SessionData = sonuc;
                GridViewSorular.DataBind();

                LabelSonuc.Text = string.Format("Sonuç sayısı : {0}", sonuc.Count());
            }
        }

        private void DersDenemeYukle()
        {
            using (var service = new DBEntities())
            {
                int alanKey = Convert.ToInt32(ComboBoxAlanAdi.SelectedItem.Value);
                List<Der> listDers = service.Ders.
                                            AsNoTracking().
                                            Where(p => alanKey == 0 || p.AlanKey == alanKey).
                                            OrderBy(p => p.Sira).
                                            ToList();
                listDers.Insert(0, new Der { DersKey = 0, Adi = "Tümü" });
                ComboBoxDersAdi.DataSource = listDers;
                ComboBoxDersAdi.DataBind();
                ComboBoxDersAdi.SelectedIndex = 0;

                List<Deneme> listDeneme = service.Denemes.
                                                  AsNoTracking().
                                                  Where(p => alanKey == 0 || p.AlanKey == alanKey).
                                                  OrderBy(p => p.Sira).
                                                  ToList();

                listDeneme.Insert(0, new Deneme { DenemeKey = 0, Adi = "Tümü" });
                ComboBoxDenemeAdi.DataSource = listDeneme;
                ComboBoxDenemeAdi.DataBind();
                ComboBoxDenemeAdi.SelectedIndex = 0;

            }
        }

        #endregion

    }
}