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
    public partial class EDergiSayfaTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "EDergiSayfaTanimIslemleri.aspx";
        private const string PageHeader = "E-Dergi Sayfa Tanım İşlemleri";
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
                EDergiSayfa data;
                if (Key == 0)
                {
                    data = new EDergiSayfa();
                }
                else
                {
                    data = service.EDergiSayfas.SingleOrDefault(p => p.EDergiSayfaKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.EDergiKey = Convert.ToInt32(ComboBoxEDergiAdi.SelectedItem.Value);
                data.Sira = Convert.ToInt32(SpinEditKitapSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.Resim = DosyaUrl;

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.EDergiSayfas.Add(data);
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

            DataLoad();
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

        protected void GridViewEDergiSayfaTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                EDergiSayfa deleteddata = service.EDergiSayfas.Single(p => p.EDergiSayfaKey == deletedkey);
                service.EDergiSayfas.Remove(deleteddata);

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
                        GridViewEDergiSayfaTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewEDergiSayfaTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewEDergiSayfaTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewEDergiSayfaTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewEDergiSayfaTanim.GetRowValues(index, new string[] { "EDergiSayfaKey" }));
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
                    List<EDergi> listEDergi = service.EDergis.
                                              AsNoTracking().
                                              OrderBy(p => p.Sira).
                                              ToList();

                    ComboBoxEDergiAdi.DataSource = listEDergi;
                    ComboBoxEDergiAdi.DataBind();
                }

                GridViewEDergiSayfaTanim.DataSource = null;
                GridViewEDergiSayfaTanim.DataSource = service.EDergiSayfas.
                                                       AsNoTracking().
                                                       Include("EDergi").
                                                       OrderBy(p => p.Sira).
                                                       ToList();
                GridViewEDergiSayfaTanim.DataBind();

                #region gruplama

                GridViewEDergiSayfaTanim.GroupBy(GridViewEDergiSayfaTanim.Columns["E-Dergi Adı"]);

                #endregion

                if (Key != 0 && !IsPostBack)
                {
                    EDergiSayfa data = service.EDergiSayfas.
                                       AsNoTracking().
                                       Single(p => p.EDergiSayfaKey == Key);

                    ComboBoxEDergiAdi.Items.FindByValue(data.EDergiKey).Selected = true;
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