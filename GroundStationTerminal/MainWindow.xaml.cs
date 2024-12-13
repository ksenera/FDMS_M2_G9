/*
 * File          : MainWindow.xaml.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Start position, toggle real-time data collection and search button for searching the database
 */

using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using SharedLibrary;
using System.Collections.ObjectModel;
using System.Net.Sockets;
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
        private TCPListener listener;
        private GUIInterfaceManager guiInterfaceManager;
        private TelemetryMediator telemetryMediator;
        private DatabaseHandler databaseHandler;
        private TelemetryCollector telemetryCollector;
        private TelemetryParser telemetryParser;

        internal ObservableCollection<ParsedData> telemetryData;
        private bool IsRealTimeModeEnabled = true;

        public MainWindow()
        {
            InitializeComponent();
            // add the connection string from remote db here not sure if azure or not 
            this.Activate();
            this.Left = 100;
            this.Top = 100;

            databaseHandler = new DatabaseHandler(); // make sure to add the connection string here

            telemetryParser = new TelemetryParser();
            
            telemetryCollector = new TelemetryCollector(telemetryParser);

            

            guiInterfaceManager = new GUIInterfaceManager(this);

            telemetryMediator = new TelemetryMediator(telemetryCollector, telemetryParser, databaseHandler, guiInterfaceManager)
            {
                IsRealTimeModeEnabled = IsRealTimeModeEnabled
            };

            telemetryData = new ObservableCollection<ParsedData>();
            TelemetryDataGrid.ItemsSource = telemetryData;

            StatusLabel.Content = "Status: Disconnected";
            int port = 8080;

            listener = new TCPListener(port, telemetryCollector);
            listener.ConnectionStatusChanged += OnConnectionStatusChanged;

            StartTelemetryListener();

            

        }

        private void OnConnectionStatusChanged(bool isConnected)
        {
            Dispatcher.Invoke(() =>
            {
                StatusLabel.Content = isConnected ? "Status: Connected" : "Status: Disconnected";
            });
        }

        private async void StartTelemetryListener()
        {
            await listener.StartListeningAsync();
        }

     
        private void Listener_ClientConnected(object sender, EventArgs e)
        {

            Dispatcher.Invoke(() =>
            {
                StatusLabel.Content = "Status: Connected";
            });
        }


        /*
         * FUNCTION : UpdateTelemetryData()
         *
         * DESCRIPTION : not implemented
         * 
         * PARAMETERS : ObservableCollection<ParsedData> telemetryDataCollected
         *
         * RETURNS : none
         */
        //internal void UpdateTelemetryData(ObservableCollection<ParsedData> telemetryDataCollected)
        //{
        //    telemetryData = telemetryDataCollected;
        //    TelemetryDataGrid.ItemsSource = telemetryData;
        //}

        /*
         * FUNCTION : ToggleRealTimeButton_Click()
         *
         * DESCRIPTION : Toggle button for real-time data collection
         * 
         * PARAMETERS : object sender, RoutedEventArgs e
         *
         * RETURNS : void
         */
        private void ToggleRealTimeButton_Click(object sender, RoutedEventArgs e)
        {
            IsRealTimeModeEnabled = !IsRealTimeModeEnabled;
            telemetryMediator.IsRealTimeModeEnabled = IsRealTimeModeEnabled;

            ToggleRealTimeButton.Content = IsRealTimeModeEnabled ? "Disable Real-Time" : "Enable Real-Time";
        }

        /*
         * FUNCTION : ToggleRealTimeButton_Click()
         *
         * DESCRIPTION : search button for searching the database
         * 
         * PARAMETERS : object sender, RoutedEventArgs e
         *
         * RETURNS : void
         */
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRealTimeModeEnabled)
            {
                var results = databaseHandler.SearchTelemetryData("C-FGAX");
                telemetryData.Clear();
                foreach (var r in results)
                {
                    telemetryData.Add((ParsedData)r);
                }
            }
            else
            {
                MessageBox.Show("Disable Real-Time mode before searching.");
            }
        }
    }
}