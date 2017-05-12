using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestLED
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Port used
        private const int LED_PIN = 4;

        // Pin of raspberry
        private GpioPin _pin;
        private DispatcherTimer _timer;
        private bool _isClicked = false;

        public MainPage()
        {
            this.InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += _timer_Tick;
            _timer.Start();

            InitGPIO();

            Unloaded += MainPage_Unloaded;
        }

        private void _timer_Tick(object sender, object e)
        {
            FlipLED();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                _pin = null;
                return;
            }

            this._pin = gpio.OpenPin(LED_PIN);
            this._pin.Write(GpioPinValue.Low);
            this._pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void FlipLED()
        {
            if (this._isClicked)
            {
                this.btnControlLED.Content = "OFF";
                this._pin.Write(GpioPinValue.High);                
            }
            else
            {
                this.btnControlLED.Content = "ON";
                this._pin.Write(GpioPinValue.Low);                
            }
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this._isClicked = false;
            this._pin.Dispose();
            this._timer.Stop();
        }

        private void btnControlLED_Click(object sender, RoutedEventArgs e)
        {
            this._isClicked = !this._isClicked;
        }
    }
}
