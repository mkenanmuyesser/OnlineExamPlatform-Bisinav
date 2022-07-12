<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMain.master" AutoEventWireup="true" CodeBehind="ProgramAyarlari.aspx.cs" Inherits="BiSinavProject.PL.Program.Program.ProgramAyarlari" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div style="width: 100%;">
        <table style="border-color: #999999; width: 100%;" border="1">
            <tr>
                <td class="TdOrtala" colspan="4">
                    <center>
                        <dx:ASPxLabel ID="LabelBaslik" runat="server" Font-Size="Large" />
                    </center>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Mail Host : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxMailHost" runat="server" Width="200px" Font-Size="Small" MaxLength="50">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Mail Port : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                     <dx:ASPxSpinEdit ID="SpinEditMailPort" runat="server" DecimalPlaces="0" Width="200px" Font-Size="Small" MaxLength="10" NumberType="Integer" AllowNull="false" MinValue="0" MaxValue="9999999999" SpinButtons-ShowIncrementButtons="false">
                    </dx:ASPxSpinEdit>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Mail Kullanıcı Adı : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxMailKullaniciAdi" runat="server" Width="200px" Font-Size="Small" MaxLength="50">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Mail Şifre : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxMailSifre" runat="server" Width="200px" Font-Size="Small" MaxLength="50">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="TdOrtala">
                    <center>
                        <dx:ASPxButton ID="ButtonGuncelle" runat="server" Text="Güncelle" ValidationGroup="Guncelle" Width="120" Font-Size="Small"  OnClick="ButtonGuncelle_Click" />
                        <dx:ASPxButton ID="ButtonIptal" runat="server" Text="İptal" Width="120" Font-Size="Small" OnClick="ButtonIptal_Click" />
                    </center>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Yedekle" Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxButton ID="ButtonYedekle" runat="server" Text="Yedekle" Width="120" Font-Size="Small" OnClick="ButtonYedekle_Click" />
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Alınan Yedekler" Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <table border="1" style="margin:3px">
                        <asp:Repeater ID="RepeaterAlinanYedekler" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("Name") %>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonIndir" runat="server" OnClick="LinkButtonIndir_Click" Text="İndir" CommandArgument='<%# Eval("Name") %>'>
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButtonSil" runat="server" OnClick="LinkButtonSil_Click" Text="Sil" CommandArgument='<%# Eval("Name") %>'>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

