/*--------------------------------------------------*\
 * Author    : Salvi Cyril
 * Date      : 7th juny 2017
 * Diploma   : RaspiHome
 * Classroom : T.IS-E2B
 * 
 * Description:
 *      RaspiHomeServer is a server TCP. It's the m
 *      ain program, where all command pass before 
 *      to be reply to the good client. 
\*--------------------------------------------------*/

using System.Collections.Generic;
using System.Net;

namespace RaspiHomeServer
{
    public class RaspberryClient
    {
        #region Fields
        #region Constant
        // Default port communication
        private const int DEFAULT_PORT = 54565;
        private static IPAddress DEFAULT_SERVER = IPAddress.Parse("127.0.0.1");
        private static string DEFAULT_LOCATION = "maison";
        private static IPEndPoint DEFAULT_IP_END_POINT = new IPEndPoint(DEFAULT_SERVER, DEFAULT_PORT);
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
        /// <summary>
        /// Constructor: Initializer
        /// </summary>
        public RaspberryClient(string paramIP, int paramPort, string paramLocation, List<Component> paramLstComponent)
        {
            this.IpClient = IPAddress.Parse(paramIP);
            this.PortServer = paramPort;
            this.Location = paramLocation;
            this.Components = paramLstComponent;
        }        
        #endregion

        #region Methods
        #endregion
    }
}
