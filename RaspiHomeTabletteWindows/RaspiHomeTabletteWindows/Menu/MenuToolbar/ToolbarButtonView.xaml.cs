using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RaspiHomeTabletteWindows.Menu.MenuToolbar
{
    /// <summary>
    /// Logique d'interaction pour ToolbarButtonView.xaml
    /// </summary>
    public partial class ToolbarButtonView : UserControl
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ToolbarButtonModel _model = null;

        public event EventHandler _click;

        private ToolTip _tTip;

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

                if (_isSelected)
                {
                    btnToolbar.Background = new  (Color.FromArgb(255, 234, 128, 33));
                }
                else
                {
                    btnToolbar.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
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
            if(iconPath != "")
                this.imgMenuToolbarButton.Source = new BitmapImage(new Uri(iconPath));
        }

        public void ShowToolTip(string description)
        {
            this._tTip = new ToolTip();
            this._tTip.Content = description;
            this.btnToolbar.ToolTip = this._tTip;
        }
        #endregion
    }
}
