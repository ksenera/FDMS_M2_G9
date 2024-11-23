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


        // Add an observer to the list of observers
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        // Remove an observer from the list of observers
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        // Notify all observers with the parsed data
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


        // deleted the receive and parse packet because i changed the tcplistener to async listen 
        // new method to handle data async from network stream 

        public async Task ProcessClientStreamAsync(NetworkStream stream)
        {

        }


        // parse and notify async method that validates the checksum and then notifies the observers 
    }
}

