using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomePiFaceDigital2
{
    public class ModelPiFaceDigital2
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        private ViewPiFaceDigital2 _vPiFace;
        #endregion
        #endregion

        #region Properties
        public ViewPiFaceDigital2 VPiFace
        {
            get
            {
                return _vPiFace;
            }

            set
            {
                _vPiFace = value;
            }
        }
        #endregion

        #region Constructors
        public ModelPiFaceDigital2(ViewPiFaceDigital2 paramView)
        {
            this.VPiFace = paramView;
        }
        #endregion

        #region Methods
        #endregion
    }
}
