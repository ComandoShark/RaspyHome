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
        #endregion

        #region Variable
        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isOpen = false;
        private bool _isClose = false;
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

        #region Constructor
        #endregion

        #region Methods
        public string ToSend()
        {
            string result = "";

            result += "IsUp=" + this.IsUp + ";" + "IsDown=" + this.IsDown + ";" + "PercentUp=" + this.PercentUp + ";";
            result += "IsOpen=" + this.IsOpen + ";" + "IsClose=" + this.IsClose + ";" + "PercentOpen=" + this.PercentOpen + ";";

            return result;
        }
        #endregion
    }
}
