using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace CareerCloud.WCF
{
    class Applicant : IApplicant
    {
        public void AddApplicantEducation(ApplicantEducationPoco[] pocos)
        {
            ApplicantEducationLogic logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>(false));
            logic.Add(pocos);
        }

        public void AddApplicantJobApplication(ApplicantJobApplicationPoco[] pocos)
        {
            ApplicantJobApplicationLogic logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>(false));
            logic.Add(pocos);

        }

        public void AddApplicantProfile(ApplicantProfilePoco[] pocos)
        {
            ApplicantProfileLogic logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>(false));
            logic.Add(pocos);
        }

        public void AddApplicantResume(ApplicantResumePoco[] pocos)
        {
            ApplicantResumeLogic logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>(false));
            logic.Add(pocos);
        }

        public void AddApplicantSkill(ApplicantSkillPoco[] pocos)
        {
            ApplicantSkillLogic logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>(false));
            logic.Add(pocos);
        }

        public void AddApplicantWorkHistory(ApplicantWorkHistoryPoco[] pocos)
        {
            ApplicantWorkHistoryLogic logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>(false));
            logic.Add(pocos);
        }

        public List<ApplicantEducationPoco> GetAllApplicantEducation()
        {
            ApplicantEducationLogic logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>(false));
            return logic.GetAll();
        }

        public List<ApplicantJobApplicationPoco> GetAllApplicantJobApplication()
        {
            ApplicantJobApplicationLogic logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>(false));
            return logic.GetAll();
        }

        public List<ApplicantProfilePoco> GetAllApplicantProfile()
        {
            ApplicantProfileLogic logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>(false));
            return logic.GetAll();
        }

        public List<ApplicantResumePoco> GetAllApplicantResume()
        {
            ApplicantResumeLogic logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>(false));
            return logic.GetAll();
        }

        public List<ApplicantSkillPoco> GetAllApplicantSkill()
        {
            ApplicantSkillLogic logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>(false));
            return logic.GetAll();
        }

        public List<ApplicantWorkHistoryPoco> GetAllApplicantWorkHistory()
        {
            ApplicantWorkHistoryLogic logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>(false));
            return logic.GetAll();
        }

        public ApplicantEducationPoco GetSingleApplicantEducation(Guid Id)
        {
            ApplicantEducationLogic logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>(false));
            return logic.Get(Id);
        }

        public ApplicantJobApplicationPoco GetSingleApplicantJobApplication(Guid Id)
        {
            ApplicantJobApplicationLogic logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>(false));
            return logic.Get(Id);
        }

        public ApplicantProfilePoco GetSingleApplicantProfile(Guid Id)
        {
            ApplicantProfileLogic logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>(false));
            return logic.Get(Id);
        }

        public ApplicantResumePoco GetSingleApplicantResume(Guid Id)
        {
            ApplicantResumeLogic logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>(false));
            return logic.Get(Id);
        }

        public ApplicantSkillPoco GetSingleApplicantSkill(Guid Id)
        {
            ApplicantSkillLogic logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>(false));
            return logic.Get(Id);
        }

        public ApplicantWorkHistoryPoco GetSingleApplicantWorkHistory(Guid Id)
        {
            ApplicantWorkHistoryLogic logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>(false));
            return logic.Get(Id);
        }

        public void RemoveApplicantEducation(ApplicantEducationPoco[] pocos)
        {
            ApplicantEducationLogic logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveApplicantJobApplication(ApplicantJobApplicationPoco[] pocos)
        {
            ApplicantJobApplicationLogic logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveApplicantProfile(ApplicantProfilePoco[] pocos)
        {
            ApplicantProfileLogic logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveApplicantResume(ApplicantResumePoco[] pocos)
        {
            ApplicantResumeLogic logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveApplicantSkill(ApplicantSkillPoco[] pocos)
        {
            ApplicantSkillLogic logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>(false));
            logic.Delete(pocos);
        }

        public void RemoveApplicantWorkHistory(ApplicantWorkHistoryPoco[] pocos)
        {
            ApplicantWorkHistoryLogic logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>(false));
            logic.Delete(pocos);
        }

        public void UpdateApplicantEducation(ApplicantEducationPoco[] pocos)
        {
            ApplicantEducationLogic logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>(false));
            logic.Update(pocos);
        }

        public void UpdateApplicantJobApplication(ApplicantJobApplicationPoco[] pocos)
        {
            ApplicantJobApplicationLogic logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>(false));
            logic.Update(pocos);

        }

        public void UpdateApplicantProfile(ApplicantProfilePoco[] pocos)
        {
            ApplicantProfileLogic logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>(false));
            logic.Update(pocos);
        }

        public void UpdateApplicantResume(ApplicantResumePoco[] pocos)
        {
            ApplicantResumeLogic logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>(false));
            logic.Update(pocos);
        }

        public void UpdateApplicantSkill(ApplicantSkillPoco[] pocos)
        {
            ApplicantSkillLogic logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>(false));
            logic.Update(pocos);
        }

        public void UpdateApplicantWorkHistory(ApplicantWorkHistoryPoco[] pocos)
        {
            ApplicantWorkHistoryLogic logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>(false));
            logic.Update(pocos);
        }
    }
}
