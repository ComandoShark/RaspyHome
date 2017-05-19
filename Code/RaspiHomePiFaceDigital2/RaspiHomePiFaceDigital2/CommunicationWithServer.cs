using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomePiFaceDigital2
{
    public class CommunicationWithServer
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ModelPiFaceDigital2 _mPiFace;
        #endregion
        #endregion

        #region Properties
        public ModelPiFaceDigital2 MPiFace2
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
        public CommunicationWithServer(ModelPiFaceDigital2 paramModel)
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
