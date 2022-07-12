using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.ASPxEditors;
using System.Web.Security;
using BiSinavProject.PL.Class.Helper;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using BiSinavProject.PL.Class.CustomType;
using System.Data.OleDb;
using DevExpress.Web.ASPxGridView;

namespace BiSinavProject.PL.Program.Sinav
{
    public partial class HizliSoruTanimIslemleri : System.Web.UI.Page
    {
        #region Properties      

        private const string PageUrl = "HizliSoruTanimIslemleri.aspx";
        private const string PageHeader = "Hızlı Soru Tanım İşlemleri";
        const string UploadDirectory = "/Uploads/Program/";
        string CevaplarDosyaUrl = null;
        List<string> SorularDosyaUrl
        {
            get
            {
                List<string> dizi = null;
                if (Session["SorularDosyaUrl"] == null)
                {
                    dizi = new List<string>();
                }
                else
                {
                    dizi = (List<string>)Session["SorularDosyaUrl"];
                }

                return dizi;
            }
            set
            {
                Session["SorularDosyaUrl"] = value;
            }
        }

        List<string> AciklamalarDosyaUrl
        {
            get
            {
                List<string> dizi = null;
                if (Session["AciklamalarDosyaUrl"] == null)
                {
                    dizi = new List<string>();
                }
                else
                {
                    dizi = (List<string>)Session["AciklamalarDosyaUrl"];
                }

                return dizi;
            }
            set
            {
                Session["AciklamalarDosyaUrl"] = value;
            }
        }

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

        protected void ButtonOlustur_Click(object sender, EventArgs e)
        {
            using (var service = new DBEntities())
            {
                #region validation

                if (UploadControlCevaplar.PostedFile.ContentLength != 0 && !UploadControlCevaplar.IsValid)
                {
                    PageHelper.MessageBox(this, "Lütfen dosya yükleyiniz!");
                    return;
                }

                if (SorularDosyaUrl.Count == 0)
                {
                    PageHelper.MessageBox(this, "Lütfen dosya yükleyiniz!");
                    return;
                }

                if (AciklamalarDosyaUrl.Count == 0)
                {
                    PageHelper.MessageBox(this, "Lütfen dosya yükleyiniz!");
                    return;
                }

                #endregion                

                //cevapları oku, soruları oku, açıklamaları oku                
                List<ExcelType> excelListe = new List<ExcelType>();
                using (OleDbConnection cnn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                                                   CombinePath(CevaplarDosyaUrl)
                                                                   + ";Extended Properties='Excel 12.0 Xml;HDR=YES';"))
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [TabloCevaplar$]", cnn);
                    cnn.Open();

                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ExcelType item = new ExcelType
                        {
                            SoruNo = Convert.ToInt32(rdr[0].ToString()),
                            DogruSik = rdr[1].ToString(),
                        };
                        excelListe.Add(item);
                    }
                    cnn.Close();
                }

