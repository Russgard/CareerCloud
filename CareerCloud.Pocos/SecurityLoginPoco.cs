using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins")]
    public class SecurityLoginPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Column("Created_Date", TypeName = "datetime2")]
        public DateTime Created { get; set; }
        [Column("Password_Update_Date", TypeName = "datetime2")]
        public DateTime? PasswordUpdate { get; set; }
        [Column("Agreement_Accepted_Date", TypeName = "datetime2")]
        public DateTime? AgreementAccepted { get; set; }
        [Column("Is_Locked")]
        public bool IsLocked { get; set; }
        [Column("Is_Inactive")]
        public bool IsInactive { get; set; }
        [Column("Email_Address")]
        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }
        [Column("Phone_Number")]
        [StringLength(20)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [Column("Full_Name")]
        [StringLength(100)]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [Column("Force_Change_Password")]
        public bool ForceChangePassword { get; set; }
        [Column("Prefferred_Language")]
        [StringLength(10)]
        [DisplayName("Prefferred Language")]
        public string PrefferredLanguage { get; set; }
        [Column("Time_Stamp", TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }

    }
}
