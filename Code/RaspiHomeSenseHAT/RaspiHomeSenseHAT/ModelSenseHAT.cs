using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private ISenseHat _senseHat;
        private ISenseHatDisplay _senseHatDisplay;
        SenseHatData _data;
        private Color _uiColor = Color.FromArgb(255, 0, 0, 0);

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
        public ModelSenseHAT(ViewSenseHAT paramView)
        {
            this.VSenseHAT = paramView;
            this.ComWithServer = new CommunicationWithServer(this);

            InitializeSenseHat();
            this.VSenseHAT.IPRasp = this.ComWithServer.GetHostName().ToString();
        }
        #endregion

        #region Methods
        public async void InitializeSenseHat()
        {
            this._senseHat = await SenseHatFactory.GetSenseHat();
            this._senseHatDisplay = this._senseHat.Display;
            this._senseHatDisplay.Fill(_uiColor);

            InitializeTimer();
        }

        private void InitializeTimer()
        {            
            //initialize the timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(TICK);
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            SetValue();
        }

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

        public string SendValues()
        {
            return "TEMP=" + Math.Round((decimal)this.Data.Temperature) + ";" + "HUMI" + Math.Round((decimal)this.Data.Humidity) + ";" + "PRES=" + Math.Round((decimal)this.Data.Pressure);
        }
        #endregion
    }
}
