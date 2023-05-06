<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FlashcardLab._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/default.css" />

    <div class="container-fluid">
        <div class="row jumbotron gradient">
            <div class="col-md-9">
                <h1 id="title">FlashcardLab.</h1>
                <p class="lead">Effective, practical way of learning new languages.</p>
            </div>
            <div class="col-md-3">
                <img src="images/logo.png" class="rotate img img-fluid center" alt="logo"/>
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <div id="carouselD" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
                <li data-target="#carouselD" data-slide-to="0" class="active"></li>
                <li data-target="#carouselD" data-slide-to="1"></li>
                <li data-target="#carouselD" data-slide-to="2"></li>
            </ol>

            <div class="carousel-inner" role="listbox">
                <div class="carousel-item item active">
                    <img class="d-block w-100 imgCarousel img img-fluid" src="images/background/glass_pink.jpeg" alt="slide1">
                    <div class="carousel-caption d-md-block">
                        <h2 class="slight-shadow">Getting started</h2>
                        <p class="stroke">By creating an account, you get access to your decks and variety of options of creating and studying them!</p>
                    </div>
                </div>
                <div class="carousel-item item">
                    <img class="d-block w-100 imgCarousel img img-fluid" src="images/background/meeting.jpg" alt="slide2">
                    <div class="carousel-caption d-md-block">
                        <h2 class="slight-shadow">How does this work?</h2>
                        <p class="stroke">FlashcardLab gives you opportunities to to learn vocabulary of languages by memorizing flashcards! Each deck will have its progress and you can see it for each deck, including having statistics about each deck and their overall progress.</p>
                    </div>
                </div>
                <div class="carousel-item item">
                    <img class="d-block w-100 imgCarousel img img-fluid" src="images/background/dictionary.jpeg" alt="slide3">
                    <div class="carousel-caption d-md-block">
                        <h2 class="slight-shadow">How do I study?</h2>
                        <p class="stroke">You can easily add decks to your account through the shop, or create one yourself in the "Decks" section and customize it. FlashcardLab provides you a way to study those decks and memorize them effeciently.</p>
                    </div>
                </div>
            </div>

            <a class="left carousel-control" href="#carouselD" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#carouselD" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>

    <br />

    <!--

    <div class="row bfield clear">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                By creating an account, you get access to your decks and variety of options of creating and studying them!
            </p>
            </div>
        <div class="col-md-4">
            <h2>How does this work?</h2>
            <p>
                FlashcardLab gives you opportunities to <s>make your own schedule or create it automatically for you</s>, each deck will have its progress and you can see it for each deck, including having different statistics about your attendance and overall progress.
            </p>
        </div>
        <div class="col-md-4">
            <h2>How do I study?</h2>
            <p>
                You can easily add decks to your account <s>via store</s>, create one yourself with automatic translation (or without it) or <s>couple</s> of ways to create it automatically. FlashcardLab provides you with <s>different games</s> to study those decks.
            </p>
        </div>
    </div>

    -->

</asp:Content>
