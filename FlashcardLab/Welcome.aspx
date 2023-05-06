<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="FlashcardLab.Welcome" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron bfield">
        <h1 id="welcomeTitle" class="smoke txt-logo" runat="server"></h1>
    </div>
    <asp:Image ID="bd" CssClass="img img-fluid center" Height="20%" Width="20%" runat="server" />
    <h2 class="mmfield" id="lbl" runat="server"></h2>
</asp:Content>
