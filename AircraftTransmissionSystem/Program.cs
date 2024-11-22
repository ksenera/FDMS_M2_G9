using System.Net.Sockets;

namespace AircraftTransmissionSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // this works :D
            //FileReader fileReader = new FileReader("");

            //fileReader.parsedData = fileReader.ParseData();

            //Console.WriteLine(fileReader.parsedData.AccelY);




            TCPCommunicator tcpCommunicator = new TCPCommunicator();

            Task task = tcpCommunicator.ConnectToGroundTerminal();
            Task task2 = tcpCommunicator.ReceiveFile();

            Console.WriteLine("finished");
        }
    }
}
