﻿/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 8th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeTabletWindows is a program 
 *   compatible with the Windows tablet. It's a 
 *   program that can be use as tactil graphic 
 *   interface to order the component linked with 
 *   the other Raspberry Pi.
\*--------------------------------------------------*/

namespace RaspiHomeTabletWindows.Modules.Information
{
    public class InformationModel : PropertyChangedBase
    {
        #region Fields
        #region Constants
        #endregion

        #region Varaibles
        private InformationView _view = null;
        #endregion
        #endregion

        #region Properties
        public InformationView View
        {
            get
            {
                return _view;
            }

            set
            {
                _view = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public InformationModel(InformationView paramView)
        {
            this.View = paramView;
        }
        #endregion

        #region Methods
        #endregion
    }
}