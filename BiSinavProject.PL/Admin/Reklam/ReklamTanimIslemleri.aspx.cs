using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.IO;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.Reklam
{
    public partial class ReklamTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "ReklamTanimIslemleri.aspx";
        private const string PageHeader = "Reklam Tanım İşlemleri";
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
                Tanitim data;
                if (Key == 0)
                {
                    data = new Tanitim();
                }
                else
                {
                    data = service.Tanitims.SingleOrDefault(p => p.TanitimKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.BolgeTipKey = Convert.ToInt32(ComboBoxBolgeTipAdi.SelectedItem.Value);
                data.Aciklama = TextBoxAciklama.Text;
                data.BaslangicTarih = Convert.ToDateTime(DateEditBaslangicTarih.Value);
                data.BitisTarih = Convert.ToDateTime(DateEditBitisTarih.Value);
                data.Link = TextBoxLink.Text;
                data.Sira = Convert.ToInt32(SpinEditKursSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;


                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.Resim = DosyaUrl;

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Tanitims.Add(data);
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

        protected void GridViewTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Tanitim deleteddata = service.Tanitims.Single(p => p.TanitimKey == deletedkey);
                service.Tanitims.Remove(deleteddata);

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
                        GridViewTanim.JSProperties["cpErrorMessage"] = true;
                    }
                    else
                    {
                        service.SaveChanges();
                        GridViewTanim.JSProperties["cpErrorMessage"] = false;
                    }
                }
                catch
                {
                    GridViewTanim.JSProperties["cpErrorMessage"] = true;
                }
            }

            DataLoad();
            e.Cancel = true;
        }

        protected void GridViewTanim_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewTanim.GetRowValues(index, new string[] { "TanitimKey" }));
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
                    var listBolge = service.BolgeTips.
                                            AsNoTracking().
                                            OrderBy(p => p.Sira).
                                            ToList().
                                            Select(p =>
                                            new
                                            {
                                                p.BolgeTipKey,
                                                Aciklama = p.BolgeTipAdi + "-" + p.Aciklama,
                                            }).
                                            ToList();

                    ComboBoxBolgeTipAdi.DataSource = listBolge;
                    ComboBoxBolgeTipAdi.DataBind();
                }

                GridViewTanim.DataSource = null;
                GridViewTanim.DataSource = service.Tanitims.
                                                   AsNoTracking().
                                                   Include("BolgeTip").
                                                   OrderByDescending(p => p.BaslangicTarih).
                                                   ToList();
                GridViewTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Tanitim data = service.Tanitims.
                                       AsNoTracking().
                                       Single(p => p.TanitimKey == Key);

                    ComboBoxBolgeTipAdi.Items.FindByValue(data.BolgeTipKey).Selected = true;
                    TextBoxAciklama.Text = data.Aciklama;
                    DateEditBaslangicTarih.Value = data.BaslangicTarih;
                    DateEditBitisTarih.Value = data.BitisTarih;
                    TextBoxLink.Text = data.Link;
                    SpinEditKursSiralama.Value = data.Sira;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }

        protected string CombinePath(string fileName)
        {
            return Server.MapPath(UploadDirectory+ fileName);
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