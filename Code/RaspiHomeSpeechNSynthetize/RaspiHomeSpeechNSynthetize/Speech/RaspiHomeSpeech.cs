using RaspiHomeSpeechNSynthetize.Synthetize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Speech
{
    public class RaspiHomeSpeech
    {
        #region Fields
        #region Constants
        #endregion

        #region Variable
        private RaspiHomeSynthetize _rhSynt;

        private SpeechRecognitionEngine _recoEngine = null;

        private Choices _commands = null;
        private GrammarBuilder _grammarBuilder = null;
        private Grammar _grammar = null;

        private Choices _commandsAction = null;
        private GrammarBuilder _grammarBuilderAction = null;
        private Grammar _grammarAction = null;

        private Choices _commandsObject = null;
        private GrammarBuilder _grammarBuilderObject = null;
        private Grammar _grammarObject = null;

        private Choices _commandsActionNoObject = null;
        private GrammarBuilder _grammarBuilderActionNoObject = null;
        private Grammar _grammarActionNoObject = null;

        private Choices _commandsLocation = null;
        private GrammarBuilder _grammarBuilderLocation = null;
        private Grammar _grammarLocation = null;

        private Choices _commandsUselessWord = null;
        private GrammarBuilder _grammarBuilderUselessWord = null;
        private Grammar _grammarUselessWord = null;

        private Choices _commandsSentence = null;
        private GrammarBuilder _grammarBuilderSentence = null;
        private Grammar _grammarSentence = null;

        private DictationGrammar _commandDictationGrammar = null;
        private DictationGrammar _defaultDictationGrammar = null;
        private DictationGrammar _spellingDictationGrammar = null;

        private RaspiHomeCommands _raspiCommands;

        private bool _isRaspiCalled = false;
        #endregion
        #endregion

        #region Properties
        public RaspiHomeSynthetize RhSynt
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
        #endregion

        #region Constructor
        public RaspiHomeSpeech(RaspiHomeSynthetize paramSynt)
        {
            this.RhSynt = paramSynt;

            this._recoEngine = new SpeechRecognitionEngine();
            SetSpeech();
        }
        #endregion

        #region Methods
        public void EnableSpeech()
        {
            this._recoEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void DisableSpeech()
        {
            this._recoEngine.RecognizeAsyncStop();
        }

        private void CallRaspi(string name)
        {
            this.RhSynt.RaspiCalled(name);
        }

        private void SendBrutCommand(string brutCommand)
        {
            this.RhSynt.RaspiCommand(brutCommand);
        }

        private void SetSpeech()
        {
            this._raspiCommands = new RaspiHomeCommands();

            // Call raspi grammar            
            this._grammarBuilder = new GrammarBuilder();
            //this._grammarBuilder.Culture = new System.Globalization.CultureInfo("fr-FR");
            this._grammarBuilder.Append("raspi");
            //this._grammarBuilder.Append(new Choices(this._raspiCommands.RaspiHomeActionKnown.ToArray()));
            //this._grammarBuilder.Append(new Choices(this._raspiCommands.RaspiHomeActionWithoutObjectKnown.ToArray()));
            //this._grammarBuilder.Append(new Choices(this._raspiCommands.RaspiHomeObjectKnown.ToArray()));
            //this._grammarBuilder.Append(new Choices(this._raspiCommands.RaspiHomeLocationKnown.ToArray()));
            //this._grammarBuilder.Append(new Choices(this._raspiCommands.RaspiHomeCommandUselessConnecter.ToArray()));
            this._grammar = new Grammar(this._grammarBuilder);
            
            //Action grammar
            this._commandsAction = new Choices();
            this._commandsAction.Add(this._raspiCommands.RaspiHomeActionKnown.ToArray());
            this._grammarBuilderAction = new GrammarBuilder();
            this._grammarBuilderAction.Culture = new System.Globalization.CultureInfo("fr-FR");
            this._grammarBuilderAction.Append(this._commandsAction);
            this._grammarAction = new Grammar(this._grammarBuilderAction);

            // Action without object grammar
            this._commandsActionNoObject = new Choices();
            this._commandsActionNoObject.Add(this._raspiCommands.RaspiHomeActionWithoutObjectKnown.ToArray());
            this._grammarBuilderActionNoObject = new GrammarBuilder();
            this._grammarBuilderActionNoObject.Culture = new System.Globalization.CultureInfo("fr-FR");
            this._grammarBuilderActionNoObject.Append(this._commandsActionNoObject);
            this._grammarActionNoObject = new Grammar(this._grammarBuilderActionNoObject);

            // Object grammar
            this._commandsObject = new Choices();
            this._commandsObject.Add(this._raspiCommands.RaspiHomeObjectKnown.ToArray());
            this._grammarBuilderObject = new GrammarBuilder();
            this._grammarBuilderObject.Culture = new System.Globalization.CultureInfo("fr-FR");
            this._grammarBuilderObject.Append(this._commandsObject);
            this._grammarObject = new Grammar(this._grammarBuilderObject);

            // Location grammar
            this._commandsLocation = new Choices();
            this._commandsLocation.Add(this._raspiCommands.RaspiHomeLocationKnown.ToArray());
            this._grammarBuilderLocation = new GrammarBuilder();
            this._grammarBuilderLocation.Culture = new System.Globalization.CultureInfo("fr-FR");
            this._grammarBuilderLocation.Append(this._commandsLocation);
            this._grammarLocation = new Grammar(this._grammarBuilderLocation);

            // Useless word grammar
            this._commandsUselessWord = new Choices();
            this._commandsUselessWord.Add(this._raspiCommands.RaspiHomeLocationKnown.ToArray());
            this._grammarBuilderUselessWord = new GrammarBuilder();
            this._grammarBuilderUselessWord.Culture = new System.Globalization.CultureInfo("fr-FR");
            this._grammarBuilderUselessWord.Append(this._commandsUselessWord);
            this._grammarUselessWord = new Grammar(this._grammarBuilderUselessWord);

            // Sentence grammar            
            this._commandsSentence = new Choices();
            this._commandsSentence.Add(this._raspiCommands.RaspiHomeGrammarCommand.ToArray());
            this._grammarBuilderSentence = new GrammarBuilder();
            this._grammarBuilderSentence.Culture = new System.Globalization.CultureInfo("fr-FR");
            this._grammarBuilderSentence.Append(this._commandsSentence);
            this._grammarSentence = new Grammar(this._grammarBuilderSentence);
            
            // Create the command dictation grammar.
            this._commandDictationGrammar = new DictationGrammar("grammar:dictation");
            this._commandDictationGrammar.Name = "command dictation";
            this._commandDictationGrammar.Enabled = true;

            // Create a default dictation grammar.
            this._defaultDictationGrammar = new DictationGrammar();
            this._defaultDictationGrammar.Name = "default dictation";
            this._defaultDictationGrammar.Enabled = true;

            // Create the spelling dictation grammar.
            this._spellingDictationGrammar = new DictationGrammar("grammar:dictation#spelling");
            this._spellingDictationGrammar.Name = "spelling dictation";
            this._spellingDictationGrammar.Enabled = true;

            // Add grammar to the engine
            this._recoEngine.LoadGrammarAsync(new Grammar(this._grammarBuilder));
            this._recoEngine.LoadGrammarAsync(this._grammarAction);
            this._recoEngine.LoadGrammarAsync(this._grammarActionNoObject);
            this._recoEngine.LoadGrammarAsync(this._grammarObject);
            this._recoEngine.LoadGrammarAsync(this._grammarLocation);
            this._recoEngine.LoadGrammarAsync(this._grammarUselessWord);
            //this._recoEngine.LoadGrammarAsync(this._grammarSentence);
            this._recoEngine.LoadGrammar(new DictationGrammar());
            //this._recoEngine.LoadGrammarAsync(this._commandDictationGrammar);
            //this._recoEngine.LoadGrammarAsync(this._defaultDictationGrammar);
            //this._recoEngine.LoadGrammarAsync(this._spellingDictationGrammar);

            this._recoEngine.SpeechRecognized += recoEngine_SpeechRecognized;
            this._recoEngine.SetInputToDefaultAudioDevice();
          
            EnableSpeech();
        }

        private void recoEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!this.IsRaspiCalled)
            {
                if (e.Result.Text == "raspi")
                {
                    CallRaspi(e.Result.Text);
                }
            }
            else
            {
                SendBrutCommand(e.Result.Text);
                //SendBrutCommand("allume la lumière");
            }

            Console.WriteLine(e.Result.Text);
        }
        #endregion
    }
}
