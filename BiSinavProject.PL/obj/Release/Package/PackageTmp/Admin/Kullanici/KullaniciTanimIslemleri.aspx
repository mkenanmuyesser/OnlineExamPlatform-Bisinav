<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMain.master" AutoEventWireup="true" CodeBehind="KullaniciTanimIslemleri.aspx.cs" Inherits="BiSinavProject.PL.Program.Kullanici.KullaniciTanimIslemleri" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div style="width: 99.9%; margin: 1px;">
        <table style="border-color: #999999; width: 99.9%;" border="1">
            <tr>
                <td class="TdOrtala">
                    <center>
                        <dx:ASPxLabel ID="LabelBaslik" runat="server" Font-Size="Large" />
                    </center>
                </td>
            </tr>
            <tr>
                <td class="TdOrtala">
                    <dx:ASPxPageControl ID="PageControlKullaniciBilgi" runat="server" ActiveTabIndex="0" AutoPostBack="false" EnableCallBacks="true" ShowLoadingPanel="true" Width="99.9%" Font-Size="Small" EnableCallbackAnimation="True" LoadingPanelText="Yükleniyor&hellip;">
                        <TabPages>
                            <dx:TabPage Text="Yönetici Kullanıcı Bilgileri">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server">
                                        <table style="border-color: #999999; width: 100%;" border="1">
                                            <tr>
                                                <td class="TdSol" style="width: 15%;">
                                                    <dx:ASPxLabel runat="server" Text="Kullanıcı Adı : " Font-Size="Small" ForeColor="Red" />
                                                </td>
                                                <td class="TdSag" style="width: 35%;">
                                                    <dx:ASPxTextBox ID="TextBoxKullaniciAdi" runat="server" Width="200px" Font-Size="Small" MaxLength="250">
                                                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                                                            <RequiredField ErrorText=" " IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td class="TdSol" style="width: 15%;">
                                                    <dx:ASPxLabel runat="server" Text="Şifre : " Font-Size="Small" ForeColor="Red" />
                                                </td>
                                                <td class="TdSag" style="width: 35%;">
                                                    <dx:ASPxTextBox ID="TextBoxSifre" runat="server" Width="200px" Font-Size="Small" MaxLength="10">
                                                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                                                            <RequiredField ErrorText=" " IsRequired="true" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdSol" style="width: 15%;">
                                                    <dx:ASPxLabel runat="server" Text="Aktif : " Font-Size="Small" ForeColor="Red" />
                                                </td>
                                                <td class="TdSag" style="width: 35%;">
                                                    <asp:CheckBox ID="CheckBoxAktif" runat="server" CssClass="mycheckBig" Checked="true" />
                                                </td>
                                                <td class="TdSol" style="width: 15%;"></td>
                                                <td class="TdSag" style="width: 35%;"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="TdOrtala">
                                                    <div id="divIslemler" runat="server" style="margin-left: 40%; float: left;">
                                                        <dx:ASPxButton ID="ButtonKaydet" runat="server" Text="Kaydet" ValidationGroup="KayitGuncelle" Width="120" Font-Size="Small" Visible="false" OnClick="ButtonKaydetGuncelle_Click" />
                                                        <dx:ASPxButton ID="ButtonGuncelle" runat="server" Text="Güncelle" ValidationGroup="KayitGuncelle" Width="120" Font-Size="Small" Visible="false" OnClick="ButtonKaydetGuncelle_Click" />
                                                        <dx:ASPxButton ID="ButtonTemizle" runat="server" Text="Temizle" Width="120" Font-Size="Small" OnClick="ButtonIptalTemizle_Click" />
                                                        <dx:ASPxButton ID="ButtonIptal" runat="server" Text="İptal" Width="120" Font-Size="Small" OnClick="ButtonIptalTemizle_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TdOrtala" colspan="4">
                                                    <dx:ASPxGridViewExporter ExportedRowType="All" GridViewID="GridViewKullaniciIslem" ID="GridViewExporterKullaniciIslem" runat="server">
                                                    </dx:ASPxGridViewExporter>
                                                    <dx:ASPxGridView ID="GridViewYoneticiKullaniciIslem" runat="server" KeyFieldName="UserId" DataMember="UserName" AutoGenerateColumns="false" Width="100%" EnableCallBacks="true" Font-Size="Small" OnRowDeleting="GridViewYoneticiKullaniciIslem_RowDeleting" OnCustomButtonCallback="GridViewYoneticiKullaniciIslem_CustomButtonCallback">
                                                        <ClientSideEvents EndCallback="function(s, e) {
	                                                                        if (s.cpErrorMessage){
                                                                            delete s.cpErrorMessage;
		                                                                        alert('Silinmek istenen veri diğer kısımlarda kullanılmaktadır!');
                                                                                }
                                                                       }" />
                                                        <Settings ShowGroupPanel="false" />
                                                        <SettingsText ConfirmDelete="Silme işlemini onaylıyor musunuz?" />
                                                        <SettingsBehavior AllowDragDrop="false" AllowFocusedRow="false" ConfirmDelete="true" />
                                                        <SettingsPager Visible="true" PageSize="15" AlwaysShowPager="true" Position="Bottom" ShowEmptyDataRows="true" />
                                                        <Paddings Padding="0px" />
                                                        <Border BorderWidth="0px" />
                                                        <BorderBottom BorderWidth="1px" />
                                                        <Columns>
                                                            <dx:GridViewDataCheckColumn FieldName="aspnet_Membership.IsApproved" Caption="Aktif">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataCheckColumn>
                                                            <dx:GridViewDataTextColumn FieldName="UserName" Caption="Kullanıcı Adı">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="aspnet_Membership.Password" Caption="Şifre">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <DataItemTemplate>
                                                                    ***
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn FieldName="aspnet_Membership.CreateDate" Caption="Oluşturma Tarihi">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataDateColumn FieldName="aspnet_Membership.LastLoginDate" Caption="Son Giriş Tarihi">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewCommandColumn Caption="İşlemler" ButtonType="Image">
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton Visibility="AllDataRows" Image-ToolTip="Düzenle" Image-Url="../../../Content/Images/Icons/edit.png" Image-Width="24" Image-Height="24">
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                                <DeleteButton Visible="true" Image-ToolTip="Sil" Image-Url="../../../Content/Images/Icons/delete.png" Image-Width="24" Image-Height="24"></DeleteButton>
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Program Kullanıcı Bilgileri">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server">
                                        <table style="border-color: #999999; width: 100%;" border="1">
                                            <tr>
                                                <td class="TdOrtala" colspan="4">
                                                    <dx:ASPxGridViewExporter ExportedRowType="All" GridViewID="GridViewKullaniciIslem" ID="ASPxGridViewExporter1" runat="server">
                                                    </dx:ASPxGridViewExporter>
                                                    <dx:ASPxGridView ID="GridViewProgramKullaniciIslem" runat="server" KeyFieldName="UserId" DataMember="UserName" AutoGenerateColumns="false" Width="100%" EnableCallBacks="true" Font-Size="Small" OnRowDeleting="GridViewProgramKullaniciIslem_RowDeleting">
                                                        <ClientSideEvents EndCallback="function(s, e) {
	                                                                        if (s.cpErrorMessage){
                                                                            delete s.cpErrorMessage;
		                                                                        alert('Silinmek istenen veri diğer kısımlarda kullanılmaktadır!');
                                                                                }
                                                                       }" />
                                                        <Settings ShowGroupPanel="false" />
                                                        <SettingsText ConfirmDelete="Silme işlemini onaylıyor musunuz?" />
                                                        <SettingsBehavior AllowDragDrop="false" AllowFocusedRow="false" ConfirmDelete="true" />
                                                        <SettingsPager Visible="true" PageSize="15" AlwaysShowPager="true" Position="Bottom" ShowEmptyDataRows="true" />
                                                        <Paddings Padding="0px" />
                                                        <Border BorderWidth="0px" />
                                                        <BorderBottom BorderWidth="1px" />
                                                        <Columns>
                                                            <dx:GridViewDataCheckColumn FieldName="aspnet_Membership.IsApproved" Caption="Aktif">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataCheckColumn>
                                                            <dx:GridViewDataTextColumn FieldName="UserName" Caption="Kullanıcı Adı">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="aspnet_Membership.Email" Caption="E-posta">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="aspnet_Membership.Password" Caption="Şifre">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <DataItemTemplate>
                                                                    ***
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn FieldName="aspnet_Membership.CreateDate" Caption="Oluşturma Tarihi">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataDateColumn FieldName="aspnet_Membership.LastLoginDate" Caption="Son Giriş Tarihi">
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewCommandColumn Caption="İşlemler" ButtonType="Image">
                                                                <DeleteButton Visible="true" Image-ToolTip="Sil" Image-Url="../../../Content/Images/Icons/delete.png" Image-Width="24" Image-Height="24"></DeleteButton>
                                                                <CellStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

