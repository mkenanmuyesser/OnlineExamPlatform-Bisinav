using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.ASPxEditors;
using System.Web.Security;
using BiSinavProject.PL.Class.Helper;

namespace BiSinavProject.PL.Program.Sinav
{
    public partial class SoruTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "SoruTanimIslemleri.aspx";
        private const string PageHeader = "Soru Tanım İşlemleri";

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

        protected void ButtonGeriIleri_Click(object sender, EventArgs e)
        {
            using (var entity = new DBEntities())
            {
                entity.Configuration.AutoDetectChangesEnabled = false;

                string soru = TextBoxSoruNo.Text;
                int pSoruNo;
                int.TryParse(soru, out pSoruNo);

                string butonadi = ((ASPxButton)sender).ID;
                var sorulistesi = entity.Sorus.
                                         AsNoTracking().
                                         ToList().
                                         OrderBy(p => p.SoruKey);

                Soru _BESTEBES_SORU = null;
                switch (butonadi)
                {
                    case "ButtonGeri":
                        _BESTEBES_SORU = sorulistesi.LastOrDefault(p => Convert.ToInt32(p.SoruKey) < pSoruNo);
                        if (_BESTEBES_SORU == null)
                        {
                            _BESTEBES_SORU = sorulistesi.First();
                        }
                        break;
                    case "ButtonIleri":
                        _BESTEBES_SORU = sorulistesi.FirstOrDefault(p => Convert.ToInt32(p.SoruKey) > pSoruNo);
                        if (_BESTEBES_SORU == null)
                        {
                            _BESTEBES_SORU = sorulistesi.Last();
                        }
                        break;
                }

                if (_BESTEBES_SORU == null)
                    PageHelper.MessageBox(this, "Soru bulunamadı.");
                else
                {
                    int key = _BESTEBES_SORU.SoruKey;
                    Response.Redirect(string.Format(PageUrl + "?Key={0}", key));
                }
            }
        }

        protected void ButtonAra_Click(object sender, EventArgs e)
        {
            using (var entity = new DBEntities())
            {
                entity.Configuration.AutoDetectChangesEnabled = false;

                string soru = TextBoxSoruNo.Text;
                int pSoruNo;
                int.TryParse(soru, out pSoruNo);
                Soru _BESTEBES_SORU = entity.Sorus.
                                             AsNoTracking().
                                             SingleOrDefault(p => p.SoruKey == pSoruNo);
                if (_BESTEBES_SORU == null)
                    PageHelper.MessageBox(this, "Soru bulunamadı.");
                else
                {
                    int key = _BESTEBES_SORU.SoruKey;
                    Response.Redirect(string.Format(PageUrl + "?Key={0}", key));
                }
            }
        }

        protected void ButtonKaydetGuncelle_Click(object sender, EventArgs e)
        {
            #region validation

            if (string.IsNullOrEmpty(HtmlEditorSoru.Html))
            {
                PageHelper.MessageBox(this, "Lütfen soru alanını doldurunuz");
                return;
            }

            #endregion

            using (var service = new DBEntities())
            {
                Soru data;
                if (Key == 0)
                {
                    data = new Soru();
                }
                else
                {
                    data = service.Sorus.SingleOrDefault(p => p.SoruKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                if (ComboBoxDenemeAdi.SelectedIndex == 0)
                {
                    data.DenemeKey = null;
                    data.SadeceDeneme = null;                 
                }
                else
                {
                    data.DenemeKey = Convert.ToInt32(ComboBoxDenemeAdi.SelectedItem.Value);
                    data.SadeceDeneme = CheckBoxSadeceDeneme.Checked;
                }
                data.DersKey = Convert.ToInt32(ComboBoxDersAdi.SelectedItem.Value);
                data.DogruSik = ComboBoxDogruSik.SelectedItem.Text;
                data.SikSayisi = Convert.ToByte(ComboBoxSikSayisi.SelectedItem.Text);
                data.Soru1 = HtmlEditorSoru.Html;
                data.Aciklama = HtmlEditorAciklama.Html;
                data.Sira = Convert.ToInt32(SpinEditSiralama.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Sorus.Add(data);
                    Temizle();
                }
                else
                {
                    data.GuncelleKisiKey = userkey;
                    data.GuncelleTarih = DateTime.Now;
                }

                service.SaveChanges();
            }

            DataLoad();
        }

        protected void ButtonIptalTemizle_Click(object sender, EventArgs e)
        {
            var button = (ASPxButton)sender;
            switch (button.ID)
            {
                case "ButtonTemizle":
                    Temizle();
                    break;
                case "ButtonIptal":
                    Response.Redirect(PageUrl);
                    break;
            }
        }

        protected void ComboBoxAlanAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DersDenemeYukle();
        }

        protected void ComboBoxDenemeAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxDenemeAdi.SelectedIndex == 0)
            {
                CheckBoxSadeceDeneme.Checked = false;
                CheckBoxSadeceDeneme.Enabled = false;
            }
            else
            {
                CheckBoxSadeceDeneme.Checked = true;
                CheckBoxSadeceDeneme.Enabled = true;
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
                    List<Alan> listBESTEBES_ALAN = service.Alans.
                                                           AsNoTracking().
                                                           OrderBy(p => p.Sira).
                                                           ToList();

                    ComboBoxAlanAdi.DataSource = listBESTEBES_ALAN;
                    ComboBoxAlanAdi.DataBind();

                    List<string> listSiklar = new List<string>();
                    for (char c = 'A'; c <= 'E'; c++)
                    {
                        listSiklar.Add(c.ToString());
                    }
                    ComboBoxDogruSik.DataSource = listSiklar;
                    ComboBoxDogruSik.DataBind();
                    ComboBoxDogruSik.SelectedIndex = 0;

                    List<string> listSikSayisi = new List<string>();
                    for (int s = 5; s > 1; s--)
                    {
                        listSikSayisi.Add(s.ToString());
                    }
                    ComboBoxSikSayisi.DataSource = listSikSayisi;
                    ComboBoxSikSayisi.DataBind();
                    ComboBoxSikSayisi.SelectedIndex = 0;

                    if (listBESTEBES_ALAN.Any())
                    {
                        ComboBoxAlanAdi.SelectedIndex = 0;
                        DersDenemeYukle();
                    }
                }

