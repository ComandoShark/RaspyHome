using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAddRaspberryOnDB
{
    class RaspiHomeDataBase
    {
        DbConnection dbConnect;

        public RaspiHomeDataBase()
        {
            dbConnect = new DbConnection();
        }

        //public bool Connect()
        //{
        //    var mysql_conn = new MySqlConnection();
        //}
    }
}
