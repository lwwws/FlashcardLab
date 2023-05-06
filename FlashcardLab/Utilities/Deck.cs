using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FlashcardLab.Utilities
{
    [Serializable]
    public class Deck
    {
        public int id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public double progress { get; set; }
        public String next { get; set; }
        public String language { get; set; }
        public int userID { get; set; }
        public bool shared { get; set; }
        public String creation { get; set; }

        public Deck(int id, String name, String description, double progress, String next, String language, int userID)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.progress = progress;
            this.next = next;
            this.language = language;
            this.userID = userID;
            this.shared = false;
            this.creation = DateTime.Today.ToString(DBLink.dateFormat);
        }

        public Deck(int id, String name, String description, double progress, String next, String language, int userID, bool shared, String creation)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.progress = progress;
            this.next = next;
            this.language = language;
            this.userID = userID;
            this.shared = shared;
            this.creation = creation;
        }

        public Deck(int id, String name, String description, double progress, String next, String language, int userID, String creation)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.progress = progress;
            this.next = next;
            this.language = language;
            this.userID = userID;
            this.shared = false;
            this.creation = creation;
        }

        public Deck(DataRow row)
        {
            this.id = (int)row[0];
            this.name = (String)row[1];
            this.description = (String)row[2];
            this.progress = Convert.ToDouble(row[3]);
            this.next = row[4].ToString();
            this.language = (String)row[5];
            this.userID = (int)row[6];
            this.shared = (bool)(row[7]);
            this.creation = row[8].ToString();
        }

        public Deck(int id, String name, String description, double progress, String next, String language, int userID, bool shared)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.progress = progress;
            this.next = next;
            this.language = language;
            this.userID = userID;
            this.shared = shared;
            this.creation = DateTime.Today.ToString(DBLink.dateFormat);
        }

        public Deck(String id, String name, String description, String progress, String next, String language, String userID)
        {
            this.id = int.Parse(id);
            this.name = name;
            this.description = description;

            double temp = 0.0;
            double.TryParse(progress, out temp);
            this.progress = temp;

            this.next = next;
            this.language = language;
            this.userID = int.Parse(userID);
            this.shared = false;
            this.creation = DateTime.Today.ToString(DBLink.dateFormat);
        }

        public Deck(String id, String name, String description, String progress, String next, String language, String userID, bool shared)
        {
            this.id = int.Parse(id);
            this.name = name;
            this.description = description;

            double temp = 0.0;
            double.TryParse(progress, out temp);
            this.progress = temp;

            this.next = next;
            this.language = language;
            this.userID = int.Parse(userID);
            this.shared = shared;
            this.creation = DateTime.Today.ToString(DBLink.dateFormat);
        }

        public void OnUpdateProgress()
        {
            progress = DBLink.UpdateDeckProgress(this.id);
            next = DBLink.UpdateDeckNextByEarliestDate(this.id);
        }


        public void AddProgressEventHandler(Queue<Card> cards)
        {
            foreach(Card card in cards)
            {
                card.progressEvent += OnUpdateProgress;
            }
        }

        public void AddProgressEventHandler(Queue<BilateralCard> cards)
        {
            foreach (BilateralCard card in cards)
            {
                card.progressEvent += OnUpdateProgress;
            }
        }

        public void UpdateDeckInDB()
        {
            DBLink.UpdateDeck(this);
        }

        public void InsertDeckInDB()
        {
            DBLink.InsertDeck(this);
        }

        public void RemoveDeckInDB()
        {
            DBLink.DeleteDeck(this.id);
        }
    }
}