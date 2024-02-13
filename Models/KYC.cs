using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
{
    public class KYC
    {
        [Key]
        public int SERIAL { get; set; }
        public string ACCOUNT_NO { get; set; }
        public string METER_NO { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string PHONE { get; set; } 
        public string E_MAIL { get; set; } 
        public DateTime? DATE_OF_BIRTH { get; set; }
        public string UPDATED_BY { get; set; } 
        public string ADDRESS { get; set; }
        public string CustomerName { get; set; } 
        public string CustomerSurname { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerAddress { get; set; }
        public int? DayOfBirth { get; set; }
        public string MonthOfBirth { get; set; }

        public string BVN { get; set; }

        public string SMS { get; set; }

        public string EmailCheck { get; set; }

        public string HardCopy { get; set; }

        public string ResidentType { get; set; }

        public string TENANT_PHONE { get; set; }

        public string TENANT_PHONE2 { get; set; }

        public string TENTANCY_DURATION { get; set; }

        public string T_D_START_DATE { get; set; }
        public string T_D_END_DATE { get; set; }
    }


}
