<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BiSinavProject.PL.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
    <div class="breaking-news">
        <div class="breaking-title">
            <h3>BİSINAV.NET</h3>
            <i></i>
        </div>
        <div class="breaking-block">
            <marquee>
                <ul>
                    <asp:Repeater ID="RepeaterIcerik" runat="server" >
                        <ItemTemplate>
                            <li>
                                  <%--<i class="fa fa-exclamation"><%# Eval("Tarih","{0:MMMM d, yyyy}") %></i>--%>
                                <h4>
                                    <a href='<%# Eval("Link") %>' target="_blank"><%# Eval("Aciklama") %></a>
                                </h4>                                                        
                            </li>   
                        </ItemTemplate>
                    </asp:Repeater>    
               </ul>
            </marquee>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="panel">
        <div class="slider">
            <div class="slider-image">
                <asp:Repeater ID="RepeaterMansetSol" runat="server">
                    <ItemTemplate>
                        <a href='<%# string.IsNullOrEmpty(Eval("Link").ToString())?"#":Eval("Link") %>' <%# string.IsNullOrEmpty(Eval("Link").ToString())?"":"target='_blank'" %> <%# Eval("Sira").ToString()=="1"?"class='active'":""%>>
                            <img src='Uploads/Program/<%# Eval("Resim") %>' class="setborder" />
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <ul class="slider-navigation">
                <asp:Repeater ID="RepeaterMansetSag" runat="server">
                    <ItemTemplate>
                        <li <%# Eval("Sira").ToString()=="1"?"class='active'":""%>>
                            <a href='<%# string.IsNullOrEmpty(Eval("Link").ToString())?"#":Eval("Link") %>' <%# string.IsNullOrEmpty(Eval("Link").ToString())?"":"target='_blank'" %>>
                                <h1><strong><%# Eval("Baslik") %></strong></h1>
                                <h3><span><%# Eval("Aciklama") %></span></h3>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>

    <div class="panel">
        <div class="p-title">
            <h2>Video Dersler</h2>
        </div>
        <div class="video-carousel">
            <a href="#" class="carousel-left"><i class="fa fa-chevron-left"></i></a>
            <a href="#" class="carousel-right"><i class="fa fa-chevron-right"></i></a>
            <div class="inner-carousel">
                <asp:Repeater ID="RepeaterVideoDers" runat="server">
                    <ItemTemplate>
                        <div class="item">
                            <a href='/VideoDers.aspx?K=<%# Eval("Key") %>'>
                                <img src="/Content/Site/Images/Resimler/video.jpg" class="item-photo" alt="" /></a>
                            <h3><a href="/VideoDers.aspx"><%# Eval("Adi") %></a></h3>
                               <span><%# Eval("KayitSayi") %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div class="panel-split">
        <div class="left-side">
            <div class="panel">
                <div class="p-title">
                    <h2>&nbsp;</h2>
                </div>
                <div class="article-list">
                    <div class="item main-artice light">
                        <div class="item-header">
                            <a href="/DenemeSinav.aspx">
                                <img src="/Content/Site/Images/Resimler/380x100-canlıdenemeler.jpg" alt="" />
                            </a>
                        </div>
                    </div>
                    <asp:Repeater ID="RepeaterDeneme" runat="server">
                        <ItemTemplate>
                            <div class="item">
                                <a href='DenemeSinav.aspx'>
                                    <img src="Content/Site/Images/Logo/arı1.jpg" alt="" class="item-photo" />
                                </a>
                                <div class="item-content">
                                    <h3><a href='DenemeSinav.aspx'><%# Eval("Adi") %></a></h3>
                                    <span></span>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <div class="right-side">
            <div class="panel">
                <div class="p-title">
                    <h2>&nbsp;</h2>
                </div>
                <div class="article-list">
                    <div class="item main-artice light">
                        <div class="item-header">
                            <a href="/EDergi.aspx">
                                <img src="/Content/Site/Images/Resimler/380x100-e-dergi.jpg" alt="" />
                            </a>
                        </div>
                    </div>
                    <asp:Repeater ID="RepeaterEDergi" runat="server">
                        <ItemTemplate>
                            <div class="item">
                                <a href='EDergi.aspx'>
                                    <img src="Content/Site/Images/Logo/arı2.jpg" alt="" class="item-photo" />
                                    <div class="item-content">
                                        <h3><a href='EDergi.aspx'><%# Eval("Adi") %></a></h3>
                                        <span></span>
                                    </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

    <div>
        <center>
            <p>
                  <asp:HyperLink runat="server" ID="HyperLinkHyperLinkOrtaUstBolge">
            </asp:HyperLink>
                <br />
            </p>
        </center>
    </div>

    <div class="panel-split">
        <div class="left-side">
            <div class="panel">
                <div class="p-title">
                    <h2>&nbsp;</h2>
                </div>
                <div class="article-list">
                    <div class="item main-artice light">
                        <div class="item-header">
                            <a href="/Kurs.aspx">
                                <img src="/Content/Site/Images/Resimler/380x100-kurslar.jpg" alt="" />
                            </a>
                        </div>
                    </div>
                    <asp:Repeater ID="RepeaterIl" runat="server">
                        <ItemTemplate>
                            <div class="item">
                                <a href='Kurs.aspx?K=<%# Eval("Key") %>'>
                                    <img src="Content/Site/Images/Logo/arı3.jpg" alt="" class="item-photo" />
                                </a>
                                <div class="item-content">
                                    <h3><a href='Kurs.aspx?K=<%# Eval("Key") %>'><%# Eval("Adi") %></a></h3>
                                    <span><%# Eval("KursSayi") %></span>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="right-side">
            <div class="panel">
                <div class="p-title">
                    <h2>&nbsp;</h2>
                </div>
                <div class="article-list">
                    <div class="item main-artice light">
                        <div class="item-header">
                            <a href="/Yayin.aspx">
                                <img src="/Content/Site/Images/Resimler/380x100-yeniyayınlar.jpg" alt="" />
                            </a>
                        </div>
                    </div>
                    <asp:Repeater ID="RepeaterKategori" runat="server">
                        <ItemTemplate>
                            <div class="item">
                                <a href='Yayin.aspx?K=<%# Eval("Key") %>'>
                                    <img src="Content/Site/Images/Logo/arı4.jpg" alt="" class="item-photo" />
                                    <div class="item-content">
                                        <h3><a href='Yayin.aspx?K=<%# Eval("Key") %>'><%# Eval("Adi") %></a></h3>
                                        <span><%# Eval("KitapSayi") %></span>
                                    </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

    <div>
        <center>
            <p>
                  <asp:HyperLink runat="server" ID="HyperLinkHyperLinkOrtaAltBolge">
            </asp:HyperLink>
                <br />
            </p>
        </center>
    </div>

    <div class="panel">
        <div class="p-title">
            <h2>GÜLÜMSEMEK İÇİN</h2>
        </div>

        <div class="review-block">
            <div class="item">
                <a href="http://zaytung.com/" target="_blank">
                    <img src="Content/Site/Images/photos/image-17.jpg" class="item-photo" alt="" /></a>
                <h3><a href="http://zaytung.com/" target="_blank">Zaytung</a></h3>
            </div>
            <div class="item">
                <a href="https://www.bobiler.org/" target="_blank">
                    <img src="Content/Site/Images/photos/res2.jpg" class="item-photo" alt="" /></a>
                <h3><a href="https://www.bobiler.org/" target="_blank">Bobiler</a></h3>
            </div>

            <div class="item">
                <a href="http://alkislarlayasiyorum.com/" target="_blank">
                    <img src="Content/Site/Images/photos/res1.jpg" class="item-photo" alt="" /></a>
                <h3><a href="http://alkislarlayasiyorum.com/" target="_blank">Alkışlarla Yaşıyorum</a></h3>
            </div>
        </div>
    </div>
</asp:Content>
