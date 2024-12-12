using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataManagementSystem.Tests
{
    [TestClass]
    public class InvalidChecksumTests
    {
        [TestMethod]
        public async Task ProcessClientStreamAsync_InvalidChecksum_PacketDropped()
        {
            TelemetryParser parser = new TelemetryParser();
            TelemetryCollector collector = new TelemetryCollector(parser);
            TestObserver observer = new TestObserver();
            collector.AddObserver(observer);

            // off-nominal condition introduced incorrect checksum:
            // 999 is used to force failure like the other tests the correct checksum is 718 
            string corruptedLine = "C-FGAX,1*2018-08-07 19:34:03,-0.319754,0.859676,2.105455,1643.844116,2154.670410,0.033622,0.022278*999\n";
            byte[] packetBytes = Encoding.UTF8.GetBytes(corruptedLine);

            using (var memStream = new MemoryStream(packetBytes))
            {
                // Act
                await collector.ProcessClientStreamAsync(memStream);

                // Assert
                // Since the checksum is invalid, no parsed data should be received.
                Assert.AreEqual(0, observer.ReceivedData.Count, "No parsed data should be observed for invalid checksum.");
            }
        }

        private class TestObserver : IObserver
        {
            public List<ParsedData> ReceivedData { get; } = new List<ParsedData>();
            public void UpdateGUI(ParsedData data) => ReceivedData.Add(data);
        }
    }
}