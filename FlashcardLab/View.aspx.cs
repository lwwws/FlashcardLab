using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlashcardLab.Profile
{
    public partial class View : System.Web.UI.Page
    {
        Utilities.User user;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("/UnauthorizedAccess.aspx");

            user = (Utilities.User)Session["user"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            usernameAlert.InnerText = "";
            passwordAlert.InnerText = "";
            mailAlert.InnerText = "";
            birthAlert.InnerText = "";
            imageAlert.InnerText = "";

            if (!Page.IsPostBack)
            {
                UpdateFields();
            }
        }


        protected void signout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            Response.Redirect("~/Default.aspx");
        }

        private void UpdateFields()
        {
            usernameProfile.InnerText = user.username;
            username.Text = "Username: " + user.username;
            password.Text = "Password: *********";
            mail.Text = "E-Mail: " + user.mail;
            birth.Text = "Birth Date: " + user.birth;
            creationDate.InnerText = "Creation of the account: " + user.creation;
            gender.Text = "Gender: " + user.gender;
            country.Text = "Country: " + user.country;
            mrow.Visible = false;


            if (user.GetType() == typeof(Utilities.Moderator))
            {
                Utilities.Moderator mod = (Utilities.Moderator)Session["user"];
                username.Text = "Username: " + user.username + "<span style='color: " + mod.prefixColor + " !important'> [MOD]</span>";
                usernameProfile.InnerHtml = user.username + "<span style='color: " + mod.prefixColor + " !important'> [MOD]</span>";
                mrow.Visible = true;
                prefixColor.Text = "Current prefix color: " + mod.prefixColor;
                prefixColor.Style.Add("color", mod.prefixColor);
                modGrantedDate.InnerText = "Moderator granted at: " + Utilities.DBLink.GetDateOnly(mod.modGranted);
            }
            if (user.id == 34)
            {
                username.Text =  user.username + " [ADMIN]";
                username.Text = "Username: <span style='color: red !important'>" + username.Text + "</span>";
                usernameProfile.InnerHtml = "<span style='color: red !important'>" + username.Text + "</span>";
            }

            byte[] bytes = Utilities.DBLink.GetProfile(user.id);

            if(bytes == null)
            {
                profileImg.ImageUrl = "images/profile.jpg";
            }
            else
            {
                String imgB64 = Convert.ToBase64String(bytes);
                profileImg.ImageUrl = "data:Image/png;base64," + imgB64;
            }
        }

        protected void btnUsername_Click(object sender, EventArgs e)
        {
            usernameAlert.InnerText = "";
            bool alt = true;

            if (altUsername.Text.Contains(' '))
            {
                usernameAlert.InnerText += "No spaces allowed. ";
                alt = false;
            }

            if (Utilities.DBLink.UsernameExists(altUsername.Text.Trim()))
            {
                usernameAlert.InnerText += "Username is already taken. ";
                alt = false;
            }

            if (altUsername.Text.Equals(""))
            {
                usernameAlert.InnerText += "Enter value to the field. ";
                alt = false;
            }

            if (!alt)
                return;

            user.username = altUsername.Text;
            Utilities.DBLink.UpdateUser(user, user.id);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            bool alt = true;
            passwordAlert.InnerText = "";

            if (altPassword.Text.Trim().Equals(""))
            {
                passwordAlert.InnerText = "Enter value to the fields. ";
                return;
            }

            if (altPassword.Text.Trim().Length < 5)
            {
                passwordAlert.InnerText += "Password is too short. ";
                alt = false;
            }

            if (!prevPassword.Text.Equals(user.password))
            {
                passwordAlert.InnerText += "Incorrect previous password. ";
                alt = false;
            }

            if (!altPassword.Text.Equals(repAltPassword.Text))
            {
                passwordAlert.InnerText += "New password does not match with repeated one. ";
                alt = false;
            }

            if (alt)
            {
                user.password = altPassword.Text;
                Utilities.DBLink.UpdateUser(user, user.id);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
            mailAlert.InnerText = "";

            if (Utilities.DBLink.MailExists(altMail.Text))
            {
                mailAlert.InnerText = "Mail is already taken. \n";
                return;
            }

            if (altMail.Text.Equals(""))
            {
                mailAlert.InnerText = "Enter value to the field. \t ";
                return;
            }

            user.mail = altMail.Text;
            Utilities.DBLink.UpdateUser(user, user.id);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnCountry_Click(object sender, EventArgs e)
        {
            user.country = altCountries.Value;
            Utilities.DBLink.UpdateUser(user, user.id);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnGender_Click(object sender, EventArgs e)
        {
            user.gender = altGender.SelectedValue;
            Utilities.DBLink.UpdateUser(user, user.id);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnBirth_Click(object sender, EventArgs e)
        {
            birthAlert.InnerText = "";

            if (!Utilities.DBLink.IsDate(altBirth.Value, "yyyy-MM-dd"))
            {
                birthAlert.InnerText = "Invalid birth date \t " + altBirth.Value;
                return;
            }

            user.birth = Utilities.DBLink.ConvertDate(altBirth.Value, "yyyy-MM-dd");
            Utilities.DBLink.UpdateUser(user, user.id);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnPrefix_Click(object sender, EventArgs e)
        {
            if (user.GetType() == typeof(Utilities.Moderator)) {
                Utilities.DBLink.UpdatePrefix(user.id, alterPrefix.Text);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Success", "alert('Re-login in order to see new changes.')", true);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            imageAlert.InnerText = "";

            if (altImage.HasFile)
            {
                bool upload = true;
                int length = altImage.PostedFile.ContentLength;

                if (length > 2000000)
                {
                    imageAlert.InnerText = "File is too big! No more than 2MB";
                    upload = false;
                }

                String ext = altImage.PostedFile.ContentType;

                if(!(ext.Equals("image/jpeg") || ext.Equals("image/jpg") || ext.Equals("image/png") || ext.Equals("image/gif")))
                {
                    imageAlert.InnerText = "File has to be an image (.jpg, .jpeg, .png, .gif)";
                    upload = false;
                }

                if (upload)
                {
                    byte[] bytes = new byte[length];
                    HttpPostedFile img = altImage.PostedFile;
                    img.InputStream.Read(bytes, 0, length);

                    if(Utilities.DBLink.GetProfile(user.id) == null)
                    {
                        Utilities.DBLink.InsertProfile(bytes, user.id);
                    }
                    else
                    {
                        Utilities.DBLink.UpdateProfile(bytes, user.id);
                    }

                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                imageAlert.InnerText = "No image selected!";
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            Utilities.DBLink.DeleteProfile(user.id);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}