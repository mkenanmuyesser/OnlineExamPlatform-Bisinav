<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="Hatirlatma.aspx.cs" Inherits="BiSinavProject.PL.Kullanici.Hatirlatma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
    <div class="breaking-news">
        <div class="breaking-title">
            <h3>BİSINAV.NET</h3>
            <i></i>
        </div>
        <div class="breaking-block">
            &nbsp;
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <center>
    <div class="p-title">
        <h2>Şifre Hatırlatma</h2>
    </div>
    <div class="item">
        <table>
            <tr>
                <div class="gallery-categories">
                    <p>
                         Şifreniz kayıtlı olan e-posta adresinize gönderilecektir.
                    </p>                   
                  <p>
                    "Spam" ya da "Gereksiz" klasörünü kontrol etmeyi unutmayınız.
                      </p>
                </div>
            </tr>
            <tr>
                <td style="padding: 2px;">
                    <asp:TextBox ID="TextBoxEposta" runat="server" placeholder="E-posta adresiniz" Width="274" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxEposta" ErrorMessage=" * " ForeColor="Red" ValidationGroup="SifreGonder">*</asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ControlToValidate="TextBoxEposta" runat="server" ErrorMessage=" * " ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="SifreGonder"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="padding: 2px;">                    
                    <asp:Label ID="LabelSonuc" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding: 2px; float:right">
                    <asp:Button ID="ButtonSifreGonder" runat="server" class="button" Text="Şifre gönder" ValidationGroup="SifreGonder" OnClick="ButtonSifreGonder_Click" />
                </td>
            </tr>
        </table>
    </div>
   </center>
</asp:Content>

