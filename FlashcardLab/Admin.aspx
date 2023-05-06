<%@ Page Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="FlashcardLab.Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/admin.css" />

    <div class="jumbotron bfield">
        <h1 id="title" class="smoke txt-logo">MODS/ADMIN PAGE</h1>
    </div>

    <div class="container sm-field" id="usersMenu" runat="server">
        <div class="row">
            <div class="col-md-6">
                <asp:TextBox placeholder="username" autocomplete="off" MaxLength="50" CssClass="spaced input-sm txt" ID="username" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="password" autocomplete="off" MaxLength="50" CssClass="spaced input-sm txt" ID="password" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="mail" autocomplete="off" MaxLength="50" CssClass="spaced input-sm txt" ID="mail" TextMode="Email" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="country" autocomplete="off" MaxLength="50" CssClass="spaced input-sm txt" ID="country" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="gender" autocomplete="off" MaxLength="5" CssClass="spaced input-sm txt" ID="gender" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="birth date DD/MM/YYYY" MaxLength="10" autocomplete="off" CssClass="spaced input-sm txt" ID="birth" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="creation date DD/MM/YYYY" MaxLength="10" autocomplete="off" CssClass="spaced input-sm txt" ID="creation" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Button ID="insert" runat="server" Text="Insert user" CssClass="btn btn-success spaced" OnClick="insert_Click" />
                <asp:Button ID="delete" runat="server" Text="Delete user (by id)" CssClass="btn btn-danger spaced" OnClick="delete_Click" />
                <asp:Button ID="update" runat="server" Text="Update user" CssClass="btn btn-warning spaced" OnClick="update_Click" />
                <asp:Button ID="view" runat="server" Text="View" CssClass="btn btn-primary spaced" OnClick="view_Click" />

                <br />
                <asp:Label ID="lblID" CssClass="cyan bold spaced" runat="server" Text="ID:"></asp:Label>
                <asp:TextBox CssClass="input-sm txt txtbID" TextMode="Number" ID="id" runat="server"></asp:TextBox>

                <br />
                <br />
                <asp:Label ID="alert" CssClass="cyan bold spaced" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <br />

    <div class="container sm-field" id="decksMenu" runat="server">
        <div class="row">
            <div class="col-md-6">
                <asp:TextBox placeholder="name" autocomplete="off" MaxLength="50" CssClass="spaced input-sm txt" ID="name" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="description" autocomplete="off" MaxLength="150" CssClass="spaced input-sm txt" ID="description" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="progress" autocomplete="off" MaxLength="5" CssClass="spaced input-sm txt" ID="progress" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="next review" autocomplete="off" MaxLength="10" CssClass="spaced input-sm txt" ID="deckNext" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="language" autocomplete="off" MaxLength="10" CssClass="spaced input-sm txt" ID="language" runat="server"></asp:TextBox>
                <asp:TextBox placeholder="userID" autocomplete="off" TextMode="Number" CssClass="spaced input-sm txt" ID="userID" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Button ID="insertDeck" runat="server" Text="Insert Deck" CssClass="spaced btn btn-success" OnClick="insertDeck_Click" />
                <asp:Button ID="deleteDeck" runat="server" Text="Delete Deck (by id)" CssClass="spaced btn btn-danger" OnClick="deleteDeck_Click" />
                <asp:Button ID="updateDeck" runat="server" Text="Update Deck" CssClass="spaced btn btn-warning" OnClick="updateDeck_Click" />
                <asp:Button ID="xmlDeck" runat="server" Text="Get XML of deck" CssClass="spaced btn btn-info" OnClick="xmlDeck_Click" />

                <br />
                <asp:Label ID="idDeckLbl" CssClass="cyan bold spaced" runat="server" Text="ID:"></asp:Label>
                <asp:TextBox autocomplete="off" CssClass="input-sm txt txtbID" TextMode="Number" ID="deckID" runat="server"></asp:TextBox>

                <br />
                <br />
                <asp:Label ID="alertDeck" CssClass="cyan bold spaced" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <br />



    <asp:GridView ID="usersGrid" runat="server" CssClass="table table-dark" AllowPaging="true" OnPageIndexChanging="usersGrid_PageIndexChanging">
        <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
    </asp:GridView>

    <div class="container-fluid" id="modsQual" runat="server">
        <div class="row spaced">
            <div class="col-md-3"><asp:Button ID="grant" runat="server" Text="Grant Mod" CssClass="btn btn-success" OnClick="grant_Click" /></div>
            <div class="col-md-3"><asp:Button ID="disclaim" runat="server" Text="Disclaim Mod" CssClass="btn btn-danger" OnClick="disclaim_Click" /></div>
            <div class="col-md-6">
                <asp:Label ID="idModLbl" CssClass="cyan bold spaced" runat="server" Text="ID:"></asp:Label>
                <asp:TextBox autocomplete="off" CssClass="input-sm txt txtbID" TextMode="Number" ID="idMod" runat="server"></asp:TextBox>
            </div>

        </div>
    </div>

    <asp:GridView ID="modsGrid" runat="server" CssClass="table table-dark" AllowPaging="true" OnPageIndexChanging="modsGrid_PageIndexChanging">
        <PagerStyle HorizontalAlign="Center" CssClass="gridPager" />
    </asp:GridView>

    <div class="parentCenter">
        <button role="button" id="regionsBtn" type="button" class="btn btn-dig spaced enhance" onclick="countryChart()">Show users country chart</button>
    </div>

    <div id="regions"></div>

    <link rel="stylesheet" href="css/jquery-jvectormap-2.0.5.css" type="text/css" media="screen"/>
    <script type="text/javascript" src="js/jquery-jvectormap-2.0.5.min.js"></script>
    <script type="text/javascript" src="js/jquery-jvectormap-world-mill.js"></script>
    <script type="text/javascript" src="js/admin.js"></script>
    <br />
</asp:Content>
