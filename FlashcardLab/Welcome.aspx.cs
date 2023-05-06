using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlashcardLab
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            welcomeTitle.InnerText = "";
            bd.Visible = false;
            lbl.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                welcomeTitle.InnerText = "WELCOM... wait what are you doing here?";
            }
            else
            {
                Utilities.User user = (Utilities.User)Session["user"];

                if (user.GetType() == typeof(Utilities.Moderator))
                {
                    Utilities.Moderator mod = (Utilities.Moderator)Session["user"];
                    welcomeTitle.InnerHtml = "WELCOME " + user.username + " <span style='color: " + mod.prefixColor + " !important'> [MOD]</span> !!!!!";
                }
                else if (user.id == 34)
                {
                    welcomeTitle.InnerHtml = "WELCOME <span style='color: red !important'>" + user.username + " [ADMIN]</span>!!!!!";
                }
                else
                {
                    welcomeTitle.InnerHtml = "WELCOME " + user.username + "!!!!!";
                }

                String today = DateTime.Today.ToString(Utilities.DBLink.dateFormat);
                if (user.birth.Equals(today))
                {
                    bd.ImageUrl = "images/cupcake.png";
                    bd.Visible = true;
                    lbl.InnerHtml = "<span style='color:#FF0000'>H</span><span style='color:#FF1B00'>a</span><span style='color:#FF3700'>p</span><span style='color:#FF5200'>p</span><span style='color:#FF6D00'>y</span> <span style='color:#FF8900'>b</span><span style='color:#FFA400'>i</span><span style='color:#FFBF00'>r</span><span style='color:#FFDB00'>t</span><span style='color:#FFF600'>h</span><span style='color:#EDFF00'>d</span><span style='color:#D1FF00'>a</span><span style='color:#B6FF00'>y</span><span style='color:#9BFF00'>!</span><span style='color:#80FF00'>!</span><span style='color:#64FF00'>!</span> <span style='color:#49FF00'>W</span><span style='color:#2EFF00'>e</span> <span style='color:#12FF00'>w</span><span style='color:#00FF09'>i</span><span style='color:#00FF24'>s</span><span style='color:#00FF40'>h</span> <span style='color:#00FF5B'>y</span><span style='color:#00FF76'>o</span><span style='color:#00FF92'>u</span> <span style='color:#00FFAD'>a</span><span style='color:#00FFC8'>l</span><span style='color:#00FFE4'>l</span> <span style='color:#00FFFF'>t</span><span style='color:#00E4FF'>h</span><span style='color:#00C8FF'>e</span> <span style='color:#00ADFF'>b</span><span style='color:#0092FF'>e</span><span style='color:#0076FF'>s</span><span style='color:#005BFF'>t</span> <span style='color:#0040FF'>a</span><span style='color:#0024FF'>n</span><span style='color:#0009FF'>d</span> <span style='color:#1200FF'>a</span><span style='color:#2E00FF'>p</span><span style='color:#4900FF'>p</span><span style='color:#6400FF'>r</span><span style='color:#7F00FF'>e</span><span style='color:#9B00FF'>c</span><span style='color:#B600FF'>i</span><span style='color:#D100FF'>a</span><span style='color:#ED00FF'>t</span><span style='color:#FF00F6'>e</span> <span style='color:#FF00DB'>y</span><span style='color:#FF00BF'>o</span><span style='color:#FF00A4'>u</span> <span style='color:#FF0089'>h</span><span style='color:#FF006D'>e</span><span style='color:#FF0052'>r</span><span style='color:#FF0037'>e</span><span style='color:#FF001B'>!</span>";
                    lbl.Visible = true;
                }
            }
        }
    }
}