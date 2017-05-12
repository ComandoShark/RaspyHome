using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Globalization;
using System.Reflection;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Emmellsoft.IoT.Rpi.SenseHat;
using Windows.UI;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TesSenseHAT
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ISenseHat senseHat;
        ISenseHatDisplay senseHatDisplay;
        bool _isClicked = false;
        Color color;

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;            
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //get a reference to SenseHat
            senseHat = await SenseHatFactory.GetSenseHat();
            senseHatDisplay = senseHat.Display;
            senseHatDisplay.Fill(Color.FromArgb(255, 0, 0, 0));

            //initialize the timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            senseHat.Sensors.HumiditySensor.Update();
            senseHat.Sensors.PressureSensor.Update();
            senseHatDisplay.Update();

            //gather data
            SenseHatData data = new SenseHatData();
            data.Temperature = senseHat.Sensors.Temperature;
            data.Humidity = senseHat.Sensors.Humidity;
            data.Pressure = senseHat.Sensors.Pressure;

            //notify UI
            tbxTemp.Text = data.Temperature.ToString();
            tbxHum.Text = data.Humidity.ToString();
            tbxPres.Text = data.Pressure.ToString();

            senseHatDisplay.Clear();
            if (this._isClicked)
            {
                btnMatrix.Content = "ON";
                senseHatDisplay.Fill(Color.FromArgb(255, 255, 255, 255));
            }
            else
            {
                btnMatrix.Content = "OFF";
                senseHatDisplay.Fill(Color.FromArgb(255, 0, 0, 0));
            }
        }

        private void btnMatrix_Click(object sender, RoutedEventArgs e)
        {
            this._isClicked = !this._isClicked;
        }
    }
}
