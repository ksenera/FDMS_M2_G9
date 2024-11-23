﻿/*
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
        private string socketAddress;
        private int port;
        private bool isConnected;
        private ParsedData data;
        private TcpListener listener;
        private TcpClient client;
        private NetworkStream stream;
        private TelemetryParser telemetryParser;
        private List<IObserver> observers;

        public TelemetryCollector(string socketAddress, int port, TelemetryParser telemetryParser)
        {
            this.socketAddress = socketAddress;
            this.port = port;
            this.isConnected = false;
            this.telemetryParser = telemetryParser;
            this.data = new ParsedData();
            this.listener = new TcpListener(System.Net.IPAddress.Any, port);
            this.client = new TcpClient();
            this.stream = null;
            this.observers = new List<IObserver>();
        }

        public bool Connect()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(socketAddress);
                listener = new TcpListener(ipAddress, port);
                listener.Start();
                Console.WriteLine("Listening for telemetry connections...");
                client = listener.AcceptTcpClient();
                stream = client.GetStream();
                isConnected = true;
                Console.WriteLine("Connected to telemetry server.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to telemetry server: " + ex.Message);
                return false;
            }
        }

        public void Disconnect()
        {
            try 
            {
                // Disconnect from the telemetry server
                if (isConnected)
                {
                    stream.Close();
                    client.Close();
                    listener.Stop();
                    isConnected = false;
                    Console.WriteLine("Disconnected from telemetry socket. ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during disconnection: {ex.Message}");
            }
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
            if (isConnected && stream != null)
            {
                byte[] buffer = new byte[1024];

                while(isConnected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
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
                Disconnect();

            }
        }

        public void ParsePacket(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    Console.WriteLine("No data transmitted.");
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
                    Console.WriteLine("Invalid checksum.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing packet: {ex.Message}");
            }
        }
    }

}

