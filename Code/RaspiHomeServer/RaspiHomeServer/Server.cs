using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeServer
{
    public class Server
    {
        #region Fields
        #region Constants
        const int DEFAULT_PORT = 8080;

        string str1 = "IPRasp=192.198.2.6;Location=Salon;Component=Light,Light,Light,Store";
        string str2 = "IPRasp=192.198.2.51;Location=Salon;Component=Sensor";
        string str3 = "IPRasp=192.198.2.20;Location=Cuisine;Component=Store,Light,Store";
        string str4 = "IPRasp=192.198.2.163;Location=Salon;Component=Light";
        string str5 = "IPRasp=192.198.2.211;Location=Salon;Component=Store,Light,Store";
        #endregion

        #region Variables
        private List<RaspberryClient> _rpiClient;
        private CommandFilter _cmdFilter = new CommandFilter();        
        #endregion
        #endregion

        #region Properties
        public List<RaspberryClient> RpiClients
        {
            get
            {
                return _rpiClient;
            }

            set
            {
                _rpiClient = value;
            }
        }

        public CommandFilter CmdFilter
        {
            get
            {
                return _cmdFilter;
            }

            set
            {
                _cmdFilter = value;
            }
        }
        #endregion

        #region Constructor
        public Server()
        {
            this.RpiClients = new List<RaspberryClient>();

            InitializeNewRaspberryClient(str1);
            InitializeNewRaspberryClient(str2);
            InitializeNewRaspberryClient(str3);
            InitializeNewRaspberryClient(str4);
            InitializeNewRaspberryClient(str5);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize a raspberry pi with the information read
        /// </summary>
        /// <param name="rpiInformation"></param>
        private void InitializeNewRaspberryClient(string rpiInformation)
        {
            // create an array of the actual string to get information
            string[] rpiInformations = rpiInformation.Split(';');
            string rpiIPv4 = "";
            string rpiLocation = "";
            string rpiComponent = "";

            foreach (var information in rpiInformations)
            {
                switch (information.Split('=').First())
                {
                    // Get the first value of the array
                    // IP of the actual Raspberry
                    case "IPRasp":
                        rpiIPv4 = information.Split('=').Last();
                        break;
                    // Get the second value of the array
                    // Location of the actual Raspberry (where to find the raspberry with the algorythme)
                    case "Location":
                        rpiLocation = information.Split('=').Last();
                        break;
                    case "Component":
                        rpiComponent = information.Split('=').Last();
                        break;
                }
            }
            Console.WriteLine(rpiIPv4);
            Console.WriteLine(rpiLocation);

            // Create a client without component
            RaspberryClient client = new RaspberryClient(rpiIPv4, DEFAULT_PORT, rpiLocation, new List<Component>());

            try
            {
                // Get all components of the Raspberry
                string[] components = rpiComponent.Split('=').Last().Split(',');

                // Get all class types of the project
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();

                // check all components
                foreach (var component in components)
                {
                    // check all class types
                    foreach (var type in types)
                    {
                        if (type.Name == component)
                        {
                            // create a new component with the type result
                            client.Components.Add((Component)Activator.CreateInstance(type));
                            break;
                        }
                    }

                    Console.WriteLine(component);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Add final client
            this.RpiClients.Add(client);
        }

        private List<RaspberryClient> FilterTheCommand(string command)
        {
            return this.CmdFilter.ApplyFilter(command, this.RpiClients);
        }

        private void UpdateWithNewValue(List<RaspberryClient> rpiClientWithNewValue)
        {
            foreach (RaspberryClient client in rpiClientWithNewValue)
            {
                
            }
        }
        #endregion
    }
}
