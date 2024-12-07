/*
 * File          : GUIInterfaceManager.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Handles all the GUI updates for the Ground Station Terminal and displayng the data
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;

namespace GroundStationTerminal
{
    public class GUIInterfaceManager : IObserver
    {
        private ObservableCollection<ParsedData> telemetryDataCollected;

        private MainWindow mainWindow;

        public GUIInterfaceManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            telemetryDataCollected = new ObservableCollection<ParsedData>();
        }

        /*
         * FUNCTION : UpdateGUI()
         *
         * DESCRIPTION : Method to update the GUI with the latest telemetry data
         * 
         * PARAMETERS : ParsedData data
         *
         * RETURNS : void
         */
        public void UpdateGUI(ParsedData data)
        {
            telemetryDataCollected.Add(data);
            mainWindow.Dispatcher.Invoke(() =>
            {
                mainWindow.UpdateTelemetryData(telemetryDataCollected);
            });
        }

        // update the GUI with the latest telemetry data
        //public void UpdateGUI(ParsedData data)
        //{
        //    mainWindow.Dispatcher.Invoke(() =>
        //    {
        //        mainWindow.UpdateTelemetryData(telemetryDataCollected);
        //    });
        //}

    }
}
