using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Vpn;
using Windows.Networking.Sockets;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using SQLite.Net;
using System.Data.Common;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestAddRaspberryOnDB
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string _path;
        SQLiteConnection _conn;

        string _ipServer = "10.134.97.117";
        string _dbName = "dbRaspiHome";

       

        public MainPage()
        {
            this.InitializeComponent();

            _path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            
            
        }

        private void btnAddToDB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveToDB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTurnOnLight_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
