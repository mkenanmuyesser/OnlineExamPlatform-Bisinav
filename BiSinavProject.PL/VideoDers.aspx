<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="VideoDers.aspx.cs" Inherits="BiSinavProject.PL.VideoDers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="p-title">
        <h2>Video Dersler İle Geleceğinizi İnşa Edin</h2>
    </div>
    <div class="panel">
         <div class="gallery-categories">
            Tamamen Ücretsiz
        </div>
        <div class="gallery-categories">
            <blockquote class="style-2">
                <p>
                    Keyifle İzleyeceğiniz Eğlenceli Anlatımlar Burada!
                </p>
            </blockquote>
        </div>
        <div class="gallery-categories">
            <asp:Repeater ID="RepeaterKayitKategori" runat="server">
                <ItemTemplate>
                    <a href='VideoDers.aspx?K=<%# Eval("Key") %>' <%# Convert.ToBoolean(Eval("Secim"))?"class='active'":""%>><%# Eval("Adi") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="photo-gallery-blocks">
            <div class="article-list">
                <asp:Repeater ID="RepeaterKayitlar" runat="server" OnItemDataBound="RepeaterKayitlar_ItemDataBound">
                    <ItemTemplate>
                        <div class="item main-artice light">
                            <div class="item-header">
                                <asp:LinkButton ID="LinkButtonGiris" runat="server" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("KayitKey") %>'>
                                               <img src='/Uploads/Program/<%# Eval("Resim") %>' alt="" /></a>
                                </asp:LinkButton>
                                <div class="article-slide">
                                    <asp:LinkButton CssClass="info-line" ID="LinkButtonAltGiris" runat="server" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("KayitKey") %>'>
                                        <asp:Label ID="LabelTanim" runat="server"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="item-content">
                                <center>
                                <h2>                                    
                                    <asp:Label runat="server" Text='<%# Eval("Baslik") %>' Font-Bold="True" Font-Size="Large" />
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
