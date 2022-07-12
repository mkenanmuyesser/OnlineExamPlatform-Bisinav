using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Yayin
{
    public partial class EDergiTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "EDergiTanimIslemleri.aspx";
        private const string PageHeader = "E-Dergi Tanım İşlemleri";
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
                EDergi data;
                if (Key == 0)
                {
                    data = new EDergi();
                }
                else
                {
                    data = service.EDergis.SingleOrDefault(p => p.EDergiKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Adi = TextBoxEDergiAdi.Text;
                data.Sira = Convert.ToInt32(SpinEditKitapSiralama.Value);
                data.Fiyat = Convert.ToDecimal(SpinEditFiyat.Value);
                data.Link = TextBoxLink.Text;
                data.YeniMi = CheckBoxYeni.Checked;
                data.DemoMu = CheckBoxDemo.Checked;
                data.HediyeMi = CheckBoxHediye.Checked;
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.Resim = DosyaUrl;

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.EDergis.Add(data);
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

        protected void GridViewEDergiTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                EDergi deleteddata = service.EDergis.Single(p => p.EDergiKey == deletedkey);
                service.EDergis.Remove(deleteddata);

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
                        GridViewEDergiTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewEDergiTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewEDergiTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewEDergiTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewEDergiTanim.GetRowValues(index, new string[] { "EDergiKey" }));
            switch (e.ButtonID)
            {
                case "ButtonGoster":
                    ASPxGridView.RedirectOnCallback(string.Format("{0}?Key={1}", "EDergiGoster.aspx", key));
                    break;
                case "ButtonDetay":
                    ASPxGridView.RedirectOnCallback(string.Format("{0}?Key={1}", PageUrl, key));
                    break;
            }
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

                }

                GridViewEDergiTanim.DataSource = null;
                GridViewEDergiTanim.DataSource = service.EDergis.
                                                       AsNoTracking().
                                                       Include("EDergiSayfas").
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewEDergiTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    EDergi data = service.EDergis.
                                       AsNoTracking().
                                       Single(p => p.EDergiKey == Key);

                    TextBoxEDergiAdi.Text = data.Adi;
                    SpinEditKitapSiralama.Value = data.Sira;
                    SpinEditFiyat.Value = data.Fiyat;
                    TextBoxLink.Text = data.Link;
                    CheckBoxYeni.Checked = data.YeniMi;
                    CheckBoxDemo.Checked = data.DemoMu;
                    CheckBoxHediye.Checked = data.HediyeMi;
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