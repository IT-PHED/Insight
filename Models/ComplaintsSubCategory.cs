using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class ComplaintsSubCategory
    {
        [Key]
        public string ComplaintSubCategoryID { get; set; }

        public string ComplaintSubCategoryName { get; set; }

        public string ComplaintCategoryID { get; set; }
    }

    public class ComplaintCategory
    {
        [Key]
        public string ComplaintCategoryID { get; set; }

        public string ComplaintCategoryName { get; set; }

    }
}