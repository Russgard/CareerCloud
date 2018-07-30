using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco
    {
        [Key]
        [StringLength(10)]
        public string LanguageID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("Native_Name")]
        [Required]
        [StringLength(50)]
        public string NativeName { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }

    }
}
