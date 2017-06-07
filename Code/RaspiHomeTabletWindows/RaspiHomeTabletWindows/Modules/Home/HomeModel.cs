using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeTabletWindows.Modules.Home
{
    public class HomeModel : PropertyChangedBase
    {
        #region Fields
        #region Constants
        private const double DEFAULT_SIZE_W = 800;
        private const double DEFAULT_SIZE_H = 600;
        #endregion

        #region Varaibles
        private HomeView _view = null;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;
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

        public double PageWidth
        {
            get
            {
                return _pageWidth;
            }

            set
            {
                _pageWidth = value;
                //OnPropertyChanged("PageWidth");
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
                //OnPropertyChanged("PageHeight");
            }
        }
        #endregion

        #region Constructor
        public HomeModel(HomeView paramView)
        {
            this.View = paramView;
        }
        #endregion

        #region Methods
        #endregion
    }
}
