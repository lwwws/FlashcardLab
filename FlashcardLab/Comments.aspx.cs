using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;

namespace FlashcardLab
{
    public partial class Comments : Page
    {
        Utilities.Deck deck;
        DataTable comments;
        bool isModOrAdmin = false;
        DataTable mods;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            mods = Utilities.DBLink.GetModsTable();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["deckComments"] == null)
                Response.Redirect("/UnauthorizedAccess.aspx");

            deck = (Utilities.Deck)Session["deckComments"];
            title.InnerText = "\"" + deck.name + "\" comments";
            isModOrAdmin = IsModOrAdmin();

            if (!IsPostBack)
            {
                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            comments = Utilities.DBLink.GetCommentsTableByDeckID(deck.id);
            comments.Columns.Add("username", typeof(System.String));
            alert.Text = "";
            alert.Visible = false;


            if (comments.Rows.Count > 0)
            {
                foreach(DataRow row in comments.Rows)
                {
                    String username = Utilities.DBLink.GetUsernameByUserID((int)row["userID"]);
                    row["username"] = username;

                    if (mods.Select("userID = " + row["userID"]).Length != 0)
                    {
                        row["username"] = username + "<span style='color: " + mods.Select("userID = " + row["userID"])[0]["prefixColor"] + " !important'> [MOD]</span>";
                    }
                    else if ((int)row["userID"] == 34)
                    {
                        row["username"] = username + "<span style='color: red !important'> [ADMIN]</span>";
                    }
                    else
                    {
                        row["username"] = username;
                    }
                }
            }
            else
            {
                alert.Visible = true;
                alert.Text = "No comments here...";
            }

            commentsGrid.DataSource = comments;
            commentsGrid.DataBind();

            if (Session["user"] != null)
            {
                Utilities.User user = (Utilities.User)Session["user"];

                if (Utilities.DBLink.UserCommentExists(user.id, deck.id))
                {
                    (commentsGrid.HeaderRow.FindControl("postComment") as Panel).Visible = false;

                    Utilities.Comment comment = Utilities.DBLink.GetCommentByUserIDAndDeckID(user.id, deck.id);
                    (commentsGrid.HeaderRow.FindControl("editText") as TextBox).Text = comment.comment;
                    (commentsGrid.HeaderRow.FindControl("editRate") as Rating).CurrentRating = comment.rating;
                }
                else
                {
                    (commentsGrid.HeaderRow.FindControl("editComment") as Panel).Visible = false;
                }
            }
            else
            {
                (commentsGrid.HeaderRow.FindControl("editComment") as Panel).Visible = false;
            }
        }

        private void Alert(string text)
        {
            Type cstype = this.GetType();
            ScriptManager.RegisterStartupScript(this, cstype, "Error Alert", "$(document).ready(alert(\'" + text + "\'));", true);
        }
        public String GetImageUrl(int userID)
        {
            byte[] bytes = Utilities.DBLink.GetProfile(userID);

            if (bytes == null)
            {
                return "images/profile.jpg";
            }
            else
            {
                String imgB64 = Convert.ToBase64String(bytes);
                return "data:Image/png;base64," + imgB64;
            }
        }

        protected void commentsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            commentsGrid.PageIndex = e.NewPageIndex;
            UpdateGrid();
        }

        protected void commentsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                Utilities.User user = (Utilities.User)Session["user"];
                bool exists = Utilities.DBLink.UserCommentExists(user.id, deck.id);

                if(Utilities.DBLink.UserHasThisDeck(user.id, deck.id))
                {
                    Alert("You cant rate your own deck!");
                    return;
                }

                if (e.CommandName.Equals("Submit"))
                {
                    if (!exists)
                    {
                        Utilities.Comment comment = new Utilities.Comment(
                            -1,
                            (commentsGrid.HeaderRow.FindControl("userComment") as TextBox).Text.Trim(),
                            (commentsGrid.HeaderRow.FindControl("userRate") as Rating).CurrentRating,
                            deck.id,
                            user.id
                            );

                        Utilities.DBLink.InsertComment(comment);
                        UpdateGrid();
                    }
                    else
                    {
                        Alert("You have already left a comment! You can change it if you want");
                        return;
                    }
                }
                else if (e.CommandName.Equals("Alter"))
                {
                    if (exists)
                    {
                        Utilities.Comment comment = Utilities.DBLink.GetCommentByUserIDAndDeckID(user.id, deck.id);
                        comment.comment = (commentsGrid.HeaderRow.FindControl("editText") as TextBox).Text.Trim();
                        comment.rating = (commentsGrid.HeaderRow.FindControl("editRate") as Rating).CurrentRating;

                        Utilities.DBLink.UpdateCommentByUserIDAndDeckID(comment);
                        UpdateGrid();
                    }
                    else
                    {
                        Alert("Your comment doesnt exist?? what are you doing here?");
                        return;
                    }
                }
                else if (e.CommandName.Equals("DeleteComment"))
                {
                    if (exists)
                    {
                        Utilities.DBLink.DeleteCommentByUserID(user.id);
                        UpdateGrid();
                    }
                    else
                    {
                        Alert("Your comment doesnt exist?? what are you doing here?");
                        return;
                    }
                }
                else if (e.CommandName.Equals("DeleteCommentMod"))
                {
                    if (isModOrAdmin)
                    {
                        Utilities.DBLink.DeleteComment((int)commentsGrid.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                        UpdateGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message);
                return;
            }
        }

        public String DateOnly(String date)
        {
            if(date == null || date.Equals(""))
            {
                return "";
            }

            return date.Substring(0, 10);
        }

        private bool IsModOrAdmin()
        {
            if (Session["user"] != null)
            {
                Utilities.User user = (Utilities.User)Session["user"];

                return (user.GetType() == typeof(Utilities.Moderator) || user.id == 34);
            }

            return false;
        }

        protected void commentsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!isModOrAdmin)
                {
                    (e.Row.FindControl("btnDelete") as LinkButton).Visible = false;
                }
            }
        }
    }
}