using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerCloud.MVC.Models
{
    public class CompanyJobPost
    {
        [Display(Name = "Job Name")]
        public string JobName { get; set;}
        [Display(Name = "Description")]
        public string JobDescription { get; set; }

        [Display(Name = "Education Major")]
        public string Major { get; set; }
        [Display(Name = "Education Importance")]
        public Int16 EduImportance { get; set; }

        [Display(Name = "Skills Requered")]
        public string Skill { get; set; }
        [Display(Name = "Skills Level")]
        public string SkillLevel { get; set; }
        [Display(Name = "Skills' Importance")]
        public int SkillImportance { get; set; }

        public Guid JobID { get; set; }
        public DateTime Created { get; set; }
        [Display(Name = "Is Inactive?")]
        public bool IfInactive { get; set; }
        [Display(Name = "Is Hidden?")]
        public bool IfHidden { get; set; }


    }
}