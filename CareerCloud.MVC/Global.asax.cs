using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CareerCloud.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };
                conn.Open();
                cmd.CommandText = @"IF (OBJECT_ID('dbo.FK_CompanyPrifile_Security_Login', 'F') IS NULL)
                                    BEGIN
                                        ALTER TABLE dbo.Company_Profiles 
                                            ADD Login uniqueidentifier, CONSTRAINT FK_CompanyPrifile_Security_Login FOREIGN KEY (Login) REFERENCES dbo.Security_Logins(Id)
                                    END
                                    ";
                int rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
