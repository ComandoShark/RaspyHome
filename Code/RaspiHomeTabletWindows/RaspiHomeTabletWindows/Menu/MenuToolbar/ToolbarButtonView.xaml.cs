using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RaspiHomeTabletWindows.Menu.MenuToolbar
{
    public sealed partial class ToolbarButtonView : UserControl
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ToolbarButtonModel _model = null;

        public event EventHandler _click;       

        private bool _isSelected = false;
        private bool _isPressed = false;

        private string _whoseButtonClicked = "";
        #endregion
        #endregion

        #region Properties
        public ToolbarButtonModel Model
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

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;

                btnToolbar.IsTabStop = _isSelected;

                if (value)
                {
                    btnToolbar.BorderThickness = new Thickness(6.5);
                    btnToolbar.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 73, 130, 5));
                }
                else
                {
                    btnToolbar.BorderThickness = new Thickness(5);
                    btnToolbar.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 76, 74, 75));
                }
            }
        }

        public bool IsPressed
        {
            get
            {
                return _isPressed;
            }

            set
            {
                _isPressed = value;
            }
        }

        public string WhoseButtonClicked
        {
            get
            {
                return _whoseButtonClicked;
            }

            set
            {
                _whoseButtonClicked = value;
            }
        }
        #endregion

        #region Constructor
        public ToolbarButtonView(string frameChoose, string folderProjectName, string folderIconName, string description, string iconLink)
        {
            InitializeComponent();

            this.Model = new ToolbarButtonModel(this);

            this.WhoseButtonClicked = frameChoose;

            SetInformation(folderProjectName, folderIconName, description, iconLink);
        }
        #endregion

        #region Events
        private void btnToolbar_Click(object sender, RoutedEventArgs e)
        {
            if (this._click != null)
                this._click(this, null);
            this.IsPressed = true;
        }
        #endregion

        #region Methods
        private void SetInformation(string folderProjectName, string folderIconName, string description, string iconLink)
        {
            this.Model.SetInformation(folderProjectName, folderIconName, description, iconLink);
        }


        public void ChangeIcon(string iconPath)
        {
            if (iconPath != "")
                this.imgMenuToolbarButton.Source = new BitmapImage(new Uri(iconPath));
        }
        #endregion
    }
}
