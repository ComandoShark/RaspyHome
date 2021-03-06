﻿/*--------------------------------------------------*\
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RaspiHomeTabletWindows.Modules.Home.Location.OtherRoom
{
    public sealed partial class OtherRoomView : Page
    {
        #region Fields
        #region Constants
        #endregion

        #region Varaibles
        private OtherRoomModel _model = null;

        private string _strTemp = "";
        private string _strHumi = "";
        private string _strPres = "";

        private bool _isOn = false;
        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isOpen = false;
        private bool _isClose = false;
        
        private bool _upActivate = false;
        private bool _downActivate = false;
        #endregion
        #endregion

        #region Properties
        public OtherRoomModel Model
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

        public string StrTemp
        {
            get
            {
                return _strTemp;
            }

            set
            {
                _strTemp = value;

                this.tblTemperature.Text = value + " °C";
            }
        }

        public string StrHumi
        {
            get
            {
                return _strHumi;
            }

            set
            {
                _strHumi = value;

                this.tblHumidity.Text = value + " %";
            }
        }

        public string StrPres
        {
            get
            {
                return _strPres;
            }

            set
            {
                _strPres = value;

                this.tblPressure.Text = value + " [hPa]";
            }
        }

        public bool IsOn
        {
            get
            {
                return _isOn;
            }

            set
            {
                _isOn = value;

                if (value)
                {
                    // Send message to save
                    this.Model.SendMessage("allumer", "lumiere");
                    // Change the picture
                    this.imgLightButton.Source = new BitmapImage(new Uri("ms-appx:///Icon/bulbLighting.png"));
                }
                else
                {
                    // Save message to save
                    this.Model.SendMessage("eteindre", "lumiere");
                    // Change the picture
                    this.imgLightButton.Source = new BitmapImage(new Uri("ms-appx:///Icon/bulb.png"));
                }
            }
        }

        public bool IsUp
        {
            get
            {
                return _isUp;
            }

            set
            {
                _isUp = value;
            }
        }

        public bool IsDown
        {
            get
            {
                return _isDown;
            }

            set
            {
                _isDown = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }

            set
            {
                _isOpen = value;
            }
        }

        public bool IsClose
        {
            get
            {
                return _isClose;
            }

            set
            {
                _isClose = value;
            }
        }

        public bool UpActivate
        {
            get
            {
                return _upActivate;
            }

            set
            {
                _upActivate = value;
            }
        }

        public bool DownActivate
        {
            get
            {
                return _downActivate;
            }

            set
            {
                _downActivate = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public OtherRoomView()
        {
            this.InitializeComponent();

            this.imgThermometer.Visibility = Visibility.Collapsed;
            this.imgHumidity.Visibility = Visibility.Collapsed;
            this.imgBarometer.Visibility = Visibility.Collapsed;

            this.Model = new OtherRoomModel(this);
        }
        #endregion

        #region Events
        /// <summary>
        /// Light control
        /// </summary>
        private void btnLightOnOff_Click(object sender, RoutedEventArgs e)
        {
            this.IsOn = !this.IsOn;
        }

        /// <summary>
        /// Store control
        /// </summary>
        private void btnStoreUp_Click(object sender, RoutedEventArgs e)
        {
            this.IsUp = true;
            // 2 states on the button; UP and STOP
            if (!this.UpActivate)
            {
                this.Model.SendMessage("monter", "store");
                this.UpActivate = true;
                this.DownActivate = false;
            }
            else
            {
                this.Model.SendMessage("stop", "store");
                this.UpActivate = false;
            }
        }

        private void btnStoreDown_Click(object sender, RoutedEventArgs e)
        {
            this.IsDown = true;
            // 2 states on the button; DOWN and STOP
            if (!this.DownActivate)
            {
                this.Model.SendMessage("descendre", "store");
                this.DownActivate = true;
                this.UpActivate = false;
            }
            else
            {
                this.Model.SendMessage("stop", "store");
                this.DownActivate = false;
            }
        }

        private void btnStoreOpen_Click(object sender, RoutedEventArgs e)
        {
            this.IsOpen = true;
            this.Model.SendMessage("ouvrir", "store");
        }

        private void btnStoreClose_Click(object sender, RoutedEventArgs e)
        {
            this.IsClose = true;
            this.Model.SendMessage("fermer", "store");

        }
        #endregion

        #region Methods
        /// <summary>
        /// Set to visible the state values if exist
        /// </summary>
        public void EnableDisplayState()
        {
            this.imgThermometer.Visibility = Visibility.Visible;
            this.imgHumidity.Visibility = Visibility.Visible;
            this.imgBarometer.Visibility = Visibility.Visible;
        }
        #endregion     
    }
}
