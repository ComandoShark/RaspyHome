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

namespace testconnexionrpi
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CommunicationWithServer communi;

        public MainPage()
        {
            this.InitializeComponent();
            this.communi = new CommunicationWithServer(this);
        }

        private void btnSendCommand_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxCommand.Text != "")
                this.communi.SendCommandToServer(this.tbxCommand.Text);
            else
                this.communi.SendCommandToServer("refresh");
        }

        private void btnSendCommand2_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxCommand2.Text != "")
                this.communi.SendCommandToServer(this.tbxCommand2.Text);
            else
                this.communi.SendCommandToServer("refresh");
        }

        public void SayInformationReply(string messageReply, string messageCommand)
        {
            string[] commandSplited = messageCommand.Split(';');
            bool temp = false, humi = false, pres = false;

            if (messageCommand.Contains("température"))
                temp = true;
            if (messageCommand.Contains("humidité"))
                humi = true;
            if (messageCommand.Contains("pression"))
                pres = true;
            if (messageCommand.Contains("état"))
            {
                temp = true;
                humi = true;
                pres = true;
            }            

            foreach (var reply in messageReply.Split(';'))
            {
                switch (reply.Split('=').First())
                {
                    case "TEMP":
                        if (temp)
                            this.tbcTempReply.Text = reply.Split('=').Last() + " [°C]";
                        break;

                    case "HUMI":
                        if (humi)
                            this.tbcHumiReply.Text = reply.Split('=').Last() + " %";
                        break;

                    case "PRES":
                        if (pres)
                            this.tbcPresReply.Text = reply.Split('=').Last() + " [bars]";
                        break;
                }
            }

            temp = false; humi = false; pres = false;

        }
    }
}
