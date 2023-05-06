<%@ Page Title="Comments" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="FlashcardLab.Comments" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron bfield">
        <h1 class="smoke txt-logo text-wrap" id="title" runat="server"></h1>
    </div>

    <asp:Label runat="server" ID="alert" CssClass="subalert center spaced cyan"/>

    <asp:GridView ID="commentsGrid" CssClass="table table-dark pink" HorizontalAlign="Center" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#212121" ShowHeaderWhenEmpty="true" ShowFooter="false" ShowHeader="true" DataKeyNames="id" AllowPaging="true" runat="server" OnRowCommand="commentsGrid_RowCommand" OnRowDataBound="commentsGrid_RowDataBound" OnPageIndexChanging="commentsGrid_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Panel ID="postComment" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:TextBox CssClass="form-control input-md no-resize comw" TextMode="MultiLine" Rows="5" Columns="100" MaxLength="200" ToolTip="Leave a comment" ID="userComment" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <div class="mmspaced">
                                <asp:LinkButton Text="Submit" CssClass="btn btn-success spaced-btn" ToolTip="Submit" CommandName="Submit" runat="server"><span class="glyphicon glyphicon-send"></span>&nbsp;Submit</asp:LinkButton>
                                <ajaxToolkit:Rating ID="userRate" runat="server" StarCssClass="glyphicon glyphicon-star yellow xincrease" WaitingStarCssClass="glyphicon glyphicon-star yellow" EmptyStarCssClass="glyphicon glyphicon-star-empty yellow" FilledStarCssClass="glyphicon glyphicon-star yellow" CurrentRating="0" AutoPostBack="true"></ajaxToolkit:Rating>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="editComment" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:TextBox CssClass="form-control input-md no-resize comw" TextMode="MultiLine" Rows="5" Columns="100" MaxLength="200" ToolTip="Alter your existing comment" ID="editText" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <div class="mmspaced">
                                    <asp:LinkButton CssClass="btn btn-warning spaced-btn" ToolTip="Alter" CommandName="Alter" runat="server"><span class="glyphicon glyphicon-pencil"></span>&nbsp;Alter</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-danger spaced-btn" ToolTip="Delete Comment" CommandName="DeleteComment" runat="server"><span class="glyphicon glyphicon-remove"></span>&nbsp;Delete</asp:LinkButton>


                                    <ajaxToolkit:Rating ID="editRate" runat="server" StarCssClass="glyphicon glyphicon-star yellow xincrease" WaitingStarCssClass="glyphicon glyphicon-star yellow" EmptyStarCssClass="glyphicon glyphicon-star-empty yellow" FilledStarCssClass="glyphicon glyphicon-star yellow" CurrentRating="0" AutoPostBack="true"></ajaxToolkit:Rating>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                    </asp:Panel>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-11">
                                <asp:Image CssClass="img-fluid img-thumbnail smaller-square mspaced" ImageUrl='<%# GetImageUrl((int)Eval("userID")) %>' runat="server" />
                                <asp:Label CssClass="cyan text-wrap" Text='<%# Eval("comment") %>' runat="server" ToolTip="Comments" CommandName="Comments" />

                                <br />
                                <asp:Label Text='<%# "|  " + Eval("username") + " - " + DateOnly(Eval("creation").ToString()) %>' CssClass="text-wrap" runat="server" />
                                <br />
                                <ajaxToolkit:Rating ID="rate" ReadOnly="true" runat="server" StarCssClass="glyphicon glyphicon-star yellow increase no-click" WaitingStarCssClass="glyphicon glyphicon-star-empty" EmptyStarCssClass="glyphicon glyphicon-star-empty yellow" FilledStarCssClass="glyphicon glyphicon-star yellow" CurrentRating='<%# (Eval("rating") as Int16?) ?? 0 %>'></ajaxToolkit:Rating>
                            </div>
                            <div class="col-md-1 parentCenter">
                                <asp:LinkButton ID="btnDelete" Text="Delete" CssClass="btn btn-danger spaced-btn" ToolTip="Delete Comment" CommandName="DeleteCommentMod" runat="server"  CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><span class="glyphicon glyphicon-remove"></span>&nbsp;Remove</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

       </Columns>

   <PagerStyle ForeColor="#ffffff" CssClass="gridPager" HorizontalAlign="Center"  />
   </asp:GridView>
</asp:Content>