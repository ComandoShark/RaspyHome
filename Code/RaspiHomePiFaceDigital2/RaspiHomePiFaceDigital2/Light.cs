using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomePiFaceDigital2
{
    public class Light : Component
    {
        #region Fields
        #region Constant
        private const byte RELAI1 = PiFaceDigital2.RelayB;
        private const byte RELAI2 = PiFaceDigital2.RelayA;

        private const byte OFF = MCP23S17.Off;
        private const byte ON = MCP23S17.On;
        #endregion

        #region Variable
        private bool _isOn = false;
        private double _luxPercent = 0.0;
        private int[] _colorARGB = { 255, 0, 0, 0 };
        private byte _relaiToActivate = RELAI1;    
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
                if (value)
                {
                    this.LuxPercent = 100.0;
                    MCP23S17.WritePin(this._relaiToActivate, ON);
                }
                else
                {
                    this.LuxPercent = 0.0;
                    MCP23S17.WritePin(this._relaiToActivate, OFF);
                }
            }
        }

        public double LuxPercent
        {
            get
            {
                return _luxPercent;
            }

            set
            {
                _luxPercent = value;

                if (value >= 100.0)
                    _luxPercent = 100.0;
                if (value <= 0.0)
                    _luxPercent = 0.0;
            }
        }

        public int[] ColorARGB
        {
            get
            {
                return _colorARGB;
            }

            set
            {
                _colorARGB = value;
            }
        }
        #endregion

        #region Constructor
        #endregion

        #region Methods
        #endregion
    }
}
