using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_System.Util
{
    public class Property_util
    {
        public static string GetPropertyString()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            string port = ConfigurationManager.AppSettings["port"];

            return $"Data Source={server},{port};Initial Catalog={database};User ID={username};Password={password}";
        }
    }
}
