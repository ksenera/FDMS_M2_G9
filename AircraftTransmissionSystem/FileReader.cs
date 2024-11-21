using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AircraftTransmissionSystem
{
    internal class FileReader(string filePath)
    {
        private string FilePath = filePath;// get a dialogue window so user can select the file with the given extensions allowed
        //private string[] FlightData;

        public string[] ReadData()
        {
            try
            {
                return File.ReadAllLines(FilePath);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error '{error.Message}' from reading file {FilePath}");
                throw new NotImplementedException(); // idk what to put here :3
            }
        }

        // maybe keep private since it's only used in this class, not needed outside
        private void ParseData(DataAdapter adapter)
        {
            throw new NotImplementedException();
        }

        private class DataAdapter(string data)
        {
            private readonly string Data = data;

            public void ConvertData()
            {
                throw new NotImplementedException();
            }
        }
    }
}
