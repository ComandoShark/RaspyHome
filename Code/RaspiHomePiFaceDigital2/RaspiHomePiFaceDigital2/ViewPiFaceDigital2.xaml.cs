/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomePiFaceDigital2 is a program who use 
 *   a PiFace Digital 2, it's an electronic card who 
 *   can be use to plug electronic component. This 
 *   program use the PiFace Digital 2 to activate 
 *   light and store. 
\*--------------------------------------------------*/

using Windows.UI.Xaml.Controls;

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
