using RaspiHomeTabletWindows.Menu.MenuToolbar;
using RaspiHomeTabletWindows.Modules.GlobalSetup;
using RaspiHomeTabletWindows.Modules.Home;
using RaspiHomeTabletWindows.Modules.Information;
using RaspiHomeTabletWindows.Modules.Setting;
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

namespace RaspiHomeTabletWindows.Menu
{
    public sealed partial class MenuView : Page
    {
        #region Fields
        #region Constants
        private const double DEFAULT_SIZE_W = 800;
        private const double DEFAULT_SIZE_H = 600;
        #endregion

        #region Variables
        private MenuModel _model = null;

        private ToolbarButtonView _btnToolbarView = null;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;

        private string _frameAlreadyChoose = "";
        private Dictionary<string, Dictionary<string, string>> _buttonInformation = new Dictionary<string, Dictionary<string, string>>() {
            { "Home", new Dictionary<string,string>() { { "Retourner à l'accueil" , ""} } },
            { "Global setup", new Dictionary<string,string>() { { "Visualiser l'ensemble des modules", "" } } },
            { "Information", new Dictionary<string,string>() { { "Regarder les information du système", "" } } },
            { "Setting", new Dictionary<string,string>() { { "Parametrage de l'application", "" } } }
        };

        private List<string> _listChoise = null;

        private List<ToolbarButtonData> _lstToolbarButtonData = null;
        private List<ToolbarButtonView> _lstToolbarButton = null;
        #endregion
        #endregion

        #region Properties
        public MenuModel Model
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

        public List<ToolbarButtonData> LstToolbarButtonData
        {
            get
            {
                return _lstToolbarButtonData;
            }

            set
            {
                _lstToolbarButtonData = value;
            }
        }

        public List<ToolbarButtonView> LstToolbarButton
        {
            get
            {
                return _lstToolbarButton;
            }

            set
            {
                _lstToolbarButton = value;
            }
        }
        #endregion

        #region Constructor
        public MenuView()
        {
            this.InitializeComponent();

            this.Loaded += UserControl_Loaded;

            this.Model = new MenuModel(this);            

            InitializeToolbarButton();
        }
        #endregion

        #region Event
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMenuToolbar();
        }

        private void MenuToolbarButton_Click(object sender, EventArgs e)
        {
            foreach (var toolbarButton in this.LstToolbarButton)
            {
                toolbarButton.IsSelected = false;
            }

            switch (((ToolbarButtonView)sender).WhoseButtonClicked)
            {
                case "Home":                    
                    this.frmMenu.Content = null;
                    this.frmMenu.Navigate(typeof(HomeView));
                    break;
                case "Global setup":                    
                    this.frmMenu.Content = null;
                    this.frmMenu.Navigate(typeof(GlobalSetupView));
                    break;
                case "Information":
                    this.frmMenu.Content = null;
                    this.frmMenu.Navigate(typeof(InformationView));
                    break;
                case "Setting":
                    this.frmMenu.Content = null;
                    this.frmMenu.Navigate(typeof(SettingView));                    
                    break;
            }

            ((ToolbarButtonView)sender).IsSelected = true;
        }
        #endregion

        #region Methods
        private void InitializeToolbarButton()
        {
            this.LstToolbarButtonData = new List<ToolbarButtonData>();
            this.LstToolbarButton = new List<ToolbarButtonView>();
            this.LstChoise = new List<string>();

            foreach (var keyInfo in this._buttonInformation.Keys)
            {
                this.LstToolbarButtonData.Add(new ToolbarButtonData(keyInfo, "RaspiHomeTabletWindows", "Icon", 
                    this._buttonInformation[keyInfo].Keys.FirstOrDefault(), 
                    this._buttonInformation[keyInfo][this._buttonInformation[keyInfo].Keys.FirstOrDefault()]));
                this.LstChoise.Add(keyInfo);
            }
        }

        private void UpdateMenuToolbar()
        {
            //foreach (ToolbarButtonView t in this.stkMenuToolbar.Children)
            //    t._click -= MenuToolbarButton_Click;
            this.stkMenuToolbar.Children.Clear();
            this.LstToolbarButton.Clear();
            foreach (ToolbarButtonData t in this.LstToolbarButtonData)
            {
                this._btnToolbarView = new ToolbarButtonView(t.FrameChoose, t.FolderProjectName, t.FolderIconName, t.Description, t.IconLink);
                this._btnToolbarView.Tag = t;
                this._btnToolbarView._click += MenuToolbarButton_Click;
                this.stkMenuToolbar.Children.Add(this._btnToolbarView);
                this.LstToolbarButton.Add(this._btnToolbarView);
            }
        }
        #endregion

    }
}
