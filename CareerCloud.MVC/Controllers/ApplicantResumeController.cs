using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CareerCloud.MVC.Controllers
{
    public class ApplicantResumeController : Controller
    {
        private readonly ApplicantResumeLogic _logic;
        private readonly ApplicantProfileLogic _profileLogic;
    
        public ApplicantResumeController()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>(false);
            _logic = new ApplicantResumeLogic(repo);
            var profileRepo = new EFGenericRepository<ApplicantProfilePoco>(false);
            _profileLogic = new ApplicantProfileLogic(profileRepo);

        }
        public ApplicantResumeController(IDataRepository<ApplicantResumePoco> repo)
        {
            _logic = new ApplicantResumeLogic(repo);
        }

        // GET: ApplicantResume
        public ActionResult Index()
        {
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            ApplicantResumePoco poco = null;
            object _resumeID = null;

            try {
                _resumeID = (from x in _logic.GetAll() where x.Applicant == _userProfileId select x.Id).FirstOrDefault();
                poco = _logic.Get((Guid)_resumeID);
            }
            catch { }
            if (poco == null) {
                return RedirectToAction("Create", "ApplicantResume");
            }
            else {
                return View(poco);
            }           
        }

        // GET: ApplicantResume/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicantResume/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Resume,Last_Updated")] ApplicantResumePoco applicantResume)
        {
            Guid UserId = (Guid)Session["UserId"];
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            applicantResume.Id = Guid.NewGuid();
            applicantResume.Applicant = _userProfileId;
            applicantResume.LastUpdated = DateTime.Now;
            _logic.Add(new ApplicantResumePoco[] { applicantResume });            
            return RedirectToAction("Index");
            
        }
       
        //Get 
        public ActionResult Edit(Guid id)
        {
           ApplicantResumePoco applicant_Resumes = _logic.Get(id);
           if (applicant_Resumes == null)
           {
                return HttpNotFound();
           }
            return View(applicant_Resumes);
         
        }

        // POST: ApplicantResume/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Resume,Last_Updated")] ApplicantResumePoco applicant_Resumes)
        {
            if (ModelState.IsValid)
            {
                applicant_Resumes.LastUpdated = DateTime.Now;
                applicant_Resumes.Applicant = (Guid) TempData["Applicant"];
                TempData.Keep();
                _logic.Update(new ApplicantResumePoco[] { applicant_Resumes });
                return RedirectToAction("Index");
            }
          return View(applicant_Resumes);
        }
    }
}