using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomePiFaceDigital2
{
    public class Store : Component
    {
        #region Fields
        #region Constant
        private const byte OPEN = PiFaceDigital2.LED6;
        private const byte CLOSE = PiFaceDigital2.LED5;        
        private const byte UP = PiFaceDigital2.LED4;
        private const byte DOWN = PiFaceDigital2.LED3;
        private const byte ACMOTEUR = PiFaceDigital2.LED2;

        private const byte OFF = MCP23S17.Off;
        private const byte ON = MCP23S17.On;
        #endregion

        #region Variable
        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isOpen = false;
        private bool _isClose = false;
        private bool _isStop = false;
        private double _percentUp = 0.0;        
        private double _percentOpen = 0.0;
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

                if (value)
                {                    
                    this.IsDown = false;
                    MCP23S17.WritePin(DOWN, OFF);
                    MCP23S17.WritePin(UP, ON);
                    this.PercentUp = 100.0;
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

                if (value)
                {
                    this.IsUp = false;
                    MCP23S17.WritePin(UP, OFF);
                    MCP23S17.WritePin(DOWN, ON);
                    this.PercentUp = 100.0;
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
                    this.IsClose = false;
                    MCP23S17.WritePin(CLOSE, OFF);
                    MCP23S17.WritePin(OPEN, ON);
                    this.PercentOpen = 100.0;
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
                    this.IsOpen = false;
                    MCP23S17.WritePin(OPEN, OFF);
                    MCP23S17.WritePin(CLOSE, ON);                    
                    this.PercentOpen = 100.0;                    
                }
            }
        }

        public double PercentUp
        {
            get
            {
                return _percentUp;
            }

            set
            {
                _percentUp = value;

                if (value >= 100.0)
                    _percentUp = 100.0;
                if (value <= 0.0)
                    _percentUp = 0.0;
            }
        }

        public double PercentOpen
        {
            get
            {
                return _percentOpen;
            }

            set
            {
                _percentOpen = value;

                if (value >= 100.0)
                    _percentOpen = 100.0;
                if (value <= 0.0)
                    _percentOpen = 0.0;
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
                    MCP23S17.WritePin(UP, OFF);
                    MCP23S17.WritePin(DOWN, OFF);
                    MCP23S17.WritePin(OPEN, OFF);
                    MCP23S17.WritePin(CLOSE, OFF);
                    value = false;
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
