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
            try
            {
                connection.Open();
                Console.WriteLine("Database connection established.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error connecting to database: " + ex.Message);
                throw;
            }
        }

        public void Disconnect()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Database connection closed.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error disconnecting from database: " + ex.Message);
                throw;
            }
        }

        // inserts parsed data into G Force Data table
        public void StoreGForceData(ParsedData data)
        {
            string query = "INSERT INTO GForceData (AircraftID, Timestamp, AccelX, AccelY, AccelZ, " +
                "Checksum) VALUES (@AircraftID, @Timestamp, @AccelX, @AccelY, @AccelZ, @Checksum)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@AircraftID", data.AircraftID);
            command.Parameters.AddWithValue("@Timestamp", data.Timestamp);
            command.Parameters.AddWithValue("@AccelX", data.AccelX);
            command.Parameters.AddWithValue("@AccelY", data.AccelY);
            command.Parameters.AddWithValue("@AccelZ", data.AccelZ);
            command.Parameters.AddWithValue("@Checksum", data.Checksum);
            command.ExecuteNonQuery();
        }

        // parsed data stored into attitude data table
        public void StoreAttitudeData(ParsedData data)
        {
            string query = "INSERT INTO AttitudeData (AircraftID, Timestamp, Pitch, Bank, Checksum) " +
                "VALUES (@AircraftID, @Timestamp, @Pitch, @Bank, @Checksum)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@AircraftID", data.AircraftID);
            command.Parameters.AddWithValue("@Timestamp", data.Timestamp);
            command.Parameters.AddWithValue("@Pitch", data.Pitch);
            command.Parameters.AddWithValue("@Bank", data.Bank);
            command.Parameters.AddWithValue("@Checksum", data.Checksum);
            command.ExecuteNonQuery();
        }

        // retrieves data from G Force Data table

        // need to add within a specified date range for the query 
        public SqlDataReader RetrieveGForceData()
        {
            string query = "SELECT * FROM GForceData";
            SqlCommand command = new(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }


        // temporary store data method 
        internal void StoreData(ParsedData data)
        {
            throw new NotImplementedException();
        }
    }
}
