using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FlashcardLab
{
    public partial class Study : System.Web.UI.Page
    {
        Utilities.User user;
        Utilities.Deck deck;
        Queue<Utilities.Card> cards;
        Queue<Utilities.BilateralCard> bicards;
        bool isDual = false;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Session["user"] == null || Session["deckStudy"] == null)
                Response.Redirect("/UnauthorizedAccess.aspx");

            user = (Utilities.User)Session["user"];
            deck = (Utilities.Deck)Session["deckStudy"];
            HttpCookie cookie = Request.Cookies[deck.id.ToString()];

            if (cookie == null)
            {
                cards = (Queue<Utilities.Card>)Session["cardsStudy"];

                if(cards == null)
                    Response.Redirect("/UnauthorizedAccess.aspx");
            }
            else
            {
                isDual = true;
                bicards = (Queue<Utilities.BilateralCard>)Session["cardsStudy"];

                if(bicards == null)
                    Response.Redirect("/UnauthorizedAccess.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            alert.Visible = false;
            previous.Visible = false;
            note.Visible = false;

            UpdateHeader();

            if (!isDual)
            {
                if (cards == null)
                {
                    Response.Redirect("~/Customize.aspx");
                }
                if (cards.Count == 0)
                {
                    Session.Remove("cardsStudy");
                    Session.Remove("countCards");

                    Utilities.DBLink.UpdateDeckNextByEarliestDate(deck.id);
                    Utilities.DBLink.UpdateDeckProgress(deck.id);
                    Response.Redirect("~/Customize.aspx");
                }

                text.InnerText = cards.Peek().front;
            }
            else
            {
                if (bicards == null)
                {
                    Response.Redirect("~/Customize.aspx");
                }
                if (bicards.Count == 0)
                {
                    Session.Remove("cardsStudy");
                    Session.Remove("countCards");

                    Utilities.DBLink.UpdateDeckNextByEarliestDate(deck.id);
                    Utilities.DBLink.UpdateDeckProgress(deck.id);
                    Response.Redirect("~/Customize.aspx");
                }

                text.InnerText = bicards.Peek().front;
            }

            answerBox.Focus();
        }

        private void UpdateHeader()
        {
            double r;

            if (isDual)
            {
                left.Text = bicards.Count.ToString() + " cards to go";
                r = (1 - (1.0 * bicards.Count / (int)Session["countCards"])) * 100;
            }
            else
            {
                left.Text = cards.Count.ToString() + " cards to go";
                r = (1 - (1.0 * cards.Count / (int)Session["countCards"])) * 100;
            }

            progress.Attributes.Add("aria-valuenow", r.ToString());
            progress.Attributes.Add("style", "width: " + r.ToString() + "%");
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            answerBox.Text = answerBox.Text.ToLower();



            if (!isDual)
            {
                if (!cards.Peek().IsCorrect(answerBox.Text))
                {
                    if (cards.Peek().HasTypo(answerBox.Text))
                    {
                        alert.Visible = true;
                        alert.Style.Add("background-color", "GoldenRod");
                        alert.Text = "Perhaps you made a typo?";
                        animWrapper.Style.Add("animation", "dive 0.8s");
                        return;
                    }
                    else
                    {
                        cards.Peek().Answered(false);

                        alert.Visible = true;
                        alert.Style.Add("background-color", "Crimson");
                        alert.Text = "WRONG";
                        animWrapper.Style.Add("animation", "shake 0.8s");

                        previous.Visible = true;
                        previous.Text = "<strong> Previous card: </strong> <br/>" + cards.Peek().front + " <b>―</b> " + cards.Peek().back;

                        if (!cards.Peek().note.Trim().Equals(""))
                        {
                            note.Visible = true;
                            note.Text = "<strong> Note: </strong> <br/>" + cards.Peek().note;
                        }

                        cards.Enqueue(cards.Dequeue());
                    }
                }
                else
                {
                    cards.Peek().Answered(true);

                    alert.Visible = true;
                    alert.Style.Add("background-color", "Green");
                    animWrapper.Style.Add("animation", "none");
                    alert.Text = "TRUE";

                    previous.Visible = true;
                    previous.Text = "<strong> Previous card: </strong> <br/>" + cards.Peek().front + " <b>―</b> " + cards.Peek().back;

                    if (!cards.Peek().note.Trim().Equals(""))
                    {
                        note.Visible = true;
                        note.Text = "<strong> Note: </strong> <br/>" + cards.Peek().note;
                    }

                    cards.Dequeue();
                }

                if (cards.Count == 0)
                {
                    Response.Redirect("~/Customize.aspx");
                }

                text.InnerText = cards.Peek().front;
            }
            else
            {
                if (!bicards.Peek().IsCorrect(answerBox.Text))
                {
                    if (bicards.Peek().HasTypo(answerBox.Text))
                    {
                        alert.Visible = true;
                        alert.Style.Add("background-color", "GoldenRod");
                        alert.Text = "Perhaps you made a typo?";
                        animWrapper.Style.Add("animation", "dive 0.8s");
                        return;
                    }
                    else
                    {
                        bicards.Peek().Answered(false);

                        alert.Visible = true;
                        alert.Style.Add("background-color", "Crimson");
                        alert.Text = "WRONG";
                        animWrapper.Style.Add("animation", "shake 0.8s");

                        previous.Visible = true;
                        previous.Text = "<strong> Previous card: </strong> <br/>" + bicards.Peek().front + " <b>―</b> " + bicards.Peek().back;

                        if (!bicards.Peek().note.Trim().Equals(""))
                        {
                            note.Visible = true;
                            note.Text = "<strong> Note: </strong> <br/>" + bicards.Peek().note;
                        }

                        bicards.Enqueue(bicards.Dequeue());
                    }
                }
                else
                {
                    bicards.Peek().Answered(true);

                    alert.Visible = true;
                    alert.Style.Add("background-color", "Green");
                    animWrapper.Style.Add("animation", "none");
                    alert.Text = "TRUE";

                    previous.Visible = true;
                    previous.Text = "<strong> Previous card: </strong> <br/>" + bicards.Peek().front + " <b>―</b> " + bicards.Peek().back;

                    if (!bicards.Peek().note.Trim().Equals(""))
                    {
                        note.Visible = true;
                        note.Text = "<strong> Note: </strong> <br/>" + bicards.Peek().note;
                    }

                    bicards.Dequeue();
                }

                if (bicards.Count == 0)
                {
                    Response.Redirect("~/Customize.aspx");
                }

                text.InnerText = bicards.Peek().front;
            }




            UpdateHeader();

            answerBox.Text = "";
        }
    }
}