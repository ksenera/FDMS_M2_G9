/*
 * File          : TelemetryCollector.cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : Processes telemetry data from the ATS, validates checksum, and parses using TelemetryParser. Also notifies observers with the parsed data.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;

namespace GroundStationTerminal
{
    public class TelemetryCollector
    {
        private readonly TelemetryParser telemetryParser;
        private readonly List<IObserver> observers;

        private const int BUFFER_SIZE = 1024;

        public TelemetryCollector(TelemetryParser telemetryParser)
        {
            this.telemetryParser = telemetryParser;
            this.observers = new List<IObserver>();
        }


        /*
         * FUNCTION : AddObserver()
         *
         * DESCRIPTION : Add an observer to the list of observers
         * 
         * PARAMETERS : IObserver observer
         *
         * RETURNS : void
         */
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }


        /*
         * FUNCTION : RemoveObserver()
         *
         * DESCRIPTION : Remove an observer from the list of observers
         * 
         * PARAMETERS : IObserver observer
         *
         * RETURNS : void
         */
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }


        /*
         * FUNCTION : RemoveObserver()
         *
         * DESCRIPTION : Notify all observers with the parsed data
         * 
         * PARAMETERS : ParsedData data
         *
         * RETURNS : void
         */
        public void NotifyObservers(ParsedData data)
        {
            foreach (var observer in observers)
            {
                try
                {
                    observer.Update(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error notifying observer: {ex.Message}");
                }
            }
        }


        /*
         * FUNCTION : ProcessClientStreamAsync()
         *
         * DESCRIPTION : Process the client stream, read data, parse and notify observers
         * 
         * PARAMETERS : NetworkStream stream
         *
         * RETURNS : void
         */
        // deleted the receive and parse packet because i changed the tcplistener to async listen 
        // new method to handle data async from network stream 
        public async Task ProcessClientStreamAsync(NetworkStream stream)
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            while(true)
            {

                try
                {                
                    // read from the network stream 
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        // conversion of received bytes to string 
                        string receivedTelemetry = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received: {receivedTelemetry}");
                        // parse the packet 
                        await ParseAndNotifyAsync(receivedTelemetry);
                    }
                    else
                    {
                        Console.WriteLine("Connection closed unexpectedly");
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Data wasn't received: {ex.Message}");
                    break;
                }
            }
        }


        /*
         * FUNCTION : ParseAndNotifyAsync()
         *
         * DESCRIPTION : parse and notify async method that validates the checksum and then notifies the observers 
         * 
         * PARAMETERS : string data
         *
         * RETURNS : none
         */
        private async Task ParseAndNotifyAsync(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine("Aircraft Transmission System did not transmit data");
                return;
            }

            // here validate the checksum and then if the sum is not valid drop the packet REQ-FN-020
            if (telemetryParser.ValidateChecksum(data))
            {
                ParsedData parsedData = telemetryParser.ParseData(data);
                Console.WriteLine($"Aircraft ID = {parsedData.AircraftID}, Timestamp = {parsedData.Timestamp}");
                NotifyObservers(parsedData);
            }
            else
            {
                Console.WriteLine("Invalid checksum, packet dropped");
            }
        }
    }
}

