using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashcardLab.Utilities
{
    public class Moderator : User
    {
        public String modGranted { get; set; }
        public String prefixColor { get; set; }


        public Moderator(int id, String username, String password, String mail, String country, String gender, String birth, String creation, String modGranted, String prefixColor) : base(id, username, password, mail, country, gender, birth, creation)
        {
            this.modGranted = modGranted;
            this.prefixColor = prefixColor;
        }

        public Moderator(User user, String modGranted, String prefixColor) : base(user.id, user.username, user.password, user.mail, user.country, user.gender, user.birth, user.creation)
        {
            this.modGranted = modGranted;
            this.prefixColor = prefixColor;
        }

        public override void PrintUser()
        {
            Console.WriteLine("*** USER *** \n\t id = {0}\n\t username = {1}\n\t password = {2}\n\t mail = {3}\n\t country = {4}\n\t gender = {5}\n\t birth = {6}\n\t creation = {7}\n\t modGranted = {8}\n\t prefixColor = {9}", id, username, password, mail, country, gender, birth, creation, modGranted, prefixColor);
        }

        public override void UpdateUserInDB()
        {
            DBLink.UpdateModerator(this);
        }

        public override void InsertUserInDB()
        {
            DBLink.InsertUser(this);
            DBLink.GrantUser(this.id);
        }

        public override void RemoveUserInDB()
        {
            DBLink.DeleteUser(this.id);
            DBLink.DeleteModFields(this.id);
        }
    }
}