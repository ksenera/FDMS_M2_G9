/*
 * File          : FileReader.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Logic for reading data from a file and parsing it into a ParsedData object
 */

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
    internal class FileReader
    {
        private string FilePath;
        public ParsedData parsedData;

        // creating file reader constructor 
        public FileReader(string filePath)
        {
            this.FilePath = filePath;
            this.parsedData = new ParsedData(); 
        }

        // read telemetry data must read each line 

        public IEnumerable<ParsedData> ReadData()
        {
            IEnumerable<string> lines = null;
            try
            {
                lines = File.ReadLines(FilePath);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error '{error.Message}' from reading file {FilePath}");
                throw new NotImplementedException(); // idk what to put here :3
            }

            if (lines != null )
            {
                foreach (var line in lines)
                {
                    yield return ParseData(line);
                }
            }

        }

        // maybe keep private since it's only used in this class, not needed outside
        public ParsedData ParseData(string line)
        {
            // instead of using this i will add the string parameter called line as 
            // each line must be read by the new read data method 
            // const string data = "7_8_2018 19:35:21,-0.799099, 0.047375, 0.028341, 2154.009033, 1124.106079, 0.022695, 0.001006"; // placeholder

            string[] parts = line.Split(',');

            // make sure there is a check for the parts in a telemetry line 
            if (parts.Length != 8)
            {
                throw new FormatException("Data format is invalid");
            }

            // rearranged according to appendix d
            string timestamp = parts[0].Trim();
            double x = Convert.ToDouble(parts[1].Trim());
            double y = Convert.ToDouble(parts[2].Trim());
            double z = Convert.ToDouble(parts[3].Trim());
            double speed = Convert.ToDouble(parts[5].Trim());
            double altitude = Convert.ToDouble(parts[4].Trim());
            double pitch = Convert.ToDouble(parts[7].Trim());
            double roll = Convert.ToDouble(parts[6].Trim());


            return new ParsedData
            {
                Timestamp = DateTime.Parse(timestamp),
                AccelX = x,
                AccelY = y,
                AccelZ = z,
                Weight = speed,
                Altitude = altitude,
                Pitch = pitch,
                Bank = roll,

            };
        }

        private interface DataAdapter
        {
            void ConvertData();
        }
    }
}
