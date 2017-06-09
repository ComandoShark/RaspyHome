using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace RaspiHomeSpeechNSynthetize
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields
        private Speecher _speecher;
        #endregion

        #region Constuctor
        public MainPage()
        {
            this.InitializeComponent();

            this._speecher = new Speecher();            
        }
        #endregion

        #region Event
        private void btnLightCommand1_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxLightCommand1.Text != "")
                this._speecher.SendBrutCommand(this.tbxLightCommand1.Text);
            else
                this._speecher.SendBrutCommand("Allumer lumiere");
        }

        private void btnLightCommand2_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxLightCommand2.Text != "")
                this._speecher.SendBrutCommand(this.tbxLightCommand2.Text);
            else
                this._speecher.SendBrutCommand("Eteindre lumiere");
        }

        private void btnState_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxState.Text != "")
                this._speecher.SendBrutCommand(this.tbxState.Text);
            else
                this._speecher.SendBrutCommand("Temperature du salon");
        }

        private void btnStore1_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxStore1.Text != "")
                this._speecher.SendBrutCommand(this.tbxStore1.Text);
            else
                this._speecher.SendBrutCommand("Monter store");
        }

        private void btnStore2_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxStore2.Text != "")
                this._speecher.SendBrutCommand(this.tbxStore2.Text);
            else
                this._speecher.SendBrutCommand("Descendre store");
        }

        private void btnStore3_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxStore3.Text != "")
                this._speecher.SendBrutCommand(this.tbxStore3.Text);
            else
                this._speecher.SendBrutCommand("Stopper store");
        }
        #endregion
    }
}
