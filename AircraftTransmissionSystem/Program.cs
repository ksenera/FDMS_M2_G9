/*
 * File          : .cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : 
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



            TCPCommunicator tcpCommunicator = new TCPCommunicator();

            // first await connection to ground term to be completed 
            await tcpCommunicator.ConnectToGroundTerminal();

            Console.WriteLine("Successfully connected to Ground Station Terminal");

            //Task task = tcpCommunicator.ConnectToGroundTerminal(); made it async task Main
            
            // add packet here somehow tos end to ground

            Console.WriteLine("finished");
        }
    }
}
