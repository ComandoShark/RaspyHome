using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Synthetize
{
    public class ActionUse
    {
        #region Properties
        /// <summary>
        /// Used to set on On or Off electric component like bulb
        /// </summary>
        public bool IsOn { get; set; }

        /// <summary>
        /// Used to set on Up for store
        /// </summary>
        public bool IsUp { get; set; }

        /// <summary>
        /// Used to set on Down for store
        /// </summary>
        public bool IsDown { get; set; }
        public bool IsStop
        {
            get
            {
                return _isStop;
            }

            set
            {
                this.IsUp = false;
                this.IsDown = false;

                _isStop = value;
            }
        }

        private bool _isStop = false;

        /// <summary>
        /// Used to know the state between open and close
        /// </summary>
        public bool IsOpen { get; set; }
        #endregion
    }
}
