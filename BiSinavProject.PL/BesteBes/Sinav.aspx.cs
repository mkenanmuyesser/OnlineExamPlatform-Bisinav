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

namespace BiSinavProject.PL.SinavBestebes
{
    public partial class Sinav : System.Web.UI.Page
    {
        #region Properties  

        private const string PageUrl = "Sinav.aspx";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            SetInitials();
        }

        protected void ButtonSinavaBasla_Click(object sender, EventArgs e)
        {
            SessionHelper.BesteBesData.SinavMode = SinavModeEnum.SinavAktif;
            SessionHelper.BesteBesData.BaslangicZamani = DateTime.Now;
            SessionHelper.BesteBesData = SessionHelper.BesteBesData;

            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ModelWelcomeClose(); ", true);
            Response.Redirect(PageUrl);
        }

        protected void LinkButtonSik_Click(object sender, EventArgs e)
        {
            var aktifSorular = SessionHelper.BesteBesData.Sorular;
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.BesteBesData.AktifSoruNo);

            LinkButton lnkButton = sender as LinkButton;
            string[] parametreler = lnkButton.CommandArgument.ToString().Split('_');
            string isaretlenenSik = parametreler[0];
            string soruNo = parametreler[1];

            if (SessionHelper.BesteBesData.SinavMode == SinavModeEnum.SinavAktif && soruNo == aktifSoru.SoruNo.ToString())
            {              
                if (string.IsNullOrEmpty(isaretlenenSik))
                    aktifSoru.SoruIsaretlenenSik = null;
                else
                    aktifSoru.SoruIsaretlenenSik = isaretlenenSik;
            }            

