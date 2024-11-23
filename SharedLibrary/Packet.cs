/*
 * File          : Packet.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : A SharedLibrary file that containts the Packet struct which is used to store the data packets that are sent between the aircraft and the ground station.
 *                  also helps with the DRY principle
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public struct Packet // in appendix A - Data Packet format
    {
        // Header
        public string AircraftTailId; // Fixed 10 chars
        public DateTime Timestamp;

        // G-Force parameters
        public double AccelX;
        public double AccelY;
        public double AccelZ;
        public double Weight;

        // Attitude parameters
        public double Altitude;
        public double Pitch;
        public double Bank;

        // Checksum
        public double Checksum;


        public double CalculateChecksum()
        {
            Checksum = (Altitude + Pitch + Bank) / 3;
            return Checksum; // also return, a getter function
        }

        public bool VerifyChecksum()
        {
            // verify the correct checksum
            double expected = CalculateChecksum();
            return Math.Abs(CalculateChecksum() - expected) < 0.0001;
        }


        // actually creating bytearray
        public byte[] CreateByteArray()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                writer.Write(Encoding.ASCII.GetBytes(AircraftTailId.PadRight(10)));
                writer.Write(Timestamp.ToBinary());

                // G-Force parameters
                writer.Write(AccelX);
                writer.Write(AccelY);
                writer.Write(AccelZ);
                writer.Write(Weight);


                // attitude parameters
                writer.Write(Altitude);
                writer.Write(Pitch);
                writer.Write(Bank);


                // cecksum
                writer.Write(Checksum);

                return ms.ToArray();
            }
        }
    }
}
