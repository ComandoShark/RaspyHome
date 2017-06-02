using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomePiFaceDigital2
{
    public class ModelPiFaceDigital2
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ViewPiFaceDigital2 _vPiFace;

        private List<Component> _components;
        private CommunicationWithServer _comWithServer;

        // Command to know
        private List<string> _raspiHomeComponentKnown = new List<string>()
        {
            "lumiere","lumieres",
            "store","stores",
            "television","televisions",
            "porte","portes",
            "fenetre","fenetres",
        };

        private List<string> _raspiHomeActionKnown = new List<string>()
        {
            "allumer","allume",
            "eteindre","eteins",
            "monter","monte",
            "descendre","descend",
            "stopper","stop",
            "ouvrir","ouvre",
            "fermer","ferme",
            "stopper","stop",
        };

        private Dictionary<string, string> _raspiLanguageTranslation = new Dictionary<string, string>()
        {
            { "lumiere","Light"}, { "lumieres","Light"},
            { "store","Store"}, { "stores","Store"},
        };

        private Dictionary<string, Dictionary<string, bool>> _raspiBooleanCommandTranslation = new Dictionary<string, Dictionary<string, bool>>()
        {
            { "allume", new Dictionary<string, bool> { { "IsOn", true } } }, { "allumer", new Dictionary<string, bool> { { "IsOn", true } } },
            { "eteins", new Dictionary<string, bool> { { "IsOn", false } } }, { "eteindre", new Dictionary<string, bool> { { "IsOn", false } } },
            { "monte", new Dictionary<string, bool> { { "IsUp", true } } }, { "monter", new Dictionary<string, bool> { { "IsUp", true } } },
            { "descends", new Dictionary<string, bool> { { "IsDown", true } } }, { "descendre", new Dictionary<string, bool> { { "IsDown", true } } },
            { "stop",new Dictionary<string, bool> { {"IsStop",true } } },{"stopper",new Dictionary<string, bool> { {"IsStop",true } } },
        };
        #endregion
        #endregion

        #region Properties
        public ViewPiFaceDigital2 VPiFace
        {
            get
            {
                return _vPiFace;
            }

            set
            {
                _vPiFace = value;
            }
        }

        public List<Component> Components
        {
            get
            {
                return _components;
            }

            set
            {
                _components = value;
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
        #endregion

        #region Constructors
        public ModelPiFaceDigital2(ViewPiFaceDigital2 paramView)
        {
            // Communication like Model-View
            this.VPiFace = paramView;

            // Initialize the components and add the components linked with the Raspberry
            this.Components = new List<Component>();
            this.Components.Add(new Light());
            this.Components.Add(new Store());

            // Initilize the PiFace Digital 2
            InitPiFace();

            // Initialize the server communication
            this.ComWithServer = new CommunicationWithServer(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the PiFace Digital 2
        /// </summary>
        private async void InitPiFace()
        {
            try
            {
                await MCP23S17.InitSPI();

                MCP23S17.InitMCP23S17();
                MCP23S17.setPinMode(0x00FF); // 0x0000 = all outputs, 0xffff=all inputs, 0x00FF is PIFace Default
                MCP23S17.pullupMode(0x00FF); // 0x0000 = no pullups, 0xffff=all pullups, 0x00FF is PIFace Default
                MCP23S17.WriteWord(0x0000); // 0x0000 = no pullups, 0xffff=all pullups, 0x00FF is PIFace Default
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Set the value to be writed on the PiFace
        /// </summary>
        /// <param name="message"></param>
        public void SetValue(string message)
        {
            string sentence = this.RemoveDiacritics(message);
            string action = this.GetActionFromSentence(sentence);
            string actionValue = this.ReadValueOfSelectedComponent(action);
            string component = this.GetComponentFromSentence(sentence);
            Type componentType = this.GetComponentType(component);

            foreach (Component itemType in this.Components)
            {
                if (itemType.GetType() == componentType)
                {
                    this.WriteValue(itemType, action, itemType.GetType().GetProperty(actionValue));
                }
            }
        }

        /// <summary>
        /// Find location exist
        /// </summary>
        /// <param name="action"></param>
        /// <returns> the action linked to the action word </returns>
        private string GetActionFromSentence(string sentence)
        {
            string result = "";
            string[] words = sentence.ToLower().Split(' ');

            foreach (var word in words)
            {
                if (this._raspiHomeActionKnown.Contains(word))
                {
                    result = word;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Get the componnent called
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string GetComponentFromSentence(string sentence)
        {
            string result = "";
            string[] words = sentence.ToLower().Split(' ');

            foreach (var word in words)
            {
                if (this._raspiHomeComponentKnown.Contains(word))
                {
                    result = word;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Find all client who have the object in the sentence
        /// </summary>
        /// <param name="componentName"></param>
        /// <returns>the object type</returns>
        private Type GetComponentType(string componentName)
        {
            Type result = null;
            Type[] types = typeof(Component).GetTypeInfo().Assembly.GetTypes();

            foreach (var typeOfComonent in types)
            {
                if (typeOfComonent.Name == this._raspiLanguageTranslation[componentName])
                {
                    result = typeOfComonent;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Read properties value of classes
        /// </summary>
        /// <param name="actionName"> name used to change the good property </param>
        /// <returns> return the name of the property to change the value </returns>
        private string ReadValueOfSelectedComponent(string actionName)
        {
            string result = "";

            foreach (var actionKeys in this._raspiBooleanCommandTranslation.Keys)
                if (actionKeys == actionName)
                {
                    // Find the Value of the dictionary trough the inner dictionary to get the first value                
                    result = this._raspiBooleanCommandTranslation[actionName].First().Key;
                    break;
                }

            return result;
        }

        /// <summary>
        /// Search the val to change
        /// </summary>
        /// <param name="component"></param>
        /// <param name="action"></param>
        /// <param name="typeVariable"></param>
        private void WriteValue(Component component, string action, PropertyInfo typeVariable)
        {
            switch (typeVariable.PropertyType.Name)
            {
                case "Boolean":
                    // Set the new value dynamicaly with value registered in an boolean dictionary
                    typeVariable.SetValue(component, this._raspiBooleanCommandTranslation[action][typeVariable.Name]);
                    break;
                case "Double":
                    break;
                case "Int16":
                case "Int32":
                case "Int64":
                    break;
            }
        }

        /// <summary>
        /// Stack Overflow solution to delete accents in strings
        /// http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string RemoveDiacritics(string sentence)
        {
            var normalizedString = sentence.Normalize(NormalizationForm.FormD);
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