            AktifSoruGetir();
        }

        protected void ButtonGeri_Click(object sender, EventArgs e)
        {
            int aktifSoruNo = SessionHelper.BesteBesData.AktifSoruNo;
            if (aktifSoruNo > 1)
                aktifSoruNo = (aktifSoruNo - 1);
            else
                aktifSoruNo = 1;

            SessionHelper.BesteBesData.AktifSoruNo = aktifSoruNo;

            AktifSoruGetir();
        }

        protected void ButtonIleri_Click(object sender, EventArgs e)
        {
            int aktifSoruNo = SessionHelper.BesteBesData.AktifSoruNo;
            if (aktifSoruNo < SessionHelper.BesteBesData.Sorular.Count())
                aktifSoruNo = (aktifSoruNo + 1);
            else
                aktifSoruNo = SessionHelper.BesteBesData.Sorular.Count();

            SessionHelper.BesteBesData.AktifSoruNo = aktifSoruNo;

            AktifSoruGetir();
        }

        protected void ButtonTemizle_Click(object sender, EventArgs e)
        {
            if (SessionHelper.BesteBesData.SinavMode == SinavModeEnum.SinavAktif)
            {
                var aktifSorular = SessionHelper.BesteBesData.Sorular;
                var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.BesteBesData.AktifSoruNo);
                aktifSoru.SoruIsaretlenenSik = null;
            }

            AktifSoruGetir();
        }

        protected void ButtonSinaviBitir_Click(object sender, EventArgs e)
        {
            //sessionda sınavı pasife çek
            SessionHelper.BesteBesData.SinavMode = SinavModeEnum.SinavPasif;
            SessionHelper.BesteBesData.BitisZamani = DateTime.Now;
            SessionHelper.BesteBesData = SessionHelper.BesteBesData;

            Response.Redirect(PageUrl, true);
        }

        protected void ButtonIncele_Click(object sender, EventArgs e)
        {
            SessionHelper.BesteBesData.AktifSoruNo = 1;
            SessionHelper.BesteBesData.SinavMode = SinavModeEnum.Inceleme;
            SessionHelper.BesteBesData = SessionHelper.BesteBesData;

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

                if (!SessionHelper.BesteBesData.SinavKayit)
                {
                    SessionHelper.BesteBesData.SinavKayit = true;
                    SessionHelper.BesteBesData = SessionHelper.BesteBesData;

                    //kayıt işlemleri
                    using (var service = new DBEntities())
                    {
                        SonucDetay sonuc = new SonucDetay
                        {
                            DenemeKullaniciKey = null,
                            UserId = userId,
                            DersKey = SessionHelper.BesteBesData.AktifDersKey,
                            DogruSayisi = Convert.ToInt32(LabelDogru.Text),
                            YanlisSayisi = Convert.ToInt32(LabelYanlis.Text),
                            BosSayisi = Convert.ToInt32(LabelBos.Text),
                            KullanilanSure = Convert.ToInt32((SessionHelper.BesteBesData.BitisZamani - SessionHelper.BesteBesData.BaslangicZamani).TotalSeconds),

                            KayitKisiKey = userId,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = userId,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        service.SonucDetays.Add(sonuc);
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

        protected void ButtonYeniSinav_Click(object sender, EventArgs e)
        {
            int dersKey = SessionHelper.BesteBesData.AktifDersKey;

            BesteBesBs.SoruOlustur(dersKey);

            Response.Redirect(PageUrl, true);
        }

        protected void ButtonSinavCikis_Click(object sender, EventArgs e)
        {
            SessionHelper.BesteBesData = null;
            Response.Redirect("/BesteBesSinav.aspx");
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

                    switch (SessionHelper.BesteBesData.SinavMode)
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
            var aktifSorular = SessionHelper.BesteBesData.Sorular;
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.BesteBesData.AktifSoruNo);

            switch (e.CommandName)
            {
                case "SoruNoCommand":
                    int aktifSoruNo = Convert.ToInt32(e.CommandArgument);
                    SessionHelper.BesteBesData.AktifSoruNo = aktifSoruNo;
                    break;
                case "SikCommand":
                    string[] parametreler = e.CommandArgument.ToString().Split('_');
                    string isaretlenenSik = parametreler[0];
                    string soruNo = parametreler[1];

                    if (SessionHelper.BesteBesData.SinavMode == SinavModeEnum.SinavAktif && soruNo == aktifSoru.SoruNo.ToString())
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
                        SessionHelper.BesteBesData.AktifSoruNo = Convert.ToInt32(soruNo);
                    }
                    break;
            }

            AktifSoruGetir();
        }

        protected void RadioButtonListSiklar_SelectedIndexChanged(object sender, EventArgs e)
        {
            var aktifSorular = SessionHelper.BesteBesData.Sorular;
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.BesteBesData.AktifSoruNo);

            var radioButtonList = (sender as RadioButtonList);
            if (radioButtonList.SelectedIndex == -1)
                aktifSoru.SoruIsaretlenenSik = null;
            else
                aktifSoru.SoruIsaretlenenSik = radioButtonList.SelectedItem.Text;

            SessionHelper.BesteBesData = SessionHelper.BesteBesData;
        }

        #endregion

        #region Methods

        public void SetInitials()
        {
            if (SessionHelper.BesteBesData == null)
            {
                Response.Redirect("/BesteBesSinav.aspx");
            }
            else
            {
                if (SessionHelper.BesteBesData.SinavMode == SinavModeEnum.SinavBasla)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ModelWelcomeOpen(); ", true);
                }
                else if (SessionHelper.BesteBesData.SinavMode == SinavModeEnum.SinavPasif)
                {
                    ResultDataLoad();
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ModelResultOpen();", true);
                }

                if (!IsPostBack)
                {
                    DataLoad();
                }

                KayitKontrol();

                switch (SessionHelper.BesteBesData.SinavMode)
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
            }
        }

        public void DataLoad()
        {
            AktifSoruGetir();

            (this.Master.Master.FindControl("LabelDenemeDers") as Label).Text = SessionHelper.BesteBesData.AktifDersAdi;

            DateTime baslangicZamani = SessionHelper.BesteBesData.BaslangicZamani;
            var gecenSure = DateTime.Now - baslangicZamani;
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

            LabelDurum.Value = SessionHelper.BesteBesData.SinavMode.ToString();
        }

        public void ResultDataLoad()
        {
            int dogru = 0;
            int yanlis = 0;
            int bos = 0;

            var sorular = SessionHelper.BesteBesData.Sorular;
            foreach (var soru in sorular)
            {
                if (string.IsNullOrEmpty(soru.SoruIsaretlenenSik))
                    bos++;
                else if (soru.SoruIsaretlenenSik == soru.SoruDogruSik)
                    dogru++;
                else
                    yanlis++;
            }

            LabelDogru.Text = dogru.ToString();
            LabelYanlis.Text = yanlis.ToString();
            LabelBos.Text = bos.ToString();
        }

        public void AktifSoruGetir()
        {
            var aktifSorular = SessionHelper.BesteBesData.Sorular;
            var aktifSoru = aktifSorular.Single(p => p.SoruNo == SessionHelper.BesteBesData.AktifSoruNo);
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

            SessionHelper.BesteBesData = SessionHelper.BesteBesData;

            if (SessionHelper.BesteBesData.AktifSoruNo == 1)
            {
                ButtonGeri.Attributes.Add("disabled", "disabled");
                ButtonGeriTek.Attributes.Add("disabled", "disabled");
            }
            else
            {
                ButtonGeri.Attributes.Remove("disabled");
                ButtonGeriTek.Attributes.Remove("disabled");
            }

            if (SessionHelper.BesteBesData.AktifSoruNo == SessionHelper.BesteBesData.Sorular.Count())
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

        }

        public void KayitKontrol()
        {
            HtmlButton buttonKaydet = LoginViewSonuc.FindControl("ButtonKaydet") as HtmlButton;
            if (buttonKaydet != null)
            {
                if (SessionHelper.BesteBesData.SinavKayit)
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