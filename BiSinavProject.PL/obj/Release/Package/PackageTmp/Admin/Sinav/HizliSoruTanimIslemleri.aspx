<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMain.master" AutoEventWireup="true" CodeBehind="HizliSoruTanimIslemleri.aspx.cs" Inherits="BiSinavProject.PL.Program.Sinav.HizliSoruTanimIslemleri" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <style type="text/css">
        #myDiv {
            height: 104px;
            width: 140px;
        }

        #myDiv img {
                max-width: 100%;
                max-height: 100%;
                margin: auto;
                display: block;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
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
                    <dx:ASPxLabel runat="server" Text="Alan Adı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxComboBox ID="ComboBoxAlanAdi" runat="server" ValueType="System.Int32" Width="200px" Font-Size="Small" TextField="Adi" ValueField="AlanKey" OnSelectedIndexChanged="ComboBoxAlanAdi_SelectedIndexChanged" AutoPostBack="true">
                        <ValidationSettings ValidationGroup="Olustur" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Ders Adı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxComboBox ID="ComboBoxDersAdi" runat="server" ValueType="System.Int32" Width="200px" Font-Size="Small" TextField="Adi" ValueField="DersKey">
                        <ValidationSettings ValidationGroup="Olustur" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Deneme Adı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="ComboBoxDenemeAdi" runat="server" ValueType="System.Int32" Width="200px" Font-Size="Small" TextField="Adi" ValueField="DenemeKey" AutoPostBack="true" OnSelectedIndexChanged="ComboBoxDenemeAdi_SelectedIndexChanged">
                                    <ValidationSettings ValidationGroup="Olustur" Display="Dynamic">
                                        <RequiredField ErrorText=" " IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Şık Sayısı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxComboBox ID="ComboBoxSikSayisi" runat="server" ValueType="System.String" Width="200px" Font-Size="Small">
                        <ValidationSettings ValidationGroup="Olustur" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Sorular : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <dx:ASPxUploadControl ID="UploadControlSorular" ClientInstanceName="UploadControlSorular" runat="server"
                                    ShowUploadButton="true" UploadMode="Advanced"
                                    AdvancedModeSettings-EnableMultiSelect="true" ShowProgressPanel="True"
                                    Width="410" OnFilesUploadComplete="UploadControlSorular_FilesUploadComplete">
                                    <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg, .jpeg, .gif, .png" NotAllowedFileExtensionErrorText="Lütfen resim dosyası yükleyiniz!" MaxFileSizeErrorText="Yüksek dosya boyutu!" GeneralErrorText="Bilinmeyen bir hata oluştu!">
                                    </ValidationSettings>
                                    <BrowseButton Text="Resim dosyalarını seçiniz. (100Mb)" />
                                    <UploadButton Text="Yükle"></UploadButton>
                                </dx:ASPxUploadControl>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Açıklamalar : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <dx:ASPxUploadControl ID="UploadControlAciklamalar" ClientInstanceName="UploadControlAciklamalar" runat="server"
                                    ShowUploadButton="true" UploadMode="Advanced"
                                    AdvancedModeSettings-EnableMultiSelect="true" ShowProgressPanel="True"
                                    Width="410" OnFilesUploadComplete="UploadControlAciklamalar_FilesUploadComplete">
                                    <ValidationSettings MaxFileSize="104857600" AllowedFileExtensions=".jpg, .jpeg, .gif, .png" NotAllowedFileExtensionErrorText="Lütfen resim dosyası yükleyiniz!" MaxFileSizeErrorText="Yüksek dosya boyutu!" GeneralErrorText="Bilinmeyen bir hata oluştu!">
                                    </ValidationSettings>
                                    <BrowseButton Text="Resim dosyalarını seçiniz. (100Mb)" />
                                    <UploadButton Text="Yükle"></UploadButton>
                                </dx:ASPxUploadControl>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Cevaplar : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxUploadControl ID="UploadControlCevaplar" ClientInstanceName="UploadControlCevaplar" runat="server" ShowUploadButton="false" AutoStartUpload="True" Width="410" AdvancedModeSettings-EnableMultiSelect="false"
                                    ShowProgressPanel="True" OnFileUploadComplete="UploadControlCevaplar_FileUploadComplete">
                                    <ValidationSettings MaxFileSize="10485760" AllowedFileExtensions=".xlsx, .xls" NotAllowedFileExtensionErrorText="Lütfen excel dosyası yükleyiniz!" MaxFileSizeErrorText="Yüksek dosya boyutu!" GeneralErrorText="Bilinmeyen bir hata oluştu!">
                                    </ValidationSettings>
                                    <BrowseButton Text="Excel dosyası yükleyiniz. (10Mb)" />
                                    <UploadButton Text="Yükle"></UploadButton>
                                </dx:ASPxUploadControl>
                            </td>
                            <td style="padding-left: 5px;">
                                <a href="/Data/CevaplarOrnekExcel.xlsx">örnek dosya</a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="TdSol" style="width: 15%;"></td>
                <td class="TdSag" style="width: 35%;"></td>
            </tr>
            <tr>
                <td colspan="4" class="TdOrtala">
                    <center>
                        <dx:ASPxButton ID="ButtonOlustur" runat="server" Text="Oluştur" ValidationGroup="Olustur" Width="120" Font-Size="Small" OnClick="ButtonOlustur_Click" />               
                        <dx:ASPxButton ID="ButtonHepsiniSil" runat="server" Text="Hepsini Sil" ValidationGroup="Olustur" Width="120" Font-Size="Small" OnClick="ButtonHepsiniSil_Click" />
                        <dx:ASPxButton ID="ButtonIptal" runat="server" Text="Temizle" Width="120" Font-Size="Small" OnClick="ButtonIptal_Click" />
                    </center>
                </td>
            </tr>
            <tr>
                <td class="TdOrtala" colspan="4">
                    <dx:ASPxGridView ID="GridViewSorular" ClientInstanceName="GridViewSorular" runat="server" KeyFieldName="SoruKey" AutoGenerateColumns="false" Width="100%" Font-Size="Small" OnCustomButtonCallback="GridViewSorular_CustomButtonCallback">
                        <Settings ShowGroupPanel="false" />
                        <SettingsBehavior AllowDragDrop="true" AllowFocusedRow="false" ConfirmDelete="false" AutoExpandAllGroups="false" />
                        <SettingsPager Visible="true" PageSize="20" AlwaysShowPager="true" Position="Bottom" ShowEmptyDataRows="true" />
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />
                        <BorderBottom BorderWidth="1px" />
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="Sira" Caption="Soru No" Width="10%">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn Caption="Soru">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <DataItemTemplate>
                                    <div id="myDiv">
                                        <%# Eval("Soru1") %>
                                    </div>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                             <dx:GridViewDataColumn Caption="Açıklama">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <DataItemTemplate>
                                    <div id="myDiv">
                                        <%# Eval("Aciklama") %>
                                    </div>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataCheckColumn FieldName="AktifMi" Caption="Aktif" Width="5%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewCommandColumn Caption="İşlemler" ButtonType="Image" Width="10%">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton Visibility="AllDataRows" Image-ToolTip="Düzenle" Image-Url="../../../Content/Images/Icons/edit.png" Image-Width="24" Image-Height="24">
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>                              
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

