using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataManagementSystem.Tests
{
    internal class TCPCommunicatorTest
    {

        [TestMethod]
        public void DuplicatePacketDetection()
        {

            ParsedData parsedData = new ParsedData
            {
                Timestamp = DateTime.Now,
                AccelX = 1.0,
                AccelY = 2.0,
                AccelZ = 3.0,
                Weight = 4.0,
                Altitude = 5.0,
                Pitch = 6.0,
                Bank = 7.0,
                Checksum = 8
            };
        }
    }
}
