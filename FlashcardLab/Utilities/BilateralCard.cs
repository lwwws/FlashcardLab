using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FlashcardLab.Utilities
{
    public class BilateralCard : Card
    {
        // NOT FOR CARD TABLE IN DATABASE!!!!
        public bool answeredCorrectly { get; set; }

        public BilateralCard twin { get; set; }

        public BilateralCard(Card card) : base(card.id, card.front, card.back, card.next, card.level, card.correct, card.incorrect, card.note, card.deckID)
        {
            // NOW THE CORRECT/INCORRECT WORKS AS COUNTER
            this.correct = 0;
            this.incorrect = 0;
            this.answeredCorrectly = false;
            this.twin = null;
        }

        public BilateralCard(Card card, BilateralCard twin) : base(card.id, card.front, card.back, card.next, card.level, card.correct, card.incorrect, card.note, card.deckID)
        {
            this.correct = 0;
            this.incorrect = 0;
            this.answeredCorrectly = false;
            this.twin = twin;
        }

        public BilateralCard(DataRow row, BilateralCard twin) : base(row)
        {
            this.answeredCorrectly = false;
            this.twin = twin;
            this.correct = 0;
            this.incorrect = 0;
        }

        public BilateralCard(DataRow row, BilateralCard twin, bool switched) : base(row)
        {
            this.answeredCorrectly = false;
            this.twin = twin;
            this.correct = 0;
            this.incorrect = 0;

            if (switched)
            {
                string temp = front;
                front = back;
                back = temp;
            }
        }

        public override void Answered(bool correctly)
        {
            if (!correctly)
            {
                cost++;
                incorrect++;
            }
            else
            {
                answeredCorrectly = true;
                correct++;
                int penalty = 1;

                if (level == 8)
                {
                    penalty = 2;
                }

                level -= ((int)Math.Round(0.6 * cost) * penalty);

                if (level < 0)
                {
                    level = 0;
                }
                else if (level > 9)
                {
                    level = 9;
                }

                if (twin.answeredCorrectly == true)
                {
                    UpdateCardInDB();
                    OnAnswered();
                }
            }
        }

        public override void UpdateCardInDB()
        {
            int avgLevel = Convert.ToInt32(Math.Floor((0.0 + this.level + twin.level) / 2));
            DBLink.UpdateCard(this.id, DateTime.Now.Add(LevelSpans.spans[avgLevel]).ToString(DBLink.datetimeFormat), avgLevel, correct + twin.correct, incorrect + twin.incorrect);
        }

        public override void InsertCardInDB()
        {
            return;
        }

        public override void RemoveCardInDB()
        {
            return;
        }
    }
}