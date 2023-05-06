<%@ Page Title="Stats" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="FlashcardLab.Stats" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/stats.css" />

    <div class="jumbotron bfield">
        <h1 class="display-1 smoke" id="title" runat="server"></h1>
        <h3 class="cyan">Choose a deck to present stats:</h3>
    </div>

    <asp:DropDownList ID="decksList" runat="server" CssClass="center txt increase" AutoPostBack="true" OnSelectedIndexChanged="decksList_SelectedIndexChanged"></asp:DropDownList>

    <div id="deckStats" runat="server" visible="false">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <canvas id="deckWeek" class="center round"></canvas>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <canvas id="lrfDeck" class="center"></canvas>
                </div>
                <div class="col-md-6">
                    <canvas id="iccDeck" class="center"></canvas>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h3 class="subalert center center-text squish">Deck progress:</h3>
                    <div class="progress" style="height: 20px;">
                        <div id="pbar" class="progress-bar progress-bar-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <h3 id="stat1" class="sm-field spaced center increase round" runat="server"></h3>
                </div>
                <div class="col-md-6">
                    <h3 id="creationStat" class="sm-field spaced center increase round" runat="server"></h3>
                </div>
            </div>
        </div>
    </div>


    <input class="hide" id="dataWeek" type="hidden" value="" runat="server"/>
    <input class="hide" id="deckName" type="hidden" value="" runat="server"/>
    <input class="hide" id="dataLRF" type="hidden" value="" runat="server"/>
    <input class="hide" id="dataCorIncor" type="hidden" value="" runat="server"/>
    <input class="hide" id="dataProgress" type="hidden" value="" runat="server"/>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
    <script type="text/javascript" src="js/stats.js"></script>
</asp:Content>
