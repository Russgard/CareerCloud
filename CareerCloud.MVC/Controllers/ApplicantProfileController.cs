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
    public class ApplicantProfileController : Controller
    {
        private readonly ApplicantProfileLogic _logic;
        private readonly SystemCountryCodeLogic _countryCodeLogic;

        public ApplicantProfileController()
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>(false);
            var countryRepo = new EFGenericRepository<SystemCountryCodePoco>(false);
            _logic = new ApplicantProfileLogic(repo);
            _countryCodeLogic = new SystemCountryCodeLogic(countryRepo);
        }
        public ApplicantProfileController(IDataRepository<ApplicantProfilePoco> repo)
        {
            _logic = new ApplicantProfileLogic(repo);
        }


        // GET: ApplicantProfile
        [HttpGet]
        public ActionResult Index()
        {
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            ApplicantProfilePoco poco = new ApplicantProfilePoco();
            poco = _logic.Get(_userProfileId);
              
            
            return View(poco);
            
        }


       //Get
        public ActionResult Edit(Guid id)
        {
            ApplicantProfilePoco applicantProfile = _logic.Get(id);
            if (applicantProfile == null)
            {
                return HttpNotFound();
            }
            TempData["TimeStamp"] = applicantProfile.TimeStamp;
            ViewBag.Country = new SelectList(_countryCodeLogic.GetAll(), "Code", "Name", applicantProfile.Country);
            return View(applicantProfile);

        }


        // POST: ApplicantProfile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Login, CurrentSalary,CurrentRate,Currency,Country, Province,Street, City, TimeStamp, PostalCode")] ApplicantProfilePoco applicantProfile)
        {
            Guid _userProfileId = (Guid)TempData["Applicant"];
            TempData.Keep();
            applicantProfile.TimeStamp = (byte[])TempData["TimeStamp"];
            applicantProfile.Id = _userProfileId;
            applicantProfile.Login = (Guid)Session["UserId"];
            SystemCountryCodePoco code = new SystemCountryCodePoco();
            //code = _countryCodeLogic.GetAll().Where<SystemCountryCodePoco>(T => T.Name == (applicantProfile.Country.Trim())).FirstOrDefault();
            applicantProfile.Country = applicantProfile.Country.Trim();
            _logic.Update(new ApplicantProfilePoco[] { applicantProfile });
            return RedirectToAction("Index");

        }

    }
}