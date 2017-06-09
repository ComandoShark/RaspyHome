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

using System;
using Windows.UI.Xaml;

namespace RaspiHomePiFaceDigital2
{
    public class Store : Component
    {
        #region Fields
        #region Constant
        // PiFace output
        private const byte OPEN = PiFaceDigital2.LED6;
        private const byte CLOSE = PiFaceDigital2.LED5;
        private const byte UP = PiFaceDigital2.LED4;
        private const byte DOWN = PiFaceDigital2.LED3;
        private const byte ACTIVEMOTEUR = PiFaceDigital2.LED2;

        // PiFace State
        private const byte OFF = MCP23S17.Off;
        private const byte ON = MCP23S17.On;

        // Max value for store (totaly open)
        private const int MAX_LEVEL = 200; // Time span total = 19seconds (raspberry latency)
        // Min value for store (totaly close)
        private const int MIN_LEVEL = 0;
        #endregion

        #region Variable
        DispatcherTimer _dTimerUp = new DispatcherTimer();
        DispatcherTimer _dTimerDown = new DispatcherTimer();

        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isOpen = false;
        private bool _isClose = false;
        private bool _isStop = false;

        private int _counterStopped = 0;
        #endregion
        #endregion

        #region Properties
        public bool IsUp
        {
            get
            {
                return _isUp;
            }

            set
            {
                _isUp = value;

                if (value && this.CounterStopped < MAX_LEVEL)
                {
                    this.SetLevel("IsUp");
                }
            }
        }

        public bool IsDown
        {
            get
            {
                return _isDown;
            }

            set
            {
                _isDown = value;

                if (value && this.CounterStopped > MIN_LEVEL)
                {
                    this.SetLevel("IsDown");
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }

            set
            {
                _isOpen = value;

                if (value)
                {
                    this.SetLevel("IsOpen");
                }
            }
        }

        public bool IsClose
        {
            get
            {
                return _isClose;
            }

            set
            {
                _isClose = value;

                if (value)
                {
                    this.SetLevel("IsClose");
                }
            }
        }                      

        public bool IsStop
        {
            get
            {
                return _isStop;
            }

            set
            {
                _isStop = value;

                if (value)
                {
                    this._dTimerUp.Stop();
                    this._dTimerDown.Stop();
                    SetLevel("IsStop");
                    this.IsStop = false;
                }
            }
        }

        public int CounterStopped
        {
            get
            {
                return _counterStopped;
            }

            set
            {
                _counterStopped = value;

                // Store manager
                if (value == MAX_LEVEL)
                {
                    this._dTimerUp.Stop();
                    SetLevel("IsStop");
                    _counterStopped = MAX_LEVEL;
                }
                else if (value == MIN_LEVEL)
                {
                    this._dTimerDown.Stop();
                    SetLevel("IsStop");
                    _counterStopped = MIN_LEVEL;
                }
            }
        }
        #endregion

        #region Constructor
        public Store()
        {
            this._dTimerUp.Interval = new TimeSpan(10);
            this._dTimerUp.Tick += _dTimerUp_Tick;

            this._dTimerDown.Interval = new TimeSpan(10);
            this._dTimerDown.Tick += _dTimerDown_Tick;
        }

        private void _dTimerUp_Tick(object sender, object e)
        {
            this.CounterStopped++;
        }

        private void _dTimerDown_Tick(object sender, object e)
        {
            this.CounterStopped--;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set the level
        /// </summary>
        /// <param name="propertyName"></param>
        private void SetLevel(string propertyName)
        {
            switch (propertyName)
            {
                case "IsUp":
                    this.IsDown = false;

                    MCP23S17.WritePin(DOWN, OFF);
                    MCP23S17.WritePin(UP, ON);

                    this.SetLevelUp();
                    break;
                case "IsDown":
                    this.IsUp = false;

                    MCP23S17.WritePin(UP, OFF);
                    MCP23S17.WritePin(DOWN, ON);

                    this.SetLevelDown();
                    break;
                case "IsOpen":
                    this.IsClose = false;

                    MCP23S17.WritePin(CLOSE, OFF);
                    MCP23S17.WritePin(OPEN, ON);
                    break;
                case "IsClose":
                    this.IsOpen = false;

                    MCP23S17.WritePin(OPEN, OFF);
                    MCP23S17.WritePin(CLOSE, ON);
                    break;
                case "IsStop":
                    this.IsUp = false;
                    this.IsDown = false;
                    this.IsOpen = false;
                    this.IsClose = false;

                    MCP23S17.WritePin(UP, OFF);
                    MCP23S17.WritePin(DOWN, OFF);
                    MCP23S17.WritePin(OPEN, OFF);
                    MCP23S17.WritePin(CLOSE, OFF);
                    break;
            }
        }

        private void SetLevelUp()
        {
            this._dTimerDown.Stop();
            this._dTimerUp.Start();            
        }

        private void SetLevelDown()
        {
            this._dTimerUp.Stop();
            this._dTimerDown.Start();
        }
        #endregion
    }
}
