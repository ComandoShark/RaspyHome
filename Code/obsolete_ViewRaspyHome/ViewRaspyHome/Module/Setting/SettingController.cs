using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewRaspyHome.Module.Setting
{
    public class SettingController
    {
        #region Fields
        #region Variables
        private SettingView _view = null;
        private SettingModel _model = null;
        #endregion
        #endregion

        #region Properties  
        public SettingView View
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

        public SettingModel Model
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
        public SettingController(SettingView view)
        {
            this.View = view;
            this.Model = new SettingModel(this);
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
