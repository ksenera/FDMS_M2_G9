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
        private int port;
        private bool isConnected;

        private const int BUFFER_SIZE = 1024;

        // constructor for port and initialize new tcp listening to any ip add 
        public TCPListener(int port)
        {
            this.port = port;
            listener = new TcpListener(IPAddress.Any, port);
            this.isConnected = false;
        }

        // instead of connecting and checking property isConnected 
        // use async method that waits for incoming aircraft transmission connections 

        public async Task StartListeningAsync()
        {
            listener.Start();
            Console.WriteLine("Listening for aircraft transmission connections ...");
            while (true)
            {
                try
                {
                    client = new TcpClient();
                    await listener.AcceptTcpClientAsync();
                    isConnected = true;
                    Console.WriteLine("Connected to Aircraft Transmission System");

                    // call the handle client async 
                }
                catch { }
            }
        }

        // this method is chained to the previous one as it is called to handle the 
        // aircraft trans client connecting to the listener 
        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                // use network stream 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                isConnected = false;
                client.Close();
                Console.WriteLine("Aircraft transmission no longer connected");
            }
        }

    }
}
