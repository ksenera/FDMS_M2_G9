/*
 * File          : Program.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Entry point for starting the Aircraft Transmission System
 */

using SharedLibrary;
using System.Net.Sockets;

namespace AircraftTransmissionSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            // this works :D (Parsing only)
            //FileReader fileReader = new FileReader("");

            //fileReader.parsedData = fileReader.ParseData();

            //Console.WriteLine(fileReader.parsedData.AccelY);

            // temporarily using a harded coded file path for testing 
            // will update config file to make sure we can select the file being sent after
            string telemetryDataFilePath = "C:\\Users\\shivm\\Desktop\\C-FGAX.txt";

            TCPCommunicator tcpCommunicator = new TCPCommunicator(telemetryDataFilePath);

            // first await connection to ground term to be completed 
            await tcpCommunicator.ConnectToGroundTerminal();

            Console.WriteLine("Successfully connected to Ground Station Terminal");

            //Task task = tcpCommunicator.ConnectToGroundTerminal(); made it async task Main

            // sending all data here 
            await tcpCommunicator.SendAllTelemetryPackets();
            
            // add packet here somehow tos end to ground

            Console.WriteLine("All packets transmitted");
        }
    }
}
