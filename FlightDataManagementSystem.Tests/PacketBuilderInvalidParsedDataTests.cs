using AircraftTransmissionSystem;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataManagementSystem.Tests
{
    [TestClass]
    public class PacketBuilderInvalidParsedDataTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PacketBuilder_Invalid_ThrowsArgumentException()
        {

            var parsedData = new ParsedData
            {
                AircraftID = "C-FGAX",
                Timestamp = DateTime.Now,
                AccelX = -0.319754,
                AccelY = 0.859676,
                AccelZ = 2.105455,
                Weight = 1643.844116,
                Altitude = double.NaN, // invalid 
                Pitch = 0.033622,
                Bank = 0.022278,
                Checksum = 100 // random checksum 
            };

            parsedData.Validate();


            PacketBuilder builder = new PacketBuilder();
            builder.BuildPacket(parsedData, parsedData.AircraftID);

            // assertion is handled by the throw exception in ParsedData Validate() method


        }
    }
}
