using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;

namespace GroundStationTerminal
{
    public class GUIInterfaceManager
    {
        private ObservableCollection<ParsedData> telemetryDataCollected;

        private MainWindow mainWindow;

        public GUIInterfaceManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            telemetryDataCollected = new ObservableCollection<ParsedData>();
        }

        // Method to update the GUI with the latest telemetry data
        public void UpdateGUI(ParsedData data)
        {
            telemetryDataCollected.Add(data);
            mainWindow.UpdateTelemetryData(telemetryDataCollected);
        }
    }
}
