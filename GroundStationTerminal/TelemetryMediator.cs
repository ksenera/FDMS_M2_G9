using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;

namespace GroundStationTerminal
{
    public class TelemetryMediator
    {
        private TelemetryCollector telemetryCollector;
        private TelemetryParser telemetryParser;
        private GUIInterfaceManager guiInterfaceManager;
        private DatabaseHandler databaseHandler;

        public TelemetryMediator(TelemetryCollector telemetryCollector, TelemetryParser telemetryParser, GUIInterfaceManager guiInterfaceManager, DatabaseHandler databaseHandler)
        {
            this.telemetryCollector = telemetryCollector;
            this.telemetryParser = telemetryParser;
            this.guiInterfaceManager = guiInterfaceManager;
            this.databaseHandler = databaseHandler;
        }



        // method to call when new telemetry data is received from the collector 
        public void ReceiveTelemetryData(string rawData)
        {


        }

        // private mediator method to update the GUI 
        private void UpdateGUI(ParsedData data)
        {
            guiInterfaceManager.UpdateGUI(data);
        }


        // private mediator method to store telemetry data in the database
        // here will be tricky as there are two tables in the database 
        // one for storin Gforcedata and another for storing Attitudedata 
        // both has a composite key AircraftTailID and TimeStamp
        // so there would be two StoreData methods in the DatabaseHandler class
        private void StoreInDatabase(ParsedData data)
        {
            databaseHandler.StoreData(data);
        }

    }
}
