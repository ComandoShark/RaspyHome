using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ViewRaspyHome.MenuView
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class MenuView : Page
    {
        #region Fields
        #region Constants
        private const double DEFAULT_SIZE_W = 800;
        private const double DEFAULT_SIZE_H = 600;
        #endregion

        #region Variables
        private MenuController controller = null;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;
        #endregion
        #endregion

        #region Properties
        public MenuController Controller
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
            }
        }
        #endregion

        #region Constructor
        public MenuView(Window w)
        {
            this.Controller = new MenuController(this);
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        #endregion
    }
}
