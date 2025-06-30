using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ecom_Application.util.PropertyUtil;

namespace Ecom_Application.util
{
    public  class DBConnection
    {
          
            private static SqlConnection connection;

            public static SqlConnection GetConnection()
            {
                if (connection == null || connection.State == System.Data.ConnectionState.Closed)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ToString();
                if (string.IsNullOrEmpty(connectionString))
                    throw new Exception("Connection string 'MyConnection' not found in App.config");

                connection = new SqlConnection(connectionString);
                }

                return connection;
            }
        }

    }

