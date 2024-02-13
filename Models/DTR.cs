using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class DTR
    {
        [Key]
        public string FeederId { get; set; }
        public string FeederName { get; set; }
        public string DTRId { get; set; }
        public string DTRName { get; set; }
    }
}