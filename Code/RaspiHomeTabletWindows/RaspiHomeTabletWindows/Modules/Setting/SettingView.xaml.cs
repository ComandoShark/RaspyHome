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

using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RaspiHomeTabletWindows.Modules.Setting
{
    public sealed partial class SettingView : Page
    {
        #region Fields
        #region Constants
        #endregion

        #region Varaibles
        private SettingModel _model = null;
        #endregion
        #endregion

        #region Properties
        public SettingModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public SettingView()
        {
            this.InitializeComponent();

            this.Model = new SettingModel(this);
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        #endregion
    }
}
