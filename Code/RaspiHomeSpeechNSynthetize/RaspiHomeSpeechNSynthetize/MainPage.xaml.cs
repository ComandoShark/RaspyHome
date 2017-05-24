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
        private void btnSendCommand_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxCommandToSend.Text != "")
                this._speecher.SendBrutCommand(this.tbxCommandToSend.Text);
            else
                this._speecher.SendBrutCommand("Quel est la température du salon");
        }
        #endregion
    }
}
