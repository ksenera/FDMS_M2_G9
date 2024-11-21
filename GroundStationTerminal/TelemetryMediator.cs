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
        private void StoreInDatabase(ParsedData data)
        {
            databaseHandler.StoreData(data);
        }

    }
}
