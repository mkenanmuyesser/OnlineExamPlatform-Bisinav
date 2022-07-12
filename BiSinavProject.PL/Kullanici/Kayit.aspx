<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="Kayit.aspx.cs" Inherits="BiSinavProject.PL.Kullanici.Kayit" %>


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
        <h2>Yeni kullanıcı hesabı oluştur</h2>
    </div>   
        <div class="item">            
                <asp:CreateUserWizard runat="server" CancelDestinationPageUrl="~/Kayit.aspx" CompleteSuccessText="Kullanıcı hesabınız başarılı bir şekilde oluşturulmuştur." ConfirmPasswordCompareErrorMessage=" * " ConfirmPasswordLabelText="Şifre tekrar :" ConfirmPasswordRequiredErrorMessage=" * " ContinueDestinationPageUrl="~/Kayit.aspx" CreateUserButtonText="Kullanıcı Kayıt" EmailLabelText="E-posta :" EmailRegularExpressionErrorMessage=" * " EmailRequiredErrorMessage=" * " FinishCompleteButtonText="Bitir" FinishDestinationPageUrl="~/Kayit.aspx" PasswordLabelText="Şifre :" PasswordRegularExpressionErrorMessage="Lütfen farklı bir şifre giriniz." PasswordRequiredErrorMessage="Şifre gereklidir." UnknownErrorMessage="Kullanıcı hesabınız oluşturulamadı. Lütfen tekrar deneyiniz." UserNameLabelText="Kullanıcı adı :" UserNameRequiredErrorMessage=" * " DuplicateEmailErrorMessage="Bu e-posta adresi daha önce kullanılmıştır." DuplicateUserNameErrorMessage="Bu kullanıcı adı daha önce kullanılmıştır." ID="ctl00">
                    <WizardSteps>
                        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" Title="">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td style="padding: 2px;">
                                            <asp:TextBox ID="UserName" runat="server" placeholder="Kullanıcı adı" Width="274" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage=" * " ForeColor="Red" ValidationGroup="ctl00">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 2px;">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="   Şifre" Width="298" Height="28" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage=" * " ForeColor="Red" ValidationGroup="ctl00">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 2px;">
                                            <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" placeholder="   Şifre tekrar" Width="298" Height="28" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage=" * " ForeColor="Red" ValidationGroup="ctl00">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 2px;">
                                            <asp:TextBox ID="Email" runat="server" placeholder="E-posta" Width="274" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage=" * " ForeColor="Red" ValidationGroup="ctl00">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 2px;">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidate" runat="server" ControlToValidate="Email" Display="Dynamic" ForeColor="Red" ErrorMessage="Yanlış e-posta formatı" ValidationGroup="ctl00" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 2px;">
                                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="Şifreler eşleşmiyor" ValidationGroup="ctl00"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 2px; color: Red;">
                                            <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <CustomNavigationTemplate>
                                <table border="0" cellspacing="5" style="width: 100%; height: 100%; padding: 2px;">
                                    <tr align="right">
                                        <td align="right" colspan="0" style="padding: 2px;">
                                            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Oluştur" ValidationGroup="ctl00" class="button" />
                                        </td>
                                    </tr>
                                </table>
                            </CustomNavigationTemplate>
                        </asp:CreateUserWizardStep>
                        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server" Title="">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td style="padding: 2px;">Kullanıcı hesabınız başarılı bir şekilde oluşturulmuştur.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="Devam et" class="button" PostBackUrl="/Default.aspx" ValidationGroup="ctl00" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:CompleteWizardStep>
                    </WizardSteps>
                </asp:CreateUserWizard>            
        </div>                   
    </center>
</asp:Content>
