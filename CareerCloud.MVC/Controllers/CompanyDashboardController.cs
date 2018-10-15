using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerCloud.MVC.Controllers
{
    public class CompanyDashboardController : Controller
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyDashboardController()
        {
            var profileRepo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(profileRepo);
        }

        // GET: CompanyDashboard
        public ActionResult Index()
        {
            Guid UserId = (Guid)Session["UserId"];
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };
                conn.Open();

                cmd.CommandText = @"SELECT A.Id FROM dbo.Company_Profiles As A WHERE A.Login = @Company";
                cmd.Parameters.AddWithValue("@Company", UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                                
                while (reader.Read())
                {
                    TempData["Company"] = (Guid)reader[0];                       
                };


            }
                conn.Close();
            return View();
        }
    }
}