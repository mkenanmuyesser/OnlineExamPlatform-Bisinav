<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMain.master" AutoEventWireup="true" CodeBehind="ServisTanimIslemleri.aspx.cs" Inherits="BiSinavProject.PL.Program.KodPaylasim.ServisTanimIslemleri" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script type="text/javascript">
        var textSeparator = ",";

        function OnListBoxDenemelerSelectionChanged(listBox, args) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemStateDenemeler();
            UpdateTextDenemeler();
        }

        function UpdateSelectAllItemStateDenemeler() {
            IsAllSelectedDenemeler() ? ListBoxDenemeler.SelectIndices([0]) : ListBoxDenemeler.UnselectIndices([0]);
        }

        function IsAllSelectedDenemeler() {
            var selectedDataItemCount = ListBoxDenemeler.GetItemCount() - (ListBoxDenemeler.GetItem(0).selected ? 0 : 1);
            return ListBoxDenemeler.GetSelectedItems().length == selectedDataItemCount;
        }

        function UpdateTextDenemeler() {
            var selectedItems = ListBoxDenemeler.GetSelectedItems();
            DropDownEditDenemeler.SetText(GetSelectedItemsText(selectedItems));
        }

        function SynchronizeListBoxDenemelerValues(dropDown, args) {
            ListBoxDenemeler.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTextsDenemeler(texts);
            ListBoxDenemeler.SelectValues(values);
            UpdateSelectAllItemStateDenemeler();
            UpdateTextDenemeler(); 
        }

        function GetValuesByTextsDenemeler(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = ListBoxDenemeler.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

        //-----------------------------------------------------

        function OnListBoxEDergilerSelectionChanged(listBox, args) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemStateEDergiler();
            UpdateTextEDergiler();
        }

        function UpdateSelectAllItemStateEDergiler() {
            IsAllSelectedEDergiler() ? ListBoxEDergiler.SelectIndices([0]) : ListBoxEDergiler.UnselectIndices([0]);
        }

        function IsAllSelectedEDergiler() {
            var selectedDataItemCount = ListBoxEDergiler.GetItemCount() - (ListBoxEDergiler.GetItem(0).selected ? 0 : 1);
            return ListBoxEDergiler.GetSelectedItems().length == selectedDataItemCount;
        }

        function UpdateTextEDergiler() {
            var selectedItems = ListBoxEDergiler.GetSelectedItems();
            DropDownEditEDergiler.SetText(GetSelectedItemsText(selectedItems));
        }

        function SynchronizeListBoxEDergilerValues(dropDown, args) {
            ListBoxEDergiler.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTextsEDergiler(texts);
            ListBoxEDergiler.SelectValues(values);
            UpdateSelectAllItemStateEDergiler();
            UpdateTextEDergiler();
        }

        function GetValuesByTextsEDergiler(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = ListBoxEDergiler.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }

        //-----------------------------------------------------

        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }
    </script>
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
                    <dx:ASPxLabel runat="server" Text="Şirket Adı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxComboBox ID="ComboBoxSirket" runat="server" ValueType="System.Int32" Width="200px" Font-Size="Small" TextField="Adi" ValueField="SirketKey">
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
                    <dx:ASPxLabel runat="server" Text="Servis Tipi : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxComboBox ID="ComboBoxServisTip" runat="server" ValueType="System.Int32" Width="200px" Font-Size="Small" TextField="ServisTipAdi" ValueField="ServisTipKey" AutoPostBack="true" OnSelectedIndexChanged="ComboBoxServisTip_SelectedIndexChanged">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Limit : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxSpinEdit ID="SpinEditLimit" runat="server" DecimalPlaces="0" Width="200px" Font-Size="Small" MaxLength="5" NumberType="Integer" AllowNull="false" MinValue="1" MaxValue="10000" SpinButtons-ShowIncrementButtons="false">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxSpinEdit>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Başlangıç Zamanı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxDateEdit ID="DateEditBaslangicZamani" runat="server" Width="200px" Font-Size="Small" DisplayFormatString="dd.MM.yyyy HH:mm" EditFormatString="dd.MM.yyyy HH:mm" TimeSectionProperties-Visible="true">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Bitiş Zamanı : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxDateEdit ID="DateEditBitisZamani" runat="server" Width="200px" Font-Size="Small" DisplayFormatString="dd.MM.yyyy HH:mm" EditFormatString="dd.MM.yyyy HH:mm" TimeSectionProperties-Visible="true">
                        <ValidationSettings ValidationGroup="KayitGuncelle" Display="Dynamic">
                            <RequiredField ErrorText=" " IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Denemeler : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxDropDownEdit  ID="DropDownEditDenemeler" ClientInstanceName="DropDownEditDenemeler" Width="200px" runat="server" AnimationType="None">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox ID="ListBoxDenemeler" ClientInstanceName="ListBoxDenemeler" runat="server"  SelectionMode="CheckColumn"
                                ValueType="System.Int32" TextField="Adi" ValueField="DenemeKey" Width="100%">
                                <Border BorderStyle="None" />
                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                <ClientSideEvents SelectedIndexChanged="OnListBoxDenemelerSelectionChanged" />
                            </dx:ASPxListBox>
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding: 4px">
                                        <dx:ASPxButton AutoPostBack="False" runat="server" Text="X" Style="float: right">
                                            <ClientSideEvents Click="function(s, e){ DropDownEditDenemeler.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents TextChanged="SynchronizeListBoxDenemelerValues" DropDown="SynchronizeListBoxDenemelerValues" />
                    </dx:ASPxDropDownEdit>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="E-Dergiler : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                   <dx:ASPxDropDownEdit  ID="DropDownEditEDergiler" ClientInstanceName="DropDownEditEDergiler" Width="200px" runat="server" AnimationType="None">
                        <DropDownWindowStyle BackColor="#EDEDED" />
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox ID="ListBoxEDergiler" ClientInstanceName="ListBoxEDergiler" runat="server"  SelectionMode="CheckColumn"
                                ValueType="System.Int32" TextField="Adi" ValueField="EDergiKey" Width="100%">
                                <Border BorderStyle="None" />
                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                <ClientSideEvents SelectedIndexChanged="OnListBoxEDergilerSelectionChanged" />
                            </dx:ASPxListBox>
                            <table style="width: 100%">
                                <tr>
                                    <td style="padding: 4px">
                                        <dx:ASPxButton AutoPostBack="False" runat="server" Text="X" Style="float: right">
                                            <ClientSideEvents Click="function(s, e){ DropDownEditEDergiler.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents TextChanged="SynchronizeListBoxEDergilerValues" DropDown="SynchronizeListBoxEDergilerValues" />
                    </dx:ASPxDropDownEdit>
                </td>
            </tr>
            <tr>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Kod : " Font-Size="Small" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <dx:ASPxTextBox ID="TextBoxKod" runat="server" Width="200px" Font-Size="Small" MaxLength="50" Enabled="false">
                    </dx:ASPxTextBox>
                </td>
                <td class="TdSol" style="width: 15%;">
                    <dx:ASPxLabel runat="server" Text="Aktif : " Font-Size="Small" ForeColor="Red" />
                </td>
                <td class="TdSag" style="width: 35%;">
                    <asp:CheckBox ID="CheckBoxAktif" runat="server" Checked="true" />
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
                    <dx:ASPxGridView ID="GridViewTanim" runat="server" KeyFieldName="ServisKey" AutoGenerateColumns="false" Width="100%" EnableCallBacks="true" Font-Size="Small" OnRowDeleting="GridViewTanim_RowDeleting" OnCustomButtonCallback="GridViewTanim_CustomButtonCallback">
                        <ClientSideEvents EndCallback="function(s, e) {
	                                                                        if (s.cpErrorMessage){
                                                                            delete s.cpErrorMessage;
		                                                                        alert('Silinmek istenen veri diğer kısımlarda kullanılmaktadır!');
                                                                                }
                                                                       }" />
                        <Settings ShowGroupPanel="false" />
                        <SettingsText ConfirmDelete="Silme işlemini onaylıyor musunuz?" />
                        <SettingsBehavior AllowDragDrop="false" AllowFocusedRow="false" AutoExpandAllGroups="false" ConfirmDelete="true" />
                        <SettingsPager Visible="true" PageSize="15" AlwaysShowPager="true" Position="Bottom" ShowEmptyDataRows="true" />
                        <Paddings Padding="0px" />
                        <Border BorderWidth="0px" />
                        <BorderBottom BorderWidth="1px" />
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="Sirket.Adi" Caption="Şirket Adı">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Aciklama" Caption="Açıklama">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ServisTip.ServisTipAdi" Caption="Servis Tipi">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Denemeler" Caption="Denemeler">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="EDergiler" Caption="EDergiler">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Kod" Caption="Kod">
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Baslangic" Caption="Başlangıç Zamanı" PropertiesTextEdit-DisplayFormatString="dd.MM.yyyy HH:mm">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Bitis" Caption="Bitiş Zamanı" PropertiesTextEdit-DisplayFormatString="dd.MM.yyyy HH:mm">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Limit" Caption="Limit">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="Uretilen" Caption="Üretilen">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Kullanilan" Caption="Kullanilan">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Kalan" Caption="Kalan">
                                <CellStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
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

