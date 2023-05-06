using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace FlashcardLab.Utilities
{
    public static class DBLink
    {
        public static String conString { get; set; } = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FlashcardLab.mdf;Integrated Security=True";
        public static int dateType = 103;
        public static int timeType = 108;
        public static String dateFormat = "dd/MM/yyyy";
        public static String datetimeFormat = "dd/MM/yyyy HH:mm:ss";
        public static void InsertUser(User user)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO users values(@username, @password, @mail, @country, @gender, CONVERT(date, @birth, @dateType), CONVERT(date, @creation, @dateType))", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("username", user.username);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.Parameters.AddWithValue("mail", user.mail);
                cmd.Parameters.AddWithValue("country", user.country);
                cmd.Parameters.AddWithValue("gender", user.gender);
                cmd.Parameters.AddWithValue("birth", user.birth);
                cmd.Parameters.AddWithValue("creation", user.creation);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void GrantUser(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO moderators values(" + id + ", GETDATE(), \'\')", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void GrantUser(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO moderators values(" + id + ", GETDATE(), \'\')", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateModerator(Moderator mod)
        {
            UpdateUser(mod);

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE moderators SET modGranted = CONVERT(date, @modGranted, @dateType), prefixColor = @prefixColor WHERE userID = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("modGranted", mod.modGranted);
                cmd.Parameters.AddWithValue("prefixColor", mod.prefixColor);
                cmd.Parameters.AddWithValue("dateType", dateType);
                cmd.Parameters.AddWithValue("id", mod.id);

                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteUser(String id)
        {
            DeleteDecksWithUserID(id);
            DeleteModFields(id);
            DeleteProfile(id);

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteUser(int id)
        {
            DeleteDecksWithUserID(id);
            DeleteModFields(id);
            DeleteProfile(id);

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete the fields of the user from the moderators table
        public static void DeleteModFields(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM moderators WHERE userID = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteModFields(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM moderators WHERE userID = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Update the prefix color of the moderator
        public static void UpdatePrefix(int id, String newPrefix)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE moderators SET prefixColor = '" + newPrefix + "' WHERE userID = " + id, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void UpdateUser(User user, String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE users SET username = @username, password = @password, mail = @mail, country = @country, gender = @gender, birth = CONVERT(date, @birth, @dateType), creation = CONVERT(date, @creation, @dateType) WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("username", user.username);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.Parameters.AddWithValue("mail", user.mail);
                cmd.Parameters.AddWithValue("country", user.country);
                cmd.Parameters.AddWithValue("gender", user.gender);
                cmd.Parameters.AddWithValue("birth", user.birth);
                cmd.Parameters.AddWithValue("creation", user.creation);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateUser(User user, int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE users SET username = @username, password = @password, mail = @mail, country = @country, gender = @gender, birth = CONVERT(date, @birth, @dateType), creation = CONVERT(date, @creation, @dateType) WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("username", user.username);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.Parameters.AddWithValue("mail", user.mail);
                cmd.Parameters.AddWithValue("country", user.country);
                cmd.Parameters.AddWithValue("gender", user.gender);
                cmd.Parameters.AddWithValue("birth", user.birth);
                cmd.Parameters.AddWithValue("creation", user.creation);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateUser(User user)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE users SET username = @username, password = @password, mail = @mail, country = @country, gender = @gender, birth = CONVERT(date, @birth, @dateType), creation = CONVERT(date, @creation, @dateType) WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("username", user.username);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.Parameters.AddWithValue("mail", user.mail);
                cmd.Parameters.AddWithValue("country", user.country);
                cmd.Parameters.AddWithValue("gender", user.gender);
                cmd.Parameters.AddWithValue("birth", user.birth);
                cmd.Parameters.AddWithValue("creation", user.creation);
                cmd.Parameters.AddWithValue("id", user.id);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        // Checks if username already exists
        public static bool UsernameExists(String username)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE username = @username", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("username", username);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }

        public static bool UserExists(String userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @userID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("userID", userID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }

        // Checks if user with this id is Moderator
        public static bool IsMod(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM moderators WHERE userID = " + id, con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }

        public static bool IsMod(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM moderators WHERE userID = " + id, con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }

        // Checks if mail exists in the table
        public static bool MailExists(String mail)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE mail = @mail", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("mail", mail);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }

        public static DataTable GetUsersTable()
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static DataTable GetUsersTableNoPassword()
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT id, username, mail, country, gender, birth, creation FROM users", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static DataTable CountUsersCountries()
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT country, COUNT(*) AS \'count\' FROM users GROUP BY country", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static DataTable GetModsTable()
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM moderators", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static User GetUserByUsername(String username)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE username = @username", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("username", username);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                if(dt.Rows.Count == 0)
                {
                    return null;
                }

                User user = new User(
                    (int)dt.Rows[0].ItemArray[0],
                    dt.Rows[0].ItemArray[1].ToString(),
                    dt.Rows[0].ItemArray[2].ToString(),
                    dt.Rows[0].ItemArray[3].ToString(),
                    dt.Rows[0].ItemArray[4].ToString(),
                    dt.Rows[0].ItemArray[5].ToString(),
                    GetDateOnly(dt.Rows[0].ItemArray[6].ToString()),
                    GetDateOnly(dt.Rows[0].ItemArray[7].ToString())
                    );

                return user;
            }
        }

        public static String GetUsernameByUserID(String userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT username FROM users WHERE id = @userID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("userID", userID);
                return cmd.ExecuteScalar().ToString();
            }
        }

        public static String GetUsernameByUserID(int userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT username FROM users WHERE id = @userID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("userID", userID);
                return cmd.ExecuteScalar().ToString();
            }
        }

        public static User GetUserByUserID(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @id", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                User user = new User(
                    (int)dt.Rows[0].ItemArray[0],
                    dt.Rows[0].ItemArray[1].ToString(),
                    dt.Rows[0].ItemArray[2].ToString(),
                    dt.Rows[0].ItemArray[3].ToString(),
                    dt.Rows[0].ItemArray[4].ToString(),
                    dt.Rows[0].ItemArray[5].ToString(),
                    GetDateOnly(dt.Rows[0].ItemArray[6].ToString()),
                    GetDateOnly(dt.Rows[0].ItemArray[7].ToString())
                    );

                return user;
            }
        }

        public static Moderator GetModeratorByUserID(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM moderators WHERE userID = " + id, con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                User user = GetUserByUserID(id);

                con.Open();
                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                Moderator mod = new Moderator(user, dt.Rows[0].ItemArray[1].ToString(), dt.Rows[0].ItemArray[2].ToString());
                return mod;
            }
        }

        public static Moderator GetModeratorByUser(User user)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM moderators WHERE userID = " + user.id, con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                Moderator mod = new Moderator(user, dt.Rows[0].ItemArray[1].ToString(), dt.Rows[0].ItemArray[2].ToString());
                return mod;
            }
        }

        public static String GetDateOnly(string datetime)
        {
            if (datetime.Length < 10)
                return datetime;

            return datetime.Substring(0, 10);
        }

        public static bool IsWholeNum(string txt)
        {
            if (txt.Equals(""))
                return false;

            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] < '0' || txt[i] > '9')
                    return false;
            }

            return true;
        }

        //
        //
        //         TMARIM
        //
        //

        public static bool IsDate(string txt)
        {
            DateTime dt;
            return DateTime.TryParseExact(txt, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
        }

        public static bool IsDate(string txt, string format)
        {
            DateTime dt;
            return DateTime.TryParseExact(txt, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
        }

        public static String ConvertDate(string date, string initialFormat)
        {
            DateTime b = DateTime.ParseExact(date, initialFormat, null);
            return b.ToString(dateFormat);
        }

        public static DateTime ConvertToDatetime(string datetime)
        {
            DateTime dt;
            DateTime.TryParseExact(datetime, datetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            return dt;
        }

        public static DateTime ConvertToDate(string date)
        {
            DateTime dt;
            DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            return dt;
        }

        //
        //
        //         DECKS
        //
        //

        public static int InsertDeck(Deck deck)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO decks values(@name, @description, @progress, CONVERT(datetime, @next, @dateType), @language, @userID, @shared, CONVERT(date, @creation, @dateType)); SELECT scope_identity();", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("name", deck.name);
                cmd.Parameters.AddWithValue("description", deck.description);
                cmd.Parameters.AddWithValue("progress", deck.progress);
                cmd.Parameters.AddWithValue("next", deck.next);
                cmd.Parameters.AddWithValue("userID", deck.userID);
                cmd.Parameters.AddWithValue("language", deck.language);
                cmd.Parameters.AddWithValue("shared", deck.shared);
                cmd.Parameters.AddWithValue("creation", deck.creation);
                cmd.Parameters.AddWithValue("dateType", dateType);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static void UpdateDeck(Deck deck, String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE decks SET name = @name, description = @description, progress = @progress, next = CONVERT(datetime, @next, @dateType), language = @language, userID = @userID, shared = @shared, creation = CONVERT(date, @creation, @dateType) WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("name", deck.name);
                cmd.Parameters.AddWithValue("description", deck.description);
                cmd.Parameters.AddWithValue("progress", deck.progress);
                cmd.Parameters.AddWithValue("next", deck.next);
                cmd.Parameters.AddWithValue("userID", deck.userID);
                cmd.Parameters.AddWithValue("id", deckID);
                cmd.Parameters.AddWithValue("language", deck.language);
                cmd.Parameters.AddWithValue("shared", deck.shared);
                cmd.Parameters.AddWithValue("creation", deck.creation);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateDeck(Deck deck, int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE decks SET name = @name, description = @description, progress = @progress, next = CONVERT(datetime, @next, @dateType), language = @language, userID = @userID, shared = @shared, creation = CONVERT(date, @creation, @dateType) WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("name", deck.name);
                cmd.Parameters.AddWithValue("description", deck.description);
                cmd.Parameters.AddWithValue("progress", deck.progress);
                cmd.Parameters.AddWithValue("next", deck.next);
                cmd.Parameters.AddWithValue("userID", deck.userID);
                cmd.Parameters.AddWithValue("id", deckID);
                cmd.Parameters.AddWithValue("language", deck.language);
                cmd.Parameters.AddWithValue("shared", deck.shared);
                cmd.Parameters.AddWithValue("creation", deck.creation);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateDeck(Deck deck)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE decks SET name = @name, description = @description, progress = @progress, next = CONVERT(datetime, @next, @dateType), language = @language, userID = @userID, shared = @shared, creation = CONVERT(date, @creation, @dateType) WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("name", deck.name);
                cmd.Parameters.AddWithValue("description", deck.description);
                cmd.Parameters.AddWithValue("progress", deck.progress);
                cmd.Parameters.AddWithValue("next", deck.next);
                cmd.Parameters.AddWithValue("userID", deck.userID);
                cmd.Parameters.AddWithValue("id", deck.id);
                cmd.Parameters.AddWithValue("language", deck.language);
                cmd.Parameters.AddWithValue("shared", deck.shared);
                cmd.Parameters.AddWithValue("creation", deck.creation);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteDeck(String id)
        {
            DeleteCardsWithDeckID(id);
            DeleteCommentsWithDeckID(id);

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM decks WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteDeck(int id)
        {
            DeleteCardsWithDeckID(id);
            DeleteCommentsWithDeckID(id);

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM decks WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteDecksWithUserID(String userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT id FROM decks WHERE userID = '" + userID + "'", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    DeleteDeck(row[0].ToString());
                }
            }
        }

        public static void DeleteDecksWithUserID(int userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT id FROM decks WHERE userID = '" + userID + "'", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    DeleteDeck(row[0].ToString());
                }
            }
        }

        // Make a copy of the deck to user this particular id
        public static void CopyDeckToUserID(int deckID, int userID)
        {
            Deck deck = GetDeckByDeckID(deckID);
            deck.progress = 0.0;
            deck.userID = userID;
            deck.shared = false;
            deck.next = DateTime.Now.ToString(datetimeFormat);
            deck.creation = DateTime.Today.ToString();
            int newID = InsertDeck(deck);

            DataTable cards = GetCardsTableByDeckID(deckID);

            foreach(DataRow row in cards.Rows)
            {
                row[3] = DateTime.Now.ToString(datetimeFormat);
                row[4] = 0;
                row[5] = 0;
                row[6] = 0;
                row[8] = newID;

                InsertCard(new Card(row));
            }
        }

        public static Deck GetDeckByDeckID(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE id = @id", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                Deck deck = new Deck(dt.Rows[0]);

                return deck;
            }
        }

        public static Deck GetDeckByDeckID(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE id = @id", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                Deck deck = new Deck(dt.Rows[0]);

                return deck;
            }
        }

        public static DataTable GetDecksTableByUserID(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE userID = @id", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        // Get decks that are shared sorted with particular query
        public static DataTable GetSharedDecksSorted(String language, String dateSort, String query)
        {
            String stCommand = "SELECT * FROM decks WHERE shared = 1";

            if (!query.Equals(""))
            {
                stCommand += " AND (name LIKE '%" + query + "%' OR description LIKE '%" + query + "%')";
            }
            if (!language.Equals(""))
            {
                stCommand += " AND (language LIKE '%" + language + "%')";
            }

            stCommand += " ORDER BY creation " + dateSort;

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand(stCommand, con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static DataTable GetSharedDecks()
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE shared = 1", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        // Update next review/learn of the deck based on the earliest date of the cards
        public static String UpdateDeckNextByEarliestDate(int deckID)
        {
            String date;

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT MIN(next) FROM cards WHERE deckID = @deckID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);
                date = cmd.ExecuteScalar().ToString();
            }

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE decks SET next = CONVERT(datetime, @next, @dateType) WHERE id = @deckID", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);
                cmd.Parameters.AddWithValue("next", date);
                cmd.Parameters.AddWithValue("dateType", dateType);
                cmd.ExecuteNonQuery();
            }

            return date;
        }

        // Update decks progress based on the ratio between the total level sum of cards
        // and cards amount * 9 (highest score)

        public static double UpdateDeckProgress(int deckID)
        {
            DataTable cards = GetCardsTableByDeckID(deckID);

            if (cards.Rows.Count == 0)
                return 0;

            double sum = 0;

            foreach (DataRow row in cards.Rows)
            {
                sum += (int)row[4];
            }

            String progress = (sum / (cards.Rows.Count * 9) * 100).ToString();

            if(progress.Length > 5)
            {
                progress = progress.Substring(0, 5);
            }

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE decks SET progress = @progress WHERE id = @deckID", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);
                cmd.Parameters.AddWithValue("progress", progress);
                cmd.ExecuteNonQuery();
            }

            return Math.Round((sum / (cards.Rows.Count * 9) * 100), 2);
        }

        public static void UpdateDecksShared(int deckID, bool shared)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE decks SET shared = @shared WHERE id = @deckID", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);
                cmd.Parameters.AddWithValue("shared", shared);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool UserHasThisDeck(int userID, int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE id = @deckID AND userID = @userID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);
                cmd.Parameters.AddWithValue("userID", userID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }

        //
        //
        //         CARDS
        //
        //
        public static void InsertCard(Card card)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO cards values(@front, @back, CONVERT(datetime, @next, @dateType), @level, @correct, @incorrect, @note, @deckID)", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("front", card.front);
                cmd.Parameters.AddWithValue("back", card.back);
                cmd.Parameters.AddWithValue("next", card.next);
                cmd.Parameters.AddWithValue("level", card.level);
                cmd.Parameters.AddWithValue("correct", card.correct);
                cmd.Parameters.AddWithValue("incorrect", card.incorrect);
                cmd.Parameters.AddWithValue("note", card.note);
                cmd.Parameters.AddWithValue("deckID", card.deckID);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCard(Card card, String cardID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE cards SET front = @front, back = @back, next = CONVERT(datetime, @next, @dateType), level = @level, correct = @correct, incorrect = @incorrect, note = @note, deckID = @deckID WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("front", card.front);
                cmd.Parameters.AddWithValue("back", card.back);
                cmd.Parameters.AddWithValue("next", card.next);
                cmd.Parameters.AddWithValue("level", card.level);
                cmd.Parameters.AddWithValue("correct", card.correct);
                cmd.Parameters.AddWithValue("incorrect", card.incorrect);
                cmd.Parameters.AddWithValue("note", card.note);
                cmd.Parameters.AddWithValue("deckID", card.deckID);
                cmd.Parameters.AddWithValue("id", cardID);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCard(Card card, int cardID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE cards SET front = @front, back = @back, next = CONVERT(datetime, @next, @dateType), level = @level, correct = @correct, incorrect = @incorrect, note = @note, deckID = @deckID WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("front", card.front);
                cmd.Parameters.AddWithValue("back", card.back);
                cmd.Parameters.AddWithValue("next", card.next);
                cmd.Parameters.AddWithValue("level", card.level);
                cmd.Parameters.AddWithValue("correct", card.correct);
                cmd.Parameters.AddWithValue("incorrect", card.incorrect);
                cmd.Parameters.AddWithValue("note", card.note);
                cmd.Parameters.AddWithValue("deckID", card.deckID);
                cmd.Parameters.AddWithValue("id", cardID);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCard(Card card)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE cards SET front = @front, back = @back, next = CONVERT(datetime, @next, @dateType), level = @level, correct = @correct, incorrect = @incorrect, note = @note, deckID = @deckID WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("front", card.front);
                cmd.Parameters.AddWithValue("back", card.back);
                cmd.Parameters.AddWithValue("next", card.next);
                cmd.Parameters.AddWithValue("level", card.level);
                cmd.Parameters.AddWithValue("correct", card.correct);
                cmd.Parameters.AddWithValue("incorrect", card.incorrect);
                cmd.Parameters.AddWithValue("note", card.note);
                cmd.Parameters.AddWithValue("deckID", card.deckID);
                cmd.Parameters.AddWithValue("id", card.id);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCard(int id, String next, int level, int addToCorrect, int addToIncorrect)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE cards SET next = CONVERT(datetime, @next, @dateType), level = @level, correct = correct + @correct, incorrect = incorrect + @incorrect WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("next", next);
                cmd.Parameters.AddWithValue("level", level);
                cmd.Parameters.AddWithValue("correct", addToCorrect);
                cmd.Parameters.AddWithValue("incorrect", addToIncorrect);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteCard(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM cards WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCard(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM cards WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCardsWithDeckID(String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM cards WHERE deckID = '" + deckID + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCardsWithDeckID(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM cards WHERE deckID = '" + deckID + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetCardsTableByDeckID(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE deckID = @id", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static DataTable GetLearnedCardsTableByDeckID(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE deckID = @id AND level != 0", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static DataTable GetCardsTableByDeckIdAndLevel(String id, int lvl)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE deckID = @id AND level = @lvl", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("lvl", lvl);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        // Fisher–Yates shuffle algorithm of random array
        public static int[] RandomArray(int size)
        {
            int[] arr = new int[size];
            Random rand = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            int n = arr.Length;

            while(n > 1)
            {
                int k = rand.Next(n--);
                int tmp = arr[n];
                arr[n] = arr[k];
                arr[k] = tmp;
            }

            return arr;
        }

        public static Queue<BilateralCard> ShuffleQueuesTogether(Queue<BilateralCard> q1, Queue<BilateralCard> q2)
        {
            Queue<BilateralCard> m = new Queue<BilateralCard>();
            Random rand = new Random();
            int n = Math.Max(q1.Count, q2.Count);

            while (q1.Count != 0 && q2.Count != 0 && n != 0)
            {
                for(int i = 0; i < rand.Next(q1.Count + 1); i++)
                {
                    m.Enqueue(q1.Dequeue());
                }
                for (int i = 0; i < rand.Next(q2.Count + 1); i++)
                {
                    m.Enqueue(q2.Dequeue());
                }

                n--;
            }

            while (q1.Count != 0)
            {
                m.Enqueue(q1.Dequeue());
            }

            while (q2.Count != 0)
            {
                m.Enqueue(q2.Dequeue());
            }

            return m;
        }

        public static Queue<BilateralCard> GetQueueBilateralCardsForReview(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM cards WHERE deckID = @deckID AND level != 0 AND level != 9 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                Queue<BilateralCard> cards = new Queue<BilateralCard>();
                Queue<BilateralCard> twins = new Queue<BilateralCard>();

                foreach (int i in RandomArray(dt.Rows.Count))
                {
                    BilateralCard card = new BilateralCard(dt.Rows[i], null);
                    BilateralCard twin = new BilateralCard(dt.Rows[i], card, true);
                    card.twin = twin;

                    cards.Enqueue(card);
                    twins.Enqueue(twin);
                }

                return ShuffleQueuesTogether(cards, twins);
            }
        }
        public static Queue<Card> GetQueueCardsForReview(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM cards WHERE deckID = @deckID AND level != 0 AND level != 9 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                Queue<Card> cards = new Queue<Card>();

                foreach (int i in RandomArray(dt.Rows.Count))
                {
                    cards.Enqueue(new Card(dt.Rows[i]));
                }

                return cards;
            }
        }

        public static int CountCardsForReview(String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM cards WHERE deckID = @deckID AND level != 0 AND level != 9 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        public static int CountCardsForReview(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM cards WHERE deckID = @deckID AND level != 0 AND level != 9 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        public static Queue<Card> GetQueueCardsForReview(String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM cards WHERE deckID = @deckID AND level != 0 AND level != 9 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                Queue<Card> cards = new Queue<Card>();

                foreach (int i in RandomArray(dt.Rows.Count))
                {
                    cards.Enqueue(new Card(dt.Rows[i]));
                }

                return cards;
            }
        }

        public static Queue<Card> GetQueueCardsForLearn(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM cards WHERE deckID = @deckID AND level = 0 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                Queue<Card> cards = new Queue<Card>();

                foreach (int i in RandomArray(dt.Rows.Count))
                {
                    cards.Enqueue(new Card(dt.Rows[i]));
                }

                return cards;
            }
        }

        public static int CountCardsForLearn(String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM cards WHERE deckID = @deckID AND level = 0 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        public static int CountCardsForLearn(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM cards WHERE deckID = @deckID AND level = 0 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return Convert.ToInt32(dt.Rows[0][0]);
            }
        }

        public static Queue<Card> GetQueueCardsForLearn(String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM cards WHERE deckID = @deckID AND level = 0 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                Queue<Card> cards = new Queue<Card>();

                foreach (int i in RandomArray(dt.Rows.Count))
                {
                    cards.Enqueue(new Card(dt.Rows[i]));
                }

                return cards;
            }
        }

        public static Queue<BilateralCard> GetQueueBilateralCardsForLearn(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM cards WHERE deckID = @deckID AND level = 0 AND next <= GETDATE()", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                Queue<BilateralCard> cards = new Queue<BilateralCard>();
                Queue<BilateralCard> twins = new Queue<BilateralCard>();

                foreach (int i in RandomArray(dt.Rows.Count))
                {
                    BilateralCard card = new BilateralCard(dt.Rows[i], null);
                    BilateralCard twin = new BilateralCard(dt.Rows[i], card, true);
                    card.twin = twin;

                    cards.Enqueue(card);
                    twins.Enqueue(twin);
                }

                return ShuffleQueuesTogether(cards, twins);
            }
        }

        public static DataTable GetCardsTableByDeckID(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE deckID = @id", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static Card GetCardByCardID(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE id = @id", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("id", id);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                Card card = new Card(dt.Rows[0]);

                return card;
            }
        }

        //
        //
        //         COMMENTS
        //
        //
        public static void InsertComment(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO comments values(@comment, @rating, @deckID, @userID, CONVERT(date, @creation, @dateType))", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("comment", comment.comment);
                cmd.Parameters.AddWithValue("rating", comment.rating);
                cmd.Parameters.AddWithValue("deckID", comment.deckID);
                cmd.Parameters.AddWithValue("userID", comment.userID);
                cmd.Parameters.AddWithValue("creation", comment.creation);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetCommentsTableByDeckID(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM comments WHERE deckID = @deckID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static DataTable GetCommentsTableByDeckID(String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM comments WHERE deckID = @deckID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
        }

        public static Comment GetCommentByUserIDAndDeckID(int userID, int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM comments WHERE userID = @userID AND deckID = @deckID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("userID", userID);
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                if (dt.Rows.Count == 0)
                    return null;

                return new Comment(dt.Rows[0]);
            }
        }

        public static void DeleteComment(String id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM comments WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteComment(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM comments WHERE id = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteCommentByUserID(int userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM comments WHERE userID = '" + userID + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteCommentsWithDeckID(String deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM comments WHERE deckID = '" + deckID + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteCommentsWithDeckID(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM comments WHERE deckID = '" + deckID + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void UpdateComment(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE comments SET comment = @comment, rating = @rating, deckID = @deckID, userID = @userID, creation = CONVERT(date, @creation, @dateType) WHERE id = @id", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("comment", comment.comment);
                cmd.Parameters.AddWithValue("rating", comment.rating);
                cmd.Parameters.AddWithValue("deckID", comment.deckID);
                cmd.Parameters.AddWithValue("userID", comment.userID);
                cmd.Parameters.AddWithValue("creation", comment.creation);
                cmd.Parameters.AddWithValue("id", comment.id);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCommentByUserIDAndDeckID(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE comments SET comment = @comment, rating = @rating, deckID = @deckID, userID = @userID, creation = CONVERT(date, @creation, @dateType) WHERE userID = @userID AND deckID = @deckID", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("comment", comment.comment);
                cmd.Parameters.AddWithValue("rating", comment.rating);
                cmd.Parameters.AddWithValue("deckID", comment.deckID);
                cmd.Parameters.AddWithValue("userID", comment.userID);
                cmd.Parameters.AddWithValue("creation", comment.creation);
                cmd.Parameters.AddWithValue("dateType", dateType);

                cmd.ExecuteNonQuery();
            }
        }

        // Get averge score and total votes of the rating
        public static double[] GetAverageAndTotalRating(int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT rating FROM comments WHERE deckID = " + deckID, con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();

                DataTable dt = new DataTable();
                ad.Fill(dt);

                double avg = 0;

                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        avg += Convert.ToInt32(row[0]);
                    }

                    avg /= dt.Rows.Count;
                }

                return new double[] { avg, dt.Rows.Count };
            }
        }

        // Check if user commented on that deck already
        public static bool UserCommentExists(int userID, int deckID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM comments WHERE userID = @userID AND deckID = @deckID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("userID", userID);
                cmd.Parameters.AddWithValue("deckID", deckID);

                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt.Rows.Count > 0;
            }
        }


        //
        //
        //         PROFILES
        //
        //


        public static void InsertProfile(byte[] imageBytes, int userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO profiles values(@userID, @imageBytes)", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("imageBytes", imageBytes);
                cmd.Parameters.AddWithValue("userID", userID);

                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteProfile(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM profiles WHERE userID = '" + id + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteProfile(String userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM profiles WHERE userID = '" + userID + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateProfile(byte[] imageBytes, int userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("UPDATE profiles SET picture = @imageBytes WHERE userID = @userID", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("imageBytes", imageBytes);
                cmd.Parameters.AddWithValue("userID", userID);

                cmd.ExecuteNonQuery();
            }
        }

        // Get bytes of the Image profile
        public static byte[] GetProfile(int userID)
        {
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand("SELECT picture FROM profiles WHERE userID = @userID", con))
            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
            {
                con.Open();
                cmd.Parameters.AddWithValue("userID", userID);

                using(SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (!rd.HasRows)
                        return null;

                    rd.Read();
                    return (byte[])rd[0];
                }
            }
        }
    }
}