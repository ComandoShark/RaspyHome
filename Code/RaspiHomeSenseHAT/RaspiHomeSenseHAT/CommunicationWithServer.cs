/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeSenseHAT is a program who use a 
 *   Sense HAT, it's an electronic card who can be 
 *   mesured value with sensor. This program use 
 *   the Sense HAT to mesure the temperature, the 
 *   humidity and the pressure. 
\*--------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace RaspiHomeSenseHAT
{
    public class CommunicationWithServer
    {
        #region Fields
        #region Constants
        // Default information to connect on the server
        private const int PORT = 54565;
        //// Need to be changed fo each configuration
        private const string IPSERVER = "10.134.97.117";// "192.168.2.8";     

        // String format for connection of the client      
        private const string FORMATSTRING = "IPRasp={0};Location={1};Component={2}";
        private const string COMMUNICATIONSEPARATOR = "@";

        // Important need to be changed if it's another room!
        private const string LOCATION = "Salon";
        private const string COMPONENT = "Sensor";
        private const string RPINAME = "SenseHAT_" + LOCATION;

        private const int MESSAGE_FULL_LENGHT = 512;
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
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        /// <param name="paramModel"></param>
        public CommunicationWithServer(ModelSenseHAT paramModel)
        {
            this.MSenseHAT = paramModel;

            Connect();
        }
        #endregion

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
        /// <param name="socket"> actual stream </param>
        /// <param name="message"> message to send </param>
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
        /// Wait data readed if exist
        /// </summary>
        /// <param name="socket"></param>
        private async void WaitForData(StreamSocket socket)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(200));
            DataReader dataReader = new DataReader(socket.InputStream);
            dataReader.InputStreamOptions = InputStreamOptions.Partial;
            var msglenght = dataReader.UnconsumedBufferLength;
            uint stringBytes = msglenght;

            try
            {        
                // Read modification in the stream       
                stringBytes = await dataReader.LoadAsync(MESSAGE_FULL_LENGHT);               
                
                // read message
                string msg = dataReader.ReadString(stringBytes);

                // Send in return if the value exist
                if (msg != "")
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(200));
                    ReplyValues();
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
        /// Send to initialize the raspberry to the server
        /// </summary>
        private void SendForInitialize()
        {
            // Message send: "@NAME@Connection:IPRASP=x.x.x.x;Location=y;Component=z,z"
            SendMessage(this.Socket, string.Format(COMMUNICATIONSEPARATOR + RPINAME + COMMUNICATIONSEPARATOR + "Connection:" + FORMATSTRING, GetHostName(), LOCATION, COMPONENT));
        }

        /// <summary>
        /// Send values in reply to the server
        /// </summary>
        public void ReplyValues()
        {
            // Message receive : "@Reply:TEMP=x;HUMI=y;PRES=z"
            SendMessage(this.Socket, COMMUNICATIONSEPARATOR + "Reply:" + this.MSenseHAT.SendValues());
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
