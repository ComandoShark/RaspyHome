using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace RaspiHomePiFaceDigital2
{
    public class CommunicationWithServer
    {
        #region Fields
        #region Constants
        private const int PORT = 8080;
        private const string IPSERVER = "10.134.97.117";// "192.168.2.8";        
        private const string FORMATSTRING = "IPRasp={0};Location={1};Component={2}";
        private const string COMMUNICATIONSEPARATOR = "@";

        // Important need to be changed if it's another room!
        private const string LOCATION = "Salon";
        private const string COMPONENT = "Light";
        private const string RPINAME = "PiFace_" + LOCATION;
        #endregion      

        #region Variables
        private ModelPiFaceDigital2 _mPiFace;

        private StreamSocket _socket = new StreamSocket();
        private StreamSocketListener _listener = new StreamSocketListener();
        private List<StreamSocket> _connections = new List<StreamSocket>();
        private bool _isConnected = false;
        private bool _connecting = false;
        #endregion
        #endregion

        #region Properties
        public ModelPiFaceDigital2 MPiFace
        {
            get
            {
                return _mPiFace;
            }

            set
            {
                _mPiFace = value;
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
        public CommunicationWithServer(ModelPiFaceDigital2 paramModel)
        {
            this.MPiFace = paramModel;

            Connect();
        }
        #endregion

        #region Methods
        #region Methods
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
        private async void SendMessage(StreamSocket socket, string message)
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
            SendMessage(this.Socket, string.Format(COMMUNICATIONSEPARATOR + RPINAME + COMMUNICATIONSEPARATOR + "Connection:" + FORMATSTRING, GetHostName(), LOCATION, GetComponent()));
        }

        /// <summary>
        /// Wait data readed if exist
        /// </summary>
        /// <param name="socket"></param>
        private async void WaitForData(StreamSocket socket)
        {
            DataReader dataReader = new DataReader(socket.InputStream);
            dataReader.InputStreamOptions = InputStreamOptions.Partial;
            var msglenght = dataReader.UnconsumedBufferLength;
            uint stringBytes = msglenght;

            try
            {
                // Read modification in the stream       
                stringBytes = await dataReader.LoadAsync(512);

                // read message
                string msg = dataReader.ReadString(stringBytes);

                // Send in return if the value exist
                if (msg != "")
                {
                    this.MPiFace.SetValue(msg);
                }
            }
            catch (Exception e)
            {
                string output = e.Message;

                if (msglenght < 1)
                    return;
            }

            WaitForData(socket);
        }

        /// <summary>
        /// Get the ip of the raspberry
        /// </summary>
        /// <returns>return a string like 192.168.1.2</returns>
        private string GetHostName()
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


        /// <summary>
        /// Get component in the list of components
        /// </summary>
        /// <returns> return a usable string for the connection on the server</returns>
        private string GetComponent()
        {
            string result = "";
            int cnt = 0;
            foreach (var component in this.MPiFace.Components)
            {                
                // Get the name of the class
                result += component.ToString().Split('.').Last();
                cnt++;
                if (cnt < this.MPiFace.Components.Count)
                    result += ",";

            }

            return result;
        }
    }
    #endregion
    #endregion
}

