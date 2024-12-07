using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SharedLibrary;
using AircraftTransmissionSystem;

namespace FlightDataManagementSystem.Tests
{
    [TestClass]
    public class PacketChecksumIntegrationTests
    {
        [TestMethod]
        public void PacketBuilder_ValidateChecksum_BothConsistent()
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
                Checksum = 718 
            };

            PacketBuilder builder = new PacketBuilder();
            TelemetryParser parser = new TelemetryParser();

            Packet packet = builder.BuildPacket(parsedData, parsedData.AircraftID);

            // raw data string 
            // Format: "AircraftID,SeqNum*Timestamp,AccelX,AccelY,AccelZ,Weight,Altitude,Pitch,Bank*Checksum"
            string packetRawData = $"C-FGAX,1*{parsedData.Timestamp:yyyy-MM-dd HH:mm:ss}," +
                                   $"{parsedData.AccelX},{parsedData.AccelY},{parsedData.AccelZ}," +
                                   $"{parsedData.Weight},{parsedData.Altitude},{parsedData.Pitch},{parsedData.Bank}*{parsedData.Checksum}";

            bool parserChecksumValid = parser.ValidateChecksum(packetRawData);

            Assert.IsTrue(packet.VerifyChecksum(), "Packet own Checksum verification must return true.");
            Assert.IsTrue(parserChecksumValid, "TelemetryParser correctly validate checksum as well.");
        }
    }
}
