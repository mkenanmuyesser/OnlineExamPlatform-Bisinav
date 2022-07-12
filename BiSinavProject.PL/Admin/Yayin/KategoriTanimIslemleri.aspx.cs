using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Data;
using System;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Yayin
{
    public partial class KategoriTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "KategoriTanimIslemleri.aspx";
        private const string PageHeader = "Kategori Tanım İşlemleri";

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
                Kategori data;
                if (Key == 0)
                {
                    data = new Kategori();
                }
                else
                {
                    data = service.Kategoris.SingleOrDefault(p => p.KategoriKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Adi = TextBoxKategoriAdi.Text;
                data.Sira = Convert.ToInt32(SpinEditKategoriSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Kategoris.Add(data);
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

        protected void GridViewKategoriTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Kategori deleteddata = service.Kategoris.Single(p => p.KategoriKey == deletedkey);
                service.Kategoris.Remove(deleteddata);

                try
                {
                    if (Key != 0)
                    {
                        GridViewKategoriTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewKategoriTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewKategoriTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewKategoriTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewKategoriTanim.GetRowValues(index, new string[] { "KategoriKey" }));
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
                GridViewKategoriTanim.DataSource = null;
                GridViewKategoriTanim.DataSource = service.Kategoris.
                                                       AsNoTracking().
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewKategoriTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Kategori data = service.Kategoris.
                                        AsNoTracking().
                                        Single(p => p.KategoriKey == Key);

                    TextBoxKategoriAdi.Text = data.Adi;
                    SpinEditKategoriSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        #endregion

    }
}