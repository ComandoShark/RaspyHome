using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewRaspyHome.Menu.MenuToolbar
{
    public class ToolbarButtonData : ObservableCollection<ToolbarButtonView>
    {
        #region Variables
        private string _frameChoose;
        private string _folderProjectName;
        private string _folderIconName;
        private string _description;
        private string _iconLink;
        #endregion

        #region Properties
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
        #endregion

        #region Constructor 
        public ToolbarButtonData(string frameChoose, string folderProjectName, string folderIconName, string description, string iconLink)
        {
            this.FrameChoose = frameChoose;
            this.FolderProjectName = folderProjectName;
            this.FolderIconName = folderIconName;
            this.Description = description;
            this.IconLink = iconLink;
        }
        #endregion
    }
}
