using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Sinav
{
    public partial class DersTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "DersTanimIslemleri.aspx";
        private const string PageHeader = "Ders Tanım İşlemleri";
        const string UploadDirectory = "/Uploads/Program/";
        string DosyaUrl = null;

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

            if (Key == 0)
            {
                if (UploadControlDosya.PostedFile.ContentLength == 0)
                {
                    PageHelper.MessageBox(this, "Lütfen dosya yükleyiniz!");
                    return;
                }
            }

            if (UploadControlDosya.PostedFile.ContentLength != 0 && !UploadControlDosya.IsValid)
            {
                PageHelper.MessageBox(this, "Lütfen dosya yükleyiniz!");
                return;
            }

            #endregion

            using (var service = new DBEntities())
            {
                Der data;
                if (Key == 0)
                {
                    data = new Der();
                }
                else
                {
                    data = service.Ders.SingleOrDefault(p => p.DersKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.AlanKey = Convert.ToInt32(ComboBoxAlanAdi.SelectedItem.Value);
                data.Adi = TextBoxDersAdi.Text;
                data.Sira = Convert.ToInt32(SpinEditDersSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.Resim = DosyaUrl;

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Ders.Add(data);
                }
                else
                {
                    if (DosyaUrl != null)
                    {
                        //eskisini sil, yenisi zaten upload edildi
                        FileInfo dosya = new FileInfo(Server.MapPath(UploadDirectory + data.Resim));
                        if (dosya != null)
                        {
                            dosya.Delete();
                        }

                        //yeni dosyayı veritabanına kaydet
                        data.Resim = DosyaUrl;
                    }

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

        protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            DosyaUrl = SavePostedFile(e.UploadedFile);
            e.CallbackData = DosyaUrl;
        }

        protected void GridViewDersTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Der deleteddata = service.Ders.Single(p => p.DersKey == deletedkey);
                service.Ders.Remove(deleteddata);

                //eskisini sil, yenisi zaten upload edildi
                FileInfo dosya = new FileInfo(Server.MapPath(UploadDirectory + deleteddata.Resim));
                if (dosya != null)
                {
                    dosya.Delete();
                }

                try
                {
                    if (Key != 0)
                    {
                        GridViewDersTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewDersTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewDersTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewDersTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewDersTanim.GetRowValues(index, new string[] { "DersKey" }));
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
                    List<Alan> listBESTEBES_ALAN = service.Alans.
                                                           AsNoTracking().
                                                           OrderBy(p => p.Sira).
                                                           ToList();

                    ComboBoxAlanAdi.DataSource = listBESTEBES_ALAN;
                    ComboBoxAlanAdi.DataBind();
                }

                GridViewDersTanim.DataSource = null;
                GridViewDersTanim.DataSource = service.Ders.
                                                       AsNoTracking().
                                                       Include("Alan").
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewDersTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Der data = service.Ders.
                                       AsNoTracking().
                                       Single(p => p.DersKey == Key);

                    ComboBoxAlanAdi.Items.FindByValue(data.AlanKey).Selected = true;
                    TextBoxDersAdi.Text = data.Adi;
                    SpinEditDersSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        protected string CombinePath(string fileName)
        {
            return Server.MapPath(UploadDirectory + fileName);
        }

        protected string SavePostedFile(UploadedFile uploadedFile)
        {
            if (!uploadedFile.IsValid)
                return string.Empty;
            string fileName = Path.ChangeExtension(Path.GetRandomFileName(), uploadedFile.FileName.Split('.').Last());
            string fullFileName = CombinePath(fileName);
            uploadedFile.SaveAs(fullFileName, true);
            return fileName;
        }

        #endregion

    }
}