﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeServer
{
    public class CommandFilter
    {
        #region Variables
        private RaspiHomeCommands _rhCommands;
        private string _sentence = "";
        private List<RaspberryClient> _lstClients;

        private string[] typeValue = { "Boolean", "Double", "Int16", "Int32", "Int64" };
        #endregion

        #region Properties
        public RaspiHomeCommands RhCommands
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

        public string Sentence
        {
            get
            {
                return _sentence;
            }

            set
            {
                _sentence = value;
            }
        }
        #endregion

        #region Constructor
        public CommandFilter()
        {
            this.RhCommands = new RaspiHomeCommands();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Receive the order and treat to find raspberrys with the order
        /// </summary>
        /// <param name="paramSentence"></param>
        /// <param name="paramRaspberryClients"></param>
        /// <returns></returns>
        public List<RaspberryClient> ApplyFilter(string paramSentence, List<RaspberryClient> paramRaspberryClients)
        {
            this.Sentence = RemoveDiacritics(paramSentence);

            List<RaspberryClient> result = new List<RaspberryClient>();
                        
            string action = "";
            string location = "";
            string componentWithoutAction = "";
            string component = this.GetComponentFromSentence(this.Sentence);

            Type componentType = null;
            string actionValue = "";

            // Different usage of the order between an component with action and without one
            // Writes values and send to the client the information or read values
            if (component != "")
            {
                action = this.GetActionFromSentence(this.Sentence);
                actionValue = ReadValueOfSelectedComponent(action);
                componentType = this.GetComponentType(component);
            }
            else
            {
                componentWithoutAction = GetIndependantComponentFromSentence(this.Sentence);
                componentType = this.GetComponentType(componentWithoutAction);
            }

            foreach (var rpiClient in paramRaspberryClients)
            {
                location = this.GetSentenceLocationOrRaspberryLocation(this.Sentence, rpiClient);

                if (rpiClient.Location.ToLower() == location.ToLower())
                {
                    foreach (var item in rpiClient.Components)
                        if (item.GetType() == componentType)
                            if(action != "")
                                // MET A JOUR LES VALEURS
                                this.WriteValue(item, action, item.GetType().GetProperty(actionValue));
                            else
                                // ENVOIE UN MESSAGE D'ATTENTE DE RETOUR D'INFROMATIONS
                                // RENVOIE AU RASPBERRY PARLEUT LES DONNEES A LIRE
                                // CREATION DE LA REQUETTE RETOUR ICI
                                // 
                                //readvalues from the sensor REQUETTES AU CLIENT 
                                //SendMessageToSensorRaspberry("")
                                //break;
                    result.Add(rpiClient);
                }
            }

            return result;
        }

        /// <summary>
        /// Find location if exist, else all location
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string GetSentenceLocationOrRaspberryLocation(string sentence, RaspberryClient rpiClient)
        {
            string result = "";
            string[] words = sentence.ToLower().Split(' ');

            foreach (var word in words)
            {
                if (this._rhCommands.RaspiHomeLocationKnown.Contains(word))
                {
                    result = word;
                }
            }

            if (result == "" || result == "maison")
                result = rpiClient.Location;

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
                if (this._rhCommands.RaspiHomeComponentKnown.Contains(word))
                {
                    result = word;
                }
            }

            return result;
        }

        /// <summary>
        /// Get the componnent called without special component connected to a special action
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string GetIndependantComponentFromSentence(string sentence)
        {
            string result = "";
            string[] words = sentence.ToLower().Split(' ');

            foreach (var word in words)
            {
                if (this._rhCommands.RaspiHomeComponentWithoutActionKnown.Contains(word))
                {
                    result = word;
                    break;
                }
            }

            return result;
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
                if (this._rhCommands.RaspiHomeActionKnown.Contains(word))
                {
                    result = word;
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
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var typeOfComonent in types)
            {
                if (typeOfComonent.Name == this._rhCommands.RaspiLanguageTranslation[componentName])
                {
                    result = typeOfComonent;
                    break;
                }
            }

            return result;
        }

        private string ReadValueOfSelectedComponent(string actionName)
        {
            string result = "";

            foreach (var actionKeys in this._rhCommands.RaspiBooleanCommandTranslation.Keys)
                if (actionKeys == actionName)
                {
                    // Find the Value of the dictionary trough the inner dictionary to get the first value                
                    result = this.RhCommands.RaspiBooleanCommandTranslation[actionName].First().Key;
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
                    typeVariable.SetValue(component, this._rhCommands.RaspiBooleanCommandTranslation[action][typeVariable.Name]);
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
        static string RemoveDiacritics(string sentence)
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