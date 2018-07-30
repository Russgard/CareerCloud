using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Resumes")]
    public class ApplicantResumePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        [Required]
        public string Resume { get; set; }
        [Column("Last_Updated", TypeName = "datetime2")]
        public DateTime? LastUpdated { get; set; }

        public virtual ApplicantProfilePoco ApplicantProfiles { get; set; }
    }
}
