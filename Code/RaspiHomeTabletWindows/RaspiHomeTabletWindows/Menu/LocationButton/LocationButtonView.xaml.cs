using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RaspiHomeTabletWindows.Menu.LocationButton
{
    public sealed partial class LocationButtonView : UserControl
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private LocationButtonModel _model = null;

        public event EventHandler _click;

        private bool _isSelected = false;
        private bool _isPressed = false;

        private string _whoseButtonClicked = "";
        #endregion
        #endregion

        #region Properties

        public LocationButtonModel Model
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

                btnButtonLocation.IsTabStop = _isSelected;

                if (value)
                {
                    btnButtonLocation.BorderThickness = new Thickness(3);
                    //btnButtonLocation.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 73, 130, 5));
                }
                else
                {
                    btnButtonLocation.BorderThickness = new Thickness(1);
                    //btnButtonLocation.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 76, 74, 75));
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
        public LocationButtonView(string frameChoose, string description)
        {
            this.InitializeComponent();

            this.Model = new LocationButtonModel(this);

            this.WhoseButtonClicked = frameChoose;

            SetInformation(description);
        }
        #endregion

        #region Events
        private void btnButtonLocation_Click(object sender, RoutedEventArgs e)
        {
            if (this._click != null)
                this._click(this, null);
            this.IsPressed = true;
        }
        #endregion

        #region Methods
        private void SetInformation(string description)
        {
            this.Model.SetInformation(description);
        }
        #endregion

    }
}
