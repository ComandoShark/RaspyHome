using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.SpeechRecognition;
using Windows.Devices.Spi;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace RaspiHomeSpeechNSynthetize
{
    public class Speecher
    {
        #region Fields
        #region Constants
        #endregion

        #region Variable
        private Synthetizer _rhSynt;
        private Commands _rhCommands;
        private CommunicationWithServer _comWithServer;

        private SpeechRecognizer _recoEngine = null;
        private SpiDevice _mcp3202;

        private bool _isRaspiCalled = false;
        #endregion
        #endregion

        #region Properties
        public Synthetizer RhSynt
        {
            get
            {
                return _rhSynt;
            }

            set
            {
                _rhSynt = value;
            }
        }

        public bool IsRaspiCalled
        {
            get
            {
                return _isRaspiCalled;
            }

            set
            {
                _isRaspiCalled = value;
            }
        }

        public CommunicationWithServer ComWithServer
        {
            get
            {
                return _comWithServer;
            }

            set
            {
                _comWithServer = value;
            }
        }

        public Commands RhCommands
        {
            get
            {
                return _rhCommands;
            }

            set
            {
                _rhCommands = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor: Initialize
        /// </summary>
        public Speecher()
        {
            // Initialize the synthetizer
            this.RhSynt = new Synthetizer(this);
            this.RhCommands = new Commands();

            // Initialize the communication with the server
            this.ComWithServer = new CommunicationWithServer(this);

            // Create the speech recognition object
            this._recoEngine = new SpeechRecognizer();

            // Initialize the recognition
            InitializeSpeechRecognizer();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialze the Spi communication
        /// </summary>
        private async void InitializeSpi()
        {
            //using SPI0 on the Pi
            var spiSettings = new SpiConnectionSettings(0);//for spi bus index 0
            spiSettings.ClockFrequency = 3600000; //3.6 MHz 
            spiSettings.Mode = SpiMode.Mode0;

            string spiQuery = SpiDevice.GetDeviceSelector("SPI0");

            var deviceInfo = await DeviceInformation.FindAllAsync(spiQuery);
            if (deviceInfo != null && deviceInfo.Count > 0)
            {
                _mcp3202 = await SpiDevice.FromIdAsync(deviceInfo[0].Id, spiSettings);
            }
        }

        /// <summary>
        /// Read digital input with SPI
        /// </summary>
        /// <returns></returns>
        private string ReadSpiData()
        {
            byte[] transmitBuffer = new byte[3] { 0x01, 0x80, 0 };
            byte[] receiveBuffer = new byte[3];

            string result = "";

            _mcp3202.TransferFullDuplex(transmitBuffer, receiveBuffer);

            return result = Encoding.UTF8.GetString(receiveBuffer);
        }

        /// <summary>
        /// (obsolete on UWP) Set the configuration of the speecher
        /// </summary>
        private void InitializeSpeechRecognizer()
        {

        }

        /// <summary>
        /// (obsolete on UWP) Enable the speech, used when raspi is not talking
        /// </summary>
        public void EnableSpeech()
        {
            //this._recoEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        /// <summary>
        /// (obsolete on UWP) Disable the speech, used when raspi is talking 
        /// </summary>
        public void DisableSpeech()
        {
           // this._recoEngine.RecognizeAsyncStop();
        }

        /// <summary>
        /// Used to call raspi
        /// </summary>
        /// <param name="nameMentioned"></param>
        private void CallRaspi(string nameMentioned)
        {
            DisableSpeech();

            this.RhSynt.RaspiCalled(nameMentioned);

            EnableSpeech();
        }

        /// <summary>
        /// Used to send the command after raspi called
        /// </summary>
        /// <param name="brutCommand"></param>
        public void SendBrutCommand(string brutCommand)
        {
            DisableSpeech();

            this.ComWithServer.SendCommandToServer(brutCommand);                        
        }

        /// <summary>
        /// Reply message to the sender (reply message error if command unknow)
        /// </summary>
        /// <param name="messageReply"></param>
        /// <param name="messageCommand"></param>
        public void ReplyForSynthetize(string messageReply, string messageCommand)
        {
            if (messageReply == "ERROR_MESSAGE")
            {
                this.RhSynt.WrongCommand();
            }
            else
            {
                this.RhSynt.RaspiSayInformation(this.RhSynt.SetProprelyInformations(messageReply, messageCommand));             
            }
            this.IsRaspiCalled = false;

            EnableSpeech();
        }
        #endregion
    }
}
