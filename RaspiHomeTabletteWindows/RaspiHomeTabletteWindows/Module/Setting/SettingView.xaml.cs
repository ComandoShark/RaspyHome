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
using RaspiHomeTabletteWindows.Module.Setting;

namespace RaspiHomeTabletteWindows.Module.Setting
{
    /// <summary>
    /// Logique d'interaction pour SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private SettingController _controller = null;
        #endregion
        #endregion

        #region Properties
        public SettingController Controller
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
        public SettingView(Grid g)
        {
            InitializeComponent();

            this.Controller = new SettingController(this);
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
