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
    public partial class KitapTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "KitapTanimIslemleri.aspx";
        private const string PageHeader = "Kitap Tanım İşlemleri";
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
                var dropDownEditKategoriler = DropDownEditKategoriler.FindControl("ListBoxKategoriler") as ASPxListBox;
                List<int> kategoriler = new List<int>();
                if (dropDownEditKategoriler != null)
                {
                    kategoriler = KodBs.KeyConverter(dropDownEditKategoriler.SelectedItems);
                }

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

                //mutlaka bir tane seçim yapılmalıdır.
                if (kategoriler.Count() == 0)
                {
                    PageHelper.MessageBox(this, "Lütfen en az bir kategori seçiniz");
                    return;
                }

                #endregion

                Kitap data;
                if (Key == 0)
                {
                    data = new Kitap();
                }
                else
                {
                    data = service.Kitaps.
                                   Include("KategoriKitaps").
                                   SingleOrDefault(p => p.KitapKey == Key);

                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }

                    for (int i = 0; i < data.KategoriKitaps.Count; i++)
                    {
                        data.KategoriKitaps.ElementAt(i).AktifMi = false;
                    }
                }

                data.Adi = TextBoxKitapAdi.Text;
                data.Link = TextBoxLink.Text;
                data.Sira = Convert.ToInt32(SpinEditKitapSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;

                foreach (int key in kategoriler)
                {
                    KategoriKitap kategoriKitap = new KategoriKitap
                    {
                        Kitap = data,
                        KategoriKey = key,
                        KitapKey = data.KitapKey,

                        KayitKisiKey = userkey,
                        KayitTarih = DateTime.Now,
                        GuncelleKisiKey = userkey,
                        GuncelleTarih = DateTime.Now,
                        AktifMi = true,
                    };
                    data.KategoriKitaps.Add(kategoriKitap);
                }

                if (Key == 0)
                {
                    data.Resim = DosyaUrl;

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Kitaps.Add(data);
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

        protected void GridViewKitapTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Kitap deleteddata = service.Kitaps.Single(p => p.KitapKey == deletedkey);
                service.Kitaps.Remove(deleteddata);

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
                        GridViewKitapTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewKitapTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewKitapTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewKitapTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewKitapTanim.GetRowValues(index, new string[] { "KitapKey" }));
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
                    List<Kategori> listKategori = service.Kategoris.
                                                      AsNoTracking().
                                                      OrderBy(p => p.Sira).
                                                      ToList();
                    listKategori.Insert(0, new Kategori
                    {
                        KategoriKey = 0,
                        Adi = "(Hepsini seç)"
                    });
                    var dropDownEditKategoriler = DropDownEditKategoriler.FindControl("ListBoxKategoriler") as ASPxListBox;
                    dropDownEditKategoriler.DataSource = listKategori;
                    dropDownEditKategoriler.DataBind();
                }

                GridViewKitapTanim.DataSource = null;
                GridViewKitapTanim.DataSource = service.Kitaps.
                                                       AsNoTracking().
                                                       Include("KategoriKitaps").
                                                       Include("KategoriKitaps.Kategori").
                                                       OrderBy(p => p.Sira).
                                                       ToList().
                                                       Select(p =>
                                                       new
                                                       {
                                                           p.KitapKey,
                                                           p.Adi,
                                                           Kategoriler = p.KategoriKitaps.Count() > 0 ?
                                                           KodBs.StringConverter(p.KategoriKitaps.Where(x => x.AktifMi).Select(x => x.Kategori.Adi)) :
                                                           "",
                                                           p.Link,
                                                           p.Sira,
                                                           p.Resim,
                                                           p.AktifMi
                                                       }).
                                                       ToList();
                GridViewKitapTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Kitap data = service.Kitaps.
                                       AsNoTracking().
                                       Include("KategoriKitaps").
                                       Include("KategoriKitaps.Kategori").
                                       Single(p => p.KitapKey == Key);

                    if (data.KategoriKitaps.Count() > 0)
                    {
                        DropDownEditKategoriler.Text = KodBs.StringConverter(data.KategoriKitaps.Where(x => x.AktifMi).Select(x => x.Kategori.Adi));
                    }

                    TextBoxKitapAdi.Text = data.Adi;
                    TextBoxLink.Text = data.Link;
                    SpinEditKitapSiralama.Value = data.Sira;
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