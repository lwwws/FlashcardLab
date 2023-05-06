using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FlashcardLab
{
    public partial class Customize : System.Web.UI.Page
    {
        Utilities.User user;
        Utilities.Deck deck;
        DataTable deckCards;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Session["user"] == null || Session["deckCustomize"] == null)
                Response.Redirect("/UnauthorizedAccess.aspx");

            user = (Utilities.User)Session["user"];
            deck = (Utilities.Deck)Session["deckCustomize"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            alertDeck.Text = "";
            title.InnerText = "Customize \"" + deck.name + "\" deck!";
            review.Text = "Review (" + Utilities.DBLink.CountCardsForReview(deck.id) + " cards left)";
            learn.Text = "Learn (" + Utilities.DBLink.CountCardsForLearn(deck.id) + " cards left)";
            gridEmpty.Visible = false;

            if (alertGrid.Text.Equals(""))
            {
                alertGrid.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                HttpCookie cookie = Request.Cookies[deck.id.ToString()];

                if (cookie != null)
                {
                    studyBoth.Checked = true;
                }
                else
                {
                    studyBoth.Checked = false;
                }

                shared.Checked = deck.shared;

                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            deckCards = Utilities.DBLink.GetCardsTableByDeckID(deck.id);

            if (deckCards.Rows.Count == 0)
            {
                gridEmpty.Visible = true;
                gridEmpty.Text = "You have no cards! Perhaps create some for your \'" + deck.name + "\' deck?";
            }

            cardsGrid.DataSource = deckCards;
            cardsGrid.DataBind();
        }

        protected void cardsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    if ((cardsGrid.HeaderRow.FindControl("frontHeader") as TextBox).Text.Trim().Equals("") || (cardsGrid.HeaderRow.FindControl("backHeader") as TextBox).Text.Trim().Equals(""))
                    {
                        alertGrid.Visible = true;
                        alertGrid.Text = "You have to fill the front and back.";
                        alertGrid.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    String front = (cardsGrid.HeaderRow.FindControl("frontHeader") as TextBox).Text.Trim();
                    String back = (cardsGrid.HeaderRow.FindControl("backHeader") as TextBox).Text.Trim();

                    Utilities.Card card = new Utilities.Card(
                        0,
                        String.Join(",", front.Split(',').Select(s => s.Trim()).ToArray()).Trim(','),
                        String.Join(",", back.Split(',').Select(s => s.Trim()).ToArray()).Trim(','),
                        DateTime.Now.ToString(Utilities.DBLink.datetimeFormat),
                        0,
                        0,
                        0,
                        (cardsGrid.HeaderRow.FindControl("noteHeader") as TextBox).Text.Trim(),
                        deck.id
                        );

                    Utilities.DBLink.InsertCard(card);
                    Utilities.DBLink.UpdateDeckProgress(deck.id);
                    Utilities.DBLink.UpdateDeckNextByEarliestDate(deck.id);
                    UpdateGrid();
                    // Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                alertGrid.Visible = true;
                alertGrid.Text = ex.Message;
                alertGrid.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
        protected void cardsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            cardsGrid.EditIndex = e.NewEditIndex;
            UpdateGrid();
        }

        protected void cardsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cardsGrid.PageIndex = e.NewPageIndex;
            UpdateGrid();
        }

        protected void cardsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cardsGrid.EditIndex = -1;
            UpdateGrid();
        }

        protected void cardsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if ((cardsGrid.Rows[e.RowIndex].FindControl("frontEdit") as TextBox).Text.Trim().Equals("") || (cardsGrid.Rows[e.RowIndex].FindControl("backEdit") as TextBox).Text.Trim().Equals(""))
                {
                    alertGrid.Visible = true;
                    alertGrid.Text = "You cannot leave front and back empty.";
                    alertGrid.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Utilities.Card initCard = Utilities.DBLink.GetCardByCardID(cardsGrid.DataKeys[e.RowIndex].Value.ToString());

                String front = (cardsGrid.Rows[e.RowIndex].FindControl("frontEdit") as TextBox).Text.Trim();
                String back = (cardsGrid.Rows[e.RowIndex].FindControl("backEdit") as TextBox).Text.Trim();

                Utilities.Card card = new Utilities.Card(
                        initCard.id,
                        String.Join(",", front.Split(',').Select(s => s.Trim()).ToArray()).Trim(','),
                        String.Join(",", back.Split(',').Select(s => s.Trim()).ToArray()).Trim(','),
                        initCard.next,
                        initCard.level,
                        initCard.correct,
                        initCard.incorrect,
                        (cardsGrid.Rows[e.RowIndex].FindControl("noteEdit") as TextBox).Text.Trim(),
                        deck.id
                        );

                cardsGrid.EditIndex = -1;
                Utilities.DBLink.UpdateCard(card);
                UpdateGrid();
            }
            catch (Exception ex)
            {
                alertGrid.Visible = true;
                alertGrid.Text = ex.Message;
                alertGrid.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void cardsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                cardsGrid.EditIndex = -1;
                Utilities.DBLink.DeleteCard(cardsGrid.DataKeys[e.RowIndex].Value.ToString());
                Utilities.DBLink.UpdateDeckProgress(deck.id);
                Utilities.DBLink.UpdateDeckNextByEarliestDate(deck.id);
                UpdateGrid();
            }
            catch (Exception ex)
            {
                alertGrid.Visible = true;
                alertGrid.Text = ex.Message;
                alertGrid.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void alterDeck_Click(object sender, EventArgs e)
        {
            if (nameDeck.Text.Equals(""))
            {
                alertDeck.Text = "You have to fill the name.";
                return;
            }

            deck.name = nameDeck.Text;
            deck.description = descriptionDeck.InnerText;
            deck.language = language.Value + "/" + language2.Value;

            Utilities.DBLink.UpdateDeck(deck);
            cardsGrid.DataBind();
        }

        public String GetTimeSpanLeft(string datetime)
        {
            DateTime now = DateTime.Now;
            DateTime next = Utilities.DBLink.ConvertToDatetime(datetime);

            if(now.CompareTo(next) > 0)
            {
                return "0 days, 0 hours, 0 minutes, 0 seconds";
            }

            TimeSpan ts = next.Subtract(now);
            return String.Format("{0} days, {1} hours, {2} minutes, {3} seconds", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
        }

        protected void learn_Click(object sender, EventArgs e)
        {
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

        protected void review_Click(object sender, EventArgs e)
        {
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

        protected void shared_CheckedChanged(object sender, EventArgs e)
        {
            Utilities.DBLink.UpdateDecksShared(deck.id, !deck.shared);

            if (deck.shared)
            {
                Utilities.DBLink.DeleteCommentsWithDeckID(deck.id);
            }

            UpdateGrid();
        }

        protected void studyBoth_CheckedChanged(object sender, EventArgs e)
        {
            if (studyBoth.Checked)
            {
                HttpCookie cookie = Request.Cookies[deck.id.ToString()];

                if (cookie == null)
                {
                    Response.Cookies.Add(new HttpCookie(deck.id.ToString())
                    {
                        Expires = DateTime.Now.AddDays(30),
                    });
                }
            }
            else
            {
                HttpCookie cookie = Request.Cookies[deck.id.ToString()];

                if (cookie != null)
                {
                    Response.Cookies[deck.id.ToString()].Expires = DateTime.Now.AddDays(-1);
                }
            }

            UpdateGrid();
        }

        public String GetHtmlSpan(int level)
        {
            return Utilities.LevelSpans.GetHtmlSpan(level);
        }
    }
}