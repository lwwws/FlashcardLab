<%@ Page Title="Study" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Study.aspx.cs" Inherits="FlashcardLab.Study" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/study.css" />

    <br />

    <div class="container">
        <div class="panel panel-default" id="card">
            <div class="panel-heading">
                <div class="row">
                    <div class="progress">
                        <div class="progress-bar greet-color" id="progress" role="progressbar" runat="server" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <asp:Label ID="left" CssClass="smoke center align-center" runat="server"></asp:Label>
                </div>
            </div>
            <div class="panel-body">
                <div class="flex-wrapper">
                    <h1 id="text" class="front center align-center text-wrap" runat="server">LOADING</h1>

                    <asp:TextBox CssClass="form-control input-lg back-input" placeholder="Your Answer" autocomplete="off" MaxLength="50" ID="answerBox" runat="server"></asp:TextBox>

                    <br />

                    <button type="submit" class="btn txt increase" runat="server" onServerClick="submit_Click" OnClientClick="return false">
                        Enter!
                        <span class="glyphicon glyphicon-menu-down"></span>
                    </button>
                </div>
            </div>
            <div class="panel-footer">
                <div class="row">
                    <div id="animWrapper" runat="server">
                        <asp:Label ID="alert" CssClass="footer-text txt round center text-wrap enhance-x" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="previous" CssClass="footer-text txt-invert round center align-center text-wrap" runat="server" Text=""></asp:Label>
                </div>
                <div class="row">
                    <asp:Label ID="note" CssClass="footer-text txt round center text-wrap" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <br />
</asp:Content>