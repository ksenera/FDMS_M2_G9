﻿using System;
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
