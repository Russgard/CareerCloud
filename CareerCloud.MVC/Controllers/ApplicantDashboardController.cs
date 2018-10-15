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
    public class ApplicantDashboardController : Controller
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantDashboardController()
        {
            var profileRepo = new EFGenericRepository<ApplicantProfilePoco>(false);
            _logic = new ApplicantProfileLogic(profileRepo);

        }


        // GET: ApplicantDashboard
        public ActionResult Index()
        {
            Guid UserId = (Guid)Session["UserId"];
            Guid _userProfileId = (from x in _logic.GetAll() where x.Login == UserId select x.Id).FirstOrDefault();
            TempData["Applicant"] = _userProfileId;
            return View();
        }
    }
}