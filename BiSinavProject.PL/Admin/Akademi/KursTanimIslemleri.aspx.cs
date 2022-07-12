using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;
using DevExpress.Web.ASPxEditors;
using BiSinavProject.PL.Class.Business;

namespace BiSinavProject.PL.Program.Akademi
{
    public partial class KursTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "KursTanimIslemleri.aspx";
        private const string PageHeader = "Kurs Tanım İşlemleri";
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
                var dropDownEditIller = DropDownEditIller.FindControl("ListBoxIller") as ASPxListBox;
                List<int> iller = new List<int>();
                if (dropDownEditIller != null)
                {
                    iller = KodBs.KeyConverter(dropDownEditIller.SelectedItems);
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
                if (iller.Count() == 0)
                {
                    PageHelper.MessageBox(this, "Lütfen en az bir il seçiniz");
                    return;
                }

                #endregion

                Kur data;
                if (Key == 0)
                {
                    data = new Kur();
                }
                else
                {
                    data = service.Kurs.
                                   Include("IlKurs").
                                   SingleOrDefault(p => p.KursKey == Key);

                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }

                    for (int i = 0; i < data.IlKurs.Count; i++)
                    {
                        data.IlKurs.ElementAt(i).AktifMi = false;
                    }
                }

                data.Adi = TextBoxKursAdi.Text;
                data.Link = TextBoxLink.Text;
                data.Sira = Convert.ToInt32(SpinEditKursSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;

                foreach (int key in iller)
                {
                    IlKur ilKur = new IlKur
                    {
                        Kur = data,
                        IlKey = key,
                        KursKey = data.KursKey,

                        KayitKisiKey = userkey,
                        KayitTarih = DateTime.Now,
                        GuncelleKisiKey = userkey,
                        GuncelleTarih = DateTime.Now,
                        AktifMi = true,
                    };
                    data.IlKurs.Add(ilKur);
                }

                if (Key == 0)
                {
                    data.Resim = DosyaUrl;

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Kurs.Add(data);
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

        protected void GridViewKursTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Kur deleteddata = service.Kurs.Single(p => p.KursKey == deletedkey);
                service.Kurs.Remove(deleteddata);

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
                        GridViewKursTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewKursTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewKursTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewKursTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewKursTanim.GetRowValues(index, new string[] { "KursKey" }));
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
                    List<Il> listIl = service.Ils.
                                              AsNoTracking().
                                              OrderBy(p => p.Sira).
                                              ToList();
                    listIl.Insert(0, new Il
                    {
                        IlKey = 0,
                        Adi = "(Hepsini seç)"
                    });
                    var dropDownEditIller = DropDownEditIller.FindControl("ListBoxIller") as ASPxListBox;
                    dropDownEditIller.DataSource = listIl;
                    dropDownEditIller.DataBind();
                }

                GridViewKursTanim.DataSource = null;
                GridViewKursTanim.DataSource = service.Kurs.
                                                       AsNoTracking().
                                                       Include("IlKurs").
                                                       Include("IlKurs.Il").
                                                       OrderBy(p => p.Sira).
                                                       ToList().
                                                       Select(p =>
                                                       new
                                                       {
                                                           p.KursKey,
                                                           p.Adi,
                                                           Iller = p.IlKurs.Count() > 0 ?
                                                           KodBs.StringConverter(p.IlKurs.Where(x => x.AktifMi).Select(x => x.Il.Adi)) :
                                                           "",
                                                           p.Link,
                                                           p.Sira,
                                                           p.Resim,
                                                           p.AktifMi
                                                       }).
                                                       ToList(); 
                GridViewKursTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Kur data = service.Kurs.
                                       AsNoTracking().
                                       Include("IlKurs").
                                       Include("IlKurs.Il").
                                       Single(p => p.KursKey == Key);

                    if (data.IlKurs.Count() > 0)
                    {
                        DropDownEditIller.Text = KodBs.StringConverter(data.IlKurs.Where(x => x.AktifMi).Select(x => x.Il.Adi));
                    }

                    TextBoxKursAdi.Text = data.Adi;
                    TextBoxLink.Text = data.Link;
                    SpinEditKursSiralama.Value = data.Sira;
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