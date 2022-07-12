<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="BesteBesSinav.aspx.cs" Inherits="BiSinavProject.PL.BesteBesSinav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="p-title">
        <h2>Beşte Beş Yapma Zamanı</h2>
    </div>
    <div class="panel">
        <div class="gallery-categories">
            Binlerce Soruyla Sınava Sıkılmadan Hazırlanın
        </div>
        <div class="gallery-categories">
            <asp:Repeater ID="RepeaterAlan" runat="server">
                <ItemTemplate>
                    <a href='BesteBesSinav.aspx?P=<%# Eval("Key") %>' <%# Convert.ToBoolean(Eval("Secim"))?"class='active'":""%>><%# Eval("Adi") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="photo-gallery-blocks">
            <div class="article-list">
                <asp:Repeater ID="RepeaterDersler" runat="server">
                    <ItemTemplate>
                        <div class="item main-artice light">
                            <div class="item-header">
                                <asp:LinkButton ID="LinkButtonGiris" runat="server" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("DersKey") %>'>
                                               <img src='/Uploads/Program/<%# Eval("Resim") %>' alt="" /></a>
                                </asp:LinkButton>
                                <div class="article-slide">
                                    <asp:LinkButton ID="LinkButtonAltGiris" runat="server" CssClass="info-line" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("DersKey") %>'>
                                          <span>TIKLA-BAŞLA</span>     
                                    </asp:LinkButton>
                                </div>
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
</asp:Content>
