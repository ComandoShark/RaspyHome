using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Synthetize
{
    public class RaspiHomeCommands
    {
        #region Commands variable
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

        private List<string> _raspiHomeObjectKnown = new List<string>()
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
        };

        private List<string> _speecherRespondingEtatRequest = new List<string>()
        {
            "Il fait","degrés Celsius","degrés Farad","degrés Kelvin","Le taux d'humidité est de","pourcent","La pression est actuellement de","bars"
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
        #endregion

        #region Properties
        public List<string> WhenCalling
        {
            get
            {
                return _whenCalling;
            }

            set
            {
                _whenCalling = value;
            }
        }

        public List<string> WhenCallingError
        {
            get
            {
                return _whenCallingError;
            }

            set
            {
                _whenCallingError = value;
            }
        }

        public List<string> SpeecherRespondingRequest
        {
            get
            {
                return _speecherRespondingRequest;
            }

            set
            {
                _speecherRespondingRequest = value;
            }
        }

        public List<string> SpeecherRespondingRequestError
        {
            get
            {
                return _speecherRespondingRequestError;
            }

            set
            {
                _speecherRespondingRequestError = value;
            }
        }

        public List<string> RaspiHomeObjectKnown
        {
            get
            {
                return _raspiHomeObjectKnown;
            }

            set
            {
                _raspiHomeObjectKnown = value;
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

        public List<string> SpeecherRespondingEtatRequest
        {
            get
            {
                return _speecherRespondingEtatRequest;
            }

            set
            {
                _speecherRespondingEtatRequest = value;
            }
        }

        public List<string> RaspiHomeActionWithoutObjectKnown
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
        #endregion
    }
}
