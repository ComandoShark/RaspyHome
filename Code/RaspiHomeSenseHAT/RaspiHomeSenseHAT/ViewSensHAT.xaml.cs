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

namespace RaspiHomeSenseHAT
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ViewSenseHAT : Page
    {
        #region Fields
        #region Constants
        private const int TICK = 2;
        #endregion

        #region Variables
        private ModelSenseHAT _mSenseHAT;
        #endregion
        #endregion

        #region Properties
        public ModelSenseHAT MSenseHAT
        {
            get
            {
                return _mSenseHAT;
            }

            set
            {
                _mSenseHAT = value;
            }
        }

        public string Temperature { set { this.tbxTemp.Text = value; } }
        public string Humidity { set { this.tbxHumi.Text = value; } }
        public string Pressure { set { this.tbxPres.Text = value; } }
        public string IPRasp { set { this.tbxIpv4.Text = value; } }
        #endregion

        #region Constructors
        public ViewSenseHAT()
        {
            this.InitializeComponent();

            this.MSenseHAT = new ModelSenseHAT(this);
        }
        #endregion

        #region Methods        
        #endregion

    }
}
