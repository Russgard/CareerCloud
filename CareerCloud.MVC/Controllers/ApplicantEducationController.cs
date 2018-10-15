using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerCloud.MVC.Controllers
{
    public class ApplicantEducationController : Controller
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationController()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>(false);
            _logic = new ApplicantEducationLogic(repo);
        }
        public ApplicantEducationController(IDataRepository<ApplicantEducationPoco> repo)
        {       
            _logic = new ApplicantEducationLogic(repo);
        }


        // GET: ApplicantEducation
        [HttpGet]
        public ActionResult Index()
        {
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            object _educationID = null;
            try
            {
                _educationID = (from x in _logic.GetAll() where x.Applicant == _userProfileId select x.Id).FirstOrDefault();
                pocos = _logic.GetAll().Where<ApplicantEducationPoco> (T => T.Applicant == _userProfileId).ToList();
            }
            catch { }
            finally { }

            if (pocos == null)
            {
                return RedirectToAction("Create", "ApplicantEducation");
                
            }
            else
            {
                return View(pocos);
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
        public ActionResult Create([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate, CompletionPercent")] ApplicantEducationPoco applicantEducation)
        {
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            applicantEducation.Id = Guid.NewGuid();
            applicantEducation.Applicant = _userProfileId;
            _logic.Add(new ApplicantEducationPoco[] { applicantEducation });
            return RedirectToAction("Index");

        }
        
        //Get 
        public ActionResult Edit(Guid id)
        {
            ApplicantEducationPoco applicant_Education = _logic.Get(id);
            if (applicant_Education == null)
            {
                return HttpNotFound();
            }
            TempData["TimeStamp"] = applicant_Education.TimeStamp;
            return View(applicant_Education);

        }

        // POST: ApplicantEducation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent")] ApplicantEducationPoco applicantEducation)
        {
            
                Guid _userProfileId = (Guid)TempData["Applicant"];
                TempData.Keep();
                applicantEducation.TimeStamp = (byte[])TempData["TimeStamp"];
                applicantEducation.Applicant = _userProfileId;
                _logic.Update(new ApplicantEducationPoco[] { applicantEducation });
                return RedirectToAction("Index");
            
        }

        // GET: ApplicantEducation/Delete
        public ActionResult Delete(Guid id)
        {
            ApplicantEducationPoco applicantEducation = _logic.Get(id);
            if (applicantEducation == null)
            {
                return HttpNotFound();
            }
            return View(applicantEducation);
        }

        // POST: ApplicantEducation/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantEducationPoco applicantEducation = _logic.Get(id);
            _logic.Delete(new ApplicantEducationPoco[] { applicantEducation });
            return RedirectToAction("Index");
        }

    }
}