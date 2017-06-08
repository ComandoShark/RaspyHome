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

namespace RaspiHomeTabletWindows.Menu
{
    public class MenuModel : PropertyChangedBase
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private MenuView _view = null;

        private CommunicationWithServer _comWithServer = null;

        // Use to store data in the cache
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        #endregion
        #endregion

        #region Properties
        public MenuView View
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

        public CommunicationWithServer ComWithServer
        {
            get
            {
                return _comWithServer;
            }

            set
            {
                _comWithServer = value;
            }
        }
        #endregion

        #region Constructor
        public MenuModel(MenuView paramView)
        {
            this.View = paramView;

            this.ComWithServer = new CommunicationWithServer();
        }
        #endregion

        #region Methods
        #endregion
    }
}
