using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace FlashcardLab
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            username.Text = "";
            password.Text = "";
            rpassword.Text = "";
            mail.Text = "";
            countries.Value = "";
            gender.Text = "";

            usernameAlert.InnerText = "";
            passwordAlert.InnerText = "";
            mailAlert.InnerText = "";
            dateAlert.InnerText = "";
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            bool register = true;

            usernameAlert.InnerText = "";
            passwordAlert.InnerText = "";
            mailAlert.InnerText = "";
            dateAlert.InnerText = "";

            if (!password.Text.Trim().Equals(rpassword.Text.Trim()))
            {
                passwordAlert.InnerText += "Password must be the same. ";
                register = false;
            }

            if (password.Text.Trim().Length < 5)
            {
                passwordAlert.InnerText += "Password is too short. ";
                register = false;
            }

            if (birth.Value.Equals("") || username.Text.Trim().Equals("") || password.Text.Trim().Equals("") || rpassword.Text.Trim().Equals("") || mail.Text.Equals(""))
            {
                usernameAlert.InnerText += "Enter value to all of the fields. ";
                register = false;
            }

            if(username.Text.Contains(' '))
            {
                usernameAlert.InnerText += "No spaces in username allowed. ";
                register = false;
            }

            if (Utilities.DBLink.UsernameExists(username.Text.Trim()))
            {
                usernameAlert.InnerText += "Username is already taken. ";
                register = false;
            }

            if (Utilities.DBLink.MailExists(mail.Text.Trim()))
            {
                mailAlert.InnerText += "Mail is already taken. ";
                register = false;
            }

            if (!Utilities.DBLink.IsDate(birth.Value, "yyyy-MM-dd"))
            {
                dateAlert.InnerText += "Invalid birth date. ";
                register = false;
            }

            if (!register)
            {
                return;
            }

            birth.Value = Utilities.DBLink.ConvertDate(birth.Value, "yyyy-MM-dd");

            Utilities.User user = new Utilities.User(
                0,
                username.Text.Trim(),
                password.Text.Trim(),
                mail.Text,
                countries.Value,
                gender.Text,
                birth.Value,
                DateTime.Today.ToString(Utilities.DBLink.dateFormat));

            Utilities.DBLink.InsertUser(user);
            Clear();
            Response.Redirect("Default.aspx");
        }
    }
}