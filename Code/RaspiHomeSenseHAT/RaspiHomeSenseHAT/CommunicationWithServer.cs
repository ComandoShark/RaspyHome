using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace RaspiHomeSenseHAT
{
    public class CommunicationWithServer
    {
        #region Fields
        #region Constants
        private const int PORT = 8080;
        private const string IPSERVER = "192.168.2.3";
        private const string FORMATSTRING = "IPRasp={0};Location={1};Component={2}";

        // Important need to be changed if it's another room!
        private const string LOCATION = "Salon";
        private const string COMPONENT = "Sensor";
        #endregion      

        #region Variables
        private ModelSenseHAT _mSenseHAT;

        private StreamSocket _socket = new StreamSocket();
        private StreamSocketListener _listener = new StreamSocketListener();
        private List<StreamSocket> _connections = new List<StreamSocket>();
        private bool _isConnected = false;
        private bool _connecting = false;
        #endregion
        #endregion

        #region Properties
        public ModelSenseHAT MSenseHAT
        {
            get
            {
                return _mSenseHAT;
            }

            set
            {
                _mSenseHAT = value;
            }
        }

        public StreamSocket Socket
        {
            get
            {
                return _socket;
            }

            set
            {
                _socket = value;
            }
        }

        public StreamSocketListener Listener
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

        public List<StreamSocket> Connections
        {
            get
            {
                return _connections;
            }

            set
            {
                _connections = value;
            }
        }

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }

            set
            {
                _isConnected = value;
            }
        }

        public bool Connecting
        {
            get
            {
                return _connecting;
            }

            set
            {
                _connecting = value;
            }
        }
        #endregion

        #region Constructors
        public CommunicationWithServer(ModelSenseHAT paramModel)
        {
            this.MSenseHAT = paramModel;

            Connect();
        }
        #endregion

        #region Methods
        #region Take information
        /*
        #region Client initialisation
        private async void InitilizeRaspberryToTheServer()
        {
            // remote device is 192.168.2.x
            Debug.WriteLine("About to connect");
            StreamSocket socket = new StreamSocket();
            StreamSocketListener listener = new StreamSocketListener();
            Debug.WriteLine("Made StreamSocket");

            CoreApplication.Properties.Add("serverAddress", IPSERVER);

            string hostName = GetHostName();
            IPAddress rpiIPAddress = GetIPAddress(hostName);

            listener.Control.KeepAlive = true;

            CoreApplication.Properties.Add("listner", listener);

            listener.ConnectionReceived += OnConnection;

            try
            {
                await listener.BindEndpointAsync(new Windows.Networking.HostName(hostName), PORT.ToString());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private async void OnConnection(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            DataReader reader = new DataReader(args.Socket.InputStream);
            try
            {
                while (true)
                {
                    // Read first 4 bytes (length of the subsequent string).
                    uint sizeFieldCount = await reader.LoadAsync(sizeof(uint));
                    if (sizeFieldCount != sizeof(uint))
                    {
                        // The underlying socket was closed before we were able to read the whole data.
                        return;
                    }

                    // Read the string.
                    uint stringLength = reader.ReadUInt32();
                    uint actualStringLength = await reader.LoadAsync(stringLength);
                    if (stringLength != actualStringLength)
                    {
                        // The underlying socket was closed before we were able to read the whole data.
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                if (Windows.Networking.Sockets.SocketError.GetStatus(e.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }
        #endregion

        #region Connection to server
        private async void ConnectSocket()
        {
            if (CoreApplication.Properties.ContainsKey("clientSocket"))
            {
                return;
            }

            // By default 'HostNameForConnect' is disabled and host name validation is not required. When enabling the
            // text box validating the host name is required since it was received from an untrusted source
            // (user input). The host name is validated by catching ArgumentExceptions thrown by the HostName
            // constructor for invalid input.
            HostName hostName;

            try
            {
                hostName = new HostName(IPSERVER);
            }
            catch (ArgumentException)
            {
                return;
            }

            StreamSocket socket = new StreamSocket();

            // If necessary, tweak the socket's control options before carrying out the connect operation.
            // Refer to the StreamSocketControl class' MSDN documentation for the full list of control options.
            socket.Control.KeepAlive = false;

            // Save the socket, so subsequent steps can use it.
            CoreApplication.Properties.Add("clientSocket", socket);

            try
            {
                // Connect to the server (by default, the listener we created in the previous step).
                await socket.ConnectAsync(hostName, PORT.ToString());

                // Mark the socket as connected. Set the value to null, as we care only about the fact that the 
                // property is set.
                CoreApplication.Properties.Add("connected", null);
            }
            catch (Exception exception)
            {
                // If this is an unknown status it means that the error is fatal and retry will likely fail.
                if (Windows.Networking.Sockets.SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }
        #endregion

        #region Send message
        private async void SendMessage()
        {
            // Need to be connected before
            if (!CoreApplication.Properties.ContainsKey("connected"))
            {
                return;
            }

            object outValue;
            StreamSocket socket;

            if (!CoreApplication.Properties.TryGetValue("clientSocket", out outValue))
            {
                return;
            }

            socket = (StreamSocket)outValue;

            // Create a DataWriter if we did not create one yet. Otherwise use one that is already cached.
            DataWriter writer;
            if (!CoreApplication.Properties.TryGetValue("clientDataWriter", out outValue))
            {
                writer = new DataWriter(socket.OutputStream);
                CoreApplication.Properties.Add("clientDataWriter", writer);
            }
            else
            {
                writer = (DataWriter)outValue;
            }

            // Write first the length of the string as UINT32 value followed up by the string. 
            // Writing data to the writer will just store data in memory.
            string stringToSend = SendValues();
            writer.WriteUInt32(writer.MeasureString(stringToSend));
            writer.WriteString(stringToSend);

            // Write the locally buffered data to the network.
            try
            {
                await writer.StoreAsync();
                this.MSenseHAT.Message = "\"" + stringToSend + "\" sent successfully.";
            }
            catch (Exception exception)
            {
                // If this is an unknown status it means that the error if fatal and retry will likely fail.
                if (Windows.Networking.Sockets.SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }
        #endregion
        */
        #endregion

        /// <summary>
        /// Connect the raspberry to the server
        /// </summary>
        private async void Connect()
        {
            try
            {
                this.Connecting = true;
                await this.Socket.ConnectAsync(new HostName(IPSERVER), PORT.ToString());
                SendForInitialize();
                this.Connecting = false;
                this.IsConnected = true;

                WaitForData(this.Socket);
            }
            catch (Exception)
            {
                this.Connecting = false;
                this.IsConnected = false;
            }
        }

        /// <summary>
        /// Listen the traffic on the port
        /// </summary>
        private async void Listen()
        {
            this.Listener.ConnectionReceived += listenerConnectionReceived;
            await this.Listener.BindServiceNameAsync(PORT.ToString());            
        }

        void listenerConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            this.Connections.Add(args.Socket);            

            WaitForData(args.Socket);
        }

        /// <summary>
        /// Send the message in input to output
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="message"></param>
        async private void SendMessage(StreamSocket socket, string message)
        {
            DataWriter dataWriter = new DataWriter(socket.OutputStream);
            var len = dataWriter.MeasureString(message); // Gets the UTF-8 string length.
            dataWriter.WriteInt32((int)len);
            dataWriter.WriteString(message);
            var ret = await dataWriter.StoreAsync();
            dataWriter.DetachStream();          
        }

        /// <summary>
        /// Send to initialize the raspberry to the server
        /// </summary>
        private void SendForInitialize()
        {
            SendMessage(this.Socket, string.Format(FORMATSTRING, GetHostName(),LOCATION,COMPONENT));
        }

        /// <summary>
        /// Send values in reply to the server
        /// </summary>
        private void ReplayValues()
        {
            SendMessage(this.Socket, this.MSenseHAT.SendValues());
        }

        /// <summary>
        /// Wait data readed if exist
        /// </summary>
        /// <param name="socket"></param>
        private async void WaitForData(StreamSocket socket)
        {
            DataReader dataReader = new DataReader(socket.InputStream);
            var stringHeader = await dataReader.LoadAsync(4);

            if (stringHeader == 0)            
                return;            

            WaitForData(socket);
        }

        /// <summary>
        /// Get the ip of the raspberry
        /// </summary>
        /// <returns>return a string like 192.168.1.2</returns>
        public string GetHostName()
        {
            List<string> IpAddress = new List<string>();
            var Hosts = Windows.Networking.Connectivity.NetworkInformation.GetHostNames().ToList();
            foreach (var Host in Hosts)
            {
                string IP = Host.DisplayName;
                IpAddress.Add(IP);
            }
            return IpAddress.Last();
        }
        #endregion
    }
}
