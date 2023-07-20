using System;
using System.ComponentModel.DataAnnotations;

namespace accountseperations.Controllers
{
    public class Category
    {
        public string secaccountno { get; set; }
        public string CategoryName { get; set; }
        [Key]
        public string primaryacctno { get; set; }
        public bool success { get; set; }
        public string message { get; internal set; }
    }
}