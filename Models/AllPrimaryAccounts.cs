using System.ComponentModel.DataAnnotations;
namespace accountseperations.Controllers
{
    internal class AllPrimaryAccounts
    {
        [Key]
        public string sn { get; set; }
        public string primaryaccount{ get; set; }
        public string requestdate  { get; set; }
        public string requestbyname  { get; set; }

        public string pending  { get; internal set; }
    }
}