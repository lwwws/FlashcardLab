using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using System.Web.Services;
using System.Collections.Generic;


namespace FlashcardLab
{
    public partial class Admin : System.Web.UI.Page
    {
        public delegate void DisplayDelegate();
        DisplayDelegate bindAndDisplay;
        Utilities.User user;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            alert.Text = "";
            alertDeck.Text = "";

            if(Session["user"] == null)
                Response.Redirect("/UnauthorizedAccess.aspx");

            user = (Utilities.User)Session["user"];
            modsQual.Visible = false;
            usersMenu.Visible = false;
            decksMenu.Visible = false;

            if (user.id == 34)
            {
                bindAndDisplay = DisplayUsers;
                modsQual.Visible = true;
                usersMenu.Visible = true;
                decksMenu.Visible = true;
            }
            else if (user.GetType() == typeof(Utilities.Moderator))
            {
                bindAndDisplay = DisplayUsersNoPassword;
            }
            else { Response.Redirect("/UnauthorizedAccess.aspx"); }

            if (!IsPostBack)
            {
                bindAndDisplay.Invoke();
                DisplayMods();
            }
        }

        protected void insert_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
                return;

            bool register = true;

            if (!Utilities.DBLink.IsDate(creation.Text, Utilities.DBLink.dateFormat) || !Utilities.DBLink.IsDate(birth.Text, Utilities.DBLink.dateFormat))
            {
                alert.Text = "Creation and birth date must be in DD/MM/YYYY format. \n";
                register = false;
            }
            if (Utilities.DBLink.UsernameExists(username.Text))
            {
                alert.Text += "Username already exists. \n";
                register = false;
            }
            if (username.Text.Contains(' '))
            {
                alert.Text += "No spaces in username allowed. \n";
                register = false;
            }
            if (username.Text.Equals(""))
            {
                alert.Text += "Username can't be empty. \n";
                register = false;
            }

            if (Utilities.DBLink.MailExists(mail.Text))
            {
                alert.Text += "Mail is already taken. \n";
                register = false;
            }

            if (password.Text.Trim().Length < 5)
            {
                alert.Text += "Password is too short. \t ";
                register = false;
            }

            if (register)
            {
                Utilities.User user = new Utilities.User(
                    0,
                    username.Text.Trim(),
                    password.Text.Trim(),
                    mail.Text.Trim(),
                    country.Text.Trim(),
                    gender.Text.Trim(),
                    birth.Text.Trim(),
                    creation.Text.Trim());

                Utilities.DBLink.InsertUser(user);

                Clear();
                bindAndDisplay.Invoke();
                DisplayMods();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void insertDeck_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
                return;

            bool register = true;

            if (!Utilities.DBLink.IsDate(deckNext.Text, Utilities.DBLink.dateFormat))
            {
                alertDeck.Text = "Next review must be in DD/MM/YYYY format. \n";
                register = false;
            }

            if (!Utilities.DBLink.UserExists(userID.Text))
            {
                alertDeck.Text = "User does not exist. \n";
                register = false;
            }

            if (userID.Text.Equals(""))
            {
                alertDeck.Text += "UserID can't be empty. \n";
                register = false;
            }

            double val = -1;

            if (!double.TryParse(progress.Text, out val) || val < 0.0 || val > 100.0)
            {
                alertDeck.Text += "Progress has to be between 0 and 100. \n";
                register = false;
            }

            if (!register)
                return;

            Utilities.Deck deck = new Utilities.Deck(
                "0",
                name.Text.Trim(),
                description.Text.Trim(),
                progress.Text.Trim(),
                deckNext.Text.Trim(),
                language.Text.Trim(),
                userID.Text.Trim()
                );

            Utilities.DBLink.InsertDeck(deck);
            ClearDeck();
            bindAndDisplay.Invoke();
            DisplayMods();
            alertDeck.Text = "Inserted successfully";
        }

        public void Clear()
        {
            username.Text = "";
            password.Text = "";
            mail.Text = "";
            country.Text = "";
            creation.Text = "";
            birth.Text = "";
            gender.Text = "";
            id.Text = "";
        }

