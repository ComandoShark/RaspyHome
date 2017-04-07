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
using ViewRaspyHome.Setting;
using ViewRaspyHome.Menu.MenuToolbar;
using System.Windows.Threading;
using System.Collections.ObjectModel;

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

        private ToolbarButtonView _btnToolbarView;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;

        private string _frameChoose = "";
        private string[,] _arrayBidim = {
            { "Home", "Retourner à l'accueil", "" },
            { "Global setup", "Visualiser l'ensemble des modules", "" },
            { "Information", "Regarder les information du système", "" },
            { "Setting", "Parametrage de l'application", "" }
        };
        private List<string> _listChoise;

        private List<ToolbarButtonData> _lstToolbarButton;

        private DispatcherTimer _dispatcherTimer = null;
        private Window _w = null;
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

        public string FrameChoose
        {
            get
            {
                return _frameChoose;
            }

            set
            {
                _frameChoose = value;
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
            this._w = w;

            this.Loaded += UserControl_Loaded;

            this.Controller = new MenuController(this);
            SetWindowsSize(w.Width, w.Height);
            this.LstToolbarButton = new List<ToolbarButtonData>();
            int i = 0;
            this._listChoise = new List<string>();
            var value = this._arrayBidim.Length - 3;
            for (int a = 0; a <= value; a++)
                if ((a % 3) == 0)
                {
                    stkMenuToolbar.Children.Add(new ToolbarButtonView(this._arrayBidim[i, 0], "ViewRaspyHome", "Icon", this._arrayBidim[i, 1], this._arrayBidim[i, 2]));
                    this.LstToolbarButton.Add(new ToolbarButtonData(this._arrayBidim[i, 0], "ViewRaspyHome", "Icon", this._arrayBidim[i, 1], this._arrayBidim[i, 2]));
                    this._listChoise.Add(this._arrayBidim[i, 0]);
                    i++;
                }
        }
        #endregion

        #region Events
        private void MenuToolbarButton_Click(object sender, EventArgs e)
        {
            switch (((ViewRaspyHome.Menu.MenuToolbar.ToolbarButtonView)sender).WhoseButtonClicked)
            {
                case "Home":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    break;
                case "Global setup":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    break;
                case "Information":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    break;
                case "Setting":
                    this.frame.Content = null;
                    this.frame.NavigationService.RemoveBackEntry();
                    this.frame.Navigate(new SettingView(this.grdFrame));
                    break;
            }
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this._dispatcherTimer = new DispatcherTimer();
            this._dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            this._dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this._dispatcherTimer.Start();

            UpdateMenuToolbar();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //UpdateMenuToolbar();
        }

    }
}
