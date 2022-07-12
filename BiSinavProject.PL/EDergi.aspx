<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="EDergi.aspx.cs" Inherits="BiSinavProject.PL.E_Dergi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="p-title">
        <h2>E-Dergiler ve E-Kitaplar</h2>
    </div>
    <div class="panel">
        <div class="gallery-categories">
            Sınava Hazırlıkta Yeni Trend
        </div>
        <div class="photo-gallery-blocks">
            <div class="article-list">
                <asp:Repeater ID="RepeaterEDergiler" runat="server" OnItemDataBound="RepeaterEDergiler_ItemDataBound">
                    <ItemTemplate>
                        <div class="item main-artice light">
                            <div class="item-header">
                                <asp:LinkButton ID="LinkButtonGiris" runat="server" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("EDergiKey") %>'>
                                               <img src='/Uploads/Program/<%# Eval("Resim") %>' alt="" /></a>
                                </asp:LinkButton>
                                <div class="article-slide">
                                    <asp:LinkButton CssClass="info-line" ID="LinkButtonAltGiris" runat="server" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("EDergiKey") %>'>
                                        <asp:Label ID="LabelTanim" runat="server"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="item-content">
                                <center>
                                <h2>                                    
                                    <asp:Label runat="server" Text='<%# Eval("Adi") %>' Font-Bold="True" Font-Size="Large" />
                                </h2>
                                    </center>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
