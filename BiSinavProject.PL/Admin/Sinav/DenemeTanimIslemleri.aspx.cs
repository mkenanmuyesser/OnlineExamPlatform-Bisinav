﻿using DevExpress.Web.ASPxGridView;
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
    public partial class DenemeTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "DenemeTanimIslemleri.aspx";
        private const string PageHeader = "Deneme Tanım İşlemleri";
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
                Deneme data;
                if (Key == 0)
                {
                    data = new Deneme();
                }
                else
                {
                    data = service.Denemes.SingleOrDefault(p => p.DenemeKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.AlanKey = Convert.ToInt32(ComboBoxAlanAdi.SelectedItem.Value);
                data.Adi = TextBoxDenemeAdi.Text;
                data.Sure = Convert.ToInt32(SpinEditSure.Value);
                data.Sira = Convert.ToInt32(SpinEditDersSiralama.Value);
                data.Fiyat= Convert.ToDecimal(SpinEditFiyat.Value);
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
                    service.Denemes.Add(data);
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

        protected void GridViewDenemeTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Deneme deleteddata = service.Denemes.Single(p => p.DenemeKey == deletedkey);
                service.Denemes.Remove(deleteddata);

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
                        GridViewDenemeTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewDenemeTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewDenemeTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewDenemeTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewDenemeTanim.GetRowValues(index, new string[] { "DenemeKey" }));
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

                GridViewDenemeTanim.DataSource = null;
                GridViewDenemeTanim.DataSource = service.Denemes.
                                                       AsNoTracking().
                                                       Include("Alan").
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewDenemeTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Deneme data = service.Denemes.
                                       AsNoTracking().
                                       Single(p => p.DenemeKey == Key);

                    ComboBoxAlanAdi.Items.FindByValue(data.AlanKey).Selected = true;
                    TextBoxDenemeAdi.Text = data.Adi;
                    SpinEditSure.Value = data.Sure;
                    SpinEditDersSiralama.Value = data.Sira;
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