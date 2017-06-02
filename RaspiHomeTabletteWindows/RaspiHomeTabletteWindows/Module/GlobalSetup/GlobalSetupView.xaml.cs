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

namespace RaspiHomeTabletteWindows.Module.GlobalSetup
{
    /// <summary>
    /// Logique d'interaction pour GlobalSetupView.xaml
    /// </summary>
    public partial class GlobalSetupView : UserControl
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private GlobalSetupController _controller = null;
        #endregion
        #endregion

        #region Properties
        public GlobalSetupController Controller
        {
            get
            {
                return _controller;
            }

            set
            {
                _controller = value;
            }
        }
        #endregion

        #region Constructor
        public GlobalSetupView(Grid g)
        {
            InitializeComponent();

            this.Controller = new GlobalSetupController(this);
            SetWindowsSize(g.Width, g.Height);
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        public void SetWindowsSize(double actualWidth, double actualHeight)
        {
            this.Controller.SetFrameSize(actualWidth, actualHeight);
        }
        #endregion
    }
}
