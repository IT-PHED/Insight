
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class MAP_CONTRACTOR
    {
        [Key]
        public string ContractorId { get; set; }

        public string Phone { get; set; }

        public string ContractorName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Surname { get; set; } 
        public string ProviderId { get; set; }

    }
}