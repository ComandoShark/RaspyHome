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

using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RaspiHomeTabletWindows.Modules.Home.Location.OtherRoom
{
    public class RoomModel
    {
        #region Fields
        #region Constants
        #endregion

        #region Varaibles
        private RoomView _view = null;

        private string _messageReaded = "";

        DispatcherTimer _dTimer = null;

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

        public string MessageReceive
        {
            get
            {
                return _messageReaded;
            }

            set
            {
                _messageReaded = value;
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

            this._dTimer = new DispatcherTimer();
            this._dTimer.Interval = new TimeSpan(200);
            this._dTimer.Tick += _dTimer_Tick; ;

            this._dTimer.Start();

            this.InitializeState();
        }

        private void _dTimer_Tick(object sender, object e)
        {
            if (localSettings.Values["ReceiveMessageFromServer"] != null)
            {
                var messageToSend = localSettings.Values["ReceiveMessageFromServer"];
                this.MessageReceive = messageToSend.ToString();
                UpDateView();
                localSettings.Values.Remove("ReceiveMessageFromServer");

                this._dTimer.Stop();
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Initialize at the start (check if the sense hat exist)
        /// </summary>
        private void InitializeState()
        {
            // Update state room     
            var locationName = localSettings.Values["NameButtonClicked"];

            if (locationName != null)
                localSettings.Values["SendMessageToServer"] = "etat " + locationName;
            else
                localSettings.Values["SendMessageToServer"] = "etat salon";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Save value to be send
        /// </summary>
        /// <param name="action"></param>
        /// <param name="component"></param>
        public void SendMessage(string action, string component)
        {
            var locationName = localSettings.Values["NameButtonClicked"];

            if (locationName != null)
                localSettings.Values["SendMessageToServer"] = action + " " + component + " " + locationName;            
        }

        /// <summary>
        /// Update state values on the view
        /// </summary>
        private async void UpDateView()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(200));

            this.View.EnableDisplayState();

            var informations = this.MessageReceive.Split(';');
            foreach (var info in informations)
            {
                switch (info.Split('=').First())
                {
                    case "TEMP":
                        this.View.StrTemp = info.Split('=').Last();
                        break;
                    case "HUMI":
                        this.View.StrHumi = info.Split('=').Last();
                        break;
                    case "PRES":
                        this.View.StrPres = info.Split('=').Last();
                        break;
                }
            }
        }
        #endregion
    }
}