        public void ClearDeck()
        {
            deckID.Text = "";
            name.Text = "";
            description.Text = "";
            progress.Text = "";
            language.Text = "";
            deckNext.Text = "";
            userID.Text = "";
        }

        public void DisplayUsers()
        {
            DataTable dt = Utilities.DBLink.GetUsersTable();
            usersGrid.DataSource = dt;
            usersGrid.DataBind();
        }

        public void DisplayMods()
        {
            DataTable dt = Utilities.DBLink.GetModsTable();
            modsGrid.DataSource = dt;
            modsGrid.DataBind();
        }
        public void DisplayUsersNoPassword()
        {
            DataTable dt = Utilities.DBLink.GetUsersTableNoPassword();
            usersGrid.DataSource = dt;
            usersGrid.DataBind();
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
                return;

            if (!Utilities.DBLink.IsWholeNum(id.Text))
            {
                alert.Text = "ID has to be whole number.";
                Clear();
                return;
            }
            if (id.Text.Equals("34"))
            {
                alert.Text = "You can't delete admin.";
                Clear();
                return;
            }

            Utilities.DBLink.DeleteUser(id.Text);
            alert.Text = "Deleted successfully.";
            bindAndDisplay.Invoke();
            DisplayMods();
        }

        protected void deleteDeck_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
                return;

            if (!Utilities.DBLink.IsWholeNum(deckID.Text))
            {
                alertDeck.Text = "Delete failed! ID has to be whole number.";
                ClearDeck();
                return;
            }

