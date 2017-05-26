using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeServer
{
    public abstract class Component { }

    // Speecher and synthetizer
    public class Microphone : Component { }

    // SenseHAT
    public class Sensor : Component { }

    #region PiFace Digital 2
    public class Light : Component
    {
        #region Variable
        private bool _isOn = false;
        private double _luxPercent = 0.0;
        private int[] _colorARGB = { 255, 0, 0, 0 };
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
                    this.LuxPercent = 100.0;
                else
                    this.LuxPercent = 0.0;
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
    }

    public class Store : Component
    {
        #region Variable
        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isOpen = false;
        private bool _isClose = false;
        private double _percentUp = 0.0;
        private double _percentOpen = 0.0;
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
        #endregion        
    }
    #endregion
}
