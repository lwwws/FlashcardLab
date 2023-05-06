using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FlashcardLab
{
    public partial class Decks : System.Web.UI.Page
    {
        DataTable userDecks;
        Utilities.User user;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("/UnauthorizedAccess.aspx");

            user = (Utilities.User)Session["user"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            listAlert.InnerText = "";
            listAlert.Visible = false;

            if (!IsPostBack)
            {
                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            userDecks = Utilities.DBLink.GetDecksTableByUserID(user.id);
            decksList.DataSource = userDecks;
            decksList.DataBind();
        }

        protected String CountLearn(String id)
        {
            return "Learn (" + Utilities.DBLink.CountCardsForLearn(id).ToString() + " cards left)";
        }
        protected String CountReview(String id)
        {
            return "Review (" + Utilities.DBLink.CountCardsForReview(id).ToString() + " cards left)"; ;
        }

        protected void decksList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;

                String progress = DataBinder.Eval(item.DataItem, "progress").ToString();

                (item.FindControl("deckBar") as System.Web.UI.HtmlControls.HtmlGenericControl).Attributes.Add("style", "width: " + progress + "%");
                (item.FindControl("deckBar") as System.Web.UI.HtmlControls.HtmlGenericControl).Attributes.Add("aria-valuenow", progress);

                int deckID = (int)DataBinder.Eval(item.DataItem, "id");
                int r, g, b;

                r = new Random(deckID).Next() % 255;
                g = new Random(deckID + 1).Next() % 255;
                b = new Random(deckID + 2).Next() % 255;

                (item.FindControl("phead") as System.Web.UI.HtmlControls.HtmlGenericControl).Attributes.Add("style", string.Format("background-color: rgb({0},{1},{2}, 0.2)", r, g, b));
            }
        }

        protected void decksList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            this.decksList.EditIndex = -1;
            this.decksList.SelectedIndex = e.NewSelectedIndex;
            UpdateGrid();
        }

        protected void decksList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.decksList.EditIndex = e.NewEditIndex;
            this.decksList.SelectedIndex = -1;
            UpdateGrid();
        }

        protected void decksList_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            this.decksList.EditIndex = -1;
            this.decksList.SelectedIndex = -1;
            UpdateGrid();
        }

        protected void decksList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            String deckID = decksList.DataKeys[e.ItemIndex].Value.ToString();
            Utilities.DBLink.DeleteDeck(deckID);
            this.decksList.EditIndex = -1;
            this.decksList.SelectedIndex = -1;
            UpdateGrid();
        }

        protected void decksList_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            String name = (e.Item.FindControl("nameInsert") as TextBox).Text.Trim();

            if (name.Equals("")) {
                listAlert.Visible = true;
                listAlert.InnerText = "You have to fill the name of the deck.";
                return;
            }

            String desc = (e.Item.FindControl("descriptionInsert") as TextBox).Text.Trim();
            String lang = (e.Item.FindControl("languageInsert") as DropDownList).SelectedValue.Trim() + "/" + (e.Item.FindControl("languageInsert2") as DropDownList).SelectedValue.Trim();

            Utilities.Deck deck = new Utilities.Deck(
                -1,
                name,
                desc,
                (double)0.0,
                DateTime.Now.ToString(Utilities.DBLink.datetimeFormat),
                lang,
                user.id
                );

            deck.InsertDeckInDB();
            this.decksList.EditIndex = -1;
            this.decksList.SelectedIndex = -1;
            UpdateGrid();
        }

        protected void decksList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            String name = (decksList.Items[e.ItemIndex].FindControl("nameEdit") as TextBox).Text.Trim();

            if (name.Equals(""))
            {
                listAlert.Visible = true;
                listAlert.InnerText = "You have to fill the name of the deck.";
                return;
            }

            String desc = (decksList.Items[e.ItemIndex].FindControl("descriptionEdit") as TextBox).Text.Trim();
            String lang = (decksList.Items[e.ItemIndex].FindControl("languageEdit") as DropDownList).SelectedValue.Trim() + "/" + (decksList.Items[e.ItemIndex].FindControl("languageEdit2") as DropDownList).SelectedValue.Trim();

            String deckID = decksList.DataKeys[e.ItemIndex].Value.ToString();
            Utilities.Deck deck = Utilities.DBLink.GetDeckByDeckID(deckID);
            deck.name = name;
            deck.description = desc;
            deck.language = lang;

            deck.UpdateDeckInDB();
            this.decksList.EditIndex = -1;
            UpdateGrid();
        }

        protected void decksList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("CustomizeDeck"))
                {
                    String idDeck = e.CommandArgument.ToString();
                    Session["deckCustomize"] = Utilities.DBLink.GetDeckByDeckID(idDeck);
                    Response.Redirect("~/Customize.aspx");
                }
                else if (e.CommandName.Equals("ReviewDeck"))
                {
                    String idDeck = e.CommandArgument.ToString();
                    Utilities.Deck deck = Utilities.DBLink.GetDeckByDeckID(idDeck);
                    HttpCookie cookie = Request.Cookies[deck.id.ToString()];
                    Session["deckCustomize"] = deck;
                    Session["deckStudy"] = deck;

                    if (cookie != null)
                    {
                        Queue<Utilities.BilateralCard> cards = Utilities.DBLink.GetQueueBilateralCardsForReview(deck.id);
                        deck.AddProgressEventHandler(cards);
                        Session["cardsStudy"] = cards;
                        Session["countCards"] = cards.Count;
                    }
                    else
                    {
                        Queue<Utilities.Card> cards = Utilities.DBLink.GetQueueCardsForReview(deck.id);
                        deck.AddProgressEventHandler(cards);
                        Session["cardsStudy"] = cards;
                        Session["countCards"] = cards.Count;
                    }

                    Response.Redirect("~/Study.aspx");
                }
                else if (e.CommandName.Equals("LearnDeck"))
                {
                    String idDeck = e.CommandArgument.ToString();
                    Utilities.Deck deck = Utilities.DBLink.GetDeckByDeckID(idDeck);
                    HttpCookie cookie = Request.Cookies[deck.id.ToString()];
                    Session["deckCustomize"] = deck;
                    Session["deckStudy"] = deck;

                    if (cookie != null)
                    {
                        Queue<Utilities.BilateralCard> cards = Utilities.DBLink.GetQueueBilateralCardsForLearn(deck.id);
                        deck.AddProgressEventHandler(cards);
                        Session["cardsStudy"] = cards;
                        Session["countCards"] = cards.Count;
                    }
                    else
                    {
                        Queue<Utilities.Card> cards = Utilities.DBLink.GetQueueCardsForLearn(deck.id);
                        deck.AddProgressEventHandler(cards);
                        Session["cardsStudy"] = cards;
                        Session["countCards"] = cards.Count;
                    }

                    Response.Redirect("~/Study.aspx");
                }
            }
            catch (Exception ex)
            {
                listAlert.Visible = true;
                listAlert.InnerText = ex.Message;
                return;
            }
        }

        protected void decksList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (decksList.FindControl("ItemDataPager") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.decksList.EditIndex = -1;
            this.decksList.SelectedIndex = -1;
            UpdateGrid();
        }
    }
}