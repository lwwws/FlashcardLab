using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FlashcardLab
{
    public partial class Shared : System.Web.UI.Page
    {
        DataTable decks;
        bool isModOrAdmin = false;
        DataTable mods;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            mods = Utilities.DBLink.GetModsTable();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            isModOrAdmin = IsModOrAdmin();

            if (!IsPostBack)
            {
                decks = Utilities.DBLink.GetSharedDecks();
                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            if (decks == null)
                return;

            decks.Columns.Add("rating", typeof(System.Double));
            decks.Columns.Add("total", typeof(System.Int32));
            decks.Columns.Add("username", typeof(System.String));

            alert.Text = "";
            alert.Visible = false;

            if (decks.Rows.Count > 0)
            {
                foreach (DataRow row in decks.Rows)
                {
                    double[] rate = Utilities.DBLink.GetAverageAndTotalRating((int)row["id"]);

                    row["rating"] = rate[0];
                    row["total"] = rate[1];

                    if (mods.Select("userID = " + row["userID"]).Length != 0)
                    {
                        row["username"] = FindUser((int)row["userID"]) + "<span style='color: " + mods.Select("userID = " + row["userID"])[0]["prefixColor"] + " !important'> [MOD]</span>";
                    }
                    else if ((int)row["userID"] == 34)
                    {
                        row["username"] = FindUser((int)row["userID"]) + "<span style='color: red !important'> [ADMIN]</span>";
                    }
                    else
                    {
                        row["username"] = FindUser((int)row["userID"]);
                    }
                }
            }
            else
            {
                alert.Visible = true;
                alert.Text = "No decks here...";
            }

            sharedGrid.DataSource = decks;
            sharedGrid.DataBind();
        }
        public String FindUser(int userID)
        {
            return Utilities.DBLink.GetUsernameByUserID(userID);
        }

        private void Alert(string text)
        {
            Type cstype = this.GetType();
            ScriptManager.RegisterStartupScript(this, cstype, "Error Alert", "$(document).ready(alert(\'" + text + "\'));", true);
        }

        protected void sharedGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    if (Session["user"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                        return;
                    }
                    else
                    {
                        Utilities.User user = (Utilities.User)Session["user"];
                        int deckID = (int)sharedGrid.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;

                        if (Utilities.DBLink.UserHasThisDeck(user.id, deckID))
                        {
                            Alert("You already have this deck!");
                            return;
                        }

                        if (Session["added"] != null)
                        {
                            LinkedList<int> list = (LinkedList<int>)Session["added"];

                            if (list.Contains(deckID))
                            {
                                Alert("You have already added this deck!");
                                return;
                            }
                            else
                            {
                                Utilities.DBLink.CopyDeckToUserID(deckID, user.id);
                                Alert("The deck has been successfully added! Check your profile");
                                list.AddLast(deckID);
                            }
                        }
                        else
                        {
                            Utilities.DBLink.CopyDeckToUserID(deckID, user.id);
                            Alert("The deck has been successfully added! Check your profile");

                            LinkedList<int> list = new LinkedList<int>();
                            list.AddLast(deckID);

                            Session["added"] = list;
                        }
                    }
                }
                else if (e.CommandName.Equals("Comments"))
                {
                    int deckID = (int)sharedGrid.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                    Session["deckComments"] = Utilities.DBLink.GetDeckByDeckID(deckID);
                    Response.Redirect("~/Comments.aspx");
                }
                else if (e.CommandName.Equals("Remove"))
                {
                    if (isModOrAdmin)
                    {
                        Utilities.DBLink.UpdateDecksShared((int)sharedGrid.DataKeys[Convert.ToInt32(e.CommandArgument)].Value, false);
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

        private bool IsModOrAdmin()
        {
            if (Session["user"] != null)
            {
                Utilities.User user = (Utilities.User)Session["user"];

                return (user.GetType() == typeof(Utilities.Moderator) || user.id == 34);
            }

            return false;
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

        protected void sharedGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            sharedGrid.PageIndex = e.NewPageIndex;
            UpdateGrid();
        }
        protected void search_Click(object sender, EventArgs e)
        {
            String q = querySearch.Text.Trim().Replace("\'", "").Replace("\"", "");
            decks = Utilities.DBLink.GetSharedDecksSorted(langSearch.SelectedValue, modeSearch.SelectedValue, q);
            UpdateGrid();
        }

        protected void sharedGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!isModOrAdmin)
                {
                    (e.Row.FindControl("btnRemove") as LinkButton).Visible = false;
                    (e.Row.FindControl("deckIDlbl") as Label).Visible = false;
                }
            }
        }
    }
}