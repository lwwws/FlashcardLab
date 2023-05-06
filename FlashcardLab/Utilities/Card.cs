using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FlashcardLab.Utilities
{
    [Serializable]
    public class Card
    {
        public delegate void ProgressDelegate();
        public event ProgressDelegate progressEvent;
        public int id { get; set; }
        public String front { get; set; }
        public String back { get; set; }
        public String next { get; set; }
        public int level { get; set; }
        public int correct { get; set; }
        public int incorrect { get; set; }
        public String note { get; set; }
        public int deckID { get; set; }


        protected double cost { get; set; } // cost incorrectly answered until correct
                                       // intializing with -1 specifically for the progress formula

        public Card(int id, String front, String back,  String next, int level, int correct, int incorrect, String note, int deckID)
        {
            this.id = id;
            this.front = front;
            this.back = back;
            this.next = next;
            this.level = level;
            this.correct = correct;
            this.incorrect = incorrect;
            this.note = note;
            this.deckID = deckID;
            cost = -1;
        }

        public Card(String id, String front, String back, String next, String level, String correct, String incorrect, String note, String deckID)
        {
            this.id = int.Parse(id);
            this.front = front;
            this.back = back;
            this.next = next;
            this.level = int.Parse(level);
            this.correct = int.Parse(correct);
            this.incorrect = int.Parse(incorrect);
            this.note = note;
            this.deckID = int.Parse(deckID);
            cost = -1;
        }

        public Card(DataRow row)
        {
            this.id = (int)row[0];
            this.front = (String)row[1];
            this.back = (String)row[2];
            this.next = row[3].ToString();
            this.level = (int)row[4];
            this.correct = (int)row[5];
            this.incorrect = (int)row[6];
            this.note = (String)row[7];
            this.deckID = (int)row[8];
            cost = -1;
        }

        public bool IsCorrect(String answer)
        {
            String[] expectedAnswers = back.Split(',');

            foreach (String st in expectedAnswers)
            {
                if (st.ToLower().Trim().Equals(answer.ToLower().Trim()))
                    return true;
            }

            return false;
        }

        // There are 10 levels (from 0 to 9)
        // 0 is when you haven't gueesed the word correctly at all, 9 is you know the word fluently
        //
        // You learn words for the first time in (Learn)
        // After guessing correctly, it will appear in (Review).
        //
        // Each time after level up the word will appear in review after certain amount of time according to LevelSpans.cs
        //
        // Based on the formula => (next_level) = (current_level) - (percent) * (cost_until_correct) * (penalty)
        // you can level down, to the point it will appear in (Learn) again

        public virtual void Answered(bool correctly)
        {
            if (!correctly)
            {
                cost += 2;
                incorrect++;
                UpdateCardInDB();
            }
            else
            {
                correct++;
                int penalty = 1;

                if(level == 8)
                {
                    penalty = 2;
                }

                level -= ((int)Math.Round(0.6 * cost) * penalty);

                if(level < 0)
                {
                    level = 0;
                }
                else if(level > 9)
                {
                    level = 9;
                }

                next = DateTime.Now.Add(LevelSpans.spans[level]).ToString(DBLink.datetimeFormat);
                UpdateCardInDB();

                progressEvent.Invoke();
            }
        }

        protected virtual void OnAnswered()
        {
            progressEvent.Invoke();
        }

        public bool HasTypo(String answer)
        {
            String[] expectedAnswers = back.Split(',');

            foreach(String st in expectedAnswers)
            {
                if (Math.Abs(st.Length - answer.Length) <= 5)
                {
                    int n = 0;

                    if (3 <= st.Length && st.Length <= 4)
                        n = 1;
                    else if (5 <= st.Length && st.Length <= 8)
                        n = 2;
                    else if (st.Length >= 9)
                        n = 3;

                    if (n != 0)
                    {
                        if (LevenshteinDistance(back.ToLower().Trim(), answer.ToLower().Trim()) <= n)
                        {
                            cost += 1;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        // Checking similarity between strings
        private static int LevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }

        public virtual void UpdateCardInDB()
        {
            DBLink.UpdateCard(this);
        }

        public virtual void InsertCardInDB()
        {
            DBLink.InsertCard(this);
        }

        public virtual void RemoveCardInDB()
        {
            DBLink.DeleteCard(this.id);
        }
    }
}