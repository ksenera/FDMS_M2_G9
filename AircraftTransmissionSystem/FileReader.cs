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
        /*
         * FUNCTION : ReadData()
         *
         * DESCRIPTION : This method reads the data from the file and returns a packet
         * 
         * PARAMETERS : none
         *
         * RETURNS : Packet packet
         */
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

            // due to format exception triggering caused by trailing commas 
            // storing valid parts of telemetry line in list
            List<string> validPartsOfTelemetryLine = new List<string>();

            foreach (string part in parts)
            {
                if (!string.IsNullOrWhiteSpace(part))
                {
                    validPartsOfTelemetryLine.Add(part);
                }
            }

            // taking list and converting to an array to validate length 
            parts = validPartsOfTelemetryLine.ToArray();

            // make sure there is a check for the parts in a telemetry line 
            if (parts.Length != 8)
            {
                throw new FormatException("Data format is invalid");
            }

            string timestamp = parts[0].Trim();
            DateTime parsedTimestamp;

            // array of possible time stamp formats 
            string[] allTimeStampFormats =
            {
                "d_M_yyyy H:mm:ss",     
                "d_M_yyyy H:m:s",       
                "d_M_yyyy HH:mm:ss",    
                "dd_MM_yyyy H:mm:ss",   
                "dd_MM_yyyy H:m:s"      
            };

#pragma warning disable S6580 // Use a format provider when parsing date and time
            bool isTimestampValid = DateTime.TryParseExact(
                timestamp,
                allTimeStampFormats,
                null,
                System.Globalization.DateTimeStyles.None,
                out parsedTimestamp
            );
#pragma warning restore S6580 // Use a format provider when parsing date and time

            // check if time stamp valid 
            if (!isTimestampValid)
            {
                Console.WriteLine($"Timestamp format invalid: {timestamp}");
                throw new FormatException("Invalid timestamp format");
            }

            // rearranged according to appendix d
            return new ParsedData
            {
                Timestamp = parsedTimestamp,
                AccelX = Convert.ToDouble(parts[1].Trim()),
                AccelY = Convert.ToDouble(parts[2].Trim()),
                AccelZ = Convert.ToDouble(parts[3].Trim()),
                Weight = Convert.ToDouble(parts[5].Trim()),
                Altitude = Convert.ToDouble(parts[4].Trim()),
                Pitch = Convert.ToDouble(parts[7].Trim()),
                Bank = Convert.ToDouble(parts[6].Trim()),
            };

        }

        private interface DataAdapter
        {
            void ConvertData();
        }
    }
}
