using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    
    public class CustomerTickets
    {

        [Key]
        public string TicketID { get; set; }

        public string TicketDate { get; set; }

        public string CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public string TicketDescription { get; set; }

        public string Status { get; set; }

        public string CompanyID { get; set; }

    }

}