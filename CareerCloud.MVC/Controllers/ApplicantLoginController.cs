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
    public class ApplicantLoginController : Controller
    {

        private readonly SecurityLoginLogic _logic;

        public ApplicantLoginController()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>(false);            
            _logic = new SecurityLoginLogic(repo);           
        }
        public ApplicantLoginController(IDataRepository<SecurityLoginPoco> repo)
        {
            _logic = new SecurityLoginLogic(repo);
        }


        // GET: ApplicantEducation
        [HttpGet]
        public ActionResult Index()
        {
            SecurityLoginPoco poco = new SecurityLoginPoco();
            poco = _logic.Get((Guid) Session["UserId"]);

            return View(poco);

        }


        //Get
        public ActionResult Edit(Guid id)
        {
            SecurityLoginPoco applicantProfile = _logic.Get(id);
            TempData["Created"] = applicantProfile.Created;
            TempData["TimeStamp"] = applicantProfile.TimeStamp;
            TempData["IsInactive"] = applicantProfile.IsInactive;
            TempData["ForceChangePassword"] = applicantProfile.IsInactive;            
            return View(applicantProfile);

        }

        // POST: ApplicantEducation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,Created, PasswordUpdate,EmailAddress, PhoneNumber, FullName, PrefferredLanguage, TimeStamp")] SecurityLoginPoco applicantLogin)
        {
            applicantLogin.TimeStamp = (byte[])TempData["TimeStamp"];
            applicantLogin.Id = (Guid) Session["UserId"];
            applicantLogin.IsInactive = (bool)TempData["IsInactive"];
            applicantLogin.Created = (DateTime)TempData["Created"];
            applicantLogin.PasswordUpdate = DateTime.Now;
            applicantLogin.ForceChangePassword = (bool)TempData["ForceChangePassword"];
            _logic.Update(new SecurityLoginPoco[] { applicantLogin });
            return RedirectToAction("Index");

        }
    }
}