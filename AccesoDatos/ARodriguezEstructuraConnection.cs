using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ARodriguezEstructuraConnection
    {
        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ARodriguezEstructura"].ToString();
        }
    }
}
