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
        private CommunicationWithServer _comToServer;

        private SpeechRecognizer _recoEngine = null;
        private SpiDevice _mcp3202;



        //private Choices _commands = null;
        //private GrammarBuilder _grammarBuilder = null;
        //private Grammar _grammar = null;

        //private Choices _commandsAction = null;
        //private GrammarBuilder _grammarBuilderAction = null;
        //private Grammar _grammarAction = null;

        //private Choices _commandsObject = null;
        //private GrammarBuilder _grammarBuilderObject = null;
        //private Grammar _grammarObject = null;

        //private Choices _commandsActionNoObject = null;
        //private GrammarBuilder _grammarBuilderActionNoObject = null;
        //private Grammar _grammarActionNoObject = null;

        //private Choices _commandsLocation = null;
        //private GrammarBuilder _grammarBuilderLocation = null;
        //private Grammar _grammarLocation = null;

        //private Choices _commandsUselessWord = null;
        //private GrammarBuilder _grammarBuilderUselessWord = null;
        //private Grammar _grammarUselessWord = null;

        //private Choices _commandsSentence = null;
        //private GrammarBuilder _grammarBuilderSentence = null;
        //private Grammar _grammarSentence = null;

        //private DictationGrammar _commandDictationGrammar = null;
        //private DictationGrammar _defaultDictationGrammar = null;
        //private DictationGrammar _spellingDictationGrammar = null;

        private Commands _raspiCommands;

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

        public CommunicationWithServer ComToServer
        {
            get
            {
                return _comToServer;
            }

            set
            {
                _comToServer = value;
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
            this.ComToServer = new CommunicationWithServer(this);

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

            return result = System.Text.Encoding.UTF8.GetString(receiveBuffer);
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

            this.ComToServer.SendCommandToServer(brutCommand);                        
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
        /// <summary>
        /// (obsolete on UWP) Set the configuration of the speecher
        /// </summary>
        private void InitializeSpeechRecognizer()
        {

        }
        #endregion
    }
}
