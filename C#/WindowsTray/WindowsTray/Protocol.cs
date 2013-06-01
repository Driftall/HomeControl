using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace WindowsTray
{
    class Protocol
    {
        String dbConnection;

        public Protocol()
        {
            dbConnection = "DataSource=protocol.db3";
        }

        public void GetIDFromReading(String Reading)
        {
            try
            {
                SQLiteConnection db = new SQLiteConnection();
                DataTable readings;
                String query = "SELECT ID, READING FROM READINGS WHERE READING = '" + Reading + "';";
                SQLiteCommand command = new SQLiteCommand();
                SQLiteDataReader reader = command.ExecuteReader();
                Dictionary<int, String> results = new Dictionary<int, String>;
                while(reader.Read())
                {
                    int ID;
                    Int32.TryParse((String)reader["ID"], out ID);
                    results.Add(ID, reader["READING"].ToString());
                }
            }
        }
    }
}
