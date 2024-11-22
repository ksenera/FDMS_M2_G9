using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftTransmissionSystem
{
    internal class PacketBuilder
    {

        // header: aircrafttail#, packet sequence

        //private string Header { get; set; }
        //private string PacketBody { get; set; }
        //private int ChecksumNumber { get; set; }

        public void AddHeader(string header)
        {
            throw new NotImplementedException();
        }

        public void AddBody(string data)
        {
            throw new NotImplementedException();
        }

        public void AddChecksum()
        {
            throw new NotImplementedException();
        }

        public string GetPacket()
        {
            throw new NotImplementedException();
        }

        private bool VerifyChecksum(Packet packet)
        {
            throw new NotImplementedException();
        }

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
