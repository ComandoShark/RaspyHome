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

namespace ViewRaspyHome.Menu.MenuToolbar
{
    /// <summary>
    /// Logique d'interaction pour ToolbarButton.xaml
    /// </summary>
    public partial class ToolbarButtonView : UserControl
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ToolbarButtonModel _model = null;

        private event EventHandler _click;
        private ToolTip _tTip;
        
        #endregion
        #endregion

        #region Properties
        public ToolbarButtonModel Model
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
        #endregion

        #region Constructor
        public ToolbarButtonView(string folderProjectName, string folderIconName, string description, string iconLink)
        {
            InitializeComponent();

            this.Model = new ToolbarButtonModel(this);

            SetInformation(folderProjectName, folderIconName, description, iconLink);
        }
        #endregion

        #region Events
        private void btnToolbar_Click(object sender, RoutedEventArgs e)
        {
            if (this._click != null)
                this._click(this, null);
        }
        #endregion

        #region Methods
        private void SetInformation(string folderProjectName, string folderIconName, string description, string iconLink)
        {
            this.Model.SetInformation(folderProjectName, folderIconName, description, iconLink);
        }

        public void ShowToolTip(string description)
        {
            this._tTip = new ToolTip();
            this._tTip.Content = description;
            this.btnToolbar.ToolTip = this._tTip;
        }
        #endregion
    }
}
