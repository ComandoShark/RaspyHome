/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 8th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeTabletWindows is a program 
 *   compatible with the Windows tablet. It's a 
 *   program that can be use as tactil graphic 
 *   interface to order the component linked with 
 *   the other Raspberry Pi.
\*--------------------------------------------------*/

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
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
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
