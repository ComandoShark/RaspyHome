/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 8th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeTabletWindows is a program 
 *   compatible with the Windows tablet. It's a 
 *   program that can be use as tactil graphic 
 *   interface to order the component linked with 
 *   the other Raspberry Pi.
\*--------------------------------------------------*/

using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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
                    btnButtonLocation.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 73, 130, 5));
                    tblLocationName.Foreground = new SolidColorBrush(Color.FromArgb(255, 73, 130, 5));
                }
                else
                {
                    btnButtonLocation.BorderThickness = new Thickness(1);
                    btnButtonLocation.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 76, 74, 75));
                    tblLocationName.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
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
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public LocationButtonView(string frameChoose, string description)
        {
            this.InitializeComponent();

            this.Model = new LocationButtonModel(this);

            this.WhoseButtonClicked = frameChoose;

            SetInformation(description);

            this.tblLocationName.Text = description;
        }
        #endregion

        #region Events
        /// <summary>
        /// Event on the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnButtonLocation_Click(object sender, RoutedEventArgs e)
        {
            if (this._click != null)
                this._click(this, null);
            this.IsPressed = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set the Text of the button
        /// </summary>
        /// <param name="description"></param>
        private void SetInformation(string description)
        {
            this.Model.SetInformation(description);
        }
        #endregion

    }
}
