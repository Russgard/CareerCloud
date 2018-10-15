using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
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
    public class ApplicantJobSearchController : Controller
    {
        private readonly CompanyJobLogic _jobLogic;
        private readonly CompanyJobSkillLogic _skillLogic;
        private readonly CompanyJobEducationLogic _educationLogic;
        private readonly CompanyDescriptionLogic _companyLogic;
        private readonly CompanyJobDescriptionLogic _descriptionLogic;
        private readonly CompanyLocationLogic _locationLogic;
        private readonly ApplicantJobApplicationLogic _appliedLogic;

        public ApplicantJobSearchController() {
            var jobRepo = new EFGenericRepository<CompanyJobPoco>(false);
            _jobLogic = new CompanyJobLogic(jobRepo);
            var skillRepo = new EFGenericRepository<CompanyJobSkillPoco>(false);
            _skillLogic = new CompanyJobSkillLogic(skillRepo);
            var educationRepo = new EFGenericRepository<CompanyJobEducationPoco>(false);
            _educationLogic = new CompanyJobEducationLogic(educationRepo);
            var companyRepo = new EFGenericRepository<CompanyDescriptionPoco>(false);
            _companyLogic = new CompanyDescriptionLogic(companyRepo);
            var _descriptionRepo = new EFGenericRepository<CompanyJobDescriptionPoco>(false);
            _descriptionLogic = new CompanyJobDescriptionLogic(_descriptionRepo);
            var locationRepo = new EFGenericRepository<CompanyLocationPoco>(false);
            _locationLogic = new CompanyLocationLogic(locationRepo);
            var AppliedRepo = new EFGenericRepository<ApplicantJobApplicationPoco>(false);
            _appliedLogic = new ApplicantJobApplicationLogic(AppliedRepo);
        }
        // GET: ApplicantJobSearch
        public ActionResult Index()
        {
            return View();
        }

        // POST: ApplicantJobSearch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Title, Location")] JobSearch applicantJobSearch)
        {
            TempData["JobTitle"] = applicantJobSearch.Title;

            TempData["Location"] = applicantJobSearch.Location;
            return RedirectToAction("JobSearchResult"); 
        }

        public ActionResult JobSearchResult()
        {
            List<JobSearchResult> result = new List<JobSearchResult>();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };
                conn.Open();

                cmd.CommandText = @"SELECT CJ.Id, CP.Id, CJD.Job_Name, CJD.Job_Descriptions, CL.City_Town, CL.State_Province_Code, CD.Company_Name, CP.Company_Website FROM Company_Profiles AS CP INNER JOIN Company_Descriptions AS CD ON CP.Id = CD.Company INNER JOIN Company_Locations AS CL ON CL.Company = CP.Id INNER JOIN Company_Jobs AS CJ ON CJ.Company = CP.Id INNER JOIN Company_Jobs_Descriptions AS CJD ON CJD.Job = CJ.Id WHERE CD.LanguageID = 'EN' AND CJD.Job_Name LIKE '%'+@JobTitle+'%' AND CL.City_Town LIKE '%'+@Location+'%'";
                cmd.Parameters.AddWithValue("@JobTitle", (TempData["JobTitle"] != null)? TempData["JobTitle"].ToString().Trim():"");
                TempData.Keep();
                cmd.Parameters.AddWithValue("@Location", (TempData["Location"] != null)? TempData["Location"].ToString().Trim():"");
                TempData.Keep();
                try
                {
                    SqlDataReader IDreader = cmd.ExecuteReader();
                    while (IDreader.Read())
                    {
                        IDataRecord myreader = (IDataRecord)IDreader;
                        JobSearchResult x = new JobSearchResult
                        {
                            JobID = (Guid)myreader[0],
                            CompID = (Guid)myreader[1],
                            JobName = myreader[2].ToString(),
                            JobDescription = myreader[3].ToString(),
                            City = myreader[4].ToString(),
                            State = myreader[5].ToString(),
                            CompanyName = myreader[6].ToString(),
                            Website = myreader[7].ToString(),
                            Applied = false
                        };                      
                       
                        try
                        {
                            ApplicantJobApplicationPoco poco = _appliedLogic.GetAll().Where<ApplicantJobApplicationPoco>(T => (T.Applicant == (Guid)TempData["Applicant"] && T.Job == x.JobID) ? true : false).FirstOrDefault();
                            TempData.Keep();
                            x.Applied = (poco != null) ? true : false;
                            result.Add(x);


                        }
                        catch { }
                        finally {
                            TempData.Keep();
                        }
                    }
                    IDreader.Close();
                    conn.Close();

                }
                catch { }
                finally { conn.Close(); }
            }
            conn.Close();

            return View(result);
        }


        //Get
        public ActionResult Apply(Guid id) {

            ApplicantJobApplicationPoco jobApp = new ApplicantJobApplicationPoco
            {
                Id = Guid.NewGuid(),
                Applicant = (Guid)TempData["Applicant"],
                Job = id,
                ApplicationDate = DateTime.Now
            };
            _appliedLogic.Add( new ApplicantJobApplicationPoco[] { jobApp });
            TempData.Keep();

            return RedirectToAction("JobSearchResult");
        }
        public ActionResult CompanyJobList(Guid id)
        {

            List<JobSearchResult> result = new List<JobSearchResult>();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };
                conn.Open();

                cmd.CommandText = @"SELECT CJ.Id, CJD.Job_Name, CJD.Job_Descriptions, CL.City_Town, CL.State_Province_Code, CD.Company_Name, CP.Company_Website FROM Company_Profiles AS CP INNER JOIN Company_Descriptions AS CD ON CP.Id = CD.Company INNER JOIN Company_Locations AS CL ON CL.Company = CP.Id INNER JOIN Company_Jobs AS CJ ON CJ.Company = CP.Id INNER JOIN Company_Jobs_Descriptions AS CJD ON CJD.Job = CJ.Id WHERE CD.LanguageID = 'EN' AND CP.Id = @CompID";
                cmd.Parameters.AddWithValue("@CompID", id);
                try
                {
                    SqlDataReader IDreader = cmd.ExecuteReader();
                    while (IDreader.Read())
                    {
                        IDataRecord myreader = (IDataRecord)IDreader;
                        JobSearchResult x = new JobSearchResult
                        {
                            JobID = (Guid)myreader[0],
                            CompID = id,
                            JobName = myreader[1].ToString(),
                            JobDescription = myreader[2].ToString(),
                            City = myreader[3].ToString(),
                            State = myreader[4].ToString(),
                            CompanyName = myreader[5].ToString(),
                            Website = myreader[6].ToString(),
                            Applied = false
                        };

                        try
                        {
                            ApplicantJobApplicationPoco poco = _appliedLogic.GetAll().Where<ApplicantJobApplicationPoco>(T => (T.Applicant == (Guid)TempData["Applicant"] && T.Job == x.JobID) ? true : false).FirstOrDefault();
                            TempData.Keep();
                            x.Applied = (poco != null) ? true : false;
                            result.Add(x);


                        }
                        catch { }
                        finally
                        {
                            TempData.Keep();
                        }
                    }
                    IDreader.Close();
                    conn.Close();

                }
                catch { }
                finally { conn.Close(); }
            }
            conn.Close();

            return View(result);

        }
    }

}