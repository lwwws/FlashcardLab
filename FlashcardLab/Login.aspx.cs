using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlashcardLab
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if(Session["user"] != null)
            {
                Response.Redirect("/View.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            address.Visible = false;
        }

        protected void enter_Click(object sender, EventArgs e)
        {
            Utilities.User user;

            if (Utilities.DBLink.UsernameExists(username.Text.Trim())) {
                user = Utilities.DBLink.GetUserByUsername(username.Text.Trim());
            }
            else
            {
                usernameAlert.InnerText = "Username does not exist";
                return;
            }

            if (user.password.Equals(password.Text))
            {

                if (Utilities.DBLink.IsMod(user.id))
                {
                    Session["user"] = Utilities.DBLink.GetModeratorByUser(user);
                }
                else
                {
                    Session["user"] = user;
                }

                Clear();
                Session.Timeout = 60;
                Response.Redirect("Welcome.aspx");
            }
            else
            {
                passwordAlert.InnerText = "Wrong Password";
                return;
            }
        }

        public void Clear()
        {
            address.Visible = false;
            username.Text = "";
            password.Text = "";
            mailAlert.Text = "";

            passwordAlert.InnerText = "";
            usernameAlert.InnerText = "";
        }

        protected void clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void forgotBtn_Click(object sender, EventArgs e)
        {
            mailAlert.Text = "Please contact admin via email:";
            address.Visible = true;
        }
    }
}