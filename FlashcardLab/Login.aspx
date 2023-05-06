<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FlashcardLab.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<div class="jumbotron bfield">
        <h1 class="display-1 smoke" id="loginTitle">Login</h1>
    </div>--%>

    <br />

    <div class="container sm-field halfw">
        <div class="row">
            <div class="col-md-12 parentCenter">
            <img src="images/stories/login.svg" alt="login picture" class="img img-fluid" height="300" width="300"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 parentCenter">
            <div class="form-group">
                <asp:Label CssClass="smoke" ID="lblUser" runat="server" Text="Username:"></asp:Label>
                <asp:TextBox CssClass="form-control input-md" ID="username" MaxLength="50" runat="server"></asp:TextBox>
                <span class="error text-danger" id="usernameAlert" runat="server"></span>
            </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 parentCenter">
            <div class="form-group">
                <asp:Label CssClass="smoke" ID="lblPsw" runat="server" Text="Password:"></asp:Label>
                <asp:TextBox TextMode="password" CssClass="form-control input-md" ID="password" MaxLength="50" runat="server"></asp:TextBox>
                <span class="error text-danger" id="passwordAlert" runat="server"></span>
            </div>
            </div>
        </div>
        <div class="row">
                <div class="col-md-12 parentCenter">
                    <asp:Button CssClass="btn btn-success spaced increase" ID="enter" runat="server" Text="Enter" OnClick="enter_Click" />
                    <asp:Button ID="clear" runat="server" Text="Clear" CssClass="btn btn-warning spaced increase" OnClick="clear_Click" />
                </div>
        </div>
        <div class="row">
                <div class="col-md-12 parentCenter">
                    <asp:LinkButton ID="forgotBtn" runat="server" CssClass="btn btn-primary spaced" OnClick="forgotBtn_Click"><span class="glyphicon glyphicon-pencil"></span> &nbsp;Forgot Password?</asp:LinkButton>
                    <br/>
                    <a href="/Register.aspx" class="mspaced"> Don't have an account? </a>

                 </div>
        </div>
        <div class="row">
                <div class="col-md-12 parentCenter">
                    <address id="address" class="subalert" runat="server">
                        <strong><asp:Label runat="server" Text="" ID="mailAlert"></asp:Label></strong>   <a href="mailto:yumifan3679@yahoo.com">yumifan3679@yahoo.com</a><br />
                    </address>
                 </div>
        </div>
    </div>

</asp:Content>
