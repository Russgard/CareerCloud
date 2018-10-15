using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public CareerCloudContext(bool createProxy = true) : base ("dbconnection") {
            Configuration.ProxyCreationEnabled = createProxy;
            Database.SetInitializer < CareerCloudContext >(null);
            //Database.Log = Console.WriteLine;
            //s => System.Diagnostics.Debug.WriteLine(s);
        }

        public virtual DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public virtual DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public virtual DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public virtual DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public virtual DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public virtual DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public virtual DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public virtual DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public virtual DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public virtual DbSet<CompanyJobDescriptionPoco> CompanyJobsDescriptions { get; set; }
        public virtual DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public virtual DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public virtual DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public virtual DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public virtual DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public virtual DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public virtual DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public virtual DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(e => e.CurrentSalary)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(e => e.Currency)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(e => e.Country)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(e => e.Province)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(e => e.PostalCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantEducations)
                .WithRequired(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantJobApplications)
                .WithRequired(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantResumes)
                .WithRequired(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantSkills)
                .WithRequired(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(e => e.ApplicantWorkHistory)
                .WithRequired(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Applicant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .Property(e => e.SkillLevel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .Property(e => e.CountryCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .Property(e => e.LanguageId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .Property(e => e.SkillLevel)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyJobPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.ApplicantJobApplications)
                .WithRequired(e => e.CompanyJobs)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.CompanyJobEducations)
                .WithRequired(e => e.CompanyJobs)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.CompanyJobSkills)
                .WithRequired(e => e.CompanyJobs)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(e => e.CompanyJobsDescriptions)
                .WithRequired(e => e.CompanyJobs)
                .HasForeignKey(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyLocationPoco>()
                .Property(e => e.CountryCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CompanyLocationPoco>()
                .Property(e => e.Province)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CompanyLocationPoco>()
                .Property(e => e.PostalCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CompanyLocationPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyProfilePoco>()
                .Property(e => e.CompanyWebsite)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyProfilePoco>()
                .Property(e => e.ContactPhone)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyProfilePoco>()
                .Property(e => e.ContactName)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyProfilePoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(e => e.CompanyDescriptions)
                .WithRequired(e => e.CompanyProfiles)
                .HasForeignKey(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(e => e.CompanyJobs)
                .WithRequired(e => e.CompanyProfiles)
                .HasForeignKey(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(e => e.CompanyLocations)
                .WithRequired(e => e.CompanyProfiles)
                .HasForeignKey(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(e => e.PrefferredLanguage)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.ApplicantProfiles)
                .WithRequired(e => e.SecurityLogins)
                .HasForeignKey(e => e.Login)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.SecurityLoginsLog)
                .WithRequired(e => e.SecurityLogins)
                .HasForeignKey(e => e.Login)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.SecurityLoginsRoles)
                .WithRequired(e => e.SecurityLogins)
                .HasForeignKey(e => e.Login)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .Property(e => e.SourceIP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<SecurityRolePoco>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityRolePoco>()
                .HasMany(e => e.SecurityLoginsRoles)
                .WithRequired(e => e.SecurityRoles)
                .HasForeignKey(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .Property(e => e.Code)
                .IsFixedLength()
                .HasMaxLength(10)
                .IsUnicode(false);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(e => e.ApplicantProfiles)
                .WithOptional(e => e.SystemCountryCodes)
                .HasForeignKey(e => e.Country);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(e => e.ApplicantWorkHistory)
                .WithRequired(e => e.SystemCountryCodes)
                .HasForeignKey(e => e.CountryCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SystemLanguageCodePoco>()
                .Property(e => e.LanguageID)
                //.IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasMany(e => e.CompanyDescriptions)
                .WithRequired(e => e.SystemLanguageCodes)
                .WillCascadeOnDelete(false);
        }

    }
}
