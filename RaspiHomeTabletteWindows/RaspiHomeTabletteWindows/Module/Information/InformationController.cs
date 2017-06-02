using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeTabletteWindows.Module.Information
{
    public class InformationController
    {
        #region Fields
        #region Variables
        private InformationView _view = null;
        private InformationModel _model = null;
        #endregion
        #endregion

        #region Properties  
        public InformationView View
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

        public InformationModel Model
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
        public InformationController(InformationView view)
        {
            this.View = view;
            this.Model = new InformationModel(this);
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