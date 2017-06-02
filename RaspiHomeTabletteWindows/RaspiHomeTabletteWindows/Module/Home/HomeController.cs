using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeTabletteWindows.Module.Home
{
    public class HomeController
    {
        #region Fields
        #region Variables
        private HomeView _view = null;
        private HomeModel _model = null;
        #endregion
        #endregion

        #region Properties  
        public HomeView View
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

        public HomeModel Model
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
        public HomeController(HomeView view)
        {
            this.View = view;
            this.Model = new HomeModel(this);
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
