/*
 * File          : Program.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Listens for aircraft transmissions from ATS solution
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundStationTerminal
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int port = 8080;


            TelemetryParser telemetryParser = new TelemetryParser();
            TelemetryCollector telemetryCollector = new TelemetryCollector(telemetryParser);            
            TCPListener listener = new TCPListener(port, telemetryCollector);

            // start the listener here 
            Console.WriteLine("Listening for incoming aircraft transmissions...");

            await listener.StartListeningAsync();

            Console.WriteLine("Listener stopped");
        }
    }
}
