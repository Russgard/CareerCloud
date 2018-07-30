using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Required]
        [StringLength(10)]
        public string LanguageId { get; set; }
        [Column("Company_Name")]
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }
        [Column("Company_Description")]
        [Required]
        [StringLength(1000)]
        public string CompanyDescription { get; set; }
        [Column("Time_Stamp", TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfilePoco CompanyProfiles { get; set; }
        public virtual SystemLanguageCodePoco SystemLanguageCodes { get; set; }
    }
}
