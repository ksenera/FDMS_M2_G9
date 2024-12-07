/*
 * File          : TelemetryMediator.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : coordiantes interactions between the TelemetryCollector, TelemetryParser, GUIInterfaceManager, and DatabaseHandler, and updates the GUI and stores data in the database
 */

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


        /*
         * FUNCTION : ReceiveTelemetryData()
         *
         * DESCRIPTION : not implemented
         * 
         * PARAMETERS : string rawData
         *
         * RETURNS : void
         */
        public void ReceiveTelemetryData(string rawData)
        {// method to call when new telemetry data is received from the collector 


        }

        /*
         * FUNCTION : ReceiveTelemetryData()
         *
         * DESCRIPTION : Updates the GUI of the Ground Terminal Window
         * 
         * PARAMETERS : ParsedData data
         *
         * RETURNS : void
         */
        private void UpdateGUI(ParsedData data)
        {// private mediator method to update the GUI 
            guiInterfaceManager.UpdateGUI(data);
        }



        /*
         * FUNCTION : StoreInDatabase()
         *
         * DESCRIPTION : Stores the telemetry data in the database
         * 
         * PARAMETERS : ParsedData data
         *
         * RETURNS : void
         */
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
