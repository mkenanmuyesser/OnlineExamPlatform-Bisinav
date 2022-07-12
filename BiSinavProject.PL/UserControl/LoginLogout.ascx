<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginLogout.ascx.cs" Inherits="BiSinavProject.PL.UserControl.LoginLogout" %>

<div class="widget">
    <div class="w-title tab-a">
        <h3>KULLANICI GİRİŞİ</h3>
    </div>
    <div>
        <asp:LoginView runat="server" ID="LoginViewGirisCikis">
            <AnonymousTemplate>
                <center>
                     <div>
            <asp:HyperLink ID="HyperLinkFacebookGiris" runat="server" Text="Facebook İle Giriş Yap" ImageUrl="/Content/Images/Icons/Facebook/3.png"></asp:HyperLink>
                       <p>
                           <br />
                       </p>
        </div>
                    <div style="display: table; margin: 0px auto; width: 100%">
                        <asp:Login runat="server" FailureText="Giriş yapılamadı. Lütfen tekrar deneyiniz." ValidatorTextStyle-ForeColor="Red" FailureTextStyle-ForeColor="Red">
                            <LayoutTemplate>
                                <table style="width:100%" >
                                    <tr>
                                        <td style="padding:2px;">
                                            <asp:TextBox ID="UserName" runat="server" placeholder="Kullanıcı adı"  Width="274" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage=" * " ForeColor="Red" ToolTip=" * " ValidationGroup="ctl00$ctl01$ctl02">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  style="padding:2px;">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="   Şifre" Width="298" Height="28" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage=" * " ForeColor="Red" ToolTip=" * " ValidationGroup="ctl00$ctl01$ctl02">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  style="padding:2px;">
                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Beni hatırla" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="color: Red;padding:2px;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right"  style="padding:2px;">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Giriş" class="button" ValidationGroup="ctl00$ctl01$ctl02" Width="100"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:2px;">
                                              <asp:HyperLink ID="PasswordRecoveryLink" runat="server" NavigateUrl="~/Kullanici/Hatirlatma.aspx">Şifre hatırlatma</asp:HyperLink>
                                        </td>
                                    </tr>
                                  <%--  <tr>
                                        <td style="padding:2px;">
                                           <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/Kullanici/Kayit.aspx"> Yeni kullanıcı hesabı oluştur</asp:HyperLink>
                                        </td>
                                    </tr>--%>
                                </table>
                            </LayoutTemplate>
                        </asp:Login>
                    </div>
                        </center>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <div style="display: table; margin: 0px auto; width: 98%;">
                    <table style="border: 1px dotted black; margin: 5px; padding: 15px; width: 100%;">
                        <tr>
                            <td style="padding: 5px; margin: 5px;">Hoşgeldiniz
                            <asp:LoginName runat="server" />
                            </td>
                        </tr>
                        <tr>

                            <td style="padding: 5px; margin: 5px;">Kullanıcı detay sayfanıza gitmek için
                                   <asp:LinkButton runat="server" PostBackUrl="~/Kullanici/Detay.aspx" ForeColor="#ff3300" Font-Underline="true">tıklayınız</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 5px; margin: 5px;">Çıkış yapmak için
                                    <asp:LoginStatus runat="server" LogoutText="tıklayınız" ForeColor="#ff3300" Font-Underline="true" />
                            </td>
                        </tr>
                    </table>
                </div>
            </LoggedInTemplate>
        </asp:LoginView>
    </div>
</div>
