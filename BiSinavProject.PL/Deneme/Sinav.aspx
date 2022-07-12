<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SinavMain.Master" AutoEventWireup="true" CodeBehind="Sinav.aspx.cs" Inherits="BiSinavProject.PL.SinavDeneme.Sinav" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <link rel="stylesheet" href="/Content/ExamCss/default.css" />

    <style type="text/css">
        .search-results-list li {
            border-bottom: 0px solid #eee;
            margin-bottom: 0px;
            padding-bottom: 10px;
            position: relative;
        }

        .beyaz {
            background: #FFF;
            min-width: 200px;
            float: left;
            width: 100%;
        }

            .beyaz:hover {
                background: #EBEBEB;
            }

        .pembe {
            background: #FFE1C4;
            min-width: 200px;
            float: left;
            width: 100%;
        }

            .pembe:hover {
                background: #EBEBEB;
            }

        .gri {
            background: #EBEBEB;
            min-width: 200px;
            float: left;
            width: 100%;
        }

        div.clear {
            clear: both;
        }

        .sinav {
            width: 24px;
            float: left;
            margin-right: 5px;
            font-weight: bold;
            color: #FF6600;
        }

        div.product-chooser {
            width: 115%;
        }

            div.product-chooser.disabled div.product-chooser-item {
                cursor: default;
            }

            div.product-chooser div.product-chooser-item {
                border-radius: 24px;
                cursor: pointer;
                position: relative;
                border: 1px solid #FF6600;
            }

                div.product-chooser div.product-chooser-item.selected {
                    background: #000;
                    color: #fff;
                    border: 1px solid #000;
                }

                div.product-chooser div.product-chooser-item img {
                    padding: 0;
                }

                div.product-chooser div.product-chooser-item span.title {
                    font-weight: bold;
                }

                div.product-chooser div.product-chooser-item span.description {
                }

                div.product-chooser div.product-chooser-item input {
                    position: absolute;
                    left: 0;
                    top: 0;
                    visibility: hidden;
                }

        .c img {
            max-width: 100%;
            max-height: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSorular" runat="server">
    <div class="">
        <table class="table table-bordered table-striped no-margins no-padding">
            <tbody>
                <tr>
                    <td style="width: 50%;">
                        <h1 style="color: crimson">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    Soru No : # 
                                    <asp:Label ID="LabelSoruNo" ClientIDMode="Static" runat="server" Font-Bold="true" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </h1>
                    </td>
                    <td style="width: 50%;" class="text-right">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="LabelDurum" ClientIDMode="Static" runat="server" />
                                            <span runat="server" id="labelDogruSik" class="badge badge-info" style="width: 120px;" data-toggle="tooltip" data-placement="top" title="Doğru Şık">&nbsp;Doğru Şık : </span>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <span runat="server" id="labelIsaretlenenSik" class="badge badge-info" style="width: 120px;" data-toggle="tooltip" data-placement="top" title="Doğru Şık">&nbsp;&nbsp;İşaretlenen Şık : </span>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:Repeater ID="RepeaterDersler" runat="server" OnItemDataBound="RepeaterDersler_ItemDataBound">
                                    <ItemTemplate>
                                        <button runat="server" id="ButtonDers" name='<%# Eval("DersKey") %>' class="btn btn-primary" type="button" onserverclick="ButtonDers_Click">
                                            <%# Eval("DersAdi") %>
                                        </button>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="full-width gri">
                                    <table style="width: 99%">
                                        <tr>
                                            <td>
                                                <button runat="server" id="ButtonGeriTek" class="btn btn-dark full-width" type="button" onserverclick="ButtonGeri_Click"><i class="fa fa-arrow-left"></i>&nbsp;Geri</button>
                                            </td>
                                            <td>
                                                <div class="center-block" style="width: 140px; text-align: center;">
                                                    <div class="satir product-chooser" data-ogrenci_detay_idx="mkov" style="width: 210px; margin: 0px; padding: 0px;">

                                                        <div style="height: 25px; width: 210px; padding: 5px 5px 5px 0px; margin: 0px;">

                                                            <asp:LinkButton runat="server" ID="LinkButtonA" Visible="false" OnClick="LinkButtonSik_Click">
                                                <div runat="server" class=" cevapanahtari product-chooser-item text-center sinav" >
                                                    A
                                                </div>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" ID="LinkButtonB" Visible="false" OnClick="LinkButtonSik_Click">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav" >
                                                    B
                                                </div>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" ID="LinkButtonC" Visible="false" OnClick="LinkButtonSik_Click">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav">
                                                    C
                                                </div>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" ID="LinkButtonD" Visible="false" OnClick="LinkButtonSik_Click">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav" >
                                                    D
                                                </div>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" ID="LinkButtonE" Visible="false" OnClick="LinkButtonSik_Click">
                                                <div runat="server" class=" evapanahtari product-chooser-item text-center sinav" >
                                                    E
                                                </div>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <button runat="server" id="ButtonIleriTek" class="btn btn-dark full-width" type="button" onserverclick="ButtonIleri_Click"><i class="fa fa-arrow-right"></i>&nbsp;İleri</button>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <table class="c" style="table-layout: fixed">
                                    <tr>
                                        <td style="width: 100%; height: 100%">
                                            <asp:Label ID="LabelSoru" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr class="hr-line-solid">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong id="aciklamaLabel">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <table class="c" style="table-layout: fixed">
                                        <tr>
                                            <td style="width: 100%; height: 100%">
                                                <asp:Label ID="LabelAciklama" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </strong>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderSiklar" runat="server">
    <table class="table table-bordered" style="width: 100%">
        <tbody>
            <tr class="no-margins no-padding">
                <td colspan="2">
                    <div class="progress progress-striped active no-margins no-padding">
                        <div id="ProgressBarSure" style="width: 99%" aria-valuemax="100" aria-valuemin="0" aria-valuenow="0" role="progressbar" class="progress-bar progress-bar-dark">
                            <%= BiSinavProject.PL.Class.Helper.SessionHelper.DenemeData.ToplamDakika %> dk.
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <button runat="server" id="ButtonSinaviBitir" class="btn btn-primary full-width" type="button" onserverclick="ButtonSinaviBitir_Click"><i class="fa fa-arrow-right"></i>&nbsp;Sınavı Bitir</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="text-left">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <button runat="server" id="ButtonGeri" class="btn btn-info full-width" type="button" onserverclick="ButtonGeri_Click"><i class="fa fa-arrow-left"></i>&nbsp;Geri</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="text-right">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <button runat="server" id="ButtonIleri" class="btn btn-info full-width" type="button" onserverclick="ButtonIleri_Click"><i class="fa fa-arrow-right"></i>&nbsp;İleri</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <button runat="server" id="ButtonTemizle" class="btn btn-warning full-width" type="button" onserverclick="ButtonTemizle_Click"><i class="fa fa-stop"></i>&nbsp;Seçimi Temizle</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="gray-bg">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="RepeaterSiklar" runat="server" OnItemDataBound="RepeaterSiklar_ItemDataBound" OnItemCommand="RepeaterSiklar_ItemCommand">
                                <ItemTemplate>
                                    <div class="full-width pembe">
                                        <div class="center-block" style="width: 210px; text-align: center;">
                                            <div class="satir product-chooser" data-ogrenci_detay_idx="mkov" style="width: 210px; margin: 0px; padding: 0px;">
                                                <div style="height: 35px; width: 210px; padding: 5px 5px 5px 0px; margin: 0px;">

                                                    <span style="float: left; font-weight: bold; margin-right: 5px; font-size: 16px; min-width: 25px;">
                                                        <asp:LinkButton runat="server" ID="ButtonSoruNo" ForeColor="#333333" Font-Underline="true"></asp:LinkButton>
                                                    </span>

                                                    <asp:LinkButton runat="server" ID="LinkButtonA" Visible="false">
                                                <div runat="server" class=" cevapanahtari product-chooser-item text-center sinav" >
                                                    A
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonB" Visible="false">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav" >
                                                    B
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonC" Visible="false">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav">
                                                    C
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonD" Visible="false">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav" >
                                                    D
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonE" Visible="false">
                                                <div runat="server" class=" evapanahtari product-chooser-item text-center sinav" >
                                                    E
                                                </div>
                                                    </asp:LinkButton>

                                                    <span runat="server" id="LabelSoruDurum" style="float: right;"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div class="full-width beyaz">
                                        <div class="center-block" style="width: 210px; text-align: center;">
                                            <div class="satir product-chooser" data-ogrenci_detay_idx="mkov" style="width: 210px; margin: 0px; padding: 0px;">
                                                <div style="height: 35px; width: 210px; padding: 5px 5px 5px 0px; margin: 0px;">

                                                    <span style="float: left; font-weight: bold; margin-right: 5px; font-size: 16px; min-width: 25px;">
                                                        <asp:LinkButton runat="server" ID="ButtonSoruNo" ForeColor="#333333" Font-Underline="true"></asp:LinkButton>
                                                    </span>

                                                    <asp:LinkButton runat="server" ID="LinkButtonA" Visible="false">
                                                <div runat="server" class=" cevapanahtari product-chooser-item text-center sinav" >
                                                    A
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonB" Visible="false">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav" >
                                                    B
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonC" Visible="false">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav">
                                                    C
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonD" Visible="false">
                                                <div runat="server" class="cevapanahtari product-chooser-item text-center sinav" >
                                                    D
                                                </div>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="LinkButtonE" Visible="false">
                                                <div runat="server" class=" evapanahtari product-chooser-item text-center sinav" >
                                                    E
                                                </div>
                                                    </asp:LinkButton>

                                                    <span runat="server" id="LabelSoruDurum" style="float: right;"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </tbody>
    </table>

    <button id="btnModalWelcomeOpen" type="button" data-toggle="modal" data-target="#modalWelcome" data-backdrop="static" style="visibility: hidden">
    </button>

    <div id="modalWelcome" class="modal inmodal" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated fadeInDown">
                <div class="modal-header">
                    <i class="fa fa-clock-o modal-icon" style="color: crimson"></i>
                    <h4 class="modal-title" style="color: crimson">Hoşgeldiniz</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <strong>Gerçek sınav deneyimi başlıyor...</strong>
                    </p>
                </div>
                <div class="modal-footer">
                    <button id="btnModalWelcomeClose" type="button" class="close" data-dismiss="modal"><span aria-hidden="true" style="visibility: hidden;">&times;</span><span class="sr-only"></span></button>
                    <button runat="server" id="ButtonSinavaBasla" class="btn btn-info btn-large" type="submit" onserverclick="ButtonSinavaBasla_Click"><i class="fa fa-check"></i>Yeni Sınava Başla</button>

                    <button runat="server" id="ButtonEskiSinavaDevamEt" class="btn btn-info btn-large" type="submit" onserverclick="ButtonSinavaBasla_Click"><i class="fa fa-spinner"></i>Eski Sınava Devam</button>
                </div>
            </div>
        </div>
    </div>

    <button id="btnModalResultOpen" type="button" data-toggle="modal" data-target="#modalResult" data-backdrop="static" style="visibility: hidden">
    </button>

    <div id="modalResult" class="modal inmodal" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated slideInDown">
                <div class="modal-header">
                    <i class="fa fa-flag-checkered modal-icon" style="color: crimson"></i>
                    <h4 class="modal-title" style="color: crimson">Sonuçlarınız</h4>
                </div>
                <div class="modal-body">
                    <table class="table no-margins">
                        <tbody>
                            <tr class="text-info">
                                <td>
                                    <h3>
                                        <strong>Doğru : </strong>
                                    </h3>
                                </td>
                                <td>
                                    <h3><span>
                                        <asp:Label ID="LabelDogru" runat="server" /></span>
                                    </h3>
                                </td>
                            </tr>
                            <tr class="text-primary">
                                <td>
                                    <h3><strong>Yanlış : </strong>
                                    </h3>
                                </td>
                                <td>
                                    <h3><span>
                                        <asp:Label ID="LabelYanlis" runat="server" />
                                    </span>
                                    </h3>
                                </td>
                            </tr>
                            <tr class="text-capitalize">
                                <td>
                                    <h3><strong>Boş : </strong>
                                    </h3>
                                </td>
                                <td>
                                    <h3><span>
                                        <asp:Label ID="LabelBos" runat="server" />
                                    </span>
                                    </h3>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table class="table  no-margins">
                                        <tr>
                                            <asp:Repeater ID="RepeaterDersSonuclari" runat="server">
                                                <ItemTemplate>
                                                    <td>
                                                        <table class="table  no-margins">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <h4>
                                                                        <strong><%# Eval("DersAdi") %> </strong>
                                                                    </h4>
                                                                </td>
                                                            </tr>
                                                            <tr class="text-info">
                                                                <td>
                                                                    <h5>
                                                                        <strong>D : </strong>
                                                                    </h5>
                                                                </td>
                                                                <td>
                                                                    <h5>
                                                                        <span>
                                                                            <%# Eval("Dogru") %>
                                                                        </span>
                                                                    </h5>
                                                                </td>
                                                            </tr>
                                                            <tr class="text-primary">
                                                                <td>
                                                                    <h5><strong>Y : </strong>
                                                                    </h5>
                                                                </td>
                                                                <td>
                                                                    <h5>
                                                                        <span>
                                                                            <%# Eval("Yanlis") %>
                                                                        </span>
                                                                    </h5>
                                                                </td>
                                                            </tr>
                                                            <tr class="text-capitalize">
                                                                <td>
                                                                    <h5><strong>B : </strong>
                                                                    </h5>
                                                                </td>
                                                                <td>
                                                                    <h5>
                                                                        <span>
                                                                            <%# Eval("Bos") %>
                                                                        </span>
                                                                    </h5>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="text-center">
                                <td colspan="2">
                                    <button runat="server" id="ButtonIncele" type="button" class="btn btn-info btn-lg" onserverclick="ButtonIncele_Click"><i class="fa fa-search-plus"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;İncele</button>
                                </td>
                            </tr>
                            <tr class="text-center">
                                <td colspan="2">
                                    <asp:LoginView ID="LoginViewSonuc" runat="server">
                                        <AnonymousTemplate>
                                            <div class="text-center">
                                                Üye olmanız halinde </br>"Kullanıcı Detay" sayfanızdan sınav sonuçlarınızı takip edebilirsiniz. 
                                            </div>
                                        </AnonymousTemplate>
                                        <LoggedInTemplate>
                                            <button runat="server" id="ButtonKaydet" type="button" class="btn btn-dark btn-lg" onserverclick="ButtonKaydet_Click"><i class="fa fa-save"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Kaydet</button>
                                        </LoggedInTemplate>
                                    </asp:LoginView>
                                </td>
                            </tr>
                            <tr class="text-center">
                                <td colspan="2">
                                    <button runat="server" id="ButtonSinavCikis" type="button" class="btn btn-primary btn-lg" onserverclick="ButtonSinavCikis_Click"><i class="fa fa-exclamation-triangle"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Çıkış</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button id="btnModaResultClose" type="button" class="close" data-dismiss="modal"><span aria-hidden="true" style="visibility: hidden;">&times;</span><span class="sr-only"></span></button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var gecmisSure= <%= BiSinavProject.PL.Class.Helper.SessionHelper.DenemeData.GecenSure %>;
        var toplamDakika= <%= BiSinavProject.PL.Class.Helper.SessionHelper.DenemeData.ToplamDakika %>;
        setInterval(myTimer, 1000);
           
        function myTimer() {
          
            var labelDurum = document.getElementById("LabelDurum");

            if (labelDurum.value == "SinavAktif") {

                var labelSunucuSaati = document.getElementById("LabelSunucuSaati");
                var labelGecenSure = document.getElementById("LabelGecenSure");
                var labelBaslangicSaati = document.getElementById("LabelBaslangic");
                var progressBarSure = document.getElementById("ProgressBarSure");

                var sunucuTarihi = labelSunucuSaati.title.split(' ')[0].split('-');
                var sunucuSaati = labelSunucuSaati.title.split(' ')[1].split(':');
                var sunucuYil = parseInt(sunucuTarihi[2]);
                var sunucuAy = parseInt(sunucuTarihi[1]);
                var sunucuGun = parseInt(sunucuTarihi[0]);
                var sunucuSaat = parseInt(sunucuSaati[0]);
                var sunucuDakika = parseInt(sunucuSaati[1]);
                var sunucuSaniye = parseInt(sunucuSaati[2]);

                var sunucuZaman = new Date(sunucuYil, sunucuAy, sunucuGun, sunucuSaat, sunucuDakika, sunucuSaniye, 0);
                sunucuZaman.setSeconds(sunucuZaman.getSeconds() + 1);

                //alert(sunucuZaman);

                var gun = sunucuZaman.getDate().toString().length == 1 ? "0" + sunucuZaman.getDate() : sunucuZaman.getDate();
                var ay = sunucuZaman.getMonth().toString().length == 1 ? "0" + sunucuZaman.getMonth() : sunucuZaman.getMonth();
                var yil = sunucuZaman.getFullYear();
                var saat = sunucuZaman.getHours().toString().length == 1 ? "0" + sunucuZaman.getHours() : sunucuZaman.getHours();
                var dakika = sunucuZaman.getMinutes().toString().length == 1 ? "0" + sunucuZaman.getMinutes() : sunucuZaman.getMinutes();
                var saniye = sunucuZaman.getSeconds().toString().length == 1 ? "0" + sunucuZaman.getSeconds() : sunucuZaman.getSeconds();

                //alert(gun + "-" + ay + "-" + yil + " " + saat + ":" + dakika + ":" + saniye);

                labelSunucuSaati.title = gun + "-" + ay + "-" + yil + " " + saat + ":" + dakika + ":" + saniye;
                labelSunucuSaati.innerHTML = saat + ":" + dakika + ":" + saniye;

                //-------------------------------------

                sunucuTarihi = labelSunucuSaati.title.split(' ')[0].split('-');
                sunucuSaati = labelSunucuSaati.title.split(' ')[1].split(':');
                sunucuYil = parseInt(sunucuTarihi[2]);
                sunucuAy = parseInt(sunucuTarihi[1]);
                sunucuGun = parseInt(sunucuTarihi[0]);
                sunucuSaat = parseInt(sunucuSaati[0]);
                sunucuDakika = parseInt(sunucuSaati[1]);
                sunucuSaniye = parseInt(sunucuSaati[2]);

                baslangicTarihi = labelBaslangicSaati.title.split(' ')[0].split('-');
                baslangicSaati = labelBaslangicSaati.title.split(' ')[1].split(':');
                baslangicYil = parseInt(baslangicTarihi[2]);
                baslangicAy = parseInt(baslangicTarihi[1]);
                baslangicGun = parseInt(baslangicTarihi[0]);
                baslangicSaat = parseInt(baslangicSaati[0]);
                baslangicDakika = parseInt(baslangicSaati[1]);
                baslangicSaniye = parseInt(baslangicSaati[2]);

                var baslangicZaman = new Date(baslangicYil, baslangicAy, baslangicGun, baslangicSaat, baslangicDakika, baslangicSaniye, 0);
                sunucuZaman = new Date(sunucuYil, sunucuAy, sunucuGun, sunucuSaat, sunucuDakika, sunucuSaniye, 0);
                var diff = sunucuZaman.getTime() - baslangicZaman.getTime() + gecmisSure*1000;
                var fark = sunucuZaman.getTime() - baslangicZaman.getTime()+ gecmisSure*1000;;

                var days = Math.floor(diff / (1000 * 60 * 60 * 24));
                diff -= days * (1000 * 60 * 60 * 24);
                days = days.toString().length == 1 ? "0" + days : days;

                var hours = Math.floor(diff / (1000 * 60 * 60));
                diff -= hours * (1000 * 60 * 60);
                hours = hours.toString().length == 1 ? "0" + hours : hours;

                var mins = Math.floor(diff / (1000 * 60));
                diff -= mins * (1000 * 60);
                mins = mins.toString().length == 1 ? "0" + mins : mins;

                var seconds = Math.floor(diff / (1000));
                diff -= seconds * (1000);
                seconds = seconds.toString().length == 1 ? "0" + seconds : seconds;

                labelGecenSure.innerHTML = hours + ":" + mins + ":" + seconds;

                var saniyeler = Math.floor(fark / (1000));
                var oran = (saniyeler / (toplamDakika*60)) * 100;
                if (oran > 100) {
                    progressBarSure.style.width = "100%";
                    progressBarSure.className = "progress-bar progress-bar-danger";
                    progressBarSure.innerHTML = toplamDakika+" dk./"+toplamDakika+" dk.";

                    //sınav bitimini otomatik yaptır
                    window.location.href=window.location.href;
                }
                else if (oran > 90) {
                    progressBarSure.style.width = oran + "%";
                    progressBarSure.className = "progress-bar progress-bar-warning";
                    progressBarSure.innerHTML = Math.floor((oran / 100 * toplamDakika)) + " dk./"+toplamDakika+" dk.";
                }
                else {
                    //5 dk. /5 dk.
                    progressBarSure.style.width = oran + "%";
                    progressBarSure.className = "progress-bar progress-bar-info";
                    progressBarSure.innerHTML = Math.floor((oran / 100 * toplamDakika)) + " dk./"+toplamDakika+" dk.";
                }
            }
        }

        function ModelWelcomeOpen() {
            $('#btnModalWelcomeOpen').click();
        }

        function ModelWelcomeClose() {
            $('#btnModalWelcomeClose').click();
        }

        function ModelResultOpen() {
            $('#btnModalResultOpen').click();
        }

        function ModelResultClose() {
            $('#btnModaResultClose').click();
        }
    </script>
</asp:Content>
