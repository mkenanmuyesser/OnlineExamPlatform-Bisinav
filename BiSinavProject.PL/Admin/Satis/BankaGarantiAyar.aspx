<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMain.master" AutoEventWireup="true" CodeBehind="BankaGarantiAyar.aspx.cs" Inherits="BiSinavProject.PL.Program.Satis.BankaGarantiAyar" %>

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
                    <dx:ASPxLabel runat="server" Text="StoreKey : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxStoreKey" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="CompanyName : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxCompanyName" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="ProvisionPassword : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxProvisionPassword" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="TerminalId : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxTerminalId" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="TerminalUserId : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxTerminalUserId" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="TerminalMerchantId : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxTerminalMerchantId" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                    </dx:ASPxTextBox>
                </td>
            </tr>
             <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="CustomerIpAddress : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxCustomerIpAddress" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="CustomerEmailAddress : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxCustomerEmailAddress" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
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
        </table>
    </div>
</asp:Content>

