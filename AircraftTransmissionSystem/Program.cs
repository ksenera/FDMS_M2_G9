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

            while (true)
            {
                // keep connection alive and after while is not true exit gracefully

                // testing telemetry data simulated 
                string telemetryData = "7_8_2018 19:35:21,-0.799099,0.047375,0.028341,2154.009033,1124.106079,0.022695,0.001006";
                
            }
            
            // add packet here somehow tos end to ground

            Console.WriteLine("finished");
        }
    }
}
