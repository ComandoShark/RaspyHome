using RaspiHomeTabletWindows.Menu.LocationButton;
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

namespace RaspiHomeTabletWindows.Modules.Home
{
    public sealed partial class HomeView : Page
    {

        #region Fields
        #region Constants
        private const double DEFAULT_SIZE_W = 800;
        private const double DEFAULT_SIZE_H = 600;
        #endregion

        #region Varaibles
        private HomeModel _model = null;

        private LocationButtonView _btnLocationButtonView = null;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;

        private string _frameAlreadyChoose = "";
        private List<string> _buttonInformation = new List<string>() {
            "Maison", "Salon", "Cuisine"
        };

        private List<string> _listChoise = null;

        private List<LocationButtonData> _lstLocationButtonData = null;
        private List<LocationButtonView> _lstLocationButton = null;
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

        public double PageWidth
        {
            get
            {
                return _pageWidth;
            }

            set
            {
                _pageWidth = value;
            }
        }

        public double PageHeight
        {
            get
            {
                return _pageHeight;
            }

            set
            {
                _pageHeight = value;
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


        private void _btnToolbarView__click(object sender, EventArgs e)
        {
            foreach (var locationButton in this.LstToolbarButton)
            {
                locationButton.IsSelected = false;
            }

            switch (((LocationButtonView)sender).WhoseButtonClicked)
            {
                case "Maison":
                    this.frmHome.Content = null;
                    this.frmHome.Navigate(typeof(Location.House.RoomView));
                    break;
                case "Salon":
                    this.frmHome.Content = null;
                    this.frmHome.Navigate(typeof(Location.LivingRoom.RoomView));
                    break;
                //case "Cuisine":
                //    this.frmHome.Content = null;
                //    this.frmHome.Navigate(typeof());
                //    break;
                //case "Setting":
                //    this.frmHome.Content = null;
                //    this.frmHome.Navigate(typeof(SettingView));
                //    break;
            }

            ((LocationButtonView)sender).IsSelected = true;
        }
        #endregion

        #region Methods

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


        private void UpdateMenuToolbar()
        {
            //foreach (ToolbarButtonView t in this.stkMenuToolbar.Children)
            //    t._click -= MenuToolbarButton_Click;
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
