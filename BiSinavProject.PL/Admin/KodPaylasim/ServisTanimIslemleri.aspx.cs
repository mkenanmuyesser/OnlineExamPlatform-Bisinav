using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using BiSinavProject.PL.Class.Business;
using BiSinavProject.PL.Class.Enum;
using BiSinavProject.PL.Class.Helper;
using BiSinavProject.PL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace BiSinavProject.PL.Program.KodPaylasim
{
    public partial class ServisTanimIslemleri : System.Web.UI.Page
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

        private const string PageUrl = "ServisTanimIslemleri.aspx";
        private const string PageHeader = "Servis Tanım İşlemleri";

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
                var dropDownEditDenemeler = DropDownEditDenemeler.FindControl("ListBoxDenemeler") as ASPxListBox;
                var dropDownEditEDergiler = DropDownEditEDergiler.FindControl("ListBoxEDergiler") as ASPxListBox;

                int sirketKey = Convert.ToInt32(ComboBoxSirket.SelectedItem.Value);


                List<int> denemeler = new List<int>();
                if (dropDownEditDenemeler != null)
                {
                    denemeler = KodBs.KeyConverter(dropDownEditDenemeler.SelectedItems);
                }

                List<int> edergiler = new List<int>();
                if (dropDownEditEDergiler != null)
                {
                    edergiler = KodBs.KeyConverter(dropDownEditEDergiler.SelectedItems);
                }

                int servisTipKey = Convert.ToInt32(ComboBoxServisTip.SelectedItem.Value);

                #region validation

                //mutlaka bir ürün seçilmelidir.
                if (Key == 0 && denemeler.Count() == 0 && edergiler.Count() == 0)
                {
                    PageHelper.MessageBox(this, "Lütfen en az bir ürün seçiniz");
                    return;
                }

                #endregion

                bool kayitmi = true;
                Servi data;
                if (Key == 0)
                {
                    kayitmi = true;
                    data = new Servi();

                    //eğer dışarı verilecek servis ise kod üret ve textboxa ata
                    if (servisTipKey == Convert.ToInt32(KodServisTip.ServisDagitim))
                    {
                        Random rnd = new Random();
                        TextBoxKod.Text = CodeHelper.GenerateNewCode(rnd, 8);
                    }
                }
                else
                {
                    kayitmi = false;
                    data = service.Servis.SingleOrDefault(p => p.ServisKey == Key);
                    if (data == null)
                    {
                        Response.Redirect(PageUrl);
                    }
                }

                data.Aciklama = TextBoxAciklama.Text;
                data.Baslangic = Convert.ToDateTime(DateEditBaslangicZamani.Value);
                data.Bitis = Convert.ToDateTime(DateEditBitisZamani.Value);
                data.AktifMi = CheckBoxAktif.Checked;

                MembershipUser user = Membership.GetUser(true);
                Guid userkey = user == null ? Guid.Empty : (Guid)user.ProviderUserKey;
                if (Key == 0)
                {
                    data.SirketKey = sirketKey;
                    data.ServisTipKey = servisTipKey;
                    data.Kod = string.IsNullOrEmpty(TextBoxKod.Text) ? null : TextBoxKod.Text;
                    data.Limit = Convert.ToInt32(SpinEditLimit.Value);

                    foreach (int key in denemeler)
                    {
                        ServisDeneme servisDeneme = new ServisDeneme
                        {
                            Servi = data,
                            DenemeKey = key,
                            ServisKey = data.ServisKey,

                            KayitKisiKey = userkey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = userkey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        data.ServisDenemes.Add(servisDeneme);
                    }

                    foreach (int key in edergiler)
                    {
                        ServisEDergi servisEDergi = new ServisEDergi
                        {
                            Servi = data,
                            EDergiKey = key,
                            ServisKey = data.ServisKey,

                            KayitKisiKey = userkey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = userkey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        data.ServisEDergis.Add(servisEDergi);
                    }

                    data.KayitKisiKey = userkey;
                    data.KayitTarih = DateTime.Now;
                    service.Servis.Add(data);
                }
                else
                {
                    data.GuncelleKisiKey = userkey;
                    data.GuncelleTarih = DateTime.Now;
                }

                service.SaveChanges();

                if (kayitmi)
                {
                    //eğer kod dağıtım ise dağıtıma satır atacak
                    if (servisTipKey == Convert.ToInt32(KodServisTip.KodDagitim))
                    {
                        KodBs.KodOlustur(data.ServisKey, data.Limit);
                    }
                }
            }

            Response.Redirect(PageUrl);
        }

        protected void ButtonIptalTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect(PageUrl);
        }

        protected void ComboBoxServisTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            int servisTipKey = Convert.ToInt32(ComboBoxServisTip.SelectedItem.Value);

            switch (servisTipKey)
            {
                case 1:
                    ButtonKaydet.Text = "Kaydet";
                    break;
                case 2:
                    ButtonKaydet.Text = "Kaydet ve Kod Üret";
                    break;
            }
        }

        protected void GridViewTanim_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            using (var service = new DBEntities())
            {
                int deletedkey = Convert.ToInt32(e.Keys[0]);
                Servi deleteddata = service.Servis.Single(p => p.ServisKey == deletedkey);
                service.Servis.Remove(deleteddata);

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
            int key = Convert.ToInt32(GridViewTanim.GetRowValues(index, new string[] { "ServisKey" }));
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

                    List<Sirket> listSirket = service.Sirkets.
                                                      AsNoTracking().
                                                      OrderBy(p => p.Sira).
                                                      ToList();

                    ComboBoxSirket.DataSource = listSirket;
                    ComboBoxSirket.DataBind();

                    List<Deneme> listDeneme = service.Denemes.
                                                      AsNoTracking().
                                                      OrderBy(p => p.Sira).
                                                      ToList();
                    listDeneme.Insert(0, new Deneme
                    {
                        DenemeKey = 0,
                        Adi = "(Hepsini seç)"
                    });
                    var dropDownEditDenemeler = DropDownEditDenemeler.FindControl("ListBoxDenemeler") as ASPxListBox;
                    dropDownEditDenemeler.DataSource = listDeneme;
                    dropDownEditDenemeler.DataBind();

                    List<EDergi> listEDergi = service.EDergis.
                                                     AsNoTracking().
                                                     OrderBy(p => p.Sira).
                                                     ToList();
                    listEDergi.Insert(0, new EDergi
                    {
                        EDergiKey = 0,
                        Adi = "(Hepsini seç)"
                    });
                    var dropDownEditEDergiler = DropDownEditEDergiler.FindControl("ListBoxEDergiler") as ASPxListBox;
                    dropDownEditEDergiler.DataSource = listEDergi;
                    dropDownEditEDergiler.DataBind();

                    List<ServisTip> listServisTip = service.ServisTips.
                                                            AsNoTracking().
                                                            Where(p => p.ServisTipKey != 3).
                                                            OrderBy(p => p.Sira).
                                                            ToList();

                    ComboBoxServisTip.DataSource = listServisTip;
                    ComboBoxServisTip.DataBind();
                }

                GridViewTanim.DataSource = null;
                GridViewTanim.DataSource = service.Servis.
                                                           AsNoTracking().
                                                           Include("Sirket").
                                                           Include("ServisDenemes").
                                                           Include("ServisDenemes.Deneme").
                                                           Include("ServisEDergis").
                                                           Include("ServisEDergis.EDergi").
                                                           Include("ServisTip").
                                                           Where(p => p.ServisTipKey != 3).
                                                           OrderBy(p => p.Sira).
                                                           ToList().
                                                           Select(p => new
                                                           {
                                                               p.ServisKey,
                                                               p.Sirket,
                                                               p.Aciklama,
                                                               p.ServisTip,
                                                               Denemeler = KodBs.StringConverter(p.ServisDenemes.Select(x => x.Deneme.Adi)),
                                                               EDergiler = KodBs.StringConverter(p.ServisEDergis.Select(x => x.EDergi.Adi)),
                                                               p.Kod,
                                                               p.Baslangic,
                                                               p.Bitis,
                                                               p.Limit,
                                                               Uretilen= KodBs.UretilenKodSayisi(p.ServisKey),
                                                               Kullanilan = KodBs.KullanilanKodSayisi(p.ServisKey),
                                                               Kalan = p.Limit- KodBs.KullanilanKodSayisi(p.ServisKey),
                                                               p.AktifMi,
                                                           }).                                                           
                                                           ToList();
                GridViewTanim.DataBind();

                if (Key != 0 && !IsPostBack)
                {
                    Servi data = service.Servis.
                                         AsNoTracking().
                                         Include("ServisDenemes").
                                         Include("ServisDenemes.Deneme").
                                         Include("ServisEDergis").
                                         Include("ServisEDergis.EDergi").
                                         Single(p => p.ServisKey == Key);

                    ComboBoxSirket.Enabled = false;
                    DropDownEditDenemeler.Enabled = false;
                    DropDownEditEDergiler.Enabled = false;
                    ComboBoxServisTip.Enabled = false;
                    SpinEditLimit.Enabled = false;

                    if (data.ServisDenemes.Count() > 0)
                    {
                        DropDownEditDenemeler.Text = KodBs.StringConverter(data.ServisDenemes.Select(x => x.Deneme.Adi));
                    }
                    if (data.ServisEDergis.Count() > 0)
                    {
                        DropDownEditEDergiler.Text = KodBs.StringConverter(data.ServisEDergis.Select(x => x.EDergi.Adi));
                    }

                    ComboBoxSirket.Items.FindByValue(data.SirketKey).Selected = true;
                    ComboBoxServisTip.Items.FindByValue(data.ServisTipKey).Selected = true;

                    TextBoxAciklama.Text = data.Aciklama;
                    TextBoxKod.Text = data.Kod;
                    SpinEditLimit.Text = data.Limit.ToString();
                    DateEditBaslangicZamani.Value = data.Baslangic;
                    DateEditBitisZamani.Value = data.Bitis;
                    CheckBoxAktif.Checked = data.AktifMi;
                }
            }
        }        

        #endregion

    }
}