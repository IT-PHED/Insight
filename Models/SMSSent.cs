using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
{
   public class SMSSent
    {
       [Key]
       public int Serial { get; set; }
       public string Status { get; set; }
       public DateTime BirthDate { get; set; }
        
    }
}
