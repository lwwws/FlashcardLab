using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FlashcardLab
{
    public partial class Stats : System.Web.UI.Page
    {
        Utilities.User user;
        DataTable cards;
        Utilities.Deck deck;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("/UnauthorizedAccess.aspx");

            user = (Utilities.User)Session["user"];
            deck = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            title.InnerText = user.username + "'s statistics ";
            deckStats.Visible = false;
            UpdateUserStats();

            if (!IsPostBack)
            {
                DataTable decks = Utilities.DBLink.GetDecksTableByUserID(user.id);

                decksList.DataSource = decks;
                decksList.DataTextField = "name";
                decksList.DataValueField = "id";
                decksList.DataBind();

                if (decks.Rows.Count == 0)
                {
                    decksList.Items.Add(new ListItem("You have no decks...", "-1"));
                }
                else
                {
                    decksList.Items.Insert(0, new ListItem("--Select--", "-1"));
                }

                //if(decks.Rows.Count == 0)
                //{
                //    decksList.Items.Add(new ListItem("You have no decks...", "-1"));
                //}
                //else
                //{
                //    decksList.Items.Add(new ListItem("Select", "-1"));
                //}

                //foreach (DataRow row in decks.Rows)
                //{
                //    decksList.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
                //}
            }
        }

        protected void decksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!decksList.SelectedValue.Equals("-1"))
            {
                ViewState["cards"] = Utilities.DBLink.GetCardsTableByDeckID(decksList.SelectedValue);
                cards = (DataTable)ViewState["cards"];
                deck = Utilities.DBLink.GetDeckByDeckID(decksList.SelectedValue);
                UpdateStats();
            }
        }

        private void UpdateStats()
        {
            deckStats.Visible = true;

            stat1.InnerHtml = "Average level of the \"" + decksList.SelectedItem.Text + "\" deck is <strong>" + Utilities.LevelSpans.GetHtmlSpan(AverageLevelDeck()) + "</strong>!";

            dataProgress.Value = deck.progress.ToString();

            deckName.Value = decksList.SelectedItem.Text;

            int[] week = new int[7];
            int numDay = (int)DateTime.Today.DayOfWeek;
            DateTime today = DateTime.Today;

            for (int i = numDay; i < week.Length; i++)
            {
                week[i] = CountCardsForDay(today);
                today = today.AddDays(1);
            }

            for (int i = 0; i < numDay; i++)
            {
                week[i] = CountCardsForDay(today);
                today = today.AddDays(1);
            }

            week[numDay] += CountDaysBefore(DateTime.Today);

            dataWeek.Value = String.Join(",", week);
            dataLRF.Value = String.Join(",", CountCardsForLRF());
            dataCorIncor.Value = CountCorrectDeck() + "," + CountIncorrectDeck();


            Page.ClientScript.RegisterStartupScript(this.GetType(), "statsScript", "deckStats();", true);
        }

        private void UpdateUserStats()
        {
            DateTime t = Utilities.DBLink.ConvertToDate(user.creation);
            TimeSpan ts = DateTime.Today.Subtract(t);

            creationStat.InnerHtml = "It has been <u>" + ts.Days + " days</u> since the creation of your account!";
        }
        private int AverageLevelDeck()
        {
            int sum = 0;

            if(cards.Rows.Count == 0)
            {
                return 0;
            }

            foreach (DataRow row in cards.Rows)
            {
                sum += (int)row[4];
            }

            return sum / cards.Rows.Count;
        }

        private int CountDaysBefore(DateTime date)
        {
            int count = 0;

            foreach (DataRow row in cards.Rows)
            {
                if (date.CompareTo(Utilities.DBLink.ConvertToDate(row[3].ToString().Substring(0, 10))) > 0)
                {
                    count++;
                }
            }

            return count;
        }

        private int CountCardsForDay(DateTime date)
        {
            int count = 0;

            foreach (DataRow row in cards.Rows)
            {
                if (date.Equals(Utilities.DBLink.ConvertToDate(row[3].ToString().Substring(0, 10))))
                {
                    count++;
                }
            }

            return count;
        }

        private int[] CountCardsForLRF()
        {
            // Learn Review Finished
            int[] count = new int[3];

            count[0] = 0;
            count[1] = 0;
            count[2] = 0;

            foreach (DataRow row in cards.Rows)
            {
                if((int)row[4] == 0)
                {
                    count[0] += 1;
                }
                else if((int)row[4] != 9)
                {
                    count[1] += 1;
                }
                else
                {
                    count[2] += 1;
                }
            }

            return count;
        }

        private int CountIncorrectDeck()
        {
            int count = 0;

            foreach (DataRow row in cards.Rows)
            {
                count += (int)row[6];
            }

            return count;
        }
        private int CountCorrectDeck()
        {
            int count = 0;

            foreach (DataRow row in cards.Rows)
            {
                count += (int)row[5];
            }

            return count;
        }
    }
}