using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeTabletWindows.Menu.MenuToolbar
{
    public class ToolbarButtonData
    {
        #region Variables
        private string _frameChoose;
        private string _folderProjectName;
        private string _folderIconName;
        private string _description;
        private string _iconLink;
        private bool _isSelected = false;
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

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                this.IsSelected = value;
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
