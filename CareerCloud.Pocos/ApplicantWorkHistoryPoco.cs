using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        [Column("Company_Name")]
        [Required]
        [StringLength(150)]
        public string CompanyName { get; set; }
        [Column("Country_Code")]
        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(50)]
        public string Location { get; set; }
        [Column("Job_Title")]
        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }
        [Column("Job_Description")]
        [Required]
        [StringLength(500)]
        public string JobDescription { get; set; }
        [Column("Start_Month")]
        public Int16 StartMonth { get; set; }
        [Column("Start_Year")]
        public int StartYear { get; set; }
        [Column("End_Month")]
        public Int16 EndMonth { get; set; }
        [Column("End_Year")]
        public int EndYear { get; set; }
        [Column("Time_Stamp", TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public virtual ApplicantProfilePoco ApplicantProfiles { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCodes { get; set; }
    }
}
