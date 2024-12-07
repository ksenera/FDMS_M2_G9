using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AircraftTransmissionSystem;
using SharedLibrary;

namespace FlightDataManagementSystem.Tests
{
    [TestClass]
    public class PacketBuilderTests
    {
        [TestMethod]
        public void PacketBuilder_Valid_ProducesValidPacket()
        {
            var parsedData = new ParsedData
            {
                AircraftID = "C-FGAX",
                Timestamp = new DateTime(2018, 8, 7, 19, 34, 3),
                AccelX = -0.319754,
                AccelY = 0.859676,
                AccelZ = 2.105455,
                Weight = 1643.844116,
                Altitude = 2154.670410,
                Pitch = 0.033622,
                Bank = 0.022278,
                // Checksum should be a truncated integer. Calculation EXPECTED:
                // checksum = round((Altitude + Pitch + Bank)/3)
                // = (2154.670410 + 0.033622 + 0.022278)/3 ≈ (2154.72631)/3 = 718.2421 → truncation = 718
                Checksum = 718
            };

            PacketBuilder builder = new PacketBuilder();

            Packet packet = builder.BuildPacket(parsedData, parsedData.AircraftID);

            Assert.IsNotNull(packet, "Packet should not be null");
            Assert.IsTrue(packet.VerifyChecksum(), "Packet checksum must be valid.");
        }
    }
}