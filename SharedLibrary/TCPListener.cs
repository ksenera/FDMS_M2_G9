using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SharedLibrary
{
    public class TCPListener
    {
        // pasting from TelemetryCollector.cs 
        private TcpListener listener;
        private TcpClient client;
        private int port;
        private bool isConnected;
        private TelemetryCollector telemetryCollector;

        private const int BUFFER_SIZE = 1024;

        public event Action<bool> ConnectionStatusChanged;

        // constructor for port and initialize new tcp listening to any ip add 
        public TCPListener(int port, TelemetryCollector telemetryCollector)
        {
            this.port = port;
            listener = new TcpListener(IPAddress.Any, port);
            this.isConnected = false;
            this.telemetryCollector = telemetryCollector;
        }

        // instead of connecting and checking property isConnected 
        // use async method that waits for incoming aircraft transmission connections 
        /*
         * FUNCTION : StartListeningAsync()
         *
         * DESCRIPTION : This method is used to start listening for incoming aircraft transmission connections
         * 
         * PARAMETERS : none
         *
         * RETURNS : none
         */
        public async Task StartListeningAsync()
        {
            listener.Start();
            Console.WriteLine("Listening for aircraft transmission connections ...");
            while (true)
            {
                try
                {
                    client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Connected");

                    ConnectionStatusChanged?.Invoke(true);

                    NetworkStream stream = client.GetStream();
                    await telemetryCollector.ProcessClientStreamAsync(stream);

                    client.Close();
                    Console.WriteLine("Disconnected");

                    ConnectionStatusChanged?.Invoke(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }



        /*
         * FUNCTION : HandleClientAsync()
         *
         * DESCRIPTION : this method is chained to the previous one as it is called to handle the aircraft trans client connecting to the listener 
         * 
         * PARAMETERS : TcpClient client
         *
         * RETURNS : none
         */
        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                // use network stream 
                using (NetworkStream stream = client.GetStream()) 
                {
                    // ground system terminal is not parsing each packet received 
                    //await telemetryCollector.ProcessClientStreamAsync(stream);
                    //byte[] buffer = new byte[BUFFER_SIZE];
                    //int bytesRead;

                    //// read the data from the client 
                    //while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    //{
                    //    string receivedTelemetry = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    //    Console.WriteLine($"Received: {receivedTelemetry}");
                    //}
                }
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


        /*
         * FUNCTION : HandleClientAsync()
         *
         * DESCRIPTION : finally stop listening 
         * 
         * PARAMETERS : none
         *
         * RETURNS : none
         */
        public void StopListening()
        {
            listener.Stop();
            Console.WriteLine("Stopped listening for aircraft transmissions");
        }
    }
}
