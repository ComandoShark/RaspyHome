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

        private List<Component> _components;
        private CommunicationWithServer _comWithServer;
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

        public List<Component> Components
        {
            get
            {
                return _components;
            }

            set
            {
                _components = value;
            }
        }

        public CommunicationWithServer ComWithServer
        {
            get
            {
                return _comWithServer;
            }

            set
            {
                _comWithServer = value;
            }
        }
        #endregion

        #region Constructors
        public ModelPiFaceDigital2(ViewPiFaceDigital2 paramView)
        {
            this.VPiFace = paramView;

            this.ComWithServer = new CommunicationWithServer(this);

            this.Components = new List<Component>();
            this.Components.Add(new Light());
            this.Components.Add(new Light());
        }
        #endregion

        #region Methods
        #endregion
    }
}
