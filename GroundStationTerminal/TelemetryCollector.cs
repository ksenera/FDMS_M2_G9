using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;

namespace GroundStationTerminal
{
    public class TelemetryCollector
    {
        private string socketAddress;
        private int port;
        private bool isConnected;
        private ParsedData data;
        private TcpListener listener;
        private TcpClient client;
        private NetworkStream stream;
        private TelemetryParser telemetryParser;
        private List<IObserver> observers;

        public TelemetryCollector(string socketAddress, int port, TelemetryParser telemetryParser)
        {
            this.socketAddress = socketAddress;
            this.port = port;
            this.isConnected = false;
            this.telemetryParser = telemetryParser;
            this.data = new ParsedData();
            this.listener = new TcpListener(System.Net.IPAddress.Any, port);
            this.client = new TcpClient();
            this.stream = null;
            this.observers = new List<IObserver>();
        }

        public bool Connect()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(socketAddress);
                listener = new TcpListener(ipAddress, port);
                listener.Start();
                Console.WriteLine("Listening for telemetry connections...");
                client = listener.AcceptTcpClient();
                stream = client.GetStream();
                isConnected = true;
                Console.WriteLine("Connected to telemetry server.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to telemetry server: " + ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            // Disconnect from the telemetry server
            if (isConnected)
            {
                stream.Close();
                client.Close();
                listener.Stop();
                isConnected = false;
                Console.WriteLine("Disconnected from telemetry socket. ");
            }
        }

        // Add an observer to the list of observers
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        // Remove an observer from the list of observers
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        // Notify all observers with the parsed data
        public void NotifyObservers(ParsedData data)
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(data);
            }
        }

        public void ReceiveData()
        {
            // Receive data from the telemetry server
        }

        public void ParsePacket(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine("No Data Transmitted. ");
                return;
            }

            if (telemetryParser.ValidateChecksum(data))
            {
                ParsedData parsedData = telemetryParser.ParseData(data);
                // console writelines telling user what data was parsed 
                NotifyObservers(parsedData);
            }
            else
            {
                Console.WriteLine("Invalid checksum");
            }
        }
    }

}

