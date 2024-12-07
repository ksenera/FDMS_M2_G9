/*
 * File          : TCPCommunicator.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : This file contains the code for Connecting to the Ground Terminal. Once connceted is sends 
 */

using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AircraftTransmissionSystem
{
    internal class TCPCommunicator
    {
        private TcpClient client;
        private NetworkStream fileReceive;
        private const int BUFFER_SIZE = 1024;

        private bool isConnected;


        //private IPAddress groundTerminalIP = "127.0.0.1";
        private string groundTerminalIP = "127.0.0.1"; // local
        private int groundTerminalPort = 8080;

        // things we need to send over to ground terminal
        private PacketBuilder packetBuilder;
        private FileReader fileReader;
        //private readonly ILogger logger;


        // adding file reader file path as parameter
        public TCPCommunicator(string filePath)
        {
            groundTerminalIP = "127.0.0.1";
            groundTerminalPort = 8080;
            isConnected = false;

            this.packetBuilder = new PacketBuilder();
            this.fileReader = new FileReader(filePath);

        }


        /*
         * FUNCTION : ConnectToGroundTerminal()
         *
         * DESCRIPTION : This method connects to the ground terminal using the IP and port number
         *
         * PARAMETERS : none
         *
         * RETURNS : none
         */
        public async Task ConnectToGroundTerminal()
        {
            try
            {
                if (isConnected)
                {
                    //logger.LogWarning("Already connected to ground terminal");
                    Console.WriteLine("Already connected to ground terminal");
                    return;
                }

                client = new TcpClient();
                await client.ConnectAsync(groundTerminalIP, groundTerminalPort);
                fileReceive = client.GetStream();
                isConnected = true;
                //logger.LogInformation($"Successfully connected to ground terminal at {groundTerminalIP}:{groundTerminalPort}");
            }
            catch (SocketException ex)
            {
                //logger.LogError($"Socket error while connecting to ground terminal: {ex.Message}");
                throw new Exception("Socket error while connecting to ground terminal", ex);
            }
        }


        // REQ: telemetry data is supposed to be read by the FileReader then passed to the PacketBuilder to 
        // form the packet it is missing there's no call to PacketBuilder 

        // new send method reads data builds packet and send and it should be called in main on program.cs

        /*
         * FUNCTION : SendAllTelemetryPackets()
         *
         * DESCRIPTION : This method sends telemtry data to the ground terminal after connection is made
         * 
         * PARAMETERS : none
         *
         * RETURNS : none
         */
        public async Task SendAllTelemetryPackets()
        {
            if (!isConnected)
                throw new InvalidOperationException("Not connected to ground terminal");

            foreach (var parsedData in fileReader.ReadData())
            {
                // Get the AircraftTailId from the FileReader
                string aircraftTailId = fileReader.GetAircraftTailID();

                // Create the packet with the tail ID
                Packet packet = packetBuilder.BuildPacket(parsedData, aircraftTailId);

                // Verify checksum before sending
                if (!packet.VerifyChecksum())
                {
                    Console.WriteLine("Packet checksum invalid, packet being skipped...");
                    continue; // Skip this packet if checksum is invalid
                }

                // Send packet
                await SendTelemetryPacketAsync(packet);

                // Delay for 1 second to simulate reading one line at a time
                await Task.Delay(1000);
            }
        }

        /*
         * FUNCTION : SendTelemetryPacketAsync()
         *
         * DESCRIPTION : This method sends telemtry data to the ground terminal
         * 
         * PARAMETERS : Packet packet
         *
         * RETURNS : none
         */
        public async Task SendTelemetryPacketAsync(Packet packet)
        {
            //    if (packet == null)
            //        throw new ArgumentNullException(nameof(packet));

            if (!isConnected)
                throw new InvalidOperationException("Not connected to ground terminal");

            try
            {
                // Verify packet checksum before sending
                /*if (!packet.VerifyChecksum())
                {
                    throw new InvalidOperationException("Invalid packet checksum");
                }*/

                byte[] packetData = packet.CreateByteArray();
                await fileReceive.WriteAsync(packetData, 0, packetData.Length);
                await fileReceive.FlushAsync();

                Console.WriteLine($"Successfully sent packet of {packetData.Length} bytes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while sending packet: {ex.Message}");
                throw new Exception("Error while sending packet", ex);
            }

            // wait for files to transfer
        }
    }
}