            Utilities.DBLink.DeleteDeck(deckID.Text);
            alertDeck.Text = "Deleted successfully.";
        }

        protected void update_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
                return;

            bool register = true;

            if (!Utilities.DBLink.IsWholeNum(id.Text) || !Utilities.DBLink.IsDate(creation.Text, Utilities.DBLink.dateFormat) || !Utilities.DBLink.IsDate(birth.Text, Utilities.DBLink.dateFormat))
            {
                alert.Text = "ID has to be whole number, creation and birth date must me in DD/MM/YYYY format. \n";
                register = false;
            }

            if (!Utilities.DBLink.IsDate(creation.Text, Utilities.DBLink.dateFormat) || !Utilities.DBLink.IsDate(birth.Text, Utilities.DBLink.dateFormat))
            {
                alert.Text += "Creation and birth date must be in DD/MM/YYYY format. \n";
                register = false;
            }
            if (Utilities.DBLink.UsernameExists(username.Text.Trim()))
            {
                alert.Text += "Username already exists. \n";
                register = false;
            }
            if (Utilities.DBLink.MailExists(mail.Text.Trim()))
            {
                alert.Text += "Mail already exists. \n";
                register = false;
            }
            if (username.Text.Contains(' '))
            {
                alert.Text += "No spaces in username allowed. \n";
                register = false;
            }
            if (username.Text.Equals(""))
            {
                alert.Text += "Username can't be empty. \n";
                register = false;
            }

            if (Utilities.DBLink.MailExists(mail.Text))
            {
                alert.Text += "Mail is already taken. \n";
                register = false;
            }

            if (password.Text.Trim().Length < 5)
            {
                alert.Text += "Password is too short. \n";
                register = false;
            }

            if (register)
            {
                Utilities.User user = new Utilities.User(
                    0,
                    username.Text.Trim(),
                    password.Text.Trim(),
                    mail.Text.Trim(),
                    country.Text.Trim(),
                    gender.Text.Trim(),
                    birth.Text.Trim(),
                    creation.Text.Trim());

                Utilities.DBLink.UpdateUser(user, id.Text);
                alert.Text = "Updated successfully.";

                Clear();
                bindAndDisplay.Invoke();
                DisplayMods();
            }
        }

        protected void updateDeck_Click(object sender, EventArgs e)
        {
            if (!IsAdmin())
                return;

            if (!Utilities.DBLink.IsWholeNum(deckID.Text) || !Utilities.DBLink.IsDate(deckNext.Text, Utilities.DBLink.dateFormat))
            {
                alertDeck.Text = "ID has to be whole number, next review has to be in format DD/MM/YYYY. ";
                return;
            }

            if (name.Text.Trim().Equals(""))
            {
                alertDeck.Text = "Name cannot be empty. ";
                return;
            }

            Utilities.Deck deck = new Utilities.Deck(
                "0",
                name.Text.Trim(),
                description.Text.Trim(),
                progress.Text.Trim(),
                deckNext.Text.Trim(),
                language.Text.Trim(),
                userID.Text.Trim()
                );

            Utilities.DBLink.UpdateDeck(deck, deckID.Text);
            alertDeck.Text = "Updated successfully.";
            ClearDeck();
        }

        protected void view_Click(object sender, EventArgs e)
        {
            bindAndDisplay.Invoke();
            DisplayMods();
        }

        protected void grant_Click(object sender, EventArgs e)
        {
            if (!Utilities.DBLink.IsMod(idMod.Text) && IsAdmin() && !idMod.Text.Equals("34"))
            {
                Utilities.DBLink.GrantUser(idMod.Text);
                DisplayMods();
            }
        }

        protected void disclaim_Click(object sender, EventArgs e)
        {
            if (IsAdmin())
            {
                Utilities.DBLink.DeleteModFields(idMod.Text);
                DisplayMods();
            }
        }

        public bool IsAdmin()
        {
            return user.id == 34;
        }

        protected void usersGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            usersGrid.PageIndex = e.NewPageIndex;
            bindAndDisplay.Invoke();
        }

        protected void modsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            modsGrid.PageIndex = e.NewPageIndex;
            DisplayMods();
        }

        protected void xmlDeck_Click(object sender, EventArgs e)
        {
            DataTable cards = Utilities.DBLink.GetCardsTableByDeckID(deckID.Text);
            Utilities.Deck deck = Utilities.DBLink.GetDeckByDeckID(deckID.Text);
            int avgLvl = AverageLevelDeck(cards);

            XmlDocument doc = new XmlDocument();

            XmlNode declarationNode = doc.CreateXmlDeclaration("1.0", "UTF-8", "");
            doc.AppendChild(declarationNode);

            if (deck != null)
            {

                XmlElement elDeck = doc.CreateElement("deck");
                elDeck.SetAttribute("deckID", deck.id.ToString());
                elDeck.SetAttribute("userID", deck.userID.ToString());
                elDeck.SetAttribute("language", deck.language);
                elDeck.SetAttribute("progress", deck.progress.ToString());
                elDeck.SetAttribute("avglevel", avgLvl.ToString());
                elDeck.SetAttribute("shared", deck.shared.ToString());

                doc.AppendChild(elDeck);

                XmlNode nodeName = doc.CreateElement("name");
                nodeName.InnerText = deck.name;
                elDeck.AppendChild(nodeName);

                XmlNode nodeDescription = doc.CreateElement("description");
                nodeDescription.InnerText = deck.description;
                elDeck.AppendChild(nodeDescription);

                XmlNode nodeNext = doc.CreateElement("next");
                nodeNext.InnerText = deck.next;
                elDeck.AppendChild(nodeNext);

                XmlNode nodeCreation = doc.CreateElement("creation");
                nodeCreation.InnerText = deck.creation;
                elDeck.AppendChild(nodeCreation);

                foreach (DataRow row in cards.Rows)
                {
                    XmlElement elCard = doc.CreateElement("card");
                    elCard.SetAttribute("cardID", row["id"].ToString());
                    elCard.SetAttribute("level", row["level"].ToString());
                    elCard.SetAttribute("correct", row["correct"].ToString());
                    elCard.SetAttribute("incorrect", row["incorrect"].ToString());

                    XmlNode nodeFront = doc.CreateElement("front");
                    nodeFront.InnerText = row["front"].ToString();
                    elCard.AppendChild(nodeFront);

                    XmlNode nodeBack = doc.CreateElement("back");
                    nodeBack.InnerText = row["back"].ToString();
                    elCard.AppendChild(nodeBack);

                    XmlNode nodeNote = doc.CreateElement("note");
                    nodeNote.InnerText = row["note"].ToString();
                    elCard.AppendChild(nodeNote);

                    XmlNode cardNodeNext = doc.CreateElement("next");
                    cardNodeNext.InnerText = row["next"].ToString();
                    elCard.AppendChild(cardNodeNext);

                    elDeck.AppendChild(elCard);
                }

                Response.Clear();
                Response.ContentType = "text/xml";
                Response.Charset = "utf-8";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + deck.name.Replace(' ','_') + "_" + DateTime.Now + ".xml");

                Response.Write(doc.OuterXml);
                Response.End();
            }
            else
            {
                alertDeck.Text = "No deck found!";
            }

        }

        private int AverageLevelDeck(DataTable cards)
        {
            int sum = 0;

            if (cards.Rows.Count == 0)
            {
                return 0;
            }

            foreach (DataRow row in cards.Rows)
            {
                sum += (int)row[4];
            }

            return sum / cards.Rows.Count;
        }

        [WebMethod]
        public static String GetCountries()
        {
            DataTable td = Utilities.DBLink.CountUsersCountries();

            StringBuilder json = new StringBuilder();

            if (td.Rows.Count > 0)
            {
                json.Append("[{");

                for (int i = 0; i < td.Rows.Count; i++)
                {
                    var countries = new Dictionary<String, String>()
                    {
                        { "Afghanistan", "AF"},
                        { "Aland Islands", "AX"},
                        { "Albania", "AL"},
                        { "Algeria", "DZ"},
                        { "American Samoa", "AS"},
                        { "Andorra", "AD"},
                        { "Angola", "AO"},
                        { "Anguilla", "AI"},
                        { "Antarctica", "AQ"},
                        { "Antigua And Barbuda", "AG"},
                        { "Argentina", "AR"},
                        { "Armenia", "AM"},
                        { "Aruba", "AW"},
                        { "Australia", "AU"},
                        { "Austria", "AT"},
                        { "Azerbaijan", "AZ"},
                        { "Bahamas", "BS"},
                        { "Bahrain", "BH"},
                        { "Bangladesh", "BD"},
                        { "Barbados", "BB"},
                        { "Belarus", "BY"},
                        { "Belgium", "BE"},
                        { "Belize", "BZ"},
                        { "Benin", "BJ"},
                        { "Bermuda", "BM"},
                        { "Bhutan", "BT"},
                        { "Bolivia", "BO"},
                        { "Bosnia And Herzegovina", "BA"},
                        { "Botswana", "BW"},
                        { "Bouvet Island", "BV"},
                        { "Brazil", "BR"},
                        { "British Indian Ocean Territory", "IO"},
                        { "Brunei Darussalam", "BN"},
                        { "Bulgaria", "BG"},
                        { "Burkina Faso", "BF"},
                        { "Burundi", "BI"},
                        { "Cambodia", "KH"},
                        { "Cameroon", "CM"},
                        { "Canada", "CA"},
                        { "Cape Verde", "CV"},
                        { "Cayman Islands", "KY"},
                        { "Central African Republic", "CF"},
                        { "Chad", "TD"},
                        { "Chile", "CL"},
                        { "China", "CN"},
                        { "Christmas Island", "CX"},
                        { "Cocos (Keeling) Islands", "CC"},
                        { "Colombia", "CO"},
                        { "Comoros", "KM"},
                        { "Congo", "CG"},
                        { "Congo, Democratic Republic", "CD"},
                        { "Cook Islands", "CK"},
                        { "Costa Rica", "CR"},
                        { "Cote D\"Ivoire", "CI"},
                        { "Croatia", "HR"},
                        { "Cuba", "CU"},
                        { "Cyprus", "CY"},
                        { "Czech Republic", "CZ"},
                        { "Denmark", "DK"},
                        { "Djibouti", "DJ"},
                        { "Dominica", "DM"},
                        { "Dominican Republic", "DO"},
                        { "Ecuador", "EC"},
                        { "Egypt", "EG"},
                        { "El Salvador", "SV"},
                        { "Equatorial Guinea", "GQ"},
                        { "Eritrea", "ER"},
                        { "Estonia", "EE"},
                        { "Ethiopia", "ET"},
                        { "Falkland Islands (Malvinas)", "FK"},
                        { "Faroe Islands", "FO"},
                        { "Fiji", "FJ"},
                        { "Finland", "FI"},
                        { "France", "FR"},
                        { "French Guiana", "GF"},
                        { "French Polynesia", "PF"},
                        { "French Southern Territories", "TF"},
                        { "Gabon", "GA"},
                        { "Gambia", "GM"},
                        { "Georgia", "GE"},
                        { "Germany", "DE"},
                        { "Ghana", "GH"},
                        { "Gibraltar", "GI"},
                        { "Greece", "GR"},
                        { "Greenland", "GL"},
                        { "Grenada", "GD"},
                        { "Guadeloupe", "GP"},
                        { "Guam", "GU"},
                        { "Guatemala", "GT"},
                        { "Guernsey", "GG"},
                        { "Guinea", "GN"},
                        { "Guinea-Bissau", "GW"},
                        { "Guyana", "GY"},
                        { "Haiti", "HT"},
                        { "Heard Island & Mcdonald Islands", "HM"},
                        { "Holy See (Vatican City State)", "VA"},
                        { "Honduras", "HN"},
                        { "Hong Kong", "HK"},
                        { "Hungary", "HU"},
                        { "Iceland", "IS"},
                        { "India", "IN"},
                        { "Indonesia", "ID"},
                        { "Iran, Islamic Republic Of", "IR"},
                        { "Iraq", "IQ"},
                        { "Ireland", "IE"},
                        { "Isle Of Man", "IM"},
                        { "Israel", "IL"},
                        { "Italy", "IT"},
                        { "Jamaica", "JM"},
                        { "Japan", "JP"},
                        { "Jersey", "JE"},
                        { "Jordan", "JO"},
                        { "Kazakhstan", "KZ"},
                        { "Kenya", "KE"},
                        { "Kiribati", "KI"},
                        { "Korea", "KR"},
                        { "North Korea", "KP"},
                        { "Kuwait", "KW"},
                        { "Kyrgyzstan", "KG"},
                        { "Lao People\"s Democratic Republic", "LA"},
                        { "Latvia", "LV"},
                        { "Lebanon", "LB"},
                        { "Lesotho", "LS"},
                        { "Liberia", "LR"},
                        { "Libyan Arab Jamahiriya", "LY"},
                        { "Liechtenstein", "LI"},
                        { "Lithuania", "LT"},
                        { "Luxembourg", "LU"},
                        { "Macao", "MO"},
                        { "Macedonia", "MK"},
                        { "Madagascar", "MG"},
                        { "Malawi", "MW"},
                        { "Malaysia", "MY"},
                        { "Maldives", "MV"},
                        { "Mali", "ML"},
                        { "Malta", "MT"},
                        { "Marshall Islands", "MH"},
                        { "Martinique", "MQ"},
                        { "Mauritania", "MR"},
                        { "Mauritius", "MU"},
                        { "Mayotte", "YT"},
                        { "Mexico", "MX"},
                        { "Micronesia, Federated States Of", "FM"},
                        { "Moldova", "MD"},
                        { "Monaco", "MC"},
                        { "Mongolia", "MN"},
                        { "Montenegro", "ME"},
                        { "Montserrat", "MS"},
                        { "Morocco", "MA"},
                        { "Mozambique", "MZ"},
                        { "Myanmar", "MM"},
                        { "Namibia", "NA"},
                        { "Nauru", "NR"},
                        { "Nepal", "NP"},
                        { "Netherlands", "NL"},
                        { "Netherlands Antilles", "AN"},
                        { "New Caledonia", "NC"},
                        { "New Zealand", "NZ"},
                        { "Nicaragua", "NI"},
                        { "Niger", "NE"},
                        { "Nigeria", "NG"},
                        { "Niue", "NU"},
                        { "Norfolk Island", "NF"},
                        { "Northern Mariana Islands", "MP"},
                        { "Norway", "NO"},
                        { "Oman", "OM"},
                        { "Pakistan", "PK"},
                        { "Palau", "PW"},
                        { "Palestine", "PS"},
                        { "Panama", "PA"},
                        { "Papua New Guinea", "PG"},
                        { "Paraguay", "PY"},
                        { "Peru", "PE"},
                        { "Philippines", "PH"},
                        { "Pitcairn", "PN"},
                        { "Poland", "PL"},
                        { "Portugal", "PT"},
                        { "Puerto Rico", "PR"},
                        { "Qatar", "QA"},
                        { "Reunion", "RE"},
                        { "Romania", "RO"},
                        { "Russian Federation", "RU"},
                        { "Rwanda", "RW"},
                        { "Saint Barthelemy", "BL"},
                        { "Saint Helena", "SH"},
                        { "Saint Kitts And Nevis", "KN"},
                        { "Saint Lucia", "LC"},
                        { "Saint Martin", "MF"},
                        { "Saint Pierre And Miquelon", "PM"},
                        { "Saint Vincent And Grenadines", "VC"},
                        { "Samoa", "WS"},
                        { "San Marino", "SM"},
                        { "Sao Tome And Principe", "ST"},
                        { "Saudi Arabia", "SA"},
                        { "Senegal", "SN"},
                        { "Serbia", "RS"},
                        { "Seychelles", "SC"},
                        { "Sierra Leone", "SL"},
                        { "Singapore", "SG"},
                        { "Slovakia", "SK"},
                        { "Slovenia", "SI"},
                        { "Solomon Islands", "SB"},
                        { "Somalia", "SO"},
                        { "South Africa", "ZA"},
                        { "South Georgia And Sandwich Isl.", "GS"},
                        { "Spain", "ES"},
                        { "Sri Lanka", "LK"},
                        { "Sudan", "SD"},
                        { "Suriname", "SR"},
                        { "Svalbard And Jan Mayen", "SJ"},
                        { "Swaziland", "SZ"},
                        { "Sweden", "SE"},
                        { "Switzerland", "CH"},
                        { "Syrian Arab Republic", "SY"},
                        { "Taiwan", "TW"},
                        { "Tajikistan", "TJ"},
                        { "Tanzania", "TZ"},
                        { "Thailand", "TH"},
                        { "Timor-Leste", "TL"},
                        { "Togo", "TG"},
                        { "Tokelau", "TK"},
                        { "Tonga", "TO"},
                        { "Trinidad And Tobago", "TT"},
                        { "Tunisia", "TN"},
                        { "Turkey", "TR"},
                        { "Turkmenistan", "TM"},
                        { "Turks And Caicos Islands", "TC"},
                        { "Tuvalu", "TV"},
                        { "Uganda", "UG"},
                        { "Ukraine", "UA"},
                        { "United Arab Emirates", "AE"},
                        { "United Kingdom", "GB"},
                        { "United States", "US"},
                        { "United States Outlying Islands", "UM"},
                        { "Uruguay", "UY"},
                        { "Uzbekistan", "UZ"},
                        { "Vanuatu", "VU"},
                        { "Venezuela", "VE"},
                        { "Vietnam", "VN"},
                        { "Virgin Islands (Brit)", "VG"},
                        { "Virgin Islands (USA)", "VI"},
                        { "Wallis And Futuna", "WF"},
                        { "Western Sahara", "EH"},
                        { "Yemen", "YE"},
                        { "Zambia", "ZM"},
                        { "Zimbabwe", "ZW" }
                    };

                    if (countries.ContainsKey(td.Rows[i][0].ToString()))
                    {
                        json.Append("\"" + countries[td.Rows[i][0].ToString()] + "\": " + td.Rows[i][1].ToString() + ",");
                    }
                }

                json.Remove(json.Length - 1, 1);

                json.Append("}]");
            }
            else
                return null;

            return json.ToString();
        }
    }
}