<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/AdminRoot.master" CodeBehind="Admin.aspx.cs" Inherits="BiSinavProject.PL.Admin" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <style type="text/css">
        .HeaderStyle {
            margin: 0px auto;
            font-size: 16px;
        }

        .ButtonStyle {
            margin: 20px 0px 0px 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
         <center>
    <div style="margin: 10% 0px 0px 0px; width:320px;">
        <table class="HeaderStyle">
            <tr>
                <td colspan="2">
                     <center>
                    <div>
                        <p>
                            <dx:ASPxLabel ID="LabelGiris" runat="server" Text="BÝSINAV.net Yönetim Paneli" Font-Size="Large"  />
                        </p>
                    </div>
                         </center>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="border: none;">
                    <dx:ASPxLabel ID="LabelKullaniciAdi" runat="server" AssociatedControlID="TextBoxKullaniciAdi" Text="Kullanýcý Adý : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag">
                    <dx:ASPxTextBox ID="TextBoxKullaniciAdi" runat="server" Width="200px" Font-Size="Small">
                        <ValidationSettings ValidationGroup="GirisDogrulama" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="border: none;">
                    <dx:ASPxLabel ID="LabelSifre" runat="server" AssociatedControlID="TextBoxSifre" Text="Þifre : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag">
                    <dx:ASPxTextBox ID="TextBoxSifre" runat="server" Password="true" Width="200px" Font-Size="Small">
                        <ValidationSettings ValidationGroup="GirisDogrulama" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="ButtonStyle">
                        <center>
                        <dx:ASPxButton ID="ButtonGiris" runat="server" Text="Giriþ" ValidationGroup="GirisDogrulama" Width="100" Font-Size="Small"
                            OnClick="ButtonGiris_Click">
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="ButtonAnasayfa" runat="server" Text="Ana Sayfa"  Width="100" Font-Size="Small"
                            OnClick="ButtonAnasayfa_Click">
                        </dx:ASPxButton>
                            </center>
                    </div>
                </td>
            </tr>
        </table>
    </div>     
         </center>        
</asp:Content>
