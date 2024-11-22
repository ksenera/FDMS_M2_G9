using SharedLibrary;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroundStationTerminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // all class instances

        private GUIInterfaceManager guiInterfaceManager;
        private TelemetryMediator telemetryMediator;
        private DatabaseHandler databaseHandler;
        private TelemetryCollector telemetryCollector;
        private TelemetryParser telemetryParser;

        public MainWindow()
        {
            InitializeComponent();
            // add the connection string from remote db here not sure if azure or not 

            databaseHandler = new DatabaseHandler(); // make sure to add the connection string here

            telemetryParser = new TelemetryParser("defaultFormat"); // provide the required 'format' parameter

            telemetryCollector = new TelemetryCollector("127.0.0.1", 8080, telemetryParser);

            guiInterfaceManager = new GUIInterfaceManager(this);

            telemetryMediator = new TelemetryMediator(telemetryCollector, telemetryParser, guiInterfaceManager, databaseHandler);

            telemetryCollector.Connect();
        }

        internal void UpdateTelemetryData(ObservableCollection<ParsedData> telemetryDataCollected)
        {
            throw new NotImplementedException();
        }

        // Toggle button for real-time data collection
        private void ToggleRealTimeButton_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        // search button for searching the database
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}