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
    public class ApplicantWorkHistoryController : Controller
    {
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryController()
        {
            var repo = new EFGenericRepository<ApplicantWorkHistoryPoco>(false);
            _logic = new ApplicantWorkHistoryLogic(repo);
        }
        public ApplicantWorkHistoryController(IDataRepository<ApplicantWorkHistoryPoco> repo)
        {
            _logic = new ApplicantWorkHistoryLogic(repo);
        }


        // GET: ApplicantEducation
        [HttpGet]
        public ActionResult Index()
        {
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            List<ApplicantWorkHistoryPoco> pocos = new List<ApplicantWorkHistoryPoco>();
            object _educationID = null;
            try
            {
                _educationID = (from x in _logic.GetAll() where x.Applicant == _userProfileId select x.Id).FirstOrDefault();
                pocos = _logic.GetAll().Where<ApplicantWorkHistoryPoco>(T => T.Applicant == _userProfileId).ToList();
            }
            catch { }
            finally { }

            if (pocos == null)
            {
                return RedirectToAction("Create", "ApplicantWorkHistory");

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
        public ActionResult Create([Bind(Include = "Id,Applicant,CompanyName,CountryCode,Location,JobTitle, JobDescription,StartMonth, StartYear, EndMonth,EndYear ")] ApplicantWorkHistoryPoco applicantWorkHistory)
        {
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            applicantWorkHistory.Id = Guid.NewGuid();
            applicantWorkHistory.Applicant = _userProfileId;
            _logic.Add(new ApplicantWorkHistoryPoco[] { applicantWorkHistory });
            return RedirectToAction("Index");

        }
        // POST: ApplicantResume/Edit/
        //Get 
        public ActionResult Edit(Guid id)
        {
            ApplicantWorkHistoryPoco applicantWorkHistory = _logic.Get(id);
            if (applicantWorkHistory == null)
            {
                return HttpNotFound();
            }
            TempData["TimeStamp"] = applicantWorkHistory.TimeStamp;
            return View(applicantWorkHistory);

        }

        // POST: ApplicantEducation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,CompanyName,CountryCode,Location,JobTitle, JobDescription,StartMonth, StartYear, EndMonth,EndYear,TimeStamp ")] ApplicantWorkHistoryPoco applicantWorkHistory)
        {

            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            applicantWorkHistory.TimeStamp = (byte[])TempData["TimeStamp"];
            applicantWorkHistory.Applicant = _userProfileId;
            _logic.Update(new ApplicantWorkHistoryPoco[] { applicantWorkHistory });
            return RedirectToAction("Index");

        }

        // GET: ApplicantEducation/Delete
        public ActionResult Delete(Guid id)
        {
            ApplicantWorkHistoryPoco applicantWorkHistory = _logic.Get(id);
            if (applicantWorkHistory == null)
            {
                return HttpNotFound();
            }
            return View(applicantWorkHistory);
        }

        // POST: ApplicantEducation/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantWorkHistoryPoco applicantWorkHistory = _logic.Get(id);
            _logic.Delete(new ApplicantWorkHistoryPoco[] { applicantWorkHistory });
            return RedirectToAction("Index");
        }
    }
}