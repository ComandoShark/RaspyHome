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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RaspiHomeTabletWindows.Modules.Home.Location.OtherRoom
{
    public sealed partial class RoomView : Page
    {
        #region Fields
        #region Constants
        #endregion

        #region Varaibles
        private RoomModel _model = null;

        private string _strTemp = "";
        private string _strHumi = "";
        private string _strPres = "";

        private bool _isOn = false;
        private bool _isUp = false;
        private bool _isDown = false;
        private bool _isOpen = false;
        private bool _isClose = false;
        #endregion
        #endregion

        #region Properties
        public RoomModel Model
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
                    this.Model.SendMessage("allumer", "lumiere");
                    this.imgLightButton.Source = new BitmapImage(new Uri("ms-appx:///Icon/bulbLighting.png"));
                }
                else
                {
                    this.Model.SendMessage("eteindre", "lumiere");
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
        #endregion

        #region Constructor
        public RoomView()
        {
            this.InitializeComponent();

            this.Model = new RoomModel(this);
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
        ///

        /// <summary>
        /// Store control
        /// </summary>
        private void btnStoreUp_Click(object sender, RoutedEventArgs e)
        {
            this.IsUp = true;
            this.Model.SendMessage("monter", "store");
        }

        private void btnStoreDown_Click(object sender, RoutedEventArgs e)
        {
            this.IsDown = true;
            this.Model.SendMessage("descendre", "store");
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
        ///
        #endregion

        #region Methods
        public void EnableDisplayState()
        {
            this.imgThermometer.Visibility = Visibility.Visible;
            this.imgHumidity.Visibility = Visibility.Visible;
            this.imgBarometer.Visibility = Visibility.Visible;
        }
        #endregion     
    }
}
