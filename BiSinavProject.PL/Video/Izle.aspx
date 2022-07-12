<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="Izle.aspx.cs" Inherits="BiSinavProject.PL.Video.Izle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="p-title">
        <h2>
            <asp:Literal ID="LiteralBaslik" runat="server"></asp:Literal>
        </h2>

    </div>
    <div class="panel">
        <br />
        <blockquote class="style-3">
            <p>
                <asp:Literal ID="LiteralAciklama" runat="server"></asp:Literal>
            </p>
        </blockquote>
        <br />
        <div class="shortcode-content">
            <div class="paragraph-row">
                <div class="column12">
                    <div class="video-container">
                        <asp:Literal ID="LiteralVideo" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
