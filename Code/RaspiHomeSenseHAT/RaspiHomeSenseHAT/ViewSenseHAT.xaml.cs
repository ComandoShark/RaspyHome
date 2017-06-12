/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeSenseHAT is a program who use a 
 *   Sense HAT, it's an electronic card who can be 
 *   mesured value with sensor. This program use 
 *   the Sense HAT to mesure the temperature, the 
 *   humidity and the pressure. 
\*--------------------------------------------------*/

using Windows.UI.Xaml.Controls;

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
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public ViewSenseHAT()
        {
            this.InitializeComponent();

            this.MSenseHAT = new ModelSenseHAT(this);
        }
        #endregion
    }
}
