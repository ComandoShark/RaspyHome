using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeTabletWindows.Menu.LocationButton
{
    public class LocationButtonData
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private string _frameChoose;
        private string _description;
        private bool _isSelected = false;
        #endregion
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
        public LocationButtonData(string frameChoose, string description)
        {
            this.FrameChoose = frameChoose;
            this.Description = description;
        }
        #endregion

        #region Methods
        #endregion
    }
}
