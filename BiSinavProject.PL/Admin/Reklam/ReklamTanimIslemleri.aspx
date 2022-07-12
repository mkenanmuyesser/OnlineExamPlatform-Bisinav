﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMain.master" AutoEventWireup="true" CodeBehind="ReklamTanimIslemleri.aspx.cs" Inherits="BiSinavProject.PL.Program.Reklam.ReklamTanimIslemleri" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div style="width: 100%;">
        <table style="border-color: #999999; width: 100%;" border="1">
            <tr>
                <td class="TdOrtala" colspan="4">
                    <center>
                        <dx:ASPxLabel ID="LabelBaslik" runat="server" Font-Size="Large" />
                    </center>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Bölge Adı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxComboBox ID="ComboBoxBolgeTipAdi" runat="server" ValueType="System.Int32" Width="200px" Font-Size="Small" TextField="Aciklama" ValueField="BolgeTipKey">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Açıklama : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxAciklama" runat="server" Width="200px" Font-Size="Small" MaxLength="100">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Başlangıç Tarih : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                <dx:ASPxDateEdit ID="DateEditBaslangicTarih" runat="server" Width="200px" Font-Size="Small" DisplayFormatString="dd.MM.yyyy HH:mm" EditFormatString="dd.MM.yyyy HH:mm" TimeSectionProperties-Visible="true">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Bitiş Tarih : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                  <dx:ASPxDateEdit ID="DateEditBitisTarih" runat="server" Width="200px" Font-Size="Small" DisplayFormatString="dd.MM.yyyy HH:mm" EditFormatString="dd.MM.yyyy HH:mm" TimeSectionProperties-Visible="true">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Link : " Font-Size="Small"/>
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxLink" runat="server" Width="200px" Font-Size="Small" MaxLength="200">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Sıralama : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxSpinEdit ID="SpinEditKursSiralama" runat="server" DecimalPlaces="0" Width="200px" Font-Size="Small" MaxLength="4" NumberType="Integer" AllowNull="false" MinValue="0" MaxValue="9999" SpinButtons-ShowIncrementButtons="false">
                    </dx:ASPxSpinEdit>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Aktif : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <asp:CheckBox ID="CheckBoxAktif" runat="server" Checked="true" />
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Resim : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxUploadControl ID="UploadControlDosya" ClientInstanceName="UploadControlDosya" runat="server" ShowUploadButton="false" AutoStartUpload="True" Width="410"
                        ShowProgressPanel="True" OnFileUploadComplete="UploadControl_FileUploadComplete">
                        <ValidationSettings MaxFileSize="10485760" AllowedFileExtensions=".jpg, .jpeg, .gif, .png" NotAllowedFileExtensionErrorText="Lütfen resim dosyası yükleyiniz!" MaxFileSizeErrorText="Yüksek dosya boyutu!" GeneralErrorText="Bilinmeyen bir hata oluştu!">
                        </ValidationSettings>
                        <BrowseButton Text="Resim dosyası yükleyiniz. (10Mb)" />
                        <UploadButton Text="Yükle"></UploadButton>
                    </dx:ASPxUploadControl>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="TdOrtala">
                    <center>
                        <dx:ASPxButton ID="ButtonKaydet" runat="server" Text="Kaydet" ValidationGroup="KayitGuncelle" Width="120" Font-Size="Small" Visible="false" OnClick="ButtonKaydetGuncelle_Click" />
                        <dx:ASPxButton ID="ButtonGuncelle" runat="server" Text="Güncelle" ValidationGroup="KayitGuncelle" Width="120" Font-Size="Small" Visible="false" OnClick="ButtonKaydetGuncelle_Click" />
                        <dx:ASPxButton ID="ButtonTemizle" runat="server" Text="Temizle" Width="120" Font-Size="Small" OnClick="ButtonIptalTemizle_Click" />
                        <dx:ASPxButton ID="ButtonIptal" runat="server" Text="İptal" Width="120" Font-Size="Small" OnClick="ButtonIptalTemizle_Click" />
                    </center>
                </td>
            </tr>
            <tr>
                <td class="TdOrtala" colspan="4">
                    <dx:ASPxGridView ID="GridViewTanim" runat="server" KeyFieldName="TanitimKey" AutoGenerateColumns="false" Width="100%" EnableCallBacks="true" Font-Size="Small" OnRowDeleting="GridViewTanim_RowDeleting" OnCustomButtonCallback="GridViewTanim_CustomButtonCallback">
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
                            <dx:GridViewDataTextColumn FieldName="BolgeTip.BolgeTipAdi" Caption="Bölge Tip Adı">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Aciklama" Caption="Açıklama">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="BaslangicTarih" Caption="Başlangıç Tarih" PropertiesTextEdit-DisplayFormatString="dd.MM.yyyy HH:mm">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="BitisTarih" Caption="Bitiş Tarih" PropertiesTextEdit-DisplayFormatString="dd.MM.yyyy HH:mm">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Link" Caption="Link">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Sira" Caption="Sıralama">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                          <dx:GridViewDataColumn Caption="Resim">
                               <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                              <DataItemTemplate>
                                  <img src='../../Uploads/Program/<%# Eval("Resim") %>' width="30" height="30"/>
                              </DataItemTemplate>
                          </dx:GridViewDataColumn>
                            <dx:GridViewDataCheckColumn FieldName="AktifMi" Caption="Aktif">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataCheckColumn>
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
    </div>
</asp:Content>

