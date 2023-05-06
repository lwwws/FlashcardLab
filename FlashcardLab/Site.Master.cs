using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlashcardLab
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            registerPage.Visible = true;
            loginPage.Visible = true;
            adminPage.Visible = false;
            userPage.Visible = false;
            signoutElem.Visible = false;
            userGreet.Text = "";


            if (Session["user"] != null)
            {
                registerPage.Visible = false;
                loginPage.Visible = false;
                userPage.Visible = true;
                signoutElem.Visible = true;

                Utilities.User user = (Utilities.User)Session["user"];

                userGreet.Text = "Hello " + user.username + "!";

                if (user.id == 34)
                {
                    adminPage.Visible = true;
                    userGreet.Text += " [ADMIN]";
                    userGreet.Style.Add("color", "red !important");

                }
                else if (user.GetType() == typeof(Utilities.Moderator))
                {
                    Utilities.Moderator mod = (Utilities.Moderator)Session["user"];
                    adminPage.Visible = true;
                    userGreet.Text += "<span style='color: " + mod.prefixColor + " !important'> [MOD]</span>";
                }
            }
        }

        protected void signoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            Response.Redirect("~/Default.aspx");
        }
    }
}