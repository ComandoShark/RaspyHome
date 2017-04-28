using SpeechLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspiHomeTextToSpeech
{
    public class RaspiSpeecher
    {
        #region Fields
        #region Constants
        private const string RASPI_NAME = "raspi";
        #endregion

        #region Variables
        private List<string> _whenCalling = new List<string>()
            {
                "Oui, que puis-je faire pour vous?","Comment puis-je vous servir?","Je suis toute ouïe"
            };

        private List<string> _whenCallingError = new List<string>()
            {
                "Je ne m'appel pas comme ça!","Je crois que vous vous êtes trompé de personne!"
            };

        private List<string> _speecherRespondingRequest = new List<string>()
            {
                "Tout de suite","Je m'y mets de ce pas!","Ne quittez pas!"
            };

        private List<string> _speecherRespondingRequestError = new List<string>()
            {
                "Je n'ai pas compris ce que vous avez demandé"
            };

        private List<string> _raspiHomeLocationKnown = new List<string>()
            {
                "maison","chambre","salon","cuisine","pièce"
            };

        private List<string> _raspiHomeObjectKnown = new List<string>()
            {
                "lumière","store","lumières","stores",
            };

        private List<string> _raspiHomeActionKnown = new List<string>()
            {
               "allumer","allume","éteindre","éteins","monter","monte","déscendre","déscend"
            };

        private List<string> _raspiHomeActionWithoutObjectKnown = new List<string>()
            {
               "température","humidité","pression","état"
            };

        private List<string> _speecherRespondingEtatRequest = new List<string>()
            {
                "Il fait","degrés Celsius","degrés Farad","degrés Kelvin","Le taux d'humidité est de","pourcent","La pression est actuellement de","bars"
            };

        private List<string> _raspiHomeCommandUselessConnecter = new List<string>()
            {
                "le","la","les","un","une","des","mon","ma","mes","son","sa","ses","ce","cet","cette","ces","cel","celle","du","de"
            };

        private Random _rnd;
        private SpVoice _voice;
        private string _commandReceiveStr = "";
        private string _commandToSend = "";

        private bool _isCalled = false;
        List<string> _lstSentenceSplited;


        #endregion
        #endregion

        #region Properties
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
        #endregion

        #region Constant
        #endregion

        #region Constructor
        public RaspiSpeecher()
        {
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
                this._lstSentenceSplited = CommandReceiveStr.Split(' ').ToList();
                foreach (string str in this._lstSentenceSplited)
                {
                    if (this._raspiHomeCommandUselessConnecter.Contains(str))
                    {                        
                        continue;
                    }
                    
                    if (this._raspiHomeActionWithoutObjectKnown.Contains(str))
                    {
                        this._commandToSend += str;
                        continue;
                    }
                    else
                    {
                        if (this._raspiHomeActionKnown.Contains(str))
                        {
                            this._commandToSend += str;
                            continue;
                        }

                        if (this._raspiHomeObjectKnown.Contains(str))
                        {
                            this._commandToSend += str;
                            continue;
                        }
                    }

                    if (this._raspiHomeLocationKnown.Contains(str))
                    {
                        this._commandToSend += str;
                        continue;
                    }
                }

            }
        }

        /// <summary>
        /// Start processus of the TextToSpeech when Raspi is called
        /// </summary>
        public void CalleRaspi(string name)
        {
            if (name.ToUpper() == RASPI_NAME.ToUpper())
            {
                //    RaspiTalk(this._whenCallingError);
                //else
                //{
                RaspiTalk(this._whenCalling);
                this.IsCalled = true;
            }
        }

        public void ReceiveCommand(string str)
        {
            this.CommandReceiveStr = str;
            RaspiProcessus();
        }

        public string SendCommand()
        {
            return this._commandToSend;
        }

        private void RaspiTalk(List<string> repertory)
        {
            this._voice.Speak(repertory[_rnd.Next(0, repertory.Count - 1)], SpeechVoiceSpeakFlags.SVSFDefault);
            Thread.Sleep(100);
        }

        #endregion      
    }
}