                if (Key != 0 && !IsPostBack)
                {
                    var data = service.Sorus.
                                       AsNoTracking().
                                       Include("Der").
                                       Include("Der.Alan").
                                       SingleOrDefault(p => p.SoruKey == Key);
                    if (data != null)
                    {
                        ComboBoxAlanAdi.Items.FindByValue(data.Der.AlanKey).Selected = true;

                        DersDenemeYukle();

                        if (data.DenemeKey == null)
                        {
                            ComboBoxDenemeAdi.SelectedIndex = 0;
                            CheckBoxSadeceDeneme.Checked = false;
                            CheckBoxSadeceDeneme.Enabled = false;
                        }
                        else
                        {
                            ComboBoxDenemeAdi.Items.FindByValue(data.DenemeKey.Value).Selected = true;
                            CheckBoxSadeceDeneme.Enabled = true;
                            CheckBoxSadeceDeneme.Checked = data.SadeceDeneme.Value;
                        }

                        ComboBoxDersAdi.Items.FindByValue(data.DersKey).Selected = true;
                        ComboBoxDogruSik.Items.FindByText(data.DogruSik).Selected = true;
                        ComboBoxSikSayisi.Items.FindByText(data.SikSayisi.ToString()).Selected = true;
                        HtmlEditorSoru.Html = data.Soru1;
                        HtmlEditorAciklama.Html = data.Aciklama;
                        SpinEditSiralama.Value = data.Sira;
                        CheckBoxAktif.Checked = data.AktifMi;
                        TextBoxSoruNo.Text = data.SoruKey.ToString();

                        OnizlemeYukle();
                    }
                }
                else if (Key != 0)
                {
                    OnizlemeYukle();
                }

            }
        }

        private void Temizle()
        {
            HtmlEditorSoru.Html = string.Empty;
            HtmlEditorAciklama.Html = string.Empty;
            CheckBoxAktif.Checked = true;
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
                    listDeneme.Insert(0, new Deneme { DenemeKey = 0, Adi = "Seçimi temizle" });

                    ComboBoxDenemeAdi.DataSource = listDeneme;
                    ComboBoxDenemeAdi.DataBind();
                    ComboBoxDenemeAdi.SelectedIndex = 0;
                }
            }
        }

        private void OnizlemeYukle()
        {
            using (var service = new DBEntities())
            {
                var data = service.Sorus.
                                   AsNoTracking().
                                   Include("Der").
                                   Include("Der.Alan").
                                   SingleOrDefault(p => p.SoruKey == Key);
                if (data != null)
                    LabelSoru.Text = string.Format("Soru No : #{0} <p>{1}</p>", data.SoruKey, data.Soru1);
            }
        }

        #endregion
        
    }
}