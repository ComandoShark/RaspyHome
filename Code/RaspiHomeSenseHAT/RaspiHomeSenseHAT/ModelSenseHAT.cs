/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeSenseHAT is a program who use a 
 *   Sense HAT, it's an electronic card who can be 
 *   mesured value with sensor. This program use 
 *   the Sense HAT to mesure the temperature, the 
 *   humidity and the pressure. 
\*--------------------------------------------------*/

using System;
using Emmellsoft.IoT.Rpi.SenseHat;
using Windows.UI;
using Windows.UI.Xaml;

namespace RaspiHomeSenseHAT
{
    public class ModelSenseHAT
    {
        #region Fields
        #region Constants        
        private const int TICK = 1;
        #endregion

        #region Variables
        private ViewSenseHAT _vSenseHAT;
        private CommunicationWithServer _comWithServer;

        // Sense HAT librairy
        private ISenseHat _senseHat;
        private ISenseHatDisplay _senseHatDisplay;
        SenseHatData _data;

        // Set default color matrix to OFF
        private Color _uiColor = Color.FromArgb(0, 0, 0, 0);

        private string _message = "";
        #endregion
        #endregion

        #region Properties
        public ViewSenseHAT VSenseHAT
        {
            get
            {
                return _vSenseHAT;
            }

            set
            {
                _vSenseHAT = value;
            }
        }

        public SenseHatData Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
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

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        /// <param name="paramView"></param>
        public ModelSenseHAT(ViewSenseHAT paramView)
        {
            // Communication like Model-View
            this.VSenseHAT = paramView;

            // Initilize the communication with the server
            this.ComWithServer = new CommunicationWithServer(this);

            // Initilize the Sense HAT (don't need to be initialized before the communication start because it's only a sensor)
            InitializeSenseHat();

            // Display the IP address of the Raspberry Pi
            this.VSenseHAT.IPRasp = this.ComWithServer.GetHostName().ToString();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the Sense HAT
        /// </summary>
        public async void InitializeSenseHat()
        {
            this._senseHat = await SenseHatFactory.GetSenseHat();
            this._senseHatDisplay = this._senseHat.Display;
            this._senseHatDisplay.Fill(_uiColor);

            InitializeTimer();
        }

        /// <summary>
        /// Initialize the timer
        /// </summary>
        private void InitializeTimer()
        {                        
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(TICK);
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            SetValue();
        }

        /// <summary>
        /// Set the value get with sensor
        /// </summary>
        public void SetValue()
        {
            this._senseHat.Sensors.HumiditySensor.Update();
            this._senseHat.Sensors.PressureSensor.Update();
            this._senseHatDisplay.Update();

            this.Data = new SenseHatData();
            this.Data.Temperature = this._senseHat.Sensors.Temperature;
            this.Data.Humidity = this._senseHat.Sensors.Humidity;
            this.Data.Pressure = this._senseHat.Sensors.Pressure;

            this.VSenseHAT.Temperature = this.Data.Temperature.ToString();
            this.VSenseHAT.Humidity = this.Data.Humidity.ToString();
            this.VSenseHAT.Pressure = this.Data.Pressure.ToString();
        }

        /// <summary>
        /// Send the values with a special format: "TEMP=x;HUMI=y;PRES=z"
        /// </summary>
        /// <returns></returns>
        public string SendValues()
        {
            return "TEMP=" + Math.Round((decimal)this.Data.Temperature) + ";" + "HUMI=" + Math.Round((decimal)this.Data.Humidity) + ";" + "PRES=" + Math.Round((decimal)this.Data.Pressure);
        }
        #endregion
    }
}
