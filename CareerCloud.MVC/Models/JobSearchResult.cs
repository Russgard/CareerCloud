using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerCloud.MVC.Models
{
    public class JobSearchResult
    {
        public Guid JobID { get; set; }
        public Guid CompID { get; set; }
        [Display(Name = "Job Name")]
        public string JobName { get; set; }
        [Display(Name = "Description")]
        public string JobDescription { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public bool Applied { get; set; }
    }
}