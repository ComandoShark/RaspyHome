using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspiHomeSpeechNSynthetize.Synthetize
{
    public class CommunicationWithServer
    {
        #region Fields
        #region Constants
        #endregion

        #region Variables
        #endregion
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public CommunicationWithServer()
        {

        }
        #endregion

        #region Methods
        public string SendCommand(string commandToSend)
        {
            string result = "";
            //SEND TO SERVER

            // for test
            result = "TEMP=25.6;HUMI=40.5;PRES=10";

            //READ REPLY AND SET RESULT

            return result;
        }
        #endregion
    }
}
