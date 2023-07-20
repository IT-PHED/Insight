using System.ComponentModel.DataAnnotations;
namespace accountseperations.Controllers
{
    public sealed class AllSubAcct
    {
        [Key]
        public int sn { get; set; }
        public string primaryact  { get; set; }
        public string secact { get; internal set; }
    }
}