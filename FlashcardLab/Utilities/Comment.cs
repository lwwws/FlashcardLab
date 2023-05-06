using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FlashcardLab.Utilities
{
    [Serializable]
    public class Comment
    {
        public int id { get; set; }
        public String comment { get; set; }
        public int rating { get; set; }
        public int deckID { get; set; }
        public int userID { get; set; }
        public String creation { get; set; }

        public Comment(int id, String comment, int rating, int deckID, int userID, String creation)
        {
            this.id = id;
            this.comment = comment;
            this.rating = rating;
            this.deckID = deckID;
            this.userID = userID;
            this.creation = creation;
        }

        public Comment(int id, String comment, int rating, int deckID, int userID)
        {
            this.id = id;
            this.comment = comment;
            this.rating = rating;
            this.deckID = deckID;
            this.userID = userID;
            this.creation = DateTime.Today.ToString(DBLink.dateFormat);
        }
        public Comment(String id, String comment, String rating, String deckID, String userID, String creation)
        {
            this.id = Convert.ToInt32(id);
            this.comment = comment;
            this.rating = Convert.ToInt32(rating);
            this.deckID = Convert.ToInt32(deckID);
            this.userID = Convert.ToInt32(userID);
            this.creation = creation;
        }

        public Comment(DataRow row)
        {
            this.id = (int)row[0];
            this.comment = (String)row[1];
            this.rating = (Int16)row[2];
            this.deckID = (int)row[3];
            this.userID = (int)row[4];
            this.creation = row[5].ToString();
        }

        public void UpdateCommentInDB()
        {
            DBLink.UpdateComment(this);
        }

        public void InsertCommentInDB()
        {
            DBLink.InsertComment(this);
        }

        public void RemoveCommentInDB()
        {
            DBLink.DeleteComment(this.id);
        }
    }
}