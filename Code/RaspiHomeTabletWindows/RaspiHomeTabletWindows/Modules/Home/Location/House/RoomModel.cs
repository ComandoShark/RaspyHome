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

namespace RaspiHomeTabletWindows.Modules.Home.Location.House
{
    public class RoomModel
    {
        #region Fields
        #region Constants
        #endregion

        #region Varaibles
        private RoomView _view = null;

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        #endregion
        #endregion

        #region Properties
        public RoomView View
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
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public RoomModel(RoomView paramView)
        {
            this.View = paramView;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Save message to be send
        /// </summary>
        /// <param name="action"></param>
        /// <param name="component"></param>
        public void SendMessage(string action, string component)
        {
            localSettings.Values["SendMessageToServer"] = action + " " + component;
        }
        #endregion
    }
}
