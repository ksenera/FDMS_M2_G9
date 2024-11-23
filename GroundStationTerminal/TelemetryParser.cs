/*
 * File          : .cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : 
 */

using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundStationTerminal
{
    public class TelemetryParser
    {
        private string dataFormat;

        public TelemetryParser()
        {

        }

        public ParsedData ParseData(string data)
        {

            // if the data is not in the correct format, throw ArgumentException
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data cannot be null or empty.");
            }

            // parse based on specific format array of strings and split
            string[] entirePacket = data.Split('*');

            // header has the aircraft tail ID and sequence number of the packet
            string[] packetHeader = entirePacket[0].Split(',');

            string aircraftTailID = packetHeader[0];

            // body has data/time accelx,y,z weight altitude pitch bank
            string[] packetBody = entirePacket[1].Split(',');

            DateTime timestamp = DateTime.Parse(packetBody[0]);
            double accelX = double.Parse(packetBody[1]);
            double accelY = double.Parse(packetBody[2]);
            double accelZ = double.Parse(packetBody[3]);
            double weight = double.Parse(packetBody[4]);
            double altitude = double.Parse(packetBody[5]);
            double pitch = double.Parse(packetBody[6]);
            double bank = double.Parse(packetBody[7]);

            // packet trailer is the checksum 
            if (!int.TryParse(entirePacket[2], out int checksum))
            {
                throw new ArgumentException("Invalid checksum.");
            }
            
            // should follow the format as per SharedLibrary.ParsedData
            return new ParsedData
            {
                AircraftID = aircraftTailID,
                Timestamp = timestamp,
                AccelX = accelX,
                AccelY = accelY,
                AccelZ = accelZ,
                Weight = weight,
                Altitude = altitude,
                Pitch = pitch,
                Bank = bank,
                Checksum = checksum
            };
            
        }

        public bool ValidateChecksum(string data)
        {
            // packet needs to be split into header, body, and trailer
            string[] entirePacket = data.Split('*');

            // checksum Calculation as per APPENDIX C requires body elements 
            string[] packetBody = entirePacket[1].Split(",");
            double altitude = double.Parse(packetBody[5]);
            double pitch = double.Parse(packetBody[6]);
            double bank = double.Parse(packetBody[7]);

            // checksum calculation
            double checksumResult = (altitude + pitch + bank) / 3;
            checksumResult = Math.Round(checksumResult, 0);

            // extract checksumResult from trailer of packet and compare
            if (!int.TryParse(entirePacket[2], out int checksum))
            {
                throw new ArgumentException("Invalid checksum.");
            }

            return checksum == (int)checksumResult;
        }
    }
}
