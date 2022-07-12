<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="Iletisim.aspx.cs" Inherits="BiSinavProject.PL.Iletisim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="panel">
        <div class="p-title">
            <h2>İLETİŞİM</h2>
        </div>
        <div class="shortcode-content">
            <div class="google-maps">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d7324.938177812432!2d33.93543675040419!3d35.13444247048252!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14dfc82f78818d4b%3A0x19d239f6ea88966f!2sSerbest+Liman+B%C3%B6lgesi%2C+Gazima%C4%9Fusa!5e0!3m2!1str!2str!4v1460211830061" width="800" height="600" frameborder="0" style="border: 0" allowfullscreen></iframe>
            </div>
            <p>
                <blockquote class="style-1">
                    <p>ForceWeb Global Communication LTD</p>
                    <p>KKTC Serbest Liman ve Bölge</p>
                    <p>SLBT No: 0542</p>
                    <p>Gazimağusa/KKTC</p>
                </blockquote>
            </p>
        </div>
    </div>
    <div class="panel">
        <div class="p-title">
            <h2>BİZE YAZIN</h2>
        </div>
        <div class="shortcode-content">
            <div id="writecomment">
                <p class="contact-form-user">
                    <asp:TextBox ID="TextBoxAdSoyad" runat="server" placeholder="Ad Soyad" name="c_name" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TextBoxAdSoyad" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationGroup="MesajGonder"></asp:RequiredFieldValidator>
                </p>
                <p class="contact-form-email">
                    <asp:TextBox ID="TextBoxEposta" runat="server" placeholder="E-posta" name="c_email" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TextBoxEposta" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationGroup="MesajGonder"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ControlToValidate="TextBoxEposta" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="MesajGonder"></asp:RegularExpressionValidator>
                </p>
                <p class="contact-form-website">
                    <asp:TextBox ID="TextBoxBaslik" runat="server" placeholder="Başlık" name="c_website" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TextBoxBaslik" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationGroup="MesajGonder"></asp:RequiredFieldValidator>
                </p>
                <p class="contact-form-website" style="width: 100%; margin-left: 0px; padding-left: 0px;">
                    <asp:TextBox ID="TextBoxMesaj" runat="server" placeholder="Mesajınız..." name="c_message" TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="TextBoxMesaj" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationGroup="MesajGonder"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="LabelSonuc" runat="server" Text="" ForeColor="Red"></asp:Label>
                </p>
                <p style="float: right">
                    <asp:Button ID="ButtonMesajGonder" runat="server" class="button" Text="Mesajınızı gönderin" OnClick="ButtonMesajGonder_Click" ValidationGroup="MesajGonder" />
                </p>

            </div>
            <p class="contact-form-website" style="visibility: hidden">
                <input type="text" placeholder="Website" name="c_website" id="c_website" />
            </p>
        </div>
    </div>
    <div itemscope itemtype="http://schema.org/Person" style="display: none;">
        <span itemprop="name">ForcewebGlobal</span>
        <span itemprop="company">ForcewebGlobal</span>
        <span itemprop="tel">bilgi@forcewebglobal.com</span>
        <a itemprop="email" href="mailto:iletisim@forcewebglobal.com">iletisim@forcewebglobal.com</a>
    </div>

</asp:Content>
