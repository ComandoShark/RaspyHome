/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomePiFaceDigital2 is a program who use 
 *   a PiFace Digital 2, it's an electronic card who 
 *   can be use to plug electronic component. This 
 *   program use the PiFace Digital 2 to activate 
 *   light and store. 
\*--------------------------------------------------*/

namespace RaspiHomePiFaceDigital2
{
    public class Light : Component
    {
        #region Fields
        #region Constant
        // PiFace output
        private const byte RELAIA = PiFaceDigital2.RelayA;
        private const byte RELAIB = PiFaceDigital2.RelayB;        

        // PiFace State
        private const byte OFF = MCP23S17.Off;
        private const byte ON = MCP23S17.On;
        #endregion

        #region Variable
        private bool _isOn = false;
        private bool _isOnA = false;
        private bool _isOnB = false;        
        #endregion
        #endregion

        #region Properties
        public bool IsOn
        {
            get
            {
                return _isOn;
            }

            set
            {
                _isOn = value;
                this.IsOnA = value;
                this.IsOnB = value;
            }
        }

        public bool IsOnA
        {
            get
            {
                return _isOnA;
            }

            set
            {
                _isOnA = value;
                if (value)
                {
                    // Turn ON the light
                    MCP23S17.WritePin(RELAIA, ON);
                }
                else
                {
                    // Turn OFF the light
                    MCP23S17.WritePin(RELAIA, OFF);
                }
            }
        }

        public bool IsOnB
        {
            get
            {
                return _isOnB;
            }

            set
            {
                _isOnB = value;
                if (value)
                {
                    // Turn ON the light
                    MCP23S17.WritePin(RELAIB, ON);
                }
                else
                {                 
                    // Turn OFF the light
                    MCP23S17.WritePin(RELAIB, OFF);
                }
            }
        }        
        #endregion

        #region Constructor
        #endregion

        #region Methods
        #endregion
    }
}
