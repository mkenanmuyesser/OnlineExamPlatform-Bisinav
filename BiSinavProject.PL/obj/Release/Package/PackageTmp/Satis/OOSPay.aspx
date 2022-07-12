<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="OOSPay.aspx.cs" Inherits="BiSinavProject.PL.Satis.OOSPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-
transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Level:
            <asp:DropDownList ID="secure3dsecuritylevel" runat="server">
                <asp:ListItem Value="OOS_PAY" Text="OOS_PAY" />
            </asp:DropDownList>
            <br />
            Refresh Time:
            <asp:TextBox ID="refreshtime" runat="server" />
            <br />
            <asp:Button ID="submit" runat="server" PostBackUrl="https://sanalposprov.garanti.com.tr/servlet/gt3dengine"
                Text="İşlemi Gönder" />
            <asp:HiddenField ID="mode" runat="server" />
            <asp:HiddenField ID="apiversion" runat="server" />
            <asp:HiddenField ID="terminalprovuserid" runat="server" />
            <asp:HiddenField ID="terminaluserid" runat="server" />
            <asp:HiddenField ID="terminalid" runat="server" />
            <asp:HiddenField ID="terminalmerchantid" runat="server" />
            <asp:HiddenField ID="orderid" runat="server" />
            <asp:HiddenField ID="customeremailaddress" runat="server" />
            <asp:HiddenField ID="customeripaddress" runat="server" />
            <asp:HiddenField ID="txntype" runat="server" />
            <asp:HiddenField ID="txnamount" runat="server" />
            <asp:HiddenField ID="txncurrencycode" runat="server" />
            <asp:HiddenField ID="companyname" runat="server" />
            <asp:HiddenField ID="txninstallmentcount" runat="server" />
            <asp:HiddenField ID="successurl" runat="server" />
            <asp:HiddenField ID="errorurl" runat="server" />
            <asp:HiddenField ID="secure3dhash" runat="server" />
            <asp:HiddenField ID="lang" runat="server" />
            <asp:HiddenField ID="txntimestamp" runat="server" />
        </div>
    </form>
</body>
</html>
