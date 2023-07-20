using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{
    public class ModuleActivity
    {
        [Key]
        public Guid ActivityId { get; set; }

        public string ActivityName { get; set; }
        [ForeignKey("AccessRight")]
        public string ActivityDefaultRights { get; set; }
        public virtual AccessRight AccessRight { get; set; }
       // public  List<AccessRight> ModuleRightList { get; set; }

        public string MenuItemId { get; set; }
        public string ActivityDesc { get; set; }

        public List<AccessRight> ModuleRightList
        {
            get
            {
                return  new List<AccessRight>
                                    {
                                        new AccessRight(){ name = "No Access", value= "No Access" },
                                        new AccessRight(){ name = "Read Only", value="Read Only" },
                                        new AccessRight(){ name = "Write Only", value="Read Write" },
                                        new AccessRight(){ name = "Read and Write", value="Read and Write" },
                                        new AccessRight(){ name = "Read and Approve", value="Read and Approve" }
                                       
                                    }; 
            }
            set
            {
               new List<AccessRight>
                                    {
                                        new AccessRight(){ name = "No Access", value= "No Access" },
                                        new AccessRight(){ name = "Read Only", value="Read Only" },
                                        new AccessRight(){ name = "Write Only", value="Read Write" },
                                        new AccessRight(){ name = "Read and Write", value="Read and Write" },
                                        new AccessRight(){ name = "Read and Approve", value="Read and Approve" }
                                       
                                    };
            }
        }

        
    }
}