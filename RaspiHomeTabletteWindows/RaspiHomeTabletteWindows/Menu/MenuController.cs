using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeTabletteWindows
{
    public class MenuController
    {
        #region Fields
        #region Variables
        private MenuView _view = null;
        private MenuModel _model = null;
        #endregion
        #endregion

        #region Properties  
        public MenuView View
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

        public MenuModel Model
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
        public MenuController(MenuView view)
        {
            this.View = view;
            this.Model = new MenuModel(this);
        }
        #endregion

        #region Methods
        public void SetWindowsSize(double actualWidth, double actualHeight)
        {
            this.Model.SetWindowsSize(actualWidth, actualHeight);
        }
        #endregion
    }
}
