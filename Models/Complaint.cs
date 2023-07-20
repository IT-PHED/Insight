using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class Complaint
    {

        [Key]
        public string ComplaintID { get; set; }
        public string ComplaintName { get; set; }
    }
}