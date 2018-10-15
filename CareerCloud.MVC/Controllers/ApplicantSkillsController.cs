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
    public class ApplicantSkillsController : Controller
    {
        // GET: ApplicantSkills
        public class ApplicantEducationController : Controller
        {
            private readonly ApplicantSkillLogic _logic;

            public ApplicantEducationController()
            {
                var repo = new EFGenericRepository<ApplicantSkillPoco>(false);
                _logic = new ApplicantSkillLogic(repo);
            }
            public ApplicantEducationController(IDataRepository<ApplicantSkillPoco> repo)
            {
                _logic = new ApplicantSkillLogic(repo);
            }


            // GET: ApplicantEducation
            [HttpGet]
            public ActionResult Index()
            {
                Guid _userProfileId = (Guid)TempData["Applicant"];
                TempData.Keep();
                List<ApplicantSkillPoco> pocos = new List<ApplicantSkillPoco>();
                object _skillID = null;
                try
                {
                    _skillID = (from x in _logic.GetAll() where x.Applicant == _userProfileId select x.Id).FirstOrDefault();
                    pocos = _logic.GetAll().Where<ApplicantSkillPoco>(T => T.Applicant == _userProfileId).ToList();
                }
                catch { }
                finally { }

                if (pocos == null)
                {
                    return RedirectToAction("Create");

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
            public ActionResult Create([Bind(Include = "Id,Applicant,Skill,SkillLevel,StartMonth,StartYear, EndMonth, EndYear")] ApplicantSkillPoco applicantSkill)
            {
                Guid _userProfileId = (Guid)TempData["Applicant"];
                TempData.Keep();
                applicantSkill.Id = Guid.NewGuid();
                applicantSkill.Applicant = _userProfileId;
                _logic.Add(new ApplicantSkillPoco[] { applicantSkill });
                return RedirectToAction("Index");

            }
            // POST: ApplicantResume/Edit/
            //Get 
            public ActionResult Edit(Guid id)
            {
                ApplicantSkillPoco applicant_Skill = _logic.Get(id);
                if (applicant_Skill == null)
                {
                    return HttpNotFound();
                }
                TempData["TimeStamp"] = applicant_Skill.TimeStamp;
                return View(applicant_Skill);

            }

            // POST: ApplicantEducation/Edit
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,Applicant,Skill,SkillLevel,StartMonth,StartYear, EndMonth, EndYear, TimeStamp")] ApplicantSkillPoco applicantSkill)
            {

                Guid _userProfileId = (Guid)TempData["Applicant"];
                TempData.Keep();
                applicantSkill.TimeStamp = (byte[])TempData["TimeStamp"];
                applicantSkill.Applicant = _userProfileId;
                _logic.Update(new ApplicantSkillPoco[] { applicantSkill });
                return RedirectToAction("Index");

            }

            // GET: ApplicantEducation/Delete
            public ActionResult Delete(Guid id)
            {
                ApplicantSkillPoco applicantSkill = _logic.Get(id);
                if (applicantSkill == null)
                {
                    return HttpNotFound();
                }
                return View(applicantSkill);
            }

            // POST: ApplicantEducation/Delete
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(Guid id)
            {
                ApplicantSkillPoco applicantSkill = _logic.Get(id);
                _logic.Delete(new ApplicantSkillPoco[] { applicantSkill });
                return RedirectToAction("Index");
            }

        }

    }
}