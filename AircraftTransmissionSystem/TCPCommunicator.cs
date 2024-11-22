using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AircraftTransmissionSystem
{
    internal class TCPCommunicator
    {
        private TcpClient client;
        private NetworkStream fileReceive;
        private const int BUFFER_SIZE = 1024;

        private bool isConnected;


        //private IPAddress groundTerminalIP = "127.0.0.1";
        private string groundTerminalIP = "127.0.0.1"; // local
        private int groundTerminalPort = 8080;

        // things we need to send over to ground terminal
        private PacketBuilder packetBuilder;
        private FileReader fileReader;
        private readonly ILogger logger;

        public TCPCommunicator()
        {
            groundTerminalIP = "127.0.0.1";
            groundTerminalPort = 8080;
            PacketBuilder packetBuilder = new PacketBuilder();
            isConnected = false;
        }


        public async Task ConnectToGroundTerminal()
        {
            try
            {
                if (isConnected)
                {
                    logger.LogWarning("Already connected to ground terminal");
                    return;
                }

                client = new TcpClient();
                await client.ConnectAsync(groundTerminalIP, groundTerminalPort);
                fileReceive = client.GetStream();
                isConnected = true;
                logger.LogInformation($"Successfully connected to ground terminal at {groundTerminalIP}:{groundTerminalPort}");
            }
            catch (SocketException ex)
            {
                logger.LogError($"Socket error while connecting to ground terminal: {ex.Message}");
            }
        }
    }
}
