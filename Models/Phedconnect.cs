using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
{


    public class Customerfeedbacks
    {
        [Key]
        public int Feedback_Id { get; set; }
        public DateTime? DateCommented { get; set; }
        public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string Comments { get; set; }
        public string ModuleName { get; set; }
        public string ModuleRating { get; set; }
        public string ServiceName { get; set; }
        public string ServiceRating { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
    public class CustomerfeedbackModel
    {


        public DateTime? DateCommented { get; set; }
        [Key]
        public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string Comments { get; set; }
        public string ModuleName { get; set; }
        public string ModuleRating { get; set; }
        public string ServiceName { get; set; }
        public string ServiceRating { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }

    public class Customercomplaints
    {
        [Key]
        public int Feedback_Id { get; set; }
        public DateTime? DateCommented { get; set; }
        public string AccountNo { get; set; }
        public string Mobileno { get; set; }
        public string ComplaintsDetails { get; set; }

        public string complainttype { get; set; }
    }

    public class CustomercomplaintsModel
    {
        public DateTime? DateCommented { get; set; }
        [Key]
        public string AccountNo { get; set; }
        public string Mobileno { get; set; }
        public string ComplaintsDetails { get; set; }


        public string complainttype { get; set; }
    }

}
