using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewRaspyHome.Menu.MenuToolbar;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using ViewRaspyHome.Module.Home;
using ViewRaspyHome.Module.GlobalSetup;
using ViewRaspyHome.Module.Information;
using ViewRaspyHome.Module.Setting;

namespace ViewRaspyHome
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        #region Fields
        #region Constants
        private const double DEFAULT_SIZE_W = 800;
        private const double DEFAULT_SIZE_H = 600;
        #endregion

        #region Variables
        private MenuController _controller = null;

        private ToolbarButtonView _btnToolbarView = null;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;

        private string _frameAlreadyChoose = "";
        private string[,] _arrayBidim = {
            { "Home", "Retourner à l'accueil", "" },
            { "Global setup", "Visualiser l'ensemble des modules", "" },
            { "Information", "Regarder les information du système", "" },
            { "Setting", "Parametrage de l'application", "" }
        };
        private List<string> _listChoise = null;

        private List<ToolbarButtonData> _lstToolbarButton = null;
        #endregion
        #endregion

        #region Properties
        public MenuController Controller
        {
            get
            {
                return _controller;
            }

            set
            {
                _controller = value;
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
        
        public List<string> ListChoise
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

        public List<ToolbarButtonData> LstToolbarButton
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
        public MenuView(Window w)
        {
            InitializeComponent();

            this.Loaded += UserControl_Loaded;

            this.Controller = new MenuController(this);
            SetWindowsSize(w.Width, w.Height);
            this.LstToolbarButton = new List<ToolbarButtonData>();
            int i = 0;
            this.ListChoise = new List<string>();
            var value = this._arrayBidim.Length - 3;
            for (int a = 0; a <= value; a++)
                if ((a % 3) == 0)
                {
                    stkMenuToolbar.Children.Add(new ToolbarButtonView(this._arrayBidim[i, 0], "ViewRaspyHome", "Icon", this._arrayBidim[i, 1], this._arrayBidim[i, 2]));
                    this.LstToolbarButton.Add(new ToolbarButtonData(this._arrayBidim[i, 0], "ViewRaspyHome", "Icon", this._arrayBidim[i, 1], this._arrayBidim[i, 2]));
                    this.ListChoise.Add(this._arrayBidim[i, 0]);
                    i++;
                }
        }
        #endregion

        #region Events
        private void MenuToolbarButton_Click(object sender, EventArgs e)
        {
            switch (((ToolbarButtonView)sender).WhoseButtonClicked)
            {
                case "Home":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    this.frame.Navigate(new HomeView(this.grdFrame));
                    break;
                case "Global setup":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    this.frame.Navigate(new GlobalSetupView(this.grdFrame));
                    break;
                case "Information":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    this.frame.Navigate(new InformationView(this.grdFrame));
                    break;
                case "Setting":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    this.frame.Navigate(new SettingView(this.grdFrame));
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMenuToolbar();
        }
        #endregion

        #region Methods
        public void SetWindowsSize(double actualWidth, double actualHeight)
        {
            this.Controller.SetWindowsSize(actualWidth, actualHeight);
        }

        private void UpdateMenuToolbar()
        {
            foreach (ToolbarButtonView t in this.stkMenuToolbar.Children)
                t._click -= MenuToolbarButton_Click;

            stkMenuToolbar.Children.Clear();

            foreach (ToolbarButtonData t in this.LstToolbarButton)
            {
                this._btnToolbarView = new ToolbarButtonView(t.FrameChoose, t.FolderProjectName, t.FolderIconName, t.Description, t.IconLink);
                this._btnToolbarView.Tag = t;
                this._btnToolbarView._click += MenuToolbarButton_Click;
                stkMenuToolbar.Children.Add(this._btnToolbarView);
            }
        }
        #endregion      
    }
}
