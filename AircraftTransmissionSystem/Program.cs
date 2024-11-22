using System.Net.Sockets;

namespace AircraftTransmissionSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            FileReader fileReader = new FileReader("");

            fileReader.parsedData = fileReader.ParseData();

            Console.WriteLine(fileReader.parsedData.AccelY);
        }


        private class ClientListener
        {

            private TcpListener TcpListener { get; set; }



            public void StartListening()
            {
                throw new NotImplementedException();
            }
        }
    }
}
