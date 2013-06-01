using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace WindowsTray
{
    class Protocol
    {
        String dbConnection;

        public Protocol()
        {
            dbConnection = "Data Source=protocol.db3";

        }

        public int GetIDFromReading(String Reading)
        {
            try
            {
                SQLiteConnection db = new SQLiteConnection(dbConnection);
                db.Open();
                //DataTable readings;
                String query = "SELECT ID FROM READINGS WHERE READING = '" + Reading + "';";
                MessageBox.Show("SELECT ID FROM READINGS WHERE READING = '" + Reading + "';");
                SQLiteCommand command = new SQLiteCommand(query, db);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ID;
                    Int32.TryParse(reader["ID"].ToString(), out ID);
                    db.Close();
                    return ID;
                }
                return -1;
                //TODO: Clean up old Dictionary code
                /*Dictionary<String, int> results = new Dictionary<String, int>();
                while(reader.Read())
                {
                    int ID;
                    Int32.TryParse((String)reader["ID"], out ID);
                    results.Add(reader["READING"].ToString(), ID);
                }*/
            }
            catch(Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
                return -1;
            }
        }
    }
}
