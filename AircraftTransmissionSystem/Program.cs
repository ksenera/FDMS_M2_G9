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
        }
    }
}
