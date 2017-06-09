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

using RaspiHomeTabletWindows.Menu.LocationButton;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RaspiHomeTabletWindows.Modules.Home
{
    public sealed partial class HomeView : Page
    {

        #region Fields
        #region Constants
        #endregion

        #region Varaibles
        private HomeModel _model = null;

        private LocationButtonView _btnLocationButtonView = null;

        private string _frameAlreadyChoose = "";
        private List<string> _buttonInformation = new List<string>() {
            "Maison", "Salon", "Cuisine",
        };

        private List<string> _listChoise = null;

        private List<LocationButtonData> _lstLocationButtonData = null;
        private List<LocationButtonView> _lstLocationButton = null;

        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        #endregion
        #endregion

        #region Properties
        public HomeModel Model
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

        public string FrameAlreadyChoose
        {
            get
            {
                return _frameAlreadyChoose;
            }

            set
            {
                _frameAlreadyChoose = value;
            }
        }

        public List<string> LstChoise
        {
            get
            {
                return _listChoise;
            }

            set
            {
                _listChoise = value;
            }
        }

        public List<LocationButtonData> LstToolbarButtonData
        {
            get
            {
                return _lstLocationButtonData;
            }

            set
            {
                _lstLocationButtonData = value;
            }
        }

        public List<LocationButtonView> LstToolbarButton
        {
            get
            {
                return _lstLocationButton;
            }

            set
            {
                _lstLocationButton = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public HomeView()
        {
            this.InitializeComponent();

            this.Loaded += HomeView_Loaded;

            this.Model = new HomeModel(this);

            InitializeLocationButton();
        }
        #endregion

        #region Events
        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMenuToolbar();
        }

        /// <summary>
        /// Check the button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnToolbarView__click(object sender, EventArgs e)
        {
            foreach (var locationButton in this.LstToolbarButton)
            {
                locationButton.IsSelected = false;
            }

            string buttonClicked = ((LocationButtonView)sender).WhoseButtonClicked;
            this.Model.SetButtonClicked(buttonClicked);

            localSettings.Values["NameButtonClicked"] = buttonClicked;

            var actualFrameChoose = localSettings.Values["NameButtonClicked"];

            switch (buttonClicked)
            {
                case "Maison":
                    if ((actualFrameChoose.ToString() != "Maison") || (actualFrameChoose == null))
                        localSettings.Values.Remove("NameButtonClicked");

                    localSettings.Values["NameButtonClicked"] = buttonClicked;
                    this.frmHome.Content = null;
                    this.frmHome.Navigate(typeof(Location.House.RoomView));
                    break;
                case "Salon":
                    if ((actualFrameChoose.ToString() != "Salon") || (actualFrameChoose == null))
                        localSettings.Values.Remove("NameButtonClicked");

                    localSettings.Values["NameButtonClicked"] = buttonClicked;
                    this.frmHome.Content = null;
                    this.frmHome.Navigate(typeof(Location.OtherRoom.RoomView));
                    break;
                case "Cuisine":
                    if ((actualFrameChoose.ToString() != "Cuisine") || (actualFrameChoose == null))
                        localSettings.Values.Remove("NameButtonClicked");

                    localSettings.Values["NameButtonClicked"] = buttonClicked;
                    this.frmHome.Content = null;
                    this.frmHome.Navigate(typeof(Location.OtherRoom.RoomView));
                    break;
                case "Bureau":
                    if ((actualFrameChoose.ToString() != "Bureau") || (actualFrameChoose == null))
                        localSettings.Values.Remove("NameButtonClicked");

                    localSettings.Values["NameButtonClicked"] = buttonClicked;
                    this.frmHome.Content = null;
                    this.frmHome.Navigate(typeof(Location.OtherRoom.RoomView));
                    break;
            }

            ((LocationButtonView)sender).IsSelected = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the button on the toolbar
        /// </summary>
        private void InitializeLocationButton()
        {
            this.LstToolbarButtonData = new List<LocationButtonData>();
            this.LstToolbarButton = new List<LocationButtonView>();
            this.LstChoise = new List<string>();

            foreach (var buttonName in this._buttonInformation)
            {
                this.LstToolbarButtonData.Add(new LocationButtonData(buttonName, buttonName));
                this.LstChoise.Add(buttonName);
            }
        }

        /// <summary>
        /// Update the toolbar item
        /// </summary>
        private void UpdateMenuToolbar()
        {
            this.stkLocationButton.Children.Clear();
            this.LstToolbarButton.Clear();
            foreach (LocationButtonData t in this.LstToolbarButtonData)
            {
                this._btnLocationButtonView = new LocationButtonView(t.FrameChoose, t.Description);
                this._btnLocationButtonView.Tag = t;
                this._btnLocationButtonView._click += _btnToolbarView__click;
                this.stkLocationButton.Children.Add(this._btnLocationButtonView);
                this.LstToolbarButton.Add(this._btnLocationButtonView);
            }
        }
        #endregion
    }
}
