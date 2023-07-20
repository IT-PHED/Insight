using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDMAP.Models
{
    public class BATCH
    {
        [Key]
        public string BatchId { get; set; }

        public string BatchName { get; set; }

        public string BatchNumber { get; set; }

        public string BatchDate { get; set; }

    }
}
