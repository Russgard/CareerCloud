using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerCloud.MVC.Models
{
    public class AppliedApplicants
    {
        public string Name { get; set; }
        public string Skills { get; set; }
        public string Resume { get; set; }
        [Display(Name = "Phone No.")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid ApplicantID { get; set; }
        [Display(Name = "State/Province")]
        public string State { get; set; }
    }
}