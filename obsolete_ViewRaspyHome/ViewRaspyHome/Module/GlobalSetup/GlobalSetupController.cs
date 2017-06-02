using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewRaspyHome.Module.GlobalSetup
{
    public class GlobalSetupController
    {
        #region Fields
        #region Variables
        private GlobalSetupView _view = null;
        private GlobalSetupModel _model = null;
        #endregion
        #endregion

        #region Properties  
        public GlobalSetupView View
        {
            get
            {
                return _view;
            }

            set
            {
                _view = value;
            }
        }

        public GlobalSetupModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
            }
        }
        #endregion

        #region Constructor
        public GlobalSetupController(GlobalSetupView view)
        {
            this.View = view;
            this.Model = new GlobalSetupModel(this);
        }
        #endregion

        #region Methods
        public void SetFrameSize(double actualWidth, double actualHeight)
        {
            this.Model.SetFrameSize(actualWidth, actualHeight);
        }
        #endregion
    }
}
