using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SharedLibrary;

namespace AircraftTransmissionSystem
{
    internal class FileReader(string filePath)
    {
        private string FilePath = filePath;// get a dialogue window so user can select the file with the given extensions allowed
        public ParsedData parsedData;

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
        public ParsedData ParseData()
        {
            const string data = "7_8_2018 19:35:21,-0.799099, 0.047375, 0.028341, 2154.009033, 1124.106079, 0.022695, 0.001006";

            string[] parts = data.Split(',');

            string timestamp = parts[0].Trim();
            double x = Convert.ToDouble(parts[1].Trim());
            double y = Convert.ToDouble(parts[2].Trim());
            double z = Convert.ToDouble(parts[3].Trim());
            double altitude = Convert.ToDouble(parts[4].Trim());
            double speed = Convert.ToDouble(parts[5].Trim());
            double roll = Convert.ToDouble(parts[6].Trim());
            double pitch = Convert.ToDouble(parts[7].Trim());


            ParsedData parsedData = new ParsedData
            {
                //Timestamp = DateTime.Parse(timestamp),
                AccelX = x,
                AccelY = y,
                AccelZ = z,
                Altitude = altitude,
                Weight = speed,
                Bank = roll,
                Pitch = pitch
            };

            return parsedData;
        }

        private interface DataAdapter
        {
            //private readonly string Data = data;

            public void ConvertData()
            {
                throw new NotImplementedException();
            }
        }
    }
}
