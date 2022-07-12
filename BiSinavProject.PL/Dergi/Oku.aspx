<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oku.aspx.cs" Inherits="BiSinavProject.PL.UserControl.Oku" %>

<%@ Register Src="~/UserControl/PageFlipper.ascx" TagPrefix="uc1" TagName="PageFlipper" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BİSINAV.net</title>
    <meta name="viewport" content="user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width, height=device-height, target-densitydpi=device-dpi" />
    <%--<meta name="apple-mobile-web-app-capable" content="yes" />--%>

    
</head>
<body style="background: url('/Content/TurnJs/PagePattern/003-granulated-loom.png') repeat">
    <uc1:PageFlipper runat="server" ID="PageFlipperControl" />
</body>
</html>
