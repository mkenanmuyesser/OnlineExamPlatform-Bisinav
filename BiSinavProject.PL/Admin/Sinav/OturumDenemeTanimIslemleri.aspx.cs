using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Sinav
{
    public partial class OturumDenemeTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "OturumDenemeTanimIslemleri.aspx";
        private const string PageHeader = "Oturum Deneme Tanım İşlemleri";

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
            using (var service = new DBEntities())
            {
                int oturumKey = Convert.ToInt32(ComboBoxOturum.SelectedItem.Value);
                int denemeKey = Convert.ToInt32(ComboBoxDenemeAdi.SelectedItem.Value);

                #region validation

                //aynı eşleştirme varmı diye kontrol edelim
                var eslesme = service.OturumDenemes.Any(p => p.OturumKey == oturumKey && p.DenemeKey == denemeKey);
                if (eslesme)
                {
                    PageHelper.MessageBox(this, "Aynı kayıt bulunmaktadır.");
                    return;
                }

                #endregion

                OturumDeneme data;
                if (Key == 0)
                {
                    data = new OturumDeneme();
                }
                else
                {
                    data = service.OturumDenemes.SingleOrDefault(p => p.OturumDenemeKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.OturumKey = oturumKey;
                data.DenemeKey = denemeKey;
                data.Sira = Convert.ToInt32(SpinEditDersSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.OturumDenemes.Add(data);
                }
                else
                {
                    data.GuncelleKisiKey = userkey;
                    data.GuncelleTarih = DateTime.Now;
                }

                service.SaveChanges();

            }

            DataLoad();
        }

        protected void ButtonIptalTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl);
        }

        protected void GridViewOturumDeneme_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                OturumDeneme deleteddata = service.OturumDenemes.Single(p => p.OturumDenemeKey == deletedkey);
                service.OturumDenemes.Remove(deleteddata);

                try
                {
                    if (Key != 0)
                    {
                        GridViewOturumDeneme.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewOturumDeneme.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewOturumDeneme.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewOturumDeneme_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewOturumDeneme.GetRowValues(index, new string[] { "OturumDenemeKey" }));
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

                    List<Oturum> listOturum = service.Oturums.
                                                      AsNoTracking().
                                                      OrderBy(p => p.Sira).
                                                      ToList();

                    ComboBoxOturum.DataSource = listOturum;
                    ComboBoxOturum.DataBind();

                    List<Deneme> listDeneme = service.Denemes.
                                                      AsNoTracking().
                                                      OrderBy(p => p.Sira).
                                                      ToList();

                    ComboBoxDenemeAdi.DataSource = listDeneme;
                    ComboBoxDenemeAdi.DataBind();
                }

                GridViewOturumDeneme.DataSource = null;
                GridViewOturumDeneme.DataSource = service.OturumDenemes.
                                                           AsNoTracking().
                                                           Include("Deneme").
                                                           Include("Deneme.Alan").
                                                           Include("Oturum").
                                                           OrderBy(p => p.Sira).
                                                           ToList();
                GridViewOturumDeneme.DataBind();

                #region gruplama

                GridViewOturumDeneme.GroupBy(GridViewOturumDeneme.Columns["Oturum.Adi"]);

                #endregion

                if (Key != 0 && !IsPostBack)
                {
                    OturumDeneme data = service.OturumDenemes.
                                                 AsNoTracking().
                                                 Single(p => p.OturumDenemeKey == Key);

                    ComboBoxOturum.Items.FindByValue(data.OturumKey).Selected = true;
                    ComboBoxDenemeAdi.Items.FindByValue(data.DenemeKey).Selected = true;
                    SpinEditDersSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        #endregion

    }
}