<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="FlashcardLab.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/about.css" />

    <div class="jumbotron bfield">
        <h1 class="smoke txt-logo">About us.</h1>
    </div>

        <div class="container-fluid">
            <div class="row parentCenter">
                <div class="col-md-6 center-text">
                    <h1 class="smoke txt-resize">Learn effectively, practically and easy with FlashcardLab.</h1>
                </div>
                <div class="col-md-6 center-text">
                    <img src="images/stories/convo_lang.svg" alt="conversation" class="img img-fluid infopic"/>
                </div>
            </div>
            <div class="row parentCenter">
                <div class="col-md-6 center-text">
                    <img src="images/stories/kids_studying.svg" alt="kids studying" class="img img-fluid infopic"/>
                </div>
                <div class="col-md-6 center-text">
                    <h1 class="smoke txt-resize">Using Spaced Repetition System, FlashcardLab gives you opportunity to learn minimally, without spending hours listening to lectures or reading books. And the results are way more better!</h1>
                </div>
            </div>
            <div class="row parentCenter">
                <div class="col-md-6 center-text">
                    <h1 class="smoke txt-resize">Track your progress with FlashcardLab.</h1>
                </div>
                <div class="col-md-6 center-text">
                    <img src="images/stories/stats_lang.svg" alt="statistics" class="img img-fluid infopic"/>
                </div>
            </div>
            <div class="row parentCenter">
                <div class="col-md-6 center-text">

                </div>
                <div class="col-md-6 center-text">

                </div>
            </div>
        </div>

        <address class="center-text">
            <strong class="smoke">Made by Seva as a SE school project in Kfar Silver, Israel. <br /> Contact me via e-mail:</strong>   <a href="mailto:yumifan3679@yahoo.com">yumifan3679@yahoo.com</a>
        </address>

    <br />
</asp:Content>
