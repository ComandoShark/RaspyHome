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

       // private ToolbarButtonView _btnToolbar;

        private double _pageWidth = DEFAULT_SIZE_W;
        private double _pageHeight = DEFAULT_SIZE_H;

        private string _frameChoose = "";
        private string[,] _arrayBidim = { { "Home", "" }, { "Setting", "" } };
        private List<string> _listChoise;
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
        #endregion

        #region Constructor
        public MenuView(Window w)
        {
            InitializeComponent();

            this.Controller = new MenuController(this);
            SetWindowsSize(w.Width, w.Height);

            stkMenuToolbar.Children.Add(new ToolbarButtonView(this._arrayBidim[0, 0], "ViewRaspyHome", "Icon", "Retour à la maison", this._arrayBidim[0,1]));
            stkMenuToolbar.Children.Add(new ToolbarButtonView(this._arrayBidim[1, 0], "ViewRaspyHome", "Icon", "Réglage de paramettre", this._arrayBidim[1, 1]));

            int i = 0;
            this._listChoise = new List<string>();
            for (int a = 0; a < this._arrayBidim.Length; a++)
                if ((a % 2) == 0)
                {
                    this._listChoise.Add(this._arrayBidim[i, 0]);
                    i++;
                }
        }
        #endregion

        #region Events
        private void MenuToolbarButton_Click(object sender, EventArgs e)
        {
            switch (this._listChoise.ToString())
            {
                case "Home":
                    break;
                case "Setting":
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
            foreach (ToolbarButtonView t in stkMenuToolbar.Children)
            {
                t._click -= MenuToolbarButton_Click;
                if (t.IsPressed)
                {
                    this.FrameChoose = t.WhoseButtonClicked;
                    t._click += MenuToolbarButton_Click;                    
                }
            }
        }
        #endregion
    }
}
