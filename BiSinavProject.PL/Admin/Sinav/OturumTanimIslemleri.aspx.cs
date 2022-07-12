using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Sinav
{
    public partial class OturumTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "OturumTanimIslemleri.aspx";
        private const string PageHeader = "Oturum Tanım İşlemleri";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitials();
            }

            DataLoad();
        }

        protected void ButtonKaydetGuncelle_Click(object sender, EventArgs e)
        {
            #region validation

            #endregion

            using (var service = new DBEntities())
            {
                Oturum data;
                if (Key == 0)
                {
                    data = new Oturum();
                }
                else
                {
                    data = service.Oturums.SingleOrDefault(p => p.OturumKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Adi = TextBoxOturumAdi.Text;
                data.Baslangic = Convert.ToDateTime(DateEditBaslangicZamani.Value);
                data.Bitis = Convert.ToDateTime(DateEditBitisZamani.Value);                
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Oturums.Add(data);
                }
                else
                {
                    data.GuncelleKisiKey = userkey;
                    data.GuncelleTarih = DateTime.Now;
                }

                service.SaveChanges();

            }

            Response.Redirect(PageUrl);
        }

        protected void ButtonIptalTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl);
        }

        protected void GridViewOturumTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Oturum deleteddata = service.Oturums.Single(p => p.OturumKey == deletedkey);
                service.Oturums.Remove(deleteddata);

                try
                {
                    if (Key != 0)
                    {
                        GridViewOturumTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewOturumTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewOturumTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewOturumTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewOturumTanim.GetRowValues(index, new string[] { "OturumKey" }));
            ASPxGridView.RedirectOnCallback(string.Format("{0}?Key={1}", PageUrl, key));
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            if (Key == 0)
            {
                ButtonKaydet.Visible = true;
                ButtonTemizle.Visible = true;
                ButtonGuncelle.Visible = false;
                ButtonIptal.Visible = false;
            }
            else
            {
                ButtonKaydet.Visible = false;
                ButtonTemizle.Visible = false;
                ButtonGuncelle.Visible = true;
                ButtonIptal.Visible = true;
            }

            LabelBaslik.Text = PageHeader;
        }

        private void DataLoad()
        {
            using (var service = new DBEntities())
            {
                if (!IsPostBack)
                {
                    DateEditBaslangicZamani.Value = DateTime.Now.Date.AddHours(9);
                    DateEditBitisZamani.Value = DateTime.Now.Date.AddHours(23).AddMinutes(55);
                }

                GridViewOturumTanim.DataSource = null;
                GridViewOturumTanim.DataSource = service.Oturums.
                                                       AsNoTracking().
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewOturumTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Oturum data = service.Oturums.
                                          AsNoTracking().
                                          Single(p => p.OturumKey == Key);

                    TextBoxOturumAdi.Text = data.Adi;
                    DateEditBaslangicZamani.Value = data.Baslangic;
                    DateEditBitisZamani.Value = data.Bitis;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        #endregion

    }
}