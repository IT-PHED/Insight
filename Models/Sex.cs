using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class Sex
    {
        public Sex()
        {
            var SexIdGuid = Guid.NewGuid();
            SexId = SexIdGuid.ToString();
        }

        [Key]
        public string SexId { get; set; }

        public string SexName { get; set; }
    }
}