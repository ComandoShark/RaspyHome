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

namespace ViewRaspyHome.Setting
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
        private SettingController controller = null;
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
        #endregion

        #region Constructor
        public SettingView(Frame f)
        {
            InitializeComponent();

            this.Controller = new SettingController(this);
            SetWindowsSize(f.ActualWidth, f.ActualHeight);
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        public void SetWindowsSize(double actualWidth, double actualHeight)
        {
            this.Controller.SetWindowsSize(actualWidth, actualHeight);
        }
        #endregion
    }
}
