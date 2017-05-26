using System;
using System.Collections.Generic;
using System.IO;
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
        private TcpClient _clientRequest;
        private Dictionary<string, Dictionary<RaspberryClient, TcpClient>> _names;
        public readonly string _roomName;
        public readonly int _port;
        public readonly int _bufferSize = 2048;  // 2048 byte
        private bool _isRunning = false;

        private Queue<string> _messageQueue;
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

        public Dictionary<string, Dictionary<RaspberryClient, TcpClient>> ClientsNames
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

        public Queue<string> MessageQueue
        {
            get
            {
                return _messageQueue;
            }

            set
            {
                _messageQueue = value;
            }
        }

        public TcpClient ClientRequest
        {
            get
            {
                return _clientRequest;
            }

            set
            {
                _clientRequest = value;
            }
        }
        #endregion

        #region Constructor
        public Server()
        {
            this.RpiClients = new List<RaspberryClient>();
            this.Clients = new List<TcpClient>();
            this.ClientsNames = new Dictionary<string, Dictionary<RaspberryClient, TcpClient>>();
            this.MessageQueue = new Queue<string>();

            StartListening();
        }
        #endregion

        #region Methods
        private void StartListening()
        {
            // Some info
            Console.WriteLine("Starting the {0} TCP Server on port {1}.", HOST_NAME, DEFAULT_PORT);
            Console.WriteLine();
            
            IPAddress ipAddress = GetIPAdress();
            this.Listener = new TcpListener(GetIPAdress(), DEFAULT_PORT);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, DEFAULT_PORT);

            this.Listener.Start();
            this.IsRunning = true;

            while (this.IsRunning)
            {
                if (this.Listener.Pending())
                {
                    this.NewConnection();
                }

                this.CheckForDisconnects();
                this.CheckForNewMessages();

                // Wait before sending and clearing messages
                Thread.Sleep(200);
            }

            // Stop the server, and clean up any connected clients
            foreach (TcpClient v in this.Clients)
            {
                this.CleanupClient(v);
            }

            this.Listener.Stop();
        }


        private void NewConnection()
        {
            bool clientIsAccepted = false;
            TcpClient newClient = this.Listener.AcceptTcpClient();
            NetworkStream netStream = newClient.GetStream();

            // Modify the default buffer sizes
            newClient.SendBufferSize = _bufferSize;
            newClient.ReceiveBufferSize = _bufferSize;

            // Print some info
            EndPoint endPoint = newClient.Client.RemoteEndPoint;
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Client information");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("{0} Handling a new client from {1}.", Environment.NewLine, endPoint);
            Console.WriteLine();

            // Let them identify themselves
            byte[] msgBuffer = new byte[_bufferSize];
            int bytesRead = netStream.Read(msgBuffer, 0, msgBuffer.Length);

            if (bytesRead > 0)
            {
                string msg = Encoding.UTF8.GetString(msgBuffer, 0, bytesRead);

                string messageConnection = msg.Split('@').Last().Split(':').Last();

                try
                {
                    // Get the name of the client
                    string name = msg.Split('@')[1];

                    if ((name != string.Empty))
                    {
                        // Add the player
                        clientIsAccepted = true;
                        this.ClientsNames.Add(name, new Dictionary<RaspberryClient, TcpClient>() { { this.InitializeNewRaspberryClient(messageConnection), newClient } });
                        this.Clients.Add(newClient);
                        
                        Console.WriteLine(msg);
                        // Tell the current players we have a new player
                        this.MessageQueue.Enqueue(String.Format("{0} has joined the server.", name));
                        Console.WriteLine();
                        Console.WriteLine("----------------------------------------");
                    }
                }
                catch (Exception)
                {
                    // Wasn't either a viewer or messenger, clean up anyways.
                    Console.WriteLine("Client wasn't able to connect.", endPoint);
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine();
                    CleanupClient(newClient);
                }

                // Clear the client if he doesn't meet our requirements
                if (!clientIsAccepted)
                    newClient.Close();
            }
        }

        private void CleanupClient(TcpClient client)
        {
            // Clean the sent TcpClient
            client.GetStream().Close();
            client.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckForDisconnects()
        {
            // For every client
            foreach (TcpClient client in this.Clients.ToArray())
            {
                if (this.IsDisconnected(client))
                {
                    try
                    {
                        // Get info about the messenger
                        foreach (string name in this.ClientsNames.Keys)
                        {
                            if (this.ClientsNames[name].ContainsValue(client))
                            {
                                // Give information of the client disconnected 
                                Console.WriteLine();
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine("Client disconnect from the server");
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine("Client named {0} has left.", name);
                                Console.WriteLine();
                                foreach (var information in this.ClientsNames[name].Keys)
                                {
                                    if (this.ClientsNames[name].ContainsKey(information))
                                    {
                                        Console.WriteLine("Location : " + information.Location);
                                        Console.WriteLine("Ip client :" + information.IpClient);
                                        Console.Write("Component : ");
                                        int cnt = 0; ;
                                        foreach (var componnent in information.Components)
                                        {                                            
                                            if(cnt > 0)
                                                Console.Write("            ");
                                            cnt++;
                                            Console.WriteLine(componnent.ToString().Split('.').Last());
                                        }
                                        Console.WriteLine();
                                    }
                                }
                                Console.WriteLine("----------------------------------------");

                                this.Clients.Remove(client);  // Remove from list
                                this.ClientsNames.Remove(name);    // Remove taken name
                                this.CleanupClient(client);   // Cleanup
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    //this.MessageQueue.Enqueue(String.Format("{0}{1} has left the game", Environment.NewLine, name));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckForNewMessages()
        {
            foreach (TcpClient client in this.Clients)
            {
                int messageLength = client.Available;
                if (messageLength > 0)
                {
                    // Get the message if there is one
                    byte[] msgBuffer = new byte[messageLength];
                    client.GetStream().Read(msgBuffer, 0, msgBuffer.Length);

                    // Attach a name to it and shove it into the queue
                    string msg = String.Format("{0}", Encoding.UTF8.GetString(msgBuffer));
                    string subject = msg.Split('@').Last().Split(':').First();
                    string informationInReply = msg.Split('@').Last().Split(':').Last();
                    Console.WriteLine();

                    switch (msg.Split('@').Last().Split(':').First())
                    {
                        case "Send":
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Command send and reply");
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine(subject);
                            List<TcpClient> clientsToSend = this.CmdFilter.ApplyFilter(informationInReply, this.RpiClients, this.ClientsNames);
                            this.ClientRequest = client;

                            foreach (TcpClient clientToSend in clientsToSend)
                            {
                                this.SendMessages(clientToSend, informationInReply);
                            }

                            break;

                        case "Reply":
                            Console.WriteLine(subject);
                            this.SendMessages(this.ClientRequest, informationInReply);
                            Console.WriteLine("----------------------------------------");
                            break;
                    }

                    this.MessageQueue.Enqueue(msg);
                }
            }
        }

        /// <summary>
        /// Disconnect client frome the server when they leave
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private bool IsDisconnected(TcpClient client)
        {
            try
            {
                Socket clientSocket = client.Client;
                return clientSocket.Poll(10 * 1000, SelectMode.SelectRead) && (clientSocket.Available == 0);
            }
            catch (SocketException)
            {
                return true;
            }
        }

        /// <summary>
        /// Convert the message in bytes and write in the stream of the client
        /// </summary>
        /// <param name="message"> message to send </param>
        public void SendMessages(TcpClient clientToSend, string message)
        {
            // Encode the message
            if (this.MessageQueue.Count != 0)
            {
                byte[] msgBuffer = Encoding.UTF8.GetBytes(message);

                // Delai between each messages
                Thread.Sleep(200);
                clientToSend.GetStream().Write(msgBuffer, 0, msgBuffer.Length);
                Thread.Sleep(200);

                Console.WriteLine(message);
            }

            this.MessageQueue.Clear();
        }

        /// <summary>
        /// Get the ip of the raspberry
        /// </summary>
        /// <returns> return the IPv4 address 192.168.1.2 </returns>
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
        #endregion
    }
}
