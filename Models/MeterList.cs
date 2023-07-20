using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
{
   public class MeterList
    {
       [Key]
       public string MeterNo { get; set; }
       public string MAPVendor { get; set; }
       public string InstallationStatus { get; set; }
       public DateTime? DateInstalled { get; set; }
       public string InstalledBy { get; set; }


       public string ApprovalStatus { get; set; }
       public string ApprovedBy { get; set; }
       public DateTime?   DateApproved { get; set; }
    }
   public class DailyMeterReading
   {

       public DateTime Date_M { get; set; }
       public Double QtyValue { get; set; }  
       public string SerialNumber { get; set; }   
       public string PlantNumber { get; set; }

       public string Days { get; set; }
   }

}
