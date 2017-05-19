using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspiHomeServer
{
    public class Server
    {
        #region Fields
        #region Constants
        const int DEFAULT_PORT = 8080;
        private const string HOST_NAME = "DESKTOP-UL17MK6";
        #endregion

        #region Variables
        private List<RaspberryClient> _rpiClients;
        private CommandFilter _cmdFilter = new CommandFilter();

        private TcpListener _listener;
        private List<TcpClient> _clients;
        private Dictionary<TcpClient, RaspberryClient> _names;
        public readonly string _roomName;
        public readonly int _port;
        public readonly int _bufferSize = 2048;  // 2048 byte
        private bool _isRunning = false;
        #endregion
        #endregion

        #region Properties
        public List<RaspberryClient> RpiClients
        {
            get
            {
                return _rpiClients;
            }

            set
            {
                _rpiClients = value;
            }
        }

        public CommandFilter CmdFilter
        {
            get
            {
                return _cmdFilter;
            }

            set
            {
                _cmdFilter = value;
            }
        }

        public TcpListener Listener
        {
            get
            {
                return _listener;
            }

            set
            {
                _listener = value;
            }
        }

        public List<TcpClient> Clients
        {
            get
            {
                return _clients;
            }

            set
            {
                _clients = value;
            }
        }

        public Dictionary<TcpClient, RaspberryClient> Names
        {
            get
            {
                return _names;
            }

            set
            {
                _names = value;
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }

            set
            {
                _isRunning = value;
            }
        }
        #endregion

        #region Constructor
        public Server()
        {
            this.RpiClients = new List<RaspberryClient>();
            this.Clients = new List<TcpClient>();
            this.Names = new Dictionary<TcpClient, RaspberryClient>();

            StartListening();
        }
        #endregion

        #region Methods
        private void StartListening()
        {
            IPAddress ipAddress = GetIPAdress();
            this.Listener = new TcpListener(GetIPAdress(), DEFAULT_PORT);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, DEFAULT_PORT);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            this.Listener.Start();
            this.IsRunning = false;

            while (this.IsRunning)
            {
                if (this.Listener.Pending())
                {
                    this.HandleNewConnection();
                }

                // Wait before sending and clearing messages
                Thread.Sleep(200);
                this.SendMessages("test");

                // Update only every 10ms
                Thread.Sleep(10);
            }

            // Stop the server, and clean up any connected clients
            foreach (TcpClient v in this.Clients)
            {
                this.CleanupClient(v);
            }

            this.Listener.Stop();
        }

        private void HandleNewConnection()
        {
            bool clientIsAccepted = false;
            TcpClient newClient = this.Listener.AcceptTcpClient();
            NetworkStream netStream = newClient.GetStream();

            // Modify the default buffer sizes
            newClient.SendBufferSize = _bufferSize;
            newClient.ReceiveBufferSize = _bufferSize;

            // Print some info
            EndPoint endPoint = newClient.Client.RemoteEndPoint;
            Console.WriteLine("{0}Handling a new client from {1}...", Environment.NewLine, endPoint);

            // Let them identify themselves
            byte[] msgBuffer = new byte[_bufferSize];
            int bytesRead = netStream.Read(msgBuffer, 0, msgBuffer.Length);
            //Console.WriteLine("Got {0} bytes.", bytesRead);
            if (bytesRead > 0)
            {
                string msg = Encoding.UTF8.GetString(msgBuffer, 0, bytesRead);

                // Check for the correct format
                if (msg.StartsWith("Client:"))
                {
                    string messageToConvert = msg.Substring(msg.IndexOf(':') + 1);

                    RaspberryClient actualClient = InitializeNewRaspberryClient(messageToConvert);

                    if (this.RpiClients != null)
                    {
                        //if ((name != string.Empty) && (!_names.ContainsValue(name)))
                        //{
                        // Add the player
                        clientIsAccepted = true;
                        this.Names.Add(newClient, actualClient);
                        this.Clients.Add(newClient);

                        // Show ip and name
                        Console.WriteLine("{0} is a player with the location {1}.", endPoint, actualClient.Location);

                        // Tell the current players we have a new player
                        //this.MessageQueue.Enqueue(String.Format("{0}{1} has joined the game.", Environment.NewLine, name));
                    }
                }
                else
                {
                    // Wasn't either a viewer or messenger, clean up anyways.
                    Console.WriteLine("Client wasn't able to connect (check message format?).", endPoint);
                    CleanupClient(newClient);
                }
            }

            // Clear the client if he doesn't meet our requirements
            if (!clientIsAccepted)
                newClient.Close();
        }

        private void CleanupClient(TcpClient client)
        {
            // Clean the sent TcpClient
            client.GetStream().Close();
            client.Close();
        }

        // Clears out the message queue (and sends it to all of the viewers
        public void SendMessages(string message)
        {
            // Encode the message
            byte[] msgBuffer = Encoding.UTF8.GetBytes(message);

            // Send the message to each client
            foreach (TcpClient client in this.Clients)
            {
                SocketAddress sa = client.Client.LocalEndPoint.Serialize();
                client.GetStream().Write(msgBuffer, 0, msgBuffer.Length);
            }

            Console.WriteLine(message);

        }

        /// <summary>
        /// Get the ip of the raspberry
        /// </summary>
        /// <returns>return the IPv4 address 192.168.1.2</returns>
        private IPAddress GetIPAdress()
        {
            IPAddress result = null;
            List<IPAddress> listAddress = Dns.GetHostAddresses(HOST_NAME).ToList();

            foreach (var ip in listAddress)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    result = ip;
            }
            return result;
        }

        #region Initialize new client
        /// <summary>
        /// Initialize a raspberry pi with the information read
        /// </summary>
        /// <param name="rpiInformation"></param>
        private RaspberryClient InitializeNewRaspberryClient(string rpiInformation)
        {
            // create an array of the actual string to get information
            string[] rpiInformations = rpiInformation.Split(';');
            string rpiIPv4 = "";
            string rpiLocation = "";
            string rpiComponent = "";
            
            foreach (var information in rpiInformations)
            {
                switch (information.Split('=').First())
                {
                    // Get the first value of the array
                    // IP of the actual Raspberry
                    case "IPRasp":
                        rpiIPv4 = information.Split('=').Last();
                        break;
                    // Get the second value of the array
                    // Location of the actual Raspberry (where to find the raspberry with the algorythme)
                    case "Location":
                        rpiLocation = information.Split('=').Last();
                        break;
                    case "Component":
                        rpiComponent = information.Split('=').Last();
                        break;
                }
            }
            Console.WriteLine(rpiIPv4);
            Console.WriteLine(rpiLocation);

            // Create a client without component
            RaspberryClient client = new RaspberryClient(rpiIPv4, DEFAULT_PORT, rpiLocation, new List<Component>());

            try
            {
                // Get all components of the Raspberry
                string[] components = rpiComponent.Split('=').Last().Split(',');

                // Get all class types of the project
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();

                // check all components
                foreach (var component in components)
                {
                    // check all class types
                    foreach (var type in types)
                    {
                        if (type.Name == component)
                        {
                            // create a new component with the type result
                            client.Components.Add((Component)Activator.CreateInstance(type));
                            break;
                        }
                    }

                    Console.WriteLine(component);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Add final client
            this.RpiClients.Add(client);
            Console.WriteLine();

            return client;
        }
        #endregion

        /// <summary>
        /// Filter the command receive from other client
        /// </summary>
        /// <param name="command"></param>
        /// <returns>List of client whose values been changed</returns>
        private List<RaspberryClient> FilterTheCommand(string command)
        {
            return this.CmdFilter.ApplyFilter(command, this.RpiClients);
        }

        /// <summary>
        /// Look for each client who ther value has changed and send to the client a refresh
        /// </summary>
        /// <param name="rpiClientWithNewValue"></param>
        private void UpdateRaspberryClientWithNewValue(List<RaspberryClient> rpiClientWithNewValue)
        {
            foreach (RaspberryClient client in rpiClientWithNewValue)
            {

            }
        }
        #endregion
    }
}
