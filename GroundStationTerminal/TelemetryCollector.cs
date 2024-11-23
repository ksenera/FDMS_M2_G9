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
        private bool isConnected;
        private ParsedData data;
        private NetworkStream dataReceive;
        private TelemetryParser telemetryParser;
        private List<IObserver> observers;

        private const int BUFFER_SIZE = 1024;

        public TelemetryCollector(NetworkStream stream, TelemetryParser telemetryParser)
        {
            this.isConnected = false;
            this.dataReceive = stream;
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

        public void ReceiveData()
        {
            if (dataReceive != null)
            {
                byte[] buffer = new byte[BUFFER_SIZE];

                while(true)
                {
                    int bytesRead = dataReceive.Read(buffer, 0, buffer.Length);
                    // Got socket exception when i ran instances of both systems 
                    if (bytesRead > 0)
                    {
                        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("Received data successfully: " + dataReceived);
                        // parse packet in actual use case above writeline is for testing 
                        ParsePacket(dataReceived);
                    }
                }
            }
            else
            {
                Console.WriteLine("Connection closed by remote host.");

            }
        }

        public void ParsePacket(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    Console.WriteLine("Data not transmitted");
                    return;
                }

                if (telemetryParser.ValidateChecksum(data))
                {
                    ParsedData parsedData = telemetryParser.ParseData(data);
                    Console.WriteLine($"Parsed data: Aircraft ID = {parsedData.AircraftID}, Timestamp = {parsedData.Timestamp}");
                    NotifyObservers(parsedData);
                }
                else
                {
                    Console.WriteLine("Invalid checksum");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing packet: {ex.Message}");
            }
        }
    }

}

