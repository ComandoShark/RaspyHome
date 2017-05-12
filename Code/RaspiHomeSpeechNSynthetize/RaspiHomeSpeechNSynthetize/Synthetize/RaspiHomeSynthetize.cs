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
    public class RaspiHomeSynthetize
    {
        #region Fields
        #region Constants
        private const string RASPI_NAME = "raspi";
        private const char SEPARATOR = ' ';
        #endregion

        #region Variable
        private RaspiHomeSpeech _rhSpeech;

        private RaspiHomeCommands _rhCommands;

        private List<ActionUse> _raspiAction;
        private List<ActionNoObjectUse> _raspiActionNoObject;
        private List<ObjectUse> _raspiObject;
        private List<LocationUse> _raspiLocation;

        private List<CommandTheRaspi> _raspiCommand;

        //private CommandTheRaspi _command;

        private Random _rnd;
        private SpVoice _voice;
        private string _commandReceiveStr = "";
        private string _commandToSend = "";

        private bool _isCalled = false;
        private bool _isCompleted = false;
        List<string> _lstSentenceSplited;
        #endregion
        #endregion

        #region Properties    
        public RaspiHomeSpeech RhSpeech
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
        #endregion

        #region Constructor
        public RaspiHomeSynthetize()
        {
            this.RhSpeech = new RaspiHomeSpeech(this);

            this._raspiAction = new List<ActionUse>();
            this._raspiActionNoObject = new List<ActionNoObjectUse>();
            this._raspiObject = new List<ObjectUse>();
            this._raspiLocation = new List<LocationUse>();
            this._raspiCommand = new List<CommandTheRaspi>();

            this._rhCommands = new RaspiHomeCommands();

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
                this._lstSentenceSplited = CommandReceiveStr.Split(SEPARATOR).ToList();
                foreach (string str in this._lstSentenceSplited)
                {
                    if (this._rhCommands.RaspiHomeCommandUselessConnecter.Contains(str))
                    {
                        continue;
                    }

                    if (this._rhCommands.RaspiHomeActionWithoutObjectKnown.Contains(str))
                    {
                        this._commandToSend += (str + SEPARATOR);
                        continue;
                    }
                    else
                    {
                        if (this._rhCommands.RaspiHomeActionKnown.Contains(str))
                        {
                            this._commandToSend += (str + SEPARATOR);
                            continue;
                        }

                        if (this._rhCommands.RaspiHomeObjectKnown.Contains(str))
                        {
                            this._commandToSend += (str + SEPARATOR);
                            continue;
                        }
                    }

                    if (this._rhCommands.RaspiHomeLocationKnown.Contains(str))
                    {
                        this._commandToSend += (str + SEPARATOR);
                        continue;
                    }
                }
                
                SendCommand();
            }
        }

        public void RaspiCalled(string name)
        {
            this.RhSpeech.DisableSpeech();     
                                         
            string raspiName = RemoveDiacritics(name).ToUpper();
            if (raspiName == RASPI_NAME.ToUpper())
            {
                RaspiTalk(this._rhCommands.WhenCalling);
                this.RhSpeech.IsRaspiCalled = true;
                this.IsCalled = true;
            }

            this.RhSpeech.EnableSpeech();
        }

        public void RaspiCommand(string command)
        {
            this.RhSpeech.DisableSpeech();

            this._voice.Speak(command, SpeechVoiceSpeakFlags.SVSFDefault);
            Thread.Sleep(100);

            this.CommandReceiveStr = RemoveDiacritics(command);
            RaspiProcessus();

            this.RhSpeech.EnableSpeech();
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
        /// Allow the Raspi, to let her talk
        /// </summary>
        /// <param name="repertory"></param>
        private void RaspiTalk(List<string> repertory)
        {
            string str = repertory[_rnd.Next(0, repertory.Count - 1)];
            this._voice.Speak(str, SpeechVoiceSpeakFlags.SVSFDefault);
            Console.WriteLine(str);            
            Thread.Sleep(100);
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
