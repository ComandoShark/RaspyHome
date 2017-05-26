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
        private const byte DEFAULT_PORT = PiFaceDigital2.LED0;

        private const byte OFF = MCP23S17.Off;
        private const byte ON = MCP23S17.On;
        #endregion

        #region Variable
        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isOpen = false;
        private bool _isClose = false;
        private double _percentUp = 0.0;        
        private double _percentOpen = 0.0;

        private byte _pinCommunicationUp = DEFAULT_PORT;
        private byte _pinCommunicationDown = DEFAULT_PORT;
        private byte _pinCommunicationOpen = DEFAULT_PORT;
        private byte _pinCommunicationClose = DEFAULT_PORT;
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
                    MCP23S17.WritePin(this.PinCommunicationDown, OFF);
                    MCP23S17.WritePin(this.PinCommunicationUp, ON);
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
                    MCP23S17.WritePin(this.PinCommunicationUp, OFF);
                    MCP23S17.WritePin(this.PinCommunicationDown, ON);
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
                    MCP23S17.WritePin(this.PinCommunicationClose, OFF);
                    MCP23S17.WritePin(this.PinCommunicationOpen, ON);
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
                    MCP23S17.WritePin(this.PinCommunicationOpen, OFF);
                    MCP23S17.WritePin(this.PinCommunicationClose, ON);                    
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

        public byte PinCommunicationDown
        {
            get
            {
                return _pinCommunicationDown;
            }

            set
            {
                _pinCommunicationDown = value;
            }
        }

        public byte PinCommunicationOpen
        {
            get
            {
                return _pinCommunicationOpen;
            }

            set
            {
                _pinCommunicationOpen = value;
            }
        }

        public byte PinCommunicationClose
        {
            get
            {
                return _pinCommunicationClose;
            }

            set
            {
                _pinCommunicationClose = value;
            }
        }

        public byte PinCommunicationUp
        {
            get
            {
                return _pinCommunicationUp;
            }

            set
            {
                _pinCommunicationUp = value;
            }
        }
        #endregion

        #region Constructor
        #endregion

        #region Methods
        #endregion
    }
}
