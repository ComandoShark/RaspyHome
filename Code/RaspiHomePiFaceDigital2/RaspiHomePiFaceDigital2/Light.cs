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
        #endregion

        #region Variable
        private bool _isOn = false;
        private double _luxPercent = 0.0;
        private int[] _colorARGB = { 255, 0, 0, 0 };
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

        #region Constructor
        #endregion

        #region Methods
        public string ToSend()
        {
            string result = "";

            result += "IsOn=" + this.IsOn + ";" + "LuxPercent=" + this.LuxPercent + ";" + "ColorARGB=";

            for (int i = 0; i < this.ColorARGB.Length-1; i++)
            {
                result += this.ColorARGB[i].ToString() + ",";
            }

            result += ";";

            return result;
        }
        #endregion
    }
}
