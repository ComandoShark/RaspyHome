using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Synthetize
{
    public class ActionNoObjectUse
    {
        #region Properties
        /// <summary>
        /// Give the actual temperature value
        /// </summary>
        public bool TemperatureValue { get; set; }

        /// <summary>
        /// Give the actual humidity value
        /// </summary>
        public bool HumidityValue { get; set; }

        /// <summary>
        /// Give the actual pressure value
        /// </summary>
        public bool PressureValue { get; set; }

        /// <summary>
        /// Give temperature humidity and pressiure value
        /// </summary>
        private bool _fullStateValue = false;
        public bool FullStateValue
        {
            get { return this._fullStateValue; }
            set
            {
                if (value)
                {
                    this.TemperatureValue = true;
                    this.HumidityValue = true;
                    this.PressureValue = true;
                }

                this._fullStateValue = value;
            }
        }
        #endregion
    }
}
