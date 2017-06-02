using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RaspiHomeTabletteWindows.Menu.MenuToolbar
{
    public class ToolbarButtonModel : PropertyChangedBase
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ToolbarButtonView _view = null;

        private string _description = "";
        private string _folderProjectName = "";
        private string _folderIconName = "";
        private string _iconLink = null;
        private BitmapImage imgSource = null;
        #endregion
        #endregion

        #region Properties
        public ToolbarButtonView View
        {
            get
            {
                return _view;
            }

            set
            {
                _view = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public string FolderProjectName
        {
            get
            {
                return _folderProjectName;
            }

            set
            {
                _folderProjectName = value;
            }
        }

        public string FolderIconName
        {
            get
            {
                return _folderIconName;
            }

            set
            {
                _folderIconName = value;
            }
        }

        public string IconLink
        {
            get
            {
                return _iconLink;
            }

            set
            {
                _iconLink = value;
            }
        }

        public string IconPath { get; set; }

        public BitmapImage ImgSource
        {
            get
            {
                return imgSource;
            }

            set
            {
                imgSource = value;
            }
        }
        #endregion

        #region Constructor 
        public ToolbarButtonModel(ToolbarButtonView view)
        {
            this.View = view;
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        public void SetInformation(string folderProjectName, string folderIconName, string description, string iconLink)
        {
            this.FolderProjectName = folderProjectName;
            this.FolderIconName = folderIconName;
            this.Description = description;
            this.IconLink = iconLink;

            if (iconLink != "")
                this.IconPath = "pack://application:,,,/" + this.FolderProjectName + ";component/" + this.FolderIconName + "/" + _iconLink;
            else
                this.IconPath = "";

            ChangeIcon();
        }

        private void ChangeIcon()
        {
            this.View.ChangeIcon(this.IconPath);
        }

        private void ShowToolInfo()
        {
            //_tt = new ToolTip();
            //_tt.Content = this.Description;
            //this.btnTool.ToolTip = _tt;
        }
        #endregion
    }
}
