using RaspiHomeSpeechNSynthetize.Speech;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Synthetize
{
    public class Synthetizer
    {
        #region Fields
        #region Constants
        private const string RASPI_NAME = "raspi";
        private const char SEPARATOR = ' ';
        #endregion

        #region Variable
        private Speecher _rhSpeech;

        private Commands _rhCommands;

        private Random _rnd;
        private SpVoice _voice;
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
        public Synthetizer(Speecher paramSpeecher)
        {
            this.RhSpeech = paramSpeecher;
            this.RhCommands = new Commands();       
            this._rnd = new Random();
            this._voice = new SpVoice();
            this._lstSentenceSplited = new List<string>();
        }
        #endregion

        #region Methods
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
                RaspiTalk(this.RhCommands.WhenCalling);
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
        /// Allow the Raspi, to let her talk with list
        /// </summary>
        /// <param name="repertory"></param>
        private void RaspiTalk(List<string> repertory)
        {
            string sentence = repertory[_rnd.Next(0, repertory.Count - 1)];
            this._voice.Speak(sentence, SpeechVoiceSpeakFlags.SVSFDefault);
            Console.WriteLine(sentence);
            Thread.Sleep(100);
        }

        /// <summary>
        /// Set information to sythetize
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        public List<string> SetProprelyInformations(string reply)
        {
            List<string> result = new List<string>();

            string[] informationSplited = reply.Split(';');
            if (informationSplited[0] != "")
            {
                foreach (string information in informationSplited)
                {
                    switch (information.Split('=').First())
                    {
                        case "TEMP":
                            result.Add(this.RhCommands.SpeecherRespondingEtatRequest[0] +
                            information.Split('=').Last().Replace('.', ',') +
                            this.RhCommands.SpeecherRespondingEtatRequest[1]);
                            break;
                        case "HUMI":
                            result.Add(this.RhCommands.SpeecherRespondingEtatRequest[4] +
                            information.Split('=').Last().Replace('.', ',') +
                            this.RhCommands.SpeecherRespondingEtatRequest[5]);
                            break;
                        case "PRES":
                            result.Add(this.RhCommands.SpeecherRespondingEtatRequest[6] +
                            information.Split('=').Last().Replace('.', ',') +
                            this.RhCommands.SpeecherRespondingEtatRequest[7]);
                            break;
                    }
                }
            }
            else
                result.Add("");
            return result;
        }

        /// <summary>
        /// Allow the Raspi, to let her talk with list of information
        /// </summary>
        /// <param name="repertory"></param>
        public void RaspiGiveInformation(List<string> informationsToGive)
        {
            foreach (string information in informationsToGive)
            {
                if (information != "")
                {
                    this._voice.Speak(RemoveDiacritics(information), SpeechVoiceSpeakFlags.SVSFDefault);
                    Console.WriteLine(information);
                    Thread.Sleep(100);
                }
            }
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
