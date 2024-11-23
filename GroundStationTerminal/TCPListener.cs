using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GroundStationTerminal
{
    public class TCPListener
    {
        // pasting from TelemetryCollector.cs 
        private TcpListener listener;
        private TcpClient client;
        private NetworkStream stream;

        private string socketAddress;
        private int port;

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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error during disconnection: {ex.Message}");
            }
        }

    }
}
