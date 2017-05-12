using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Synthetize
{
    public class CommandTheRaspi
    {
        #region Variables
        private ActionUse _actionUse;
        private ActionNoObjectUse _actionNoObjectUse;
        private ObjectUse _objectUse;
        private LocationUse _locationUse;
        #endregion

        #region Properties
        public ActionUse ActionUse
        {
            get
            {
                return _actionUse;
            }

            set
            {
                _actionUse = value;
            }
        }

        public ActionNoObjectUse ActionNoObjectUse
        {
            get
            {
                return _actionNoObjectUse;
            }

            set
            {
                _actionNoObjectUse = value;
            }
        }

        public ObjectUse ObjectUse
        {
            get
            {
                return _objectUse;
            }

            set
            {
                _objectUse = value;
            }
        }

        public LocationUse LocationUse
        {
            get
            {
                return _locationUse;
            }

            set
            {
                _locationUse = value;
            }
        }
        #endregion

        #region Constructor
        public CommandTheRaspi()
        {
            this.ActionUse = new ActionUse();
            this.ActionNoObjectUse = new ActionNoObjectUse();
            this.ObjectUse = new ObjectUse();
            this.LocationUse = new LocationUse();
        }
        #endregion
    }
}
