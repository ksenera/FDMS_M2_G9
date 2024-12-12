using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataManagementSystem.Tests
{
    [TestClass]
    public class PacketTransmissionTests
    {
        [TestMethod]
        public async Task ProcessClientStreamAsync_ValidPacket_ParsedDataReceived()
        {
            TelemetryParser parser = new TelemetryParser();
            TelemetryCollector collector = new TelemetryCollector(parser);

            // Mock observer
            TestObserver observer = new TestObserver();
            collector.AddObserver(observer);

            // Valid packet line
            string packetLine = "C-FGAX,1*2018-08-07 19:34:03,-0.319754,0.859676,2.105455,1643.844116,2154.670410,0.033622,0.022278*718\n";
            byte[] packetBytes = Encoding.UTF8.GetBytes(packetLine);

            using (var memStream = new MemoryStream(packetBytes))
            {
                await collector.ProcessClientStreamAsync(memStream);

                Assert.AreEqual(1, observer.ReceivedData.Count, "Observer should see one parsed data object.");
                var data = observer.ReceivedData[0];
                Assert.AreEqual("C-FGAX", data.AircraftID);
                Assert.AreEqual(new DateTime(2018, 8, 7, 19, 34, 3), data.Timestamp);
            }
        }

        private class TestObserver : IObserver
        {
            public List<ParsedData> ReceivedData { get; } = new List<ParsedData>();
            public void UpdateGUI(ParsedData data)
            {
                ReceivedData.Add(data);
            }
        }
    }
}
