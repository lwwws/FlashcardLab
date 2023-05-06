using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashcardLab.Utilities
{
    public class User
    {
        public int id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String mail { get; set; }
        public String country { get; set; }
        public String gender { get; set; }
        public String birth { get; set; }
        public String creation { get; set; }
        public User(int id, String username, String password, String mail, String country, String gender, String birth, String creation)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.mail = mail;
            this.country = country;
            this.gender = gender;
            this.birth = birth;
            this.creation = creation;
        }

        public User(String id, String username, String password, String mail, String country, String gender, String birth, String creation)
        {
            this.id = int.Parse(id);
            this.username = username;
            this.password = password;
            this.mail = mail;
            this.country = country;
            this.gender = gender;
            this.birth = birth;
            this.creation = creation;
        }

        public virtual void PrintUser()
        {
            Console.WriteLine("*** USER *** \n\t id = {0}\n\t username = {1}\n\t password = {2}\n\t mail = {3}\n\t country = {4}\n\t gender = {5}\n\t birth = {6}\n\t creation = {7}", id, username, password, mail, country, gender, birth, creation);
        }

        public virtual void UpdateUserInDB()
        {
            DBLink.UpdateUser(this);
        }

        public virtual void InsertUserInDB()
        {
            DBLink.InsertUser(this);
        }

        public virtual void RemoveUserInDB()
        {
            DBLink.DeleteUser(this.id);
        }
    }
}