<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="DenemeSinav.aspx.cs" Inherits="BiSinavProject.PL.DenemeSinav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="p-title">
        <h2>Canlı Denemelere Katılın Gerçek Derecenizi Öğrenin</h2>
    </div>
    <div class="panel">
        <div class="gallery-categories">
            Sınav Formatında Sorularla Gerçek Sınav Deneyimi
        </div>
        <div class="gallery-categories">
            <blockquote class="style-2">
                <p>
                    Sınavınızın sorunsuz gerçekleşebilmesi amacıyla özel bir server sistemi ve yazılım programı kullanılmaktadır. 
                    Bu program her bir işleminizi otomatik olarak kaydetmektedir. 
                    Böylelikle kendi internet bağlantınızda bir kopma yaşamanız halinde dahi, tekrar bağlandığınızda sınavınıza kaldığınız yerden devam edebilirsiniz.  
                </p>
            </blockquote>
        </div>
        <div class="gallery-categories">
            <asp:Repeater ID="RepeaterAlan" runat="server">
                <ItemTemplate>
                    <a href='DenemeSinav.aspx?P=<%# Eval("Key") %>' <%# Convert.ToBoolean(Eval("Secim"))?"class='active'":""%>><%# Eval("Adi") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="photo-gallery-blocks">
            <div class="article-list">
                <asp:Repeater ID="RepeaterDenemeler" runat="server" OnItemDataBound="RepeaterDenemeler_ItemDataBound">
                    <ItemTemplate>
                        <div class="item main-artice light">
                            <div class="item-header">
                                <asp:LinkButton ID="LinkButtonGiris" runat="server" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("DenemeKey") %>'>
                                               <img src='/Uploads/Program/<%# Eval("Resim") %>' alt="" /></a>
                                </asp:LinkButton>
                                <div class="article-slide">                        
                                     <asp:LinkButton CssClass="info-line" ID="LinkButtonAltGiris" runat="server" OnClick="LinkButtonGiris_Click" CommandArgument='<%# Eval("DenemeKey") %>'>
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
