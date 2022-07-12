<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageFlipper.ascx.cs" Inherits="BiSinavProject.PL.UserControl.PageFlipper" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>


<!-- External JS dependencies -->
<script type="text/javascript" src="/Content/TurnJs/assets/js/jquery-2.0.3.min.js"></script>
<script type="text/javascript" src="/Content/TurnJs/assets/js/underscore-min.js"></script>
<script type="text/javascript" src="/Content/TurnJs/assets/js/backbone-min.js"></script>

<!-- Turn.js UI Kit -->
<script type="text/javascript" src="/Content/TurnJs/assets/js/turn.min.js"></script>

<!-- App dependencies -->
<script type="text/javascript" src="/Content/TurnJs/assets/js/app.js"></script>


<!-- External CSS dependencies -->
<link type="text/css" rel="stylesheet" href="/Content/TurnJs/assets/css/font-awesome.min.css" />
<link rel="icon" type="image/png" href="/Content/TurnJs/assets/img/favicon.png" />
<link href='http://fonts.googleapis.com/css?family=Carrois+Gothic+SC' rel='stylesheet' type='text/css' />

<!-- App CSS dependencies -->
<link type="text/css" rel="stylesheet" href="/Content/TurnJs/assets/css/main.css" />

<style type="text/css">
    html, body {
        -webkit-text-size-adjust: none;
    }
</style>

<div class="catalog-app">
    <div style="padding: 5px;">
        <div style="float: right">
            <form runat="server">
                <dx:ASPxButton ID="ButtonCikis" runat="server" Text="Çıkış" Width="100" Font-Size="Small" OnClick="ButtonCikis_Click" Theme="Metropolis" />
            </form>

        </div>

        <center>
                        <asp:Label runat="server" ID="LabelBaslik" Font-Names="Arial" Font-Bold="True" Font-Size="Large"></asp:Label>
                          </center>
    </div>
    <div id="viewer">
        <div id="flipbook" class="ui-flipbook">
            <a ignore="1" class="ui-arrow-control ui-arrow-next-page"></a>
            <a ignore="1" class="ui-arrow-control ui-arrow-previous-page"></a>
        </div>
    </div>

    <div id="controls">
        <div class="all">
            <div class="ui-slider" id="page-slider">
                <div class="bar">
                    <div class="progress-width">
                        <div class="progress">
                            <div class="handler"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ui-options" id="options">
                <a class="ui-icon" id="ui-icon-table-contents">
                    <i class="fa fa-bars"></i>
                </a>
                <a class="ui-icon show-hint" id="ui-icon-miniature">
                    <i class="fa fa-th"></i>
                </a>
                <a class="ui-icon" id="ui-icon-zoom">
                    <i class="fa fa-file-o"></i>
                </a>
                <a class="ui-icon show-hint" title="Tam Ekran" id="ui-icon-full-screen">
                    <i class="fa fa-expand"></i>
                </a>
                <a class="ui-icon show-hint" id="ui-icon-toggle">
                    <i class="fa fa-ellipsis-v"></i>
                </a>
            </div>

            <div id="zoom-slider-view" class="zoom-slider">
                <div class="bg">
                    <div class="ui-slider" id="zoom-slider">
                        <div class="bar">
                            <div class="progress-width">
                                <div class="progress">
                                    <div class="handler"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ui-icon-expand-options">
            <a class="ui-icon show-hint">
                <i class="fa fa-ellipsis-h"></i>
            </a>
        </div>

    </div>

    <div id="miniatures" class="ui-miniatures-slider">
    </div>

    <script type="text/javascript">
        <% var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
        var jsVariable = <%= serializer.Serialize(Sayfalar()) %>;

        FlipbookSettings = {
            options: {
                pageWidth: 1115,
                pageHeight: 1443,
                pages: <%= Sayfalar().Count()%>,
               
            },           
            pageFolder: '/Uploads/Program/',
            sayfalar:jsVariable,
            loadRegions: false
        };
  </script>
