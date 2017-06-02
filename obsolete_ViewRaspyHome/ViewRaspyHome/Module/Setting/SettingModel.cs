using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewRaspyHome.Module.Setting
{
    public class SettingModel : PropertyChangedBase
    {
        #region Fields
        #region Constants
        private const double DEFAULT_SIZE_W = 800;
        private const double DEFAULT_SIZE_H = 600;
        #endregion

        #region Variables
        private SettingController controller = null;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;
        #endregion
        #endregion

        #region Properties
        public SettingController Controller
        {
            get
            {
                return controller;
            }

            set
            {
                controller = value;
            }
        }

        public double PageWidth
        {
            get
            {
                return _pageWidth;
            }

            set
            {
                _pageWidth = value;
                OnPropertyChanged("PageWidth");
            }
        }

        public double PageHeight
        {
            get
            {
                return _pageHeight;
            }

            set
            {
                _pageHeight = value;
                OnPropertyChanged("PageHeight");
            }
        }
        #endregion

        #region Constructor
        public SettingModel(SettingController controller)
        {
            this.Controller = controller;
        }
        #endregion

        #region Methods
        public void SetFrameSize(double actualWidth, double actualHeight)
        {
            this.PageWidth = actualWidth;
            this.PageHeight = actualHeight;
        }
        #endregion
    }
}
