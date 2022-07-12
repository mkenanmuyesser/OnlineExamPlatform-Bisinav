<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMain.master" AutoEventWireup="true" CodeBehind="Kurs.aspx.cs" Inherits="BiSinavProject.PL.Kurs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAkisMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMiddleContent" runat="server">
    <div class="panel">
        <div class="gallery-categories">
            <asp:Repeater ID="RepeaterIl" runat="server">
                <ItemTemplate>
                    <a href='Kurs.aspx?K=<%# Eval("Key") %>' <%# Convert.ToBoolean(Eval("Secim"))?"class='active'":""%>><%# Eval("Adi") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="photo-gallery-blocks">
            <asp:Repeater ID="RepeaterKurs" runat="server" >
                <ItemTemplate>
                    <div class="item" style="text-align: center">
                        <div class="item-header">
                            <a href='<%# string.IsNullOrEmpty(Eval("Link").ToString())?"#":Eval("Link") %>' <%# string.IsNullOrEmpty(Eval("Link").ToString())?"":"target='_blank'" %>>
                                <img src='/Uploads/Program/<%# Eval("Resim") %>' alt="" /></a>
                        </div>
                        <div class="item-content">
                            <h3><a href='<%# string.IsNullOrEmpty(Eval("Link").ToString())?"#":Eval("Link") %>' <%# string.IsNullOrEmpty(Eval("Link").ToString())?"":"target='_blank'" %>><%# Eval("Adi") %></a></h3>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
