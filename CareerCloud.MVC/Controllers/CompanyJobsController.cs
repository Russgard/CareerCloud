using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
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
    public class CompanyJobsController : Controller
    {
        private readonly CompanyJobLogic _jobLogic;
        private readonly CompanyJobDescriptionLogic _descLogic;
        private readonly CompanyJobSkillLogic _skillLogic;
        private readonly CompanyJobEducationLogic _eduLogic;
        public CompanyJobsController()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>(false);
            _jobLogic = new CompanyJobLogic(repo);

            var descRepo = new EFGenericRepository<CompanyJobDescriptionPoco>(false);
            _descLogic = new CompanyJobDescriptionLogic(descRepo);

            var skillRepo = new EFGenericRepository<CompanyJobSkillPoco>(false);
            _skillLogic = new CompanyJobSkillLogic(skillRepo);

            var eduRepo = new EFGenericRepository<CompanyJobEducationPoco>(false);
            _eduLogic = new CompanyJobEducationLogic(eduRepo);
        }
        public CompanyJobsController(IDataRepository<CompanyJobPoco> repo, 
                                    IDataRepository<CompanyJobDescriptionPoco> descRepo,
                                    IDataRepository<CompanyJobSkillPoco> skillRepo,
                                    IDataRepository<CompanyJobEducationPoco> eduRepo
            )
        {
            _jobLogic = new CompanyJobLogic(repo);
            _descLogic = new CompanyJobDescriptionLogic(descRepo);
            _skillLogic = new CompanyJobSkillLogic(skillRepo);
            _eduLogic = new CompanyJobEducationLogic(eduRepo);
        }


        // GET: CompanyJobs
        public ActionResult Index()
        {
            Guid _userProfileId = (Guid)TempData["Company"];
            TempData.Keep();
            List<CompanyJobPost> postedJobs = new List<CompanyJobPost>();
            List<CompanyJobPoco> jobPocos = new List<CompanyJobPoco>();
            Dictionary<Guid, CompanyJobDescriptionPoco> descPocos = new Dictionary<Guid, CompanyJobDescriptionPoco>();
            Dictionary<Guid, CompanyJobSkillPoco> skillPocos = new Dictionary<Guid, CompanyJobSkillPoco>();
            Dictionary<Guid, CompanyJobEducationPoco> eduPocos = new Dictionary<Guid, CompanyJobEducationPoco>();

            Guid UserID = (Guid)TempData["Company"];
            TempData.Keep();
            jobPocos = _jobLogic.GetAll().Where(T => T.Company == UserID).ToList();
            foreach (CompanyJobPoco x in jobPocos) {
                descPocos.Add(x.Id, _descLogic.GetAll().Where(T => T.Job == x.Id).FirstOrDefault());
                skillPocos.Add(x.Id, _skillLogic.GetAll().Where(T => T.Job == x.Id).FirstOrDefault());
                eduPocos.Add(x.Id, _eduLogic.GetAll().Where(T => T.Job == x.Id).FirstOrDefault());
            }
            foreach (CompanyJobPoco x in jobPocos)
            {
                CompanyJobPost y = new CompanyJobPost
                {
                    JobID = x.Id,
                    JobName = descPocos[x.Id].JobName,
                    JobDescription = descPocos[x.Id].JobDescriptions,
                    Major = eduPocos[x.Id].Major,
                    EduImportance = eduPocos[x.Id].Importance,
                    Skill = skillPocos[x.Id].Skill,
                    SkillLevel = skillPocos[x.Id].SkillLevel,
                    SkillImportance = skillPocos[x.Id].Importance,
                    Created = x.ProfileCreated,
                    IfInactive = x.IsInactive,
                    IfHidden = x.IsCompanyHidden
                };
                postedJobs.Add(y);

            }
            
                return View(postedJobs);
            
        }
        // GET: CompanyJobs/Create
        public ActionResult Create() {

            return View();
        }
  
        //Post: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobName,JobDescription,Major,EduImportance, Skill, SkillLevel, SkillImportance, Created, IfInactive,IfHidden")] CompanyJobPost companyJobPost)
        {
            CompanyJobPoco jobPoco = new CompanyJobPoco
            {
                Id = Guid.NewGuid(),
                Company = (Guid)TempData["Company"],
                ProfileCreated = DateTime.Now,
                IsInactive = companyJobPost.IfInactive,
                IsCompanyHidden = companyJobPost.IfHidden
            };
            TempData.Keep();
            _jobLogic.Add(new CompanyJobPoco[] { jobPoco });

            CompanyJobDescriptionPoco jobDescPoco = new CompanyJobDescriptionPoco
            {
                Id = Guid.NewGuid(),
                Job = jobPoco.Id,
                JobName = companyJobPost.JobName,
                JobDescriptions = companyJobPost.JobDescription
            };
            _descLogic.Add(new CompanyJobDescriptionPoco[] { jobDescPoco });

            CompanyJobSkillPoco jobSkillPoco = new CompanyJobSkillPoco()
            {
                Id = Guid.NewGuid(),
                Job = jobPoco.Id,
                Skill = companyJobPost.Skill,
                SkillLevel = companyJobPost.SkillLevel,
                Importance = companyJobPost.SkillImportance
            };
            _skillLogic.Add(new CompanyJobSkillPoco[] { jobSkillPoco });

            CompanyJobEducationPoco jobEduPoco = new CompanyJobEducationPoco()
            {
                Id = Guid.NewGuid(),
                Job = jobPoco.Id,
                Major = companyJobPost.Major,
                Importance = companyJobPost.EduImportance
            };
            _eduLogic.Add(new CompanyJobEducationPoco[] { jobEduPoco });

           return RedirectToAction("Index");

        }
        //Get
        public ActionResult Resumes(Guid id)
        {
            List<AppliedApplicants> Applicants = new List<AppliedApplicants>();
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand { Connection = conn };
                conn.Open();
                if (Session["UserId"] != null)
                {
                    cmd.CommandText = @"SELECT SL.Full_Name, APS.Skill, AR.Resume, SL.Phone_Number, SL.Email_Address, AP.Id, AP.State_Province_Code FROM dbo.Applicant_Job_Applications AS AJA INNER JOIN dbo.Company_Jobs AS CJ ON AJA.Job = CJ.Id INNER JOIN dbo.Applicant_Profiles AS AP ON AP.Id = AJA.Applicant INNER JOIN dbo.Applicant_Educations AS AE ON AE.Applicant = AP.Id INNER JOIN dbo.Applicant_Resumes AS AR ON AR.Applicant = AP.Id INNER JOIN dbo.Applicant_Skills AS APS ON APS.Applicant = AP.Id INNER JOIN dbo.Applicant_Work_History AS AWH ON AWH.Applicant = AP.Id INNER JOIN dbo.Security_Logins AS SL ON SL.Id = AP.Login WHERE CJ.Id = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataReader IDreader = cmd.ExecuteReader();
                    while (IDreader.Read())
                    {
                        IDataRecord myreader = (IDataRecord)IDreader;
                        AppliedApplicants x = new AppliedApplicants
                        {
                            Name = myreader[0].ToString().Trim(),
                            Skills = myreader[1].ToString().Trim(),
                            Resume = myreader[2].ToString().Trim(),
                            Phone = myreader[3].ToString().Trim(),
                            Email = myreader[4].ToString().Trim(),
                            ApplicantID = (Guid)myreader[5],
                            State = myreader[5].ToString().Trim()
                        };
                        Applicants.Add(x);

                    }
                    IDreader.Close();
                    conn.Close();
                }
                conn.Close();
            }
            return View(Applicants);
        }
    }
}