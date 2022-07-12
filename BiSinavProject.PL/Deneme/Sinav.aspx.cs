using DevExpress.Web.ASPxEditors;
using BiSinavProject.PL.Class.Business;
using BiSinavProject.PL.Class.CustomType;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;

namespace BiSinavProject.PL.SinavDeneme
{
    public partial class Sinav : System.Web.UI.Page
    {
        #region Properties

        private const string PageUrl = "Sinav.aspx";

        List<DersSonucType> dersSonuclari = new List<DersSonucType>();

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            SetInitials();
        }

        protected void ButtonSinavaBasla_Click(object sender, EventArgs e)
        {
            HtmlButton btn = sender as HtmlButton;

            switch (btn.ID)
            {
                case "ButtonSinavaBasla":
                    DenemeBs.SoruOlustur(SessionHelper.DenemeData.AktifDenemeKey, SinavDurumEnum.SinavYeni);
                    break;
                case "ButtonEskiSinavaDevamEt":
                    DenemeBs.SoruOlustur(SessionHelper.DenemeData.AktifDenemeKey, SinavDurumEnum.SinavDevam);
                    break;
            }

            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ModelWelcomeClose(); ", true);
            Response.Redirect(PageUrl);
        }

        protected void LinkButtonSik_Click(object sender, EventArgs e)
        {
            var aktifSorular = SessionHelper.DenemeData.Sorular.Where(p => p.DersKey == SessionHelper.DenemeData.AktifDersKey);
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.DenemeData.AktifSoruNo);

            LinkButton lnkButton = sender as LinkButton;
            string[] parametreler = lnkButton.CommandArgument.ToString().Split('_');
            string isaretlenenSik = parametreler[0];
            string soruNo = parametreler[1];

            if (SessionHelper.DenemeData.SinavMode == SinavModeEnum.SinavAktif && soruNo == aktifSoru.SoruNo.ToString())
            {
                if (string.IsNullOrEmpty(isaretlenenSik))
                    aktifSoru.SoruIsaretlenenSik = null;
                else
                    aktifSoru.SoruIsaretlenenSik = isaretlenenSik;
            }

            AktifSoruGetir();
        }

        protected void ButtonDers_Click(object sender, EventArgs e)
        {
            HtmlButton btn = sender as HtmlButton;
            int aktifDersKey = Convert.ToInt32(btn.Attributes["name"]);
            SessionHelper.DenemeData.AktifDersKey = aktifDersKey;
            SessionHelper.DenemeData.AktifSoruNo = 1;

            if (SinavSureKontrol())
            {
                DenemeKullaniciSoruKaydet();
                DenemeKullaniciKaydet();
                SessionHelper.DenemeData.IslemZamani = DateTime.Now;
            }

            AktifSoruGetir();
        }

        protected void ButtonGeri_Click(object sender, EventArgs e)
        {
            int aktifSoruNo = SessionHelper.DenemeData.AktifSoruNo;
            if (aktifSoruNo > 1)
            {
                aktifSoruNo = (aktifSoruNo - 1);
            }
            else
            {
                aktifSoruNo = 1;
            }
            SessionHelper.DenemeData.AktifSoruNo = aktifSoruNo;

            if (SinavSureKontrol())
            {
                DenemeKullaniciSoruKaydet();
                DenemeKullaniciKaydet();
                SessionHelper.DenemeData.IslemZamani = DateTime.Now;
            }

            AktifSoruGetir();
        }

        protected void ButtonIleri_Click(object sender, EventArgs e)
        {
            int aktifSoruNo = SessionHelper.DenemeData.AktifSoruNo;
            if (aktifSoruNo < SessionHelper.DenemeData.Sorular.Count())
            {
                aktifSoruNo = (aktifSoruNo + 1);
            }
            else
            {
                aktifSoruNo = SessionHelper.DenemeData.Sorular.Count();
            }
            SessionHelper.DenemeData.AktifSoruNo = aktifSoruNo;

            if (SinavSureKontrol())
            {
                DenemeKullaniciSoruKaydet();
                DenemeKullaniciKaydet();
                SessionHelper.DenemeData.IslemZamani = DateTime.Now;
            }

            AktifSoruGetir();
        }

        protected void ButtonSoruNo_Click(object sender, EventArgs e)
        {
            HtmlButton btn = sender as HtmlButton;
            int aktifSoruNo = Convert.ToInt32(btn.InnerText);

            SessionHelper.DenemeData.AktifSoruNo = aktifSoruNo;
            if (SinavSureKontrol())
            {
                DenemeKullaniciSoruKaydet();
                DenemeKullaniciKaydet();
                SessionHelper.DenemeData.IslemZamani = DateTime.Now;
            }

            AktifSoruGetir();
        }

        protected void ButtonTemizle_Click(object sender, EventArgs e)
        {
            if (SessionHelper.DenemeData.SinavMode == SinavModeEnum.SinavAktif)
            {
                var aktifSorular = SessionHelper.DenemeData.Sorular.Where(p => p.DersKey == SessionHelper.DenemeData.AktifDersKey);
                var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.DenemeData.AktifSoruNo);
                aktifSoru.SoruIsaretlenenSik = null;
            }

            if (SinavSureKontrol())
            {
                DenemeKullaniciSoruKaydet();
                DenemeKullaniciKaydet();
                SessionHelper.DenemeData.IslemZamani = DateTime.Now;
            }

            AktifSoruGetir();
        }

        protected void ButtonSinaviBitir_Click(object sender, EventArgs e)
        {
            SinavBitir();
        }

        protected void ButtonIncele_Click(object sender, EventArgs e)
        {
            SessionHelper.DenemeData.AktifDersKey = SessionHelper.DenemeData.Sorular.First().DersKey;
            SessionHelper.DenemeData.AktifSoruNo = 1;
            SessionHelper.DenemeData.SinavMode = SinavModeEnum.Inceleme;

            SessionHelper.DenemeData = SessionHelper.DenemeData;

            Response.Redirect(PageUrl);
        }

        protected void ButtonKaydet_Click(object sender, EventArgs e)
        {
            var membershipUser = Membership.GetUser(true);

            if (membershipUser == null)
            {
                PageHelper.MessageBox(this, "Lütfen giriş yapınız.");
                return;
            }
            else
            {
                Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                if (!SessionHelper.DenemeData.SinavKayit)
                {
                    SessionHelper.DenemeData.SinavKayit = true;
                    SessionHelper.DenemeData = SessionHelper.DenemeData;
                    //kayıt işlemleri
                    using (var service = new DBEntities())
                    {
                        foreach (var dersSonuc in dersSonuclari)
                        {
                            //her ders için ayrı ayrı istatistik verebilmek amacıyla ayrı ayrı sayiları tutuyoruz
                            SonucDetay sonuc = new SonucDetay
                            {
                                DenemeKullaniciKey = SessionHelper.DenemeData.DenemeKullaniciKey,
                                UserId = userId,
                                DersKey = dersSonuc.DersKey,
                                DogruSayisi = dersSonuc.Dogru,
                                YanlisSayisi = dersSonuc.Yanlis,
                                BosSayisi = dersSonuc.Bos,
                                KullanilanSure = Convert.ToInt32(SessionHelper.DenemeData.IslemZamani.Second),

                                KayitKisiKey = userId,
                                KayitTarih = DateTime.Now,
                                GuncelleKisiKey = userId,
                                GuncelleTarih = DateTime.Now,
                                AktifMi = true,
                            };
                            service.SonucDetays.Add(sonuc);
                        }

                        service.SaveChanges();
                    }

                    PageHelper.MessageBox(this, "Kayıt işleminiz gerçekleşmiştir.");
                }
                else
                {
                    PageHelper.MessageBox(this, "Kayıt işleminiz daha önce yapılmıştır.");
                    return;
                }
            }

            KayitKontrol();
        }

        protected void ButtonSinavCikis_Click(object sender, EventArgs e)
        {
            SessionHelper.DenemeData = null;
            Response.Redirect("~/DenemeSinav.aspx");
        }

        protected void RepeaterSiklar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    SoruType soruType = (SoruType)e.Item.DataItem;
                    LinkButton buttonSoruNo = (LinkButton)e.Item.FindControl("ButtonSoruNo");
                    HtmlGenericControl labelSoruDurum = (HtmlGenericControl)e.Item.FindControl("LabelSoruDurum");

                    buttonSoruNo.Text = soruType.SoruNo.ToString();
                    buttonSoruNo.CommandArgument = soruType.SoruNo.ToString();
                    buttonSoruNo.CommandName = "SoruNoCommand";

                    for (int i = 1; i <= soruType.SoruSikSayisi; i++)
                    {
                        LinkButton lnkButton = new LinkButton();
                        switch (i)
                        {
                            case 1:
                                lnkButton = (LinkButton)e.Item.FindControl("LinkButtonA");
                                lnkButton.CommandArgument = "A_" + soruType.SoruNo.ToString();
                                lnkButton.Visible = true;
                                break;
                            case 2:
                                lnkButton = (LinkButton)e.Item.FindControl("LinkButtonB");
                                lnkButton.CommandArgument = "B_" + soruType.SoruNo.ToString();
                                lnkButton.Visible = true;
                                break;
                            case 3:
                                lnkButton = (LinkButton)e.Item.FindControl("LinkButtonC");
                                lnkButton.CommandArgument = "C_" + soruType.SoruNo.ToString();
                                lnkButton.Visible = true;
                                break;
                            case 4:
                                lnkButton = (LinkButton)e.Item.FindControl("LinkButtonD");
                                lnkButton.CommandArgument = "D_" + soruType.SoruNo.ToString();
                                lnkButton.Visible = true;
                                break;
                            case 5:
                                lnkButton = (LinkButton)e.Item.FindControl("LinkButtonE");
                                lnkButton.CommandArgument = "E_" + soruType.SoruNo.ToString();
                                lnkButton.Visible = true;
                                break;
                        }

                        lnkButton.CommandName = "SikCommand";
                    }

                    if (!string.IsNullOrEmpty(soruType.SoruIsaretlenenSik))
                    {
                        string isaretlenenSik = "LinkButton" + soruType.SoruIsaretlenenSik;
                        LinkButton lnkBtn = (LinkButton)e.Item.FindControl(isaretlenenSik);
                        ((HtmlGenericControl)lnkBtn.Controls[0]).Attributes["class"] = "selected cevapanahtari product-chooser-item text-center sinav";
                    }

                    switch (SessionHelper.DenemeData.SinavMode)
                    {
                        default:
                        case SinavModeEnum.SinavAktif:

                            break;
                        case SinavModeEnum.SinavPasif:
                        case SinavModeEnum.Inceleme:
                            if (string.IsNullOrEmpty(soruType.SoruIsaretlenenSik))
                            {
                                labelSoruDurum.Attributes["class"] = "fa fa-2x fa-square-o";
                            }
                            else if (soruType.SoruIsaretlenenSik == soruType.SoruDogruSik)
                            {
                                labelSoruDurum.Attributes["class"] = "fa fa-2x fa-check-square-o";
                            }
                            else
                            {
                                labelSoruDurum.Attributes["class"] = "fa fa-2x fa-minus-square-o";
                            }
                            break;
                    }

                    break;
            }
        }

        protected void RepeaterSiklar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var aktifSorular = SessionHelper.DenemeData.Sorular.Where(p => p.DersKey == SessionHelper.DenemeData.AktifDersKey);
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.DenemeData.AktifSoruNo);

            switch (e.CommandName)
            {
                case "SoruNoCommand":
                    int aktifSoruNo = Convert.ToInt32(e.CommandArgument);
                    SessionHelper.DenemeData.AktifSoruNo = aktifSoruNo;
                    break;
                case "SikCommand":
                    string[] parametreler = e.CommandArgument.ToString().Split('_');
                    string isaretlenenSik = parametreler[0];
                    string soruNo = parametreler[1];

                    if (SessionHelper.DenemeData.SinavMode == SinavModeEnum.SinavAktif && soruNo == aktifSoru.SoruNo.ToString())
                    {
                        if (string.IsNullOrEmpty(isaretlenenSik))
                            aktifSoru.SoruIsaretlenenSik = null;
                        else
                            aktifSoru.SoruIsaretlenenSik = isaretlenenSik;

                        for (int i = 1; i <= aktifSoru.SoruSikSayisi; i++)
                        {
                            LinkButton lnkBtn = new LinkButton();
                            switch (i)
                            {
                                case 1:
                                    lnkBtn = (LinkButton)e.Item.FindControl("LinkButtonA");
                                    break;
                                case 2:
                                    lnkBtn = (LinkButton)e.Item.FindControl("LinkButtonB");

                                    break;
                                case 3:
                                    lnkBtn = (LinkButton)e.Item.FindControl("LinkButtonC");

                                    break;
                                case 4:
                                    lnkBtn = (LinkButton)e.Item.FindControl("LinkButtonD");

                                    break;
                                case 5:
                                    lnkBtn = (LinkButton)e.Item.FindControl("LinkButtonE");
                                    break;
                            }

                            HtmlGenericControl cntrl = ((HtmlGenericControl)lnkBtn.Controls[0]);
                            if (!string.IsNullOrEmpty(aktifSoru.SoruIsaretlenenSik))
                            {
                                if (aktifSoru.SoruIsaretlenenSik == lnkBtn.CommandArgument.ToString())
                                {
                                    cntrl.Attributes["class"] = "selected cevapanahtari product-chooser-item text-center sinav";
                                }
                                else
                                {
                                    cntrl.Attributes["class"] = "cevapanahtari product-chooser-item text-center sinav";
                                }
                            }
                            else
                            {
                                cntrl.Attributes["class"] = "cevapanahtari product-chooser-item text-center sinav";
                            }
                        }
                    }
                    else
                    {
                        SessionHelper.DenemeData.AktifSoruNo = Convert.ToInt32(soruNo);
                    }
                    break;
            }


            if (SinavSureKontrol())
            {
                DenemeKullaniciSoruKaydet();
                DenemeKullaniciKaydet();
                SessionHelper.DenemeData.IslemZamani = DateTime.Now;
            }

            AktifSoruGetir();
        }

        protected void RepeaterDersler_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlButton btn = e.Item.FindControl("ButtonDers") as HtmlButton;
            int dersSayisi = SessionHelper.DenemeData.Sorular.
                                          Select(p => new
                                          {
                                              p.DersKey,
                                              p.DersAdi
                                          }).
                                          Distinct().Count();
            if (dersSayisi==1)
            {
                btn.Style.Add("width", "20%");
            }

            else
            {
                string yuzde = ((96m / dersSayisi)).ToString(CultureInfo.GetCultureInfo("en")) + "%";
                btn.Style.Add("width", yuzde);
            }           

            var data = e.Item.DataItem;
            int dersKey = Convert.ToInt32(e.Item.DataItem.GetType().GetProperty("DersKey").GetValue(data));
            if (dersKey == SessionHelper.DenemeData.AktifDersKey)
            {
                btn.Attributes["class"] = "btn btn-primary";
            }
            else
            {
                btn.Attributes["class"] = "btn btn-default";
            }
        }

        #endregion

        #region Methods

        public void SetInitials()
        {
            if (SessionHelper.DenemeData == null)
            {
                Response.Redirect("/DenemeSinav.aspx", true);
                return;
            }
            else
            {
                if (SessionHelper.DenemeData.SinavMode == SinavModeEnum.SinavBasla)
                {
                    if (!DenemeDogrula())
                    {
                        ButtonEskiSinavaDevamEt.Style.Add("display", "none");
                    }

                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ModelWelcomeOpen(); ", true);
                }
                else if (SessionHelper.DenemeData.SinavMode == SinavModeEnum.SinavPasif)
                {
                    ResultDataLoad();
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ModelResultOpen();", true);
                }
                else
                {
                    SinavSureKontrol();

                    if (!IsPostBack)
                    {
                        DataLoad();
                    }
                }

                switch (SessionHelper.DenemeData.SinavMode)
                {
                    default:
                    case SinavModeEnum.SinavAktif:
                        labelDogruSik.Visible = false;
                        labelIsaretlenenSik.Visible = false;
                        LabelAciklama.Visible = false;
                        ButtonTemizle.Visible = true;
                        break;
                    case SinavModeEnum.SinavPasif:
                    case SinavModeEnum.Inceleme:
                        labelDogruSik.Visible = true;
                        labelIsaretlenenSik.Visible = true;
                        LabelAciklama.Visible = true;
                        ButtonTemizle.Visible = false;
                        break;
                }

                KayitKontrol();
            }
        }

        public void DataLoad()
        {
            DenemeKullaniciSoruKaydet();
            AktifSoruGetir();           

            (this.Master.Master.FindControl("LabelDenemeDers") as Label).Text = SessionHelper.DenemeData.AktifDenemeAdi;

            DateTime baslangicZamani = SessionHelper.DenemeData.BaslangicZamani;
            TimeSpan gecmisSure = new TimeSpan(0, 0, SessionHelper.DenemeData.GecenSure);
            var gecenSure = (DateTime.Now - baslangicZamani).Add(gecmisSure);
            var gecenSaat = gecenSure.Hours.ToString().Length == 1 ? "0" + gecenSure.Hours.ToString() : gecenSure.Hours.ToString();
            var gecenDakika = gecenSure.Minutes.ToString().Length == 1 ? "0" + gecenSure.Minutes.ToString() : gecenSure.Minutes.ToString();
            var gecenSaniye = gecenSure.Seconds.ToString().Length == 1 ? "0" + gecenSure.Seconds.ToString() : gecenSure.Seconds.ToString();

            Label LabelBaslangic = this.Master.Master.FindControl("LabelBaslangic") as Label;
            Label LabelSunucuSaati = this.Master.Master.FindControl("LabelSunucuSaati") as Label;
            Label LabelGecenSure = this.Master.Master.FindControl("LabelGecenSure") as Label;

            LabelBaslangic.Text = baslangicZamani.ToString("HH:mm:ss");
            LabelBaslangic.ToolTip = baslangicZamani.ToString("dd-MM-yyyy HH:mm:ss");
            LabelSunucuSaati.Text = DateTime.Now.ToString("HH:mm:ss");
            LabelSunucuSaati.ToolTip = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            LabelGecenSure.Text = gecenSaat + ":" + gecenDakika + ":" + gecenSaniye;

            LabelDurum.Value = SessionHelper.DenemeData.SinavMode.ToString();
        }

        public void ResultDataLoad()
        {
            int dogru = 0;
            int yanlis = 0;
            int bos = 0;

            var sorular = SessionHelper.DenemeData.Sorular;

            foreach (var soru in sorular)
            {
                DersSonucType dersSonucType = null;
                if (dersSonuclari.Any(p => p.DersKey == soru.DersKey))
                {
                    dersSonucType = dersSonuclari.Single(p => p.DersKey == soru.DersKey);
                }
                else
                {
                    dersSonucType = new DersSonucType
                    {
                        DersAdi = soru.DersAdi,
                        DersKey = soru.DersKey,
                        Dogru = 0,
                        Yanlis = 0,
                        Bos = 0
                    };

                    dersSonuclari.Add(dersSonucType);
                }

                if (string.IsNullOrEmpty(soru.SoruIsaretlenenSik))
                {
                    bos++;
                    dersSonucType.Bos += 1;
                }
                else if (soru.SoruIsaretlenenSik == soru.SoruDogruSik)
                {
                    dogru++;
                    dersSonucType.Dogru += 1;
                }
                else
                {
                    yanlis++;
                    dersSonucType.Yanlis += 1;
                }
            }

            LabelDogru.Text = dogru.ToString();
            LabelYanlis.Text = yanlis.ToString();
            LabelBos.Text = bos.ToString();

            RepeaterDersSonuclari.DataSource = null;
            RepeaterDersSonuclari.DataSource = dersSonuclari;
            RepeaterDersSonuclari.DataBind();
        }

        public void AktifSoruGetir()
        {
            var aktifSorular = SessionHelper.DenemeData.Sorular.Where(p => p.DersKey == SessionHelper.DenemeData.AktifDersKey);
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.DenemeData.AktifSoruNo);
            aktifSoru.SoruGoruntulendiMi = true;

            labelDogruSik.InnerHtml = " Doğru Şık : " + aktifSoru.SoruDogruSik;
            labelIsaretlenenSik.InnerHtml = " İşaretlenen Şık : " + aktifSoru.SoruIsaretlenenSik;
            LabelSoruNo.Text = aktifSoru.SoruNo.ToString();
            LabelSoru.Text = aktifSoru.Soru;
            LabelAciklama.Text = aktifSoru.SoruAciklama;

            LinkButtonA.Visible = false;
            LinkButtonB.Visible = false;
            LinkButtonC.Visible = false;
            LinkButtonD.Visible = false;
            LinkButtonE.Visible = false;

            for (int i = 1; i <= aktifSoru.SoruSikSayisi; i++)
            {
                LinkButton lnkButton = new LinkButton();
                switch (i)
                {
                    case 1:
                        lnkButton = LinkButtonA;
                        lnkButton.CommandArgument = "A_" + aktifSoru.SoruNo.ToString();
                        lnkButton.Visible = true;
                        break;
                    case 2:
                        lnkButton = LinkButtonB;
                        lnkButton.CommandArgument = "B_" + aktifSoru.SoruNo.ToString();
                        lnkButton.Visible = true;
                        break;
                    case 3:
                        lnkButton = LinkButtonC;
                        lnkButton.CommandArgument = "C_" + aktifSoru.SoruNo.ToString();
                        lnkButton.Visible = true;
                        break;
                    case 4:
                        lnkButton = LinkButtonD;
                        lnkButton.CommandArgument = "D_" + aktifSoru.SoruNo.ToString();
                        lnkButton.Visible = true;
                        break;
                    case 5:
                        lnkButton = LinkButtonE;
                        lnkButton.CommandArgument = "E_" + aktifSoru.SoruNo.ToString();
                        lnkButton.Visible = true;
                        break;
                }

                lnkButton.CommandName = "SikCommand";

                HtmlGenericControl cntrl = ((HtmlGenericControl)lnkButton.Controls[0]);
                if (!string.IsNullOrEmpty(aktifSoru.SoruIsaretlenenSik))
                {
                    if (aktifSoru.SoruIsaretlenenSik == lnkButton.CommandArgument.ToString().Split('_')[0])
                    {
                        cntrl.Attributes["class"] = "selected cevapanahtari product-chooser-item text-center sinav";
                    }
                    else
                    {
                        cntrl.Attributes["class"] = "cevapanahtari product-chooser-item text-center sinav";
                    }
                }
                else
                {
                    cntrl.Attributes["class"] = "cevapanahtari product-chooser-item text-center sinav";
                }

            }

            if (aktifSoru.SoruDogruSik == aktifSoru.SoruIsaretlenenSik)
                labelIsaretlenenSik.Attributes["class"] = "badge badge-success";
            else
                labelIsaretlenenSik.Attributes["class"] = "badge badge-danger";

            RepeaterSiklar.DataSource = null;
            RepeaterSiklar.DataSource = aktifSorular;
            RepeaterSiklar.DataBind();

            RepeaterDersler.DataSource = null;
            RepeaterDersler.DataSource = SessionHelper.DenemeData.Sorular.
                                         Select(p => new
                                         {
                                             p.DersKey,
                                             p.DersAdi
                                         }).
                                         Distinct().
                                         ToList();
            RepeaterDersler.DataBind();

            if (SessionHelper.DenemeData.AktifSoruNo == 1)
            {
                ButtonGeri.Attributes.Add("disabled", "disabled");
                ButtonGeriTek.Attributes.Add("disabled", "disabled");
            }
            else
            {
                ButtonGeri.Attributes.Remove("disabled");
                ButtonGeriTek.Attributes.Remove("disabled");
            }

            if (SessionHelper.DenemeData.AktifSoruNo == aktifSorular.Count())
            {
                ButtonIleri.Attributes.Add("disabled", "disabled");
                ButtonIleriTek.Attributes.Add("disabled", "disabled");
            }
            else
            {
                ButtonIleri.Attributes.Remove("disabled");
                ButtonIleriTek.Attributes.Remove("disabled");
            }

            if (string.IsNullOrEmpty(aktifSoru.SoruIsaretlenenSik))
            {
                ButtonTemizle.Attributes.Add("disabled", "disabled");
            }
            else
            {
                ButtonTemizle.Attributes.Remove("disabled");
            }

            SessionHelper.DenemeData = SessionHelper.DenemeData;

        }

        public void DenemeKullaniciSoruKaydet()
        {
            var aktifSorular = SessionHelper.DenemeData.Sorular.Where(p => p.DersKey == SessionHelper.DenemeData.AktifDersKey);
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.DenemeData.AktifSoruNo);

            //eğer giriş yapmamış kullanıcı ise demomu kriteri aranacak!
            if (Membership.GetUser(true) != null)
            {
                //yoksa veritabanına yaz, varsa değiştir
                using (var service = new DBEntities())
                {
                    var denemeKullaniciSoru = service.DenemeKullaniciSorus.
                                                      SingleOrDefault(p => p.DenemeKullaniciKey == SessionHelper.DenemeData.DenemeKullaniciKey &&
                                                                           p.SoruKey == aktifSoru.SoruKey);

                    MembershipUser user = Membership.GetUser(true);
                    Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                    if (denemeKullaniciSoru == null)
                    {
                        denemeKullaniciSoru = new DenemeKullaniciSoru
                        {
                            DenemeKullaniciKey = SessionHelper.DenemeData.DenemeKullaniciKey,
                            SoruKey = aktifSoru.SoruKey,
                            CevapSik = aktifSoru.SoruIsaretlenenSik,
                            IslemZamani = DateTime.Now,

                            KayitKisiKey = userkey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = userkey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };

                        service.DenemeKullaniciSorus.Add(denemeKullaniciSoru);
                    }
                    else
                    {
                        denemeKullaniciSoru.IslemZamani = DateTime.Now;
                        denemeKullaniciSoru.CevapSik = aktifSoru.SoruIsaretlenenSik;

                        denemeKullaniciSoru.GuncelleKisiKey = userkey;
                        denemeKullaniciSoru.GuncelleTarih = DateTime.Now;
                    }

                    service.SaveChanges();
                }
            }
        }

        public void DenemeKullaniciKaydet()
        {
            var aktifSorular = SessionHelper.DenemeData.Sorular.Where(p => p.DersKey == SessionHelper.DenemeData.AktifDersKey);
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.DenemeData.AktifSoruNo);

            //eğer giriş yapmamış kullanıcı ise demomu kriteri aranacak!
            if (Membership.GetUser(true) != null)
            {
                using (var service = new DBEntities())
                {
                    //zamanlama içinde denemekullanıcıya son işlem zamanı ve geçen süre hesaplanarak yazılacak
                    //gecen süre işlem zamanı üzerinden yürüyecek
                    var denemeKullanici = service.DenemeKullanicis.
                                                  Single(p => p.DenemeKullaniciKey == SessionHelper.DenemeData.DenemeKullaniciKey);

                    denemeKullanici.AktifSoruKey = aktifSoru.SoruKey;

                    TimeSpan gecenZaman = DateTime.Now - SessionHelper.DenemeData.IslemZamani;
                    int gecenSure = Convert.ToInt32(gecenZaman.TotalSeconds);
                    denemeKullanici.GecenSure += gecenSure;

                    service.SaveChanges();
                }
            }
        }

        public void SinavBitir()
        {
            //sessionda sınavı pasife çek
            SessionHelper.DenemeData.SinavMode = SinavModeEnum.SinavPasif;
            SessionHelper.DenemeData = SessionHelper.DenemeData;

            //eğer giriş yapmamış kullanıcı ise kayıtlar yapılmayacak!
            if (Membership.GetUser(true) != null)
            {
                //denemeyi pasife çek ve kapat
                using (var service = new DBEntities())
                {
                    var denemeKullanici = service.DenemeKullanicis.
                                                  Include("Deneme").
                                                  SingleOrDefault(p => p.AktifMi &&
                                                              p.DenemeKullaniciKey == SessionHelper.DenemeData.DenemeKullaniciKey);
                    if (denemeKullanici != null)
                    {
                        TimeSpan gecenZaman = DateTime.Now - SessionHelper.DenemeData.IslemZamani;
                        int gecenSure = Convert.ToInt32(gecenZaman.TotalSeconds);
                        if (denemeKullanici.Deneme.Sure * 60 <= gecenSure)
                        {
                            denemeKullanici.GecenSure = denemeKullanici.Deneme.Sure * 60;
                        }
                        else
                        {
                            denemeKullanici.GecenSure += gecenSure;
                        }

                        denemeKullanici.AktifMi = false;

                        service.SaveChanges();
                    }
                }
            }

            Response.Redirect(PageUrl, true);
        }

        public bool SinavSureKontrol()
        {
            bool durum = true;

            //Sınav modu inceleme ise direk false döndür
            if (SessionHelper.DenemeData.SinavMode == SinavModeEnum.Inceleme)
            {
                durum = false;
            }
            else
            {
                //gecen süreyi baz alıcaz ona göre devam yada tamam diyebiliriz                       
                TimeSpan gecenZaman = DateTime.Now - SessionHelper.DenemeData.BaslangicZamani;
                int gecenSure = Convert.ToInt32(gecenZaman.TotalSeconds) + SessionHelper.DenemeData.GecenSure;

                if ((SessionHelper.DenemeData.ToplamDakika * 60) <= gecenSure)
                {
                    durum = false;
                    SinavBitir();
                }
            }

            return durum;
        }

        public bool DenemeDogrula()
        {
            bool dogrulama = false;
            var membershipUser = Membership.GetUser(true);

            using (var service = new DBEntities())
            {
                //eğer giriş yapmamış kullanıcı ise demomu kriteri aranacak!
                if (membershipUser == null)
                {
                    dogrulama = false;
                }
                else
                {
                    Guid userId = Guid.Parse(membershipUser.ProviderUserKey.ToString());

                    dogrulama = service.DenemeKullanicis.
                                        Any(p => p.AktifMi &&
                                                 p.DenemeKey == SessionHelper.DenemeData.AktifDenemeKey &&
                                                 p.UserId == userId);
                }
            }

            return dogrulama;
        }

        public void KayitKontrol()
        {
            HtmlButton buttonKaydet = LoginViewSonuc.FindControl("ButtonKaydet") as HtmlButton;
            if (buttonKaydet != null)
            {
                if (SessionHelper.DenemeData.SinavKayit)
                {
                    buttonKaydet.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    buttonKaydet.Attributes.Remove("disabled");
                }
            }
        }

        #endregion

    }
}