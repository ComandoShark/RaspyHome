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

using RaspiHomeTabletWindows.Menu.MenuToolbar;
using RaspiHomeTabletWindows.Modules.GlobalSetup;
using RaspiHomeTabletWindows.Modules.Home;
using RaspiHomeTabletWindows.Modules.Information;
using RaspiHomeTabletWindows.Modules.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RaspiHomeTabletWindows.Menu
{
    public sealed partial class MenuView : Page
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private MenuModel _model = null;

        private ToolbarButtonView _btnToolbarView = null;

        private string _frameAlreadyChoose = "";
        private Dictionary<string, Dictionary<string, string>> _buttonInformation = new Dictionary<string, Dictionary<string, string>>() {
            { "Home", new Dictionary<string,string>() { { "Retourner à l'accueil" , "Home.png" } } },
            { "Global setup", new Dictionary<string,string>() { { "Visualiser l'ensemble des modules", "GlobalSetup.png" } } },
            { "Information", new Dictionary<string,string>() { { "Regarder les information du système", "Information.png" } } },
            { "Setting", new Dictionary<string,string>() { { "Parametrage de l'application", "Setting.png" } } }
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
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
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
                this.LstToolbarButtonData.Add(new ToolbarButtonData(keyInfo,
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
                this._btnToolbarView = new ToolbarButtonView(t.FrameChoose, t.Description, t.IconLink);
                this._btnToolbarView.Tag = t;
                this._btnToolbarView._click += MenuToolbarButton_Click;
                this.stkMenuToolbar.Children.Add(this._btnToolbarView);
                this.LstToolbarButton.Add(this._btnToolbarView);
            }
        }
        #endregion

    }
}
