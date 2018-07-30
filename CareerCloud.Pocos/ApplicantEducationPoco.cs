using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Educations")]
    public class ApplicantEducationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        [Required]
        [StringLength(100)]
        public string Major { get; set; }
        [Column("Certificate_Diploma")]
        [StringLength(100)]
        public string CertificateDiploma { get; set; }
        [Column("Start_Date", TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column("Completion_Date", TypeName = "date")]
        public DateTime? CompletionDate { get; set; }
        [Column("Completion_Percent")]
        public byte? CompletionPercent { get; set; }
        [Column("Time_Stamp", TypeName = "timestamp")]       
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public virtual ApplicantProfilePoco ApplicantProfiles { get; set; }

    }
}
