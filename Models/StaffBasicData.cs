using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
     
    public class StaffBasicData
    {
        [Key]
        public string Staff_Id { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

        public string Phone { get; set; }

        public string CUG { get; set; }

        public string Email { get; set; }

        public string DepartmentID { get; set; }

        public string TeamID { get; set; }


        public string Department { get; set; }

        public string IBC { get; set; }

        public string BSU { get; set; }

        public string Status { get; set; }

        public string IMEI1 { get; set; }

        public string IMEI2 { get; set; }

        public string IMEILogin { get; set; }
    }


}