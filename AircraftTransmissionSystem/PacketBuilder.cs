/*
 * File          : PacketBuilder.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Builds the packet and does a checksum calculation before sending to GroundTerminal
 */

using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftTransmissionSystem
{

    public class PacketBuilder
    {
        //private string Header { get; set; }
        //private string PacketBody { get; set; }

        //private double ChecksumNumber { get; set; }

        /*
         * FUNCTION : BuildPacket()
         *
         * DESCRIPTION : This method builds the packet by taking the ParsedData object and inserting the data into the packet
         * 
         * PARAMETERS : ParsedData data
         *
         * RETURNS : Packet packet
         */
        public Packet BuildPacket(ParsedData data, string aircraftTailID)
        {
            Packet packet = new Packet
            {
                // due to null reference exception in packet line 59 default assigned 
                AircraftTailId = aircraftTailID,
                Timestamp = data.Timestamp,
                AccelX  = data.AccelX,
                AccelY = data.AccelY,
                AccelZ = data.AccelZ,
                Weight = data.Weight,
                Altitude = data.Altitude,   
                Pitch = data.Pitch,
                Bank = data.Bank,
            };

            packet.Checksum = packet.CalculateChecksum();
            return packet;
        }

        //public void AddHeader(string header)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddBody(string data)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddChecksum()
        //{
        //    throw new NotImplementedException();
        //}

        //public string GetPacket()
        //{
        //    throw new NotImplementedException();
        //}

        //private bool VerifyChecksum(Packet packet)
        //{
        //    throw new NotImplementedException();
        //}

        //private class Packet
        //{

        //    public Packet(string header, string packetBody, string checksum)
        //    {
        //        this.header = header;
        //        this.packetBody = packetBody;
        //        this.checksum = checksum;
        //    }

        //    public bool VerifyCheckSum()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
