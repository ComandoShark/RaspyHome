/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeServer is a server TCP. It's the m
 *      ain program, where all command pass before 
 *      to be reply to the good client. 
\*--------------------------------------------------*/

using System.Collections.Generic;

namespace RaspiHomeServer
{
    public class RaspiHomeCommands
    {
        #region Commands variable
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
            "descendre","descends",
            "stopper","stop",
            "ouvrir","ouvre",
            "fermer","ferme",
            "stopper","stop",
        };

        private List<string> _raspiHomeActionWithoutObjectKnown = new List<string>()
        {
            "temperature","humidite","pression","etat"
        };

        private List<string> _raspiHomeLocationKnown = new List<string>()
        {
            "maison","salon","cuisine","parent","enfant","bureau","toilette","bain"
        };

        private List<string> _raspiHomeCommandUselessConnecter = new List<string>()
        {
            "le","la","les","un","une","des","mon","ma","mes","son","sa","ses","ce","cet","cette","ces","cel","celle","du","de"
        };

        private List<string> _raspiHomeGrammarCommand = new List<string>()
        {
            "allume la lumière", "allume les lumières", "allumer la lumière","allumer les lumières",
            "éteint la lumière", "éteint les lumières", "éteindre la lumière","éteindre les lumières",
            "monte le store","monte les stores","monter le store","monter les stores",
            "déscend le store","déscend les stores","déscendre le store","déscendre les stores",
            "ouvre le store", "ouvre les stores","ouvrir le store", "ouvrir les stores",
            "femre le store", "ferme les stores","fermer le store", "fermer les stores",
            "quelle est la température", "quelle est l'humidité", "quelle est la préssion", "quelle est l'état",
            "de la maison","de la cuisine","du salon","de la salle de bain","de la chambre","de la pièce",
        };

        Dictionary<string, string> _raspiLanguageTranslation = new Dictionary<string, string>()
        {
            { "lumiere","Light"},
            { "store","Store"},
            { "temperature","Sensor"},
            { "humidite","Sensor"},
            { "pression","Sensor"},
            { "etat","Sensor"},
        };

        Dictionary<string, Dictionary<string, bool>> _raspiBooleanCommandTranslation = new Dictionary<string, Dictionary<string, bool>>()
        {
            { "allume", new Dictionary<string, bool> { { "IsOn", true } } }, { "allumer", new Dictionary<string, bool> { { "IsOn", true } } },
            { "eteins", new Dictionary<string, bool> { { "IsOn", false } } }, { "eteindre", new Dictionary<string, bool> { { "IsOn", false } } },
            { "monte", new Dictionary<string, bool> { { "IsUp", true } } }, { "monter", new Dictionary<string, bool> { { "IsUp", true } } },
            { "descends", new Dictionary<string, bool> { { "IsDown", true } } }, { "descendre", new Dictionary<string, bool> { { "IsDown", true } } },
            { "stop",new Dictionary<string, bool> { {"IsStop",true } } },{"stopper",new Dictionary<string, bool> { {"IsStop",true } } },
        };
        #endregion

        #region Properties
        public List<string> RaspiHomeComponentKnown
        {
            get
            {
                return _raspiHomeComponentKnown;
            }

            set
            {
                _raspiHomeComponentKnown = value;
            }
        }

        public List<string> RaspiHomeActionKnown
        {
            get
            {
                return _raspiHomeActionKnown;
            }

            set
            {
                _raspiHomeActionKnown = value;
            }
        }

        public List<string> RaspiHomeComponentWithoutActionKnown
        {
            get
            {
                return _raspiHomeActionWithoutObjectKnown;
            }

            set
            {
                _raspiHomeActionWithoutObjectKnown = value;
            }
        }

        public List<string> RaspiHomeLocationKnown
        {
            get
            {
                return _raspiHomeLocationKnown;
            }

            set
            {
                _raspiHomeLocationKnown = value;
            }
        }

        public List<string> RaspiHomeCommandUselessConnecter
        {
            get
            {
                return _raspiHomeCommandUselessConnecter;
            }

            set
            {
                _raspiHomeCommandUselessConnecter = value;
            }
        }

        public List<string> RaspiHomeGrammarCommand
        {
            get
            {
                return _raspiHomeGrammarCommand;
            }

            set
            {
                _raspiHomeGrammarCommand = value;
            }
        }

        public Dictionary<string, string> RaspiLanguageTranslation
        {
            get
            {
                return _raspiLanguageTranslation;
            }

            set
            {
                _raspiLanguageTranslation = value;
            }
        }
        
        public Dictionary<string, Dictionary<string, bool>> RaspiBooleanCommandTranslation
        {
            get
            {
                return _raspiBooleanCommandTranslation;
            }

            set
            {
                _raspiBooleanCommandTranslation = value;
            }
        }
        #endregion
    }
}
