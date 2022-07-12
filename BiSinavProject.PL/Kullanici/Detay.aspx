<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="Detay.aspx.cs" Inherits="BiSinavProject.PL.Kullanici.Detay" %>

<%@ Register Src="~/UserControl/UserReport.ascx" TagPrefix="uc1" TagName="UserReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="p-title">
        <h2>Kullanıcı Detay Bilgileriniz</h2>
    </div>
    <div>
        <uc1:UserReport runat="server" ID="UserReport" />
    </div>
</asp:Content>

