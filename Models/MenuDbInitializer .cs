using PHEDServe.Models;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MRsoftEsPWebV2.Models
{
    public class MenuDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {

                        var menuItems = new List<MenuItem>{
                new MenuItem{MenuText = "First Link", LinkUrl = "#", MenuOrder = 1},
                new MenuItem{MenuText = "Second Link", LinkUrl = "#", MenuOrder = 2},
                new MenuItem{MenuText = "Third Link", LinkUrl = "#", MenuOrder = 3},
                new MenuItem{MenuText = "Fourth Link", LinkUrl = "#", MenuOrder = 4},
                new MenuItem{MenuText = "Fifth Link", LinkUrl = "#", MenuOrder = 5},
                new MenuItem{MenuText = "First Child Link", LinkUrl = "#", MenuOrder = 1, ParentMenuItemId = 1},
                new MenuItem{MenuText = "Second Child Link", LinkUrl = "#", MenuOrder = 2, ParentMenuItemId = 1},
                new MenuItem{MenuText = "Third Child Link", LinkUrl = "#", MenuOrder = 3, ParentMenuItemId = 1},
                new MenuItem{MenuText = "First Grandchild Link", LinkUrl = "#",  MenuOrder = 1, ParentMenuItemId = 7},
                new MenuItem{MenuText = "Second Grandchild Link", LinkUrl = "#", MenuOrder = 2, ParentMenuItemId = 7},
                new MenuItem{MenuText = "Third Grandchild Link", LinkUrl = "#", MenuOrder = 3, ParentMenuItemId = 7}
            };



                        //foreach (MenuItem std in menuItems)
                        //    context.Menus.Add(std);


                        //menuItems.ForEach(s => context.MenuItems.Add(s));
                        //context.SaveChanges();

                        Menu menu = new Menu();

                        menu.MenuItems = menuItems;
                        context.Menus.Add(menu);
                        context.SaveChanges();
        }
    }
}