using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;
using BiSinavProject.PL.Class.Business;
using DevExpress.Web.ASPxEditors;

namespace BiSinavProject.PL.Program.Yayin
{
    public partial class KayitTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "KayitTanimIslemleri.aspx";
        private const string PageHeader = "Kayıt Tanım İşlemleri";
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
            using (var service = new DBEntities())
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

                Kayit data;
                if (Key == 0)
                {
                    data = new Kayit();
                }
                else
                {
                    data = service.Kayits.
                                   Include("KayitKategori").
                                   SingleOrDefault(p => p.KayitKey == Key);

                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Baslik = TextBoxBaslik.Text;
                data.Aciklama = TextBoxAciklama.Text;
                data.KayitKategoriKey = Convert.ToInt32(ComboBoxKayitKategori.SelectedItem.Value);
                data.EmbedCode = TextBoxEmbedCode.Text;
                data.Sira = Convert.ToInt32(SpinEditKayitSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;

                if (Key == 0)
                {
                    data.Resim = DosyaUrl;

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Kayits.Add(data);
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

        protected void GridViewKayitTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Kayit deleteddata = service.Kayits.Single(p => p.KayitKey == deletedkey);
                service.Kayits.Remove(deleteddata);

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
                        GridViewKayitTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewKayitTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewKayitTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewKayitTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewKayitTanim.GetRowValues(index, new string[] { "KayitKey" }));
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
                    List<KayitKategori> listKayitKategori = service.KayitKategoris.
                                               AsNoTracking().
                                               OrderBy(p => p.Sira).
                                               ToList();

                    ComboBoxKayitKategori.DataSource = listKayitKategori;
                    ComboBoxKayitKategori.DataBind();
                }

                GridViewKayitTanim.DataSource = null;
                GridViewKayitTanim.DataSource = service.Kayits.
                                                       AsNoTracking().
                                                       Include("KayitKategori").
                                                       OrderBy(p => p.Sira).
                                                       ToList().
                                                       Select(p =>
                                                       new
                                                       {
                                                           p.KayitKey,
                                                           p.Baslik,
                                                           p.Aciklama,
                                                           KayitKategori = p.KayitKategori.Adi,
                                                           p.EmbedCode,
                                                           p.Sira,
                                                           p.Resim,
                                                           p.AktifMi
                                                       }).
                                                       ToList();
                GridViewKayitTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Kayit data = service.Kayits.
                                       AsNoTracking().
                                       Include("KayitKategori").
                                       Single(p => p.KayitKey == Key);

                    TextBoxBaslik.Text = data.Baslik;
                    TextBoxAciklama.Text = data.Aciklama;
                    ComboBoxKayitKategori.Items.FindByValue(data.KayitKategoriKey).Selected = true;
                    TextBoxEmbedCode.Text = data.EmbedCode;
                    SpinEditKayitSiralama.Value = data.Sira;
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