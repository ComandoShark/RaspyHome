using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeTabletWindows.Menu.LocationButton
{
    public class LocationButtonModel : PropertyChangedBase
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private LocationButtonView _view = null;

        private string _description = "";
        private string _folderProjectName = "";
        #endregion
        #endregion

        #region Properties

        public LocationButtonView View
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

        public string LocationName
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                OnPropertyChanged("LocationName");
            }
        }
        #endregion

        #region Constructor
        public LocationButtonModel(LocationButtonView paramView)
        {
            this.View = paramView;
        }
        #endregion

        #region Methods
        public void SetInformation(string description)
        {
            this.LocationName = description;
        }
        #endregion
    }
}
