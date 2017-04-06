using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ViewRaspyHome.Menu.MenuToolbar;

namespace ViewRaspyHome
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
        private BitmapImage imgSource;
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
                return "pack://application:,,,/" + this.FolderProjectName + ";component/" + this.FolderIconName + "/" + _iconLink;
            }

            set
            {
                _iconLink = value;
            }
        }

        public BitmapImage ImgSource
        {
            get
            {
                return imgSource;
            }

            set
            {
                imgSource = value;
                OnPropertyChanged("ImgSource");
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
            this.FolderProjectName = FolderProjectName;
            this.FolderIconName = FolderIconName;
            this.Description = description;
            this.IconLink = iconLink;

            this.ImgSource.BeginInit();
            this.ImgSource.UriSource = new Uri(this.IconLink);
            this.ImgSource.EndInit();


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
