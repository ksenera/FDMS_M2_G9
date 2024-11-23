/*
 * File          : DatabaseHandler.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Handles connection to and from the database, stores and received from the database.
 */

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
            //connectionString = connection.ConnectionString;
            //connection = new SqlConnection(connectionString);
        }


        /*
         * FUNCTION : Connect()
         *
         * DESCRIPTION : Connects to the database
         * 
         * PARAMETERS : none
         *
         * RETURNS : void
         */
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

        /*
         * FUNCTION : Disconnect()
         *
         * DESCRIPTION : Disconnects from the database
         * 
         * PARAMETERS : none
         *
         * RETURNS : void
         */
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

        /*
         * FUNCTION : StoreGForceData()
         *
         * DESCRIPTION : inserts parsed data into G Force Data table
         * 
         * PARAMETERS : ParsedData data
         *
         * RETURNS : void
         */
        public void StoreGForceData(ParsedData data)
        {
            string query = "INSERT INTO GForceData (AircraftID, Timestamp, AccelX, AccelY, AccelZ, Weight, " +
                "Checksum) VALUES (@AircraftID, @Timestamp, @AccelX, @AccelY, @AccelZ, @Weight, @Checksum)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@AircraftID", data.AircraftID);
            command.Parameters.AddWithValue("@Timestamp", data.Timestamp);
            command.Parameters.AddWithValue("@AccelX", data.AccelX);
            command.Parameters.AddWithValue("@AccelY", data.AccelY);
            command.Parameters.AddWithValue("@AccelZ", data.AccelZ);
            command.Parameters.AddWithValue("@Weight", data.Weight);
            command.Parameters.AddWithValue("@Checksum", data.Checksum);
            command.ExecuteNonQuery();
        }

        /*
         * FUNCTION : StoreGForceData()
         *
         * DESCRIPTION : parsed data stored into attitude data table
         * 
         * PARAMETERS : ParsedData data
         *
         * RETURNS : void
         */
        public void StoreAttitudeData(ParsedData data)
        {
            string query = "INSERT INTO AttitudeData (AircraftID, Timestamp, Altitude, Pitch, Bank, Checksum) " +
                "VALUES (@AircraftID, @Timestamp, @Altitude, @Pitch, @Bank, @Checksum)";
            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@AircraftID", data.AircraftID);
            command.Parameters.AddWithValue("@Timestamp", data.Timestamp);
            command.Parameters.AddWithValue("@Altitude", data.Altitude);
            command.Parameters.AddWithValue("@Pitch", data.Pitch);
            command.Parameters.AddWithValue("@Bank", data.Bank);
            command.Parameters.AddWithValue("@Checksum", data.Checksum);
            command.ExecuteNonQuery();
        }


        /*
         * FUNCTION : RetrieveGForceData()
         *
         * DESCRIPTION : retrieves data from G Force Data table
         * 
         * PARAMETERS : none
         *
         * RETURNS : void
         */
        // need to add within a specified date range for the query 
        public SqlDataReader RetrieveGForceData()
        {
            string query = "SELECT * FROM GForceData";
            SqlCommand command = new(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        /*
         * FUNCTION : StoreData()
         *
         * DESCRIPTION : not implemented (temporary store data method)
         * 
         * PARAMETERS : none
         *
         * RETURNS : void
         */
        internal void StoreData(ParsedData data)
        {
            throw new NotImplementedException();
        }
    }
}
