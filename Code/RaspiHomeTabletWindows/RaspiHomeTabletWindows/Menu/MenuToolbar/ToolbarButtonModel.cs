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

using Windows.UI.Xaml.Media.Imaging;

namespace RaspiHomeTabletWindows.Menu.MenuToolbar
{
    public class ToolbarButtonModel : PropertyChangedBase
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ToolbarButtonView _view = null;

        private string _description = "";
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
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public ToolbarButtonModel(ToolbarButtonView paramView)
        {
            this.View = paramView;
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        /// <summary>
        /// Set informations for the button
        /// </summary>
        /// <param name="description"></param>
        /// <param name="iconLink"></param>
        public void SetInformation(string description, string iconLink)
        {
            this.Description = description;
            this.IconLink = iconLink;

            if (iconLink != "")
                this.IconPath = "ms-appx:///Icon/" + _iconLink;
            else
                this.IconPath = "";

            ChangeIcon();
        }

        /// <summary>
        /// Change the icon of the button
        /// </summary>
        private void ChangeIcon()
        {
            this.View.ChangeIcon(this.IconPath);
        }
        #endregion
    }
}