                //eğer sayılar tutmuyorsa burada işlemi durdur
                if (excelListe.Count == SorularDosyaUrl.Count && excelListe.Count == AciklamalarDosyaUrl.Count && SorularDosyaUrl.Count == AciklamalarDosyaUrl.Count)
                {
                    MembershipUser user = Membership.GetUser(true);
                    Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                    int sira = 0;
                    foreach (ExcelType item in excelListe)
                    {
                        Soru soru = new Soru
                        {
                            DersKey = Convert.ToInt32(ComboBoxDersAdi.SelectedItem.Value),
                            DenemeKey = Convert.ToInt32(ComboBoxDenemeAdi.SelectedItem.Value),
                            SadeceDeneme = true,
                            DogruSik = item.DogruSik,
                            SikSayisi = Convert.ToByte(ComboBoxSikSayisi.SelectedItem.Text),
                            Soru1 = @"<img src='/Uploads/Program/" + SorularDosyaUrl[sira] + "' alt='' />",
                            Aciklama = @"<img src='/Uploads/Program/" + AciklamalarDosyaUrl[sira] + "' alt='' />",

                            KayitKisiKey = userkey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = userkey,
                            GuncelleTarih = DateTime.Now,
                            Sira = ++sira,
                            AktifMi = true,
                        };

                        service.Sorus.Add(soru);
                    }

                    service.SaveChanges();
                }
                else
                {
                    PageHelper.MessageBox(this, "Yüklenen dosya sayıları ve excel dosyası birbiriyle uyuşmuyor.");
                    return;
                }
            }

            GridDoldur();
        }

        protected void ButtonHepsiniSil_Click(object sender, EventArgs e)
        {
            using (var service = new DBEntities())
            {
                try
                {
                    List<Soru> sorular = GridDoldur();

                    foreach (Soru soru in sorular)
                    {
                        try
                        {
                            //eskilerini sil
                            string soruText = soru.Soru1.Replace("<img src='/Uploads/Program/", "");
                            soruText = soruText.Split('\'')[0];

                            FileInfo dosyaSoru = new FileInfo(Server.MapPath(UploadDirectory + soruText));
                            if (dosyaSoru != null)
                            {
                                dosyaSoru.Delete();
                            }

                            string aciklamaText = soru.Soru1.Replace("<img src='/Uploads/Program/", "");
                            aciklamaText = aciklamaText.Split('\'')[0];

                            FileInfo dosyaAciklama = new FileInfo(Server.MapPath(UploadDirectory + aciklamaText));
                            if (dosyaAciklama != null)
                            {
                                dosyaAciklama.Delete();
                            }
                        }
                        catch
                        {
                        }

                        Soru silinecek = service.Sorus.SingleOrDefault(p => p.SoruKey == soru.SoruKey);
                        service.Sorus.Remove(silinecek);
                    }

                    service.SaveChanges();

                    GridDoldur();
                }
                catch (Exception ex)
                {
                    PageHelper.MessageBox(this, "Silme işlemi sırasında bir hata oluştu.</br>" + ex.Message);
                }
            }
        }

        protected void ButtonIptal_Click(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl);
        }

        protected void ComboBoxAlanAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DersDenemeYukle();

            GridDoldur();
        }

        protected void ComboBoxDenemeAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDoldur();
        }

        protected void UploadControlCevaplar_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            CevaplarDosyaUrl = SavePostedFile(e.UploadedFile);
            e.CallbackData = CevaplarDosyaUrl;
        }

        protected void UploadControlSorular_FilesUploadComplete(object sender, FilesUploadCompleteEventArgs e)
        {
            if (UploadControlSorular.UploadedFiles.Count() == 1 && UploadControlSorular.UploadedFiles[0].ContentLength == 0)
            {
                return;
            }

            List<string> data = new List<string>();

            foreach (var file in UploadControlSorular.UploadedFiles)
            {
                string dosyaUrl = SavePostedFile(file);
                data.Add(dosyaUrl);
            }

            SorularDosyaUrl = data;
            e.CallbackData = "";
        }

        protected void UploadControlAciklamalar_FilesUploadComplete(object sender, FilesUploadCompleteEventArgs e)
        {
            if (UploadControlAciklamalar.UploadedFiles.Count() == 1 && UploadControlAciklamalar.UploadedFiles[0].ContentLength == 0)
            {
                return;
            }

            List<string> data = new List<string>();

            foreach (var file in UploadControlAciklamalar.UploadedFiles)
            {
                string dosyaUrl = SavePostedFile(file);
                data.Add(dosyaUrl);
            }

            AciklamalarDosyaUrl = data;
            e.CallbackData = "";
        }

        protected void GridViewSorular_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            int index = e.VisibleIndex;
            int key = Convert.ToInt32(GridViewSorular.GetRowValues(index, new string[] { "SoruKey" }));
            ASPxGridView.RedirectOnCallback(string.Format("SoruTanimIslemleri.aspx?Key={0}", key));
        }

        #endregion

        #region Methods

        private void SetInitials()
        {
            LabelBaslik.Text = PageHeader;

            Session["SorularDosyaUrl"] = null;
            Session["AciklamalarDosyaUrl"] = null;
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

                    if (listBESTEBES_ALAN.Any())
                    {
                        ComboBoxAlanAdi.SelectedIndex = 0;
                        DersDenemeYukle();
                    }

                    List<string> listSikSayisi = new List<string>();
                    for (int s = 5; s > 1; s--)
                    {
                        listSikSayisi.Add(s.ToString());
                    }
                    ComboBoxSikSayisi.DataSource = listSikSayisi;
                    ComboBoxSikSayisi.DataBind();
                    ComboBoxSikSayisi.SelectedIndex = 0;
                }
            }

            GridDoldur();
        }

        private List<Soru> GridDoldur()
        {
            List<Soru> sorular = new List<Soru>();

            using (var service = new DBEntities())
            {
                GridViewSorular.DataSource = null;

                if (ComboBoxDersAdi.SelectedItem != null && ComboBoxDenemeAdi.SelectedItem != null)
                {
                    int DersKey = Convert.ToInt32(ComboBoxDersAdi.SelectedItem.Value);
                    int DenemeKey = Convert.ToInt32(ComboBoxDenemeAdi.SelectedItem.Value);

                    sorular = service.Sorus.
                                                                         AsNoTracking().
                                                                         Where(p => p.DersKey == DersKey &&
                                                                                    p.DenemeKey == DenemeKey).
                                                                         OrderBy(p => p.Sira).
                                                                         ToList();

                    GridViewSorular.DataSource = sorular;
                    GridViewSorular.DataBind();
                }
            }

            return sorular;
        }

        private void DersDenemeYukle()
        {
            if (ComboBoxAlanAdi.SelectedIndex != -1)
            {
                using (var service = new DBEntities())
                {
                    int alankey = Convert.ToInt32(ComboBoxAlanAdi.SelectedItem.Value);
                    List<Der> listBESTEBES_DERS = service.Ders.
                                                          AsNoTracking().
                                                          Where(p => p.AlanKey == alankey).
                                                          OrderBy(p => p.Sira).
                                                          ToList();

                    ComboBoxDersAdi.DataSource = listBESTEBES_DERS;
                    ComboBoxDersAdi.DataBind();
                    ComboBoxDersAdi.SelectedIndex = 0;

                    List<Deneme> listDeneme = service.Denemes.
                                                      AsNoTracking().
                                                      Where(p => p.AlanKey == alankey).
                                                      OrderBy(p => p.Sira).
                                                      ToList();

                    ComboBoxDenemeAdi.DataSource = listDeneme;
                    ComboBoxDenemeAdi.DataBind();
                    ComboBoxDenemeAdi.SelectedIndex = 0;
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