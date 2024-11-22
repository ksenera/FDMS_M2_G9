using System.Net.Sockets;

namespace AircraftTransmissionSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // this works :D (Parsing only)
            //FileReader fileReader = new FileReader("");

            //fileReader.parsedData = fileReader.ParseData();

            //Console.WriteLine(fileReader.parsedData.AccelY);



            TCPCommunicator tcpCommunicator = new TCPCommunicator();

            Task task = tcpCommunicator.ConnectToGroundTerminal();
            
            // add packet here somehow tos end to ground

            Console.WriteLine("finished");
        }
    }
}
