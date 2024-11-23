/*
 * File          : .cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : 
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
        static void Main(string[] args)
        {
            TelemetryParser telemetryParser = new TelemetryParser("default_format");
            TelemetryCollector telemetryCollector = new TelemetryCollector("127.0.0.1", 8080, telemetryParser);

            // start the listener here 
            Console.WriteLine("Listening for incoming aircraft transmissions...");
            telemetryCollector.Connect();

            // loop to receive the telemetry data and call the correct classes...
            while (true)
            {
                telemetryCollector.ReceiveData(); // continually listen for live updates 
            }
        }
    }
}
