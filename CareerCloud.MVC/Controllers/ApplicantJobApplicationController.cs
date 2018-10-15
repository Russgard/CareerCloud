using CareerCloud.BusinessLogicLayer;
using CareerCloud.MVC.Models;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerCloud.MVC.Controllers
{
    public class ApplicantJobApplicationController : Controller
    {
       // GET: ApplicantJobApplication
        public ActionResult Index()
        {
            List<ApplicantAppliedJobs> result = new List<ApplicantAppliedJobs>();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };
                conn.Open();

                cmd.CommandText = @"SELECT AJA.Id, CJD.Job_Name, CJD.Job_Descriptions, CL.City_Town, CL.State_Province_Code, CD.Company_Name, CP.Company_Website  FROM Applicant_Job_Applications AS AJA INNER JOIN .Applicant_Profiles AS AP ON AP.Id = AJA.Applicant INNER JOIN Company_Jobs AS CJ ON CJ.Id = AJA.Job INNER JOIN Company_Profiles AS CP ON CP.Id = CJ.Company INNER JOIN Company_Descriptions AS CD ON CD.Company = CP.Id INNER JOIN Company_Jobs_Descriptions AS CJD ON CJD.Job = CJ.Id INNER JOIN Company_Locations AS CL ON CL.Company = CP.Id WHERE CD.LanguageID = 'EN' AND AJA.Applicant = @Applicant";
                cmd.Parameters.AddWithValue("@Applicant", (Guid)TempData["Applicant"]);
                TempData.Keep();
                try
                {
                    SqlDataReader IDreader = cmd.ExecuteReader();
                    while (IDreader.Read())
                    {
                        IDataRecord myreader = (IDataRecord)IDreader;
                        ApplicantAppliedJobs x = new ApplicantAppliedJobs
                        {
                            AplliedJobID = (Guid)myreader[0],
                            JobName = myreader[1].ToString(),
                            JobDescription = myreader[2].ToString(),
                            City = myreader[3].ToString(),
                            State = myreader[4].ToString(),
                            CompanyName = myreader[5].ToString(),
                            Website = myreader[6].ToString()
                        };
                        result.Add(x);
                    }
                    IDreader.Close();
                    conn.Close();

                }
                catch { }
            }
            conn.Close();

            return View(result);
        }
        // GET: ApplicantJobApplication
        public ActionResult Withdraw(Guid id) {

            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            using (conn)
            {

                SqlCommand cmd = new SqlCommand { Connection = conn };

                conn.Open();
                cmd.CommandText = @"DELETE FROM dbo.Applicant_Job_Applications WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                conn.Close();
            }

            return RedirectToAction("Index");
        }
    }

}