using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Windows.Media.SpeechSynthesis;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;

namespace RaspiHomeSpeechNSynthetize
{
    public class Synthetizer
    {
        #region Fields
        #region Constants
        private const string RASPI_NAME = "raspi";
        private const char SEPARATOR = ' ';
        // Change value when new update ("en" to "fr")
        private const string LANGUAGE_SELECTION = "en";
        private const double TIME_TO_WAIT = 3.0;
        #endregion

        #region Variable
        private Speecher _rhSpeech;

        private Commands _rhCommands;

        private Random _rnd;
        private SpeechSynthesizer _voice;
        private string _commandReceiveStr = "";
        private string _commandToSend = "";

        private bool _isCalled = false;
        private bool _isCompleted = false;
        private List<string> _lstSentenceSplited;
        #endregion
        #endregion

        #region Properties    
        public Speecher RhSpeech
        {
            get
            {
                return _rhSpeech;
            }

            set
            {
                _rhSpeech = value;
            }
        }

        public string CommandReceiveStr
        {
            get
            {
                return _commandReceiveStr;
            }

            set
            {
                _commandReceiveStr = value;
            }
        }

        public bool IsCalled
        {
            get
            {
                return _isCalled;
            }

            set
            {
                _isCalled = value;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }

            set
            {
                _isCompleted = value;
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
        public Synthetizer(Speecher paramSpeecher)
        {
            this.RhSpeech = paramSpeecher;
            this.RhCommands = new Commands();
            this._rnd = new Random();
            this._voice = new SpeechSynthesizer();
            this._lstSentenceSplited = new List<string>();
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Raspberry processus, wait calling to start communication with server
        /// </summary>
        private void RaspiProcessus()
        {
            if (this.IsCalled)
            {
                SendCommand();
            }
        }

        /// <summary>
        /// Send to the synthetize method the order to reply
        /// </summary>
        /// <param name="name"></param>
        public void RaspiCalled(string name)
        {
            string raspiName = RemoveDiacritics(name).ToLower();

            if (raspiName == RASPI_NAME.ToLower())
            {
                RaspiCalled(this.RhCommands.WhenCalling);
                this.RhSpeech.IsRaspiCalled = true;
                this.IsCalled = true;
            }
        }

        /// <summary>
        /// Send the instruction for the Raspberry
        /// </summary>
        /// <returns></returns>
        public string SendCommand()
        {
            this.RhSpeech.IsRaspiCalled = false;
            this.IsCalled = false;

            this.IsCompleted = true;
            return this._commandToSend;
        }

        /// <summary>
        /// Processus to choose the sentence to say
        /// </summary>
        /// <param name="repertory"> List of sentence to say </param>
        private async void RaspiCalled(List<string> repertory)
        {
            string messageToSay = repertory[_rnd.Next(0, repertory.Count - 1)];

            this.RaspiTalk(messageToSay);

            // Work like Thread.Sleep(TIME_TO_WAIT)
            await Task.Delay(TimeSpan.FromSeconds(TIME_TO_WAIT));
        }

        /// <summary>
        /// Allow the raspi to let her talk
        /// </summary>
        /// <param name="messageToSay"> sentence to say </param>
        private async void RaspiTalk(string messageToSay)
        {
            // Get the output element (audio jack)
            MediaElement mediaElement = new MediaElement();
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Set the default language
            foreach (VoiceInformation vInfo in SpeechSynthesizer.AllVoices)
            {
                if (vInfo.Language.Contains(LANGUAGE_SELECTION))
                {
                    synth.Voice = vInfo;
                    break;
                }
                else
                    synth.Voice = vInfo;
            }

            SpeechSynthesisStream synthStream = await synth.SynthesizeTextToStreamAsync(messageToSay);
     
            mediaElement.SetSource(synthStream, synthStream.ContentType);
            // 0 = min / 1 = max
            mediaElement.Volume = 1;
            mediaElement.Play();
        }

        /// <summary>
        /// Called when there is any error
        /// </summary>
        public void WrongCommand()
        {
            // Reach the error resquest sentences to say
            this.RaspiCalled(this.RhCommands.SpeecherRespondingRequestError);
        }

        /// <summary>
        /// Allow the Raspi, to let her talk with list of information
        /// </summary>
        /// <param name="informationsToGive"></param>
        public async void RaspiSayInformation(List<string> informationsToGive)
        {
            foreach (string informationToSay in informationsToGive)
            {
                if (informationToSay != "")
                {
                    this.RaspiTalk(informationToSay);
                    // Work like Thread.Sleep(TIME_TO_WAIT)
                    await Task.Delay(TimeSpan.FromSeconds(TIME_TO_WAIT));
                }
            }
        }

        /// <summary>
        /// Set information to sythetize
        /// </summary>
        public List<string> SetProprelyInformations(string messageReply, string messageCommand)
        {
            List<string> result = new List<string>();

            bool temp = false, humi = false, pres = false;

            if (messageCommand.Contains("température"))
                temp = true;
            if (messageCommand.Contains("humidité"))
                humi = true;
            if (messageCommand.Contains("pression"))
                pres = true;
            if (messageCommand.Contains("état"))
            {
                temp = true;
                humi = true;
                pres = true;
            }

            string[] informationSplited = messageReply.Split(';');
            if (informationSplited[0] != "")
            {
                foreach (string information in informationSplited)
                {
                    switch (information.Split('=').First())
                    {
                        case "TEMP":
                            if (temp)
                            {
                                result.Add(this.RhCommands.SpeecherRespondingEtatRequest[0] + 
                                    information.Split('=').Last() + 
                                    this.RhCommands.SpeecherRespondingEtatRequest[1]);
                            }
                            break;
                        case "HUMI":
                            if (humi)
                            {
                                result.Add(this.RhCommands.SpeecherRespondingEtatRequest[4] + 
                                    information.Split('=').Last() + 
                                    this.RhCommands.SpeecherRespondingEtatRequest[5]);
                            }
                            break;
                        case "PRES":
                            if (pres)
                            {
                                result.Add(this.RhCommands.SpeecherRespondingEtatRequest[6] + 
                                    information.Split('=').Last() + 
                                    this.RhCommands.SpeecherRespondingEtatRequest[7]);
                            }
                            break;
                    }
                }
            }
            else
                result.Add("");
            return result;
        }    

        /// <summary>
        /// Stack Overflow solution to delete accents in strings
        /// http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string RemoveDiacritics(string str)
        {
            var normalizedString = str.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        #endregion
    }
}
