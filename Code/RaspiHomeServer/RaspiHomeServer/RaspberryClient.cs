using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeServer
{
    public class RaspberryClient
    {
        #region Fields
        #region Constant
        public static IPAddress DEFAULT_SERVER = IPAddress.Parse("127.0.0.1");
        public static int DEFAULT_PORT = 8080;
        public static string DEFAULT_LOCATION = "maison";
        public static IPEndPoint DEFAULT_IP_END_POINT = new IPEndPoint(DEFAULT_SERVER, DEFAULT_PORT);
        #endregion

        #region Variable
        private IPAddress _ipClient = null;
        private int _portServer = DEFAULT_PORT;
        private string _location = "";       

        private List<Component> _asComponent = new List<Component>();
        #endregion
        #endregion

        #region Properties
        public IPAddress IpClient
        {
            get
            {
                return _ipClient;
            }

            set
            {
                _ipClient = value;
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }

        public List<Component> Components
        {
            get
            {
                return _asComponent;
            }

            set
            {
                _asComponent = value;
            }
        }

        public int PortServer
        {
            get
            {
                return _portServer;
            }

            set
            {
                _portServer = value;
            }
        }
        #endregion

        #region Constructor


        public RaspberryClient(string paramIP, int paramPort, string paramLocation, List<Component> paramLstComponent)
        {
            InitializeRaspberry(new IPEndPoint(IPAddress.Parse(paramIP), paramPort));
            this.IpClient = IPAddress.Parse(paramIP);
            this.PortServer = paramPort;
            this.Location = paramLocation;
            this.Components = paramLstComponent;
        }        
        #endregion

        #region Methods
        private void InitializeRaspberry(IPEndPoint ipNport)
        {
            
        }
        #endregion
    }
}
