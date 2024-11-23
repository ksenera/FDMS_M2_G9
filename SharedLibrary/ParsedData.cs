/*
 * File          : ParsedData.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Used for the entire project, this class is used to store the parsed data from the aircraft as an object and quick way to access flight data
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class ParsedData
    {
        public string AircraftID { get; set; }
        public DateTime Timestamp { get; set; }
        public double AccelX { get; set; }
        public double AccelY { get; set; }
        public double AccelZ { get; set; }
        public double Weight { get; set; }
        public double Altitude { get; set; }
        public double Pitch { get; set; }
        public double Bank { get; set; }
        public int Checksum { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(AircraftID)) throw new ArgumentException("AircraftID cannot be null or empty.");
            if (AccelX < -10 || AccelX > 10) throw new ArgumentOutOfRangeException("AccelX is out of range.");
        }

    }
}
