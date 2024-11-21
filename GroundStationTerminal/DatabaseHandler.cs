using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GroundStationTerminal
{
    public class DatabaseHandler
    {
        private string connectionString;
        private SqlConnection connection;

        public DatabaseHandler()
        {
            connectionString = connection.ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        public void Connect()
        {
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Close();
        }

        // inserts parsed data into G Force Data table
        public void StoreGForceData(ParsedData data)
        {
            string query = "INSERT INTO GForceData (AircraftID, Timestamp, AccelX, AccelY, AccelZ, Checksum) VALUES (@AircraftID, @Timestamp, @AccelX, @AccelY, @AccelZ, @Checksum)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@AircraftID", data.AircraftID);
            command.Parameters.AddWithValue("@Timestamp", data.Timestamp);
            command.Parameters.AddWithValue("@AccelX", data.AccelX);
            command.Parameters.AddWithValue("@AccelY", data.AccelY);
            command.Parameters.AddWithValue("@AccelZ", data.AccelZ);
            command.Parameters.AddWithValue("@Checksum", data.Checksum);
            command.ExecuteNonQuery();
        }



        // temporary store data method 
        internal void StoreData(ParsedData data)
        {
            throw new NotImplementedException();
        }
    }
}
