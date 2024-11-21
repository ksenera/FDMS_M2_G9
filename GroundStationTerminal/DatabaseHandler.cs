using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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



        // temporary store data method 
        internal void StoreData(ParsedData data)
        {
            throw new NotImplementedException();
        }
    }
}
