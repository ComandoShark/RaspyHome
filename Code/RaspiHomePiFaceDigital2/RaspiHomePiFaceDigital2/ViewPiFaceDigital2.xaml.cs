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

namespace RaspiHomePiFaceDigital2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ViewPiFaceDigital2 : Page
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ModelPiFaceDigital2 _mPiFace;
        #endregion
        #endregion

        #region Properties
        public ModelPiFaceDigital2 MPiFace
        {
            get
            {
                return _mPiFace;
            }

            set
            {
                _mPiFace = value;
            }
        }
        #endregion

        #region Constructors
        public ViewPiFaceDigital2()
        {
            this.InitializeComponent();

            this.MPiFace = new ModelPiFaceDigital2(this);     
        }       
        #endregion

        #region Methods
        #endregion        
    }
}
