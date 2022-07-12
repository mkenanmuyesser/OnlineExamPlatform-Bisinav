<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserReport.ascx.cs" Inherits="BiSinavProject.PL.UserControl.UserReport" %>


<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">

<link href="/Content/css/bootstrap.min.css" rel="stylesheet">
<link href="/Content/css/font-awesome.css" rel="stylesheet">
<link href="/Content/css/animate.css" rel="stylesheet">
<link href="/Content/css/style2.css" rel="stylesheet">


<div class="row">
    <div class="col-lg-12">
        <div class="panel blank-panel">
            <div class="panel-heading">
                <div class="panel-options" >
                    <ul class="nav nav-tabs"  >
                        <li class="active" ><a data-toggle="tab" style="color: crimson;" href="#tab_1">Hesabınızdaki Ürünler</a></li>
                        <li><a data-toggle="tab" href="#tab_2" style="color: darkslateblue;">Beşte Beş İstatistik</a></li>
                        <li class=""><a data-toggle="tab" style="color: darkslateblue;" href="#tab_3">Canlı Deneme İstatistik</a></li>
                        <li class=""><a data-toggle="tab" style="color: darkgrey;" href="#tab_4">Şifre İşlemleri</a></li>
                    </ul>
                </div>
            </div>
            <div class="panel-body">
                <div class="tab-content">
                    <div id="tab_1" class="tab-pane active">
                        <div class="p-title" style="color: dodgerblue;" >
                            <h2>Canlı Denemeler</h2>
                        </div>
                        <div class="panel" style="margin-right: 10px;">
                            <div class="photo-gallery-blocks">
                                <asp:Repeater ID="RepeaterDenemeler" runat="server">
                                    <ItemTemplate>
                                        <div class="item" style="text-align: center">
                                            <div class="item-header">
                                                <img src='/Uploads/Program/<%# Eval("Resim") %>' alt="" /></a>                     
                                            </div>
                                            <div class="item-content">
                                                <h3>
                                                    <asp:Label runat="server" Text='<%# Eval("Adi") %>' Font-Bold="True" Font-Size="X-Large" />
                                                </h3>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <hr />
                        <div class="p-title" style="color: dodgerblue;" >
                            <h2>E-Dergiler</h2>
                        </div>
                        <div class="panel" style="margin-right: 10px;">
                            <div class="photo-gallery-blocks">
                                <asp:Repeater ID="RepeaterEDergiler" runat="server">
                                    <ItemTemplate>
                                        <div class="item" style="text-align: center">
                                            <div class="item-header">
                                                <img src='/Uploads/Program/<%# Eval("Resim") %>' alt="" /></a> 
                                            </div>
                                            <div class="item-content">
                                                <h3>
                                                    <asp:Label runat="server" Text='<%# Eval("Adi") %>' Font-Bold="True" Font-Size="X-Large" />
                                                </h3>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div id="tab_2" class="tab-pane">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Toplam Sınav Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">
                                            <asp:Label ID="LabelBesteBesToplamSinavSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Toplam Giriş Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">
                                            <asp:Label ID="LabelBesteBesToplamGirisSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Başarı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelBesteBesOrtalamaBasari" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Doğru Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelBesteBesOrtalamaDogruSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Yanlış Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelBesteBesOrtalamaYanlisSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Boş Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelBesteBesOrtalamaBosSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Girdiğiniz Sınavlar</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="table-responsive">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Tarih </th>
                                                        <th>Alan Adı </th>
                                                        <th>Kullanılan Süre </th>
                                                        <th>Doğru </th>
                                                        <th>Yanlış </th>
                                                        <th>Boş </th>
                                                        <%--<th>İncele </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterBesteBes" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("Sira") %></td>
                                                                <td><%# Eval("Tarih") %></td>
                                                                <td><%# Eval("DersAdi") %></td>
                                                                <td>- dk.</td>
                                                                <td><%# Eval("Dogru") %></td>
                                                                <td><%# Eval("Yanlis") %></td>
                                                                <td><%# Eval("Bos") %></td>
                                                                <%--<td></td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tab_3" class="tab-pane">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Toplam Sınav Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">
                                            <asp:Label ID="LabelDenemeToplamSinavSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Toplam Giriş Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">
                                            <asp:Label ID="LabelDenemeToplamGirisSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Başarı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelDenemeOrtalamaBasari" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Doğru Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelDenemeOrtalamaDogruSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Yanlış Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelDenemeOrtalamaYanlisSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Ortalama Boş Sayısı</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins">%
                                            <asp:Label ID="LabelDenemeOrtalamaBosSayisi" runat="server" Text="-"></asp:Label>
                                        </h1>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Girdiğiniz Sınavlar</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="table-responsive">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Tarih </th>
                                                        <th>Alan Adı </th>
                                                        <th>Toplam Süre </th>
                                                        <th>Kullanılan Süre </th>
                                                        <th>Doğru </th>
                                                        <th>Yanlış </th>
                                                        <th>Boş </th>
                                                        <th>Aktif </th>
                                                        <%--<th>İncele </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RepeaterDeneme" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("Sira") %></td>
                                                                <td><%# Eval("Tarih") %></td>
                                                                <td><%# Eval("AlanAdi") %></td>
                                                                <td><%# Eval("ToplamSure") %> dk.</td>
                                                                <td><%# Eval("KullanilanSure") %> dk.</td>
                                                                <td><%# Eval("Dogru") %></td>
                                                                <td><%# Eval("Yanlis") %></td>
                                                                <td><%# Eval("Bos") %></td>
                                                                <td>
                                                                    <%# 
                                                                        Convert.ToBoolean(Eval("Aktif"))?
                                                                            "<i class='fa fa fa-check-circle-o text-navy'></i>":
                                                                            "<i class='fa fa fa-circle-o text-danger'></i>" %></td>
                                                                <%--<td></td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tab_4" class="tab-pane">
                        <center>
                        <div class="item">
                            <table>                                
                                <tr>
                                    <td>
                                        <p>
                                            <asp:TextBox ID="TextBoxYeniSifre" runat="server" TextMode="Password" placeholder="   Yeni Şifre" MaxLength="50" Width="298" Height="28"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="TextBoxYeniSifre" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationGroup="SifreDegistir"></asp:RequiredFieldValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            <asp:TextBox ID="TextBoxYeniSifreTekrar" runat="server" TextMode="Password" placeholder="   Şifre Tekrar"  MaxLength="50" Width="298" Height="28"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="TextBoxYeniSifreTekrar" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationGroup="SifreDegistir"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator runat="server" ErrorMessage=" * " ForeColor="Red" ValidationGroup="SifreDegistir" ControlToCompare="TextBoxYeniSifre" ControlToValidate="TextBoxYeniSifreTekrar"></asp:CompareValidator>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <p>
                                            <asp:Button ID="ButtonSifreDegistir" runat="server" class="button" Text="Şifrenizi değiştirin" ValidationGroup="SifreDegistir" OnClick="ButtonSifreDegistir_Click" />
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            <div id="writecomment">
                                <p class="contact-form-user">
                                </p>
                                <p class="contact-form-email">
                                </p>
                                <p class="contact-form-website">
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                            </div>
                        </div>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<!-- Mainly scripts -->
<script src="/Content/Scripts/jquery-2.1.1.js"></script>
<script src="/Content/Scripts/bootstrap.min.js"></script>
<script src="/Content/Scripts/plugins/metisMenu/jquery.metisMenu.js"></script>
<script src="/Content/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"></script>

<!-- Custom and plugin javascript -->
<script src="/Content/Scripts/inspinia.js"></script>


