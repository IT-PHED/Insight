MyApp = {};
MyApp.TreeViewModel = function (data) {

    var self = this;
    var mapping = {
        items: {
            create: function (options) {
                return new MyApp.TreeViewModel(options.data);
            }
        },
        "modulesHeadList": {
            create: function (options) {
                return new moduleListVW(options.data);
            }
        }
    };

    self.checked = ko.observable(false);
    self.checked.subscribe(self.onChecked, self);
    self.items = ko.observableArray();
    self.autoNumber = ko.observable(0);
    self.selectedStaff = ko.observable(null);

    self.isProcessing = ko.observable(false);
    self.assignModuleList = ko.observableArray([]);
    ko.mapping.fromJS(data, mapping, self);
    
    var dataToPost = ko.mapping.fromJS(data, mapping, self);

    self.selectUserRole = function () {

        if (self.selectedStaff() == null)
        {
            self.clearTreeView();
            return;
        }

        //console.log("Selected Staff  " + self.selectedStaff().Id());
        var getRoleOfCurrentUser = ko.utils.arrayFilter(self.allStaffRoleList(), function (staffRoleList) {
            return staffRoleList.StaffId() == self.selectedStaff().Id();
        });

        //console.log("ALl Staff Roles " + self.allStaffRoleList());
        ////console.log("ALl Data Selected" + self.items());
        ko.utils.arrayForEach(self.items(), function (item) {

            /////console.log("Item Checked value " + item.checked());

            ko.utils.arrayForEach(item.items(), function (subitem) {
                //  //console.log("Sub Item Checked value " + subitem.checked());
                ////console.log("Staff menu Item " + subitem.MenuItemId);
                var singleUserRole = ko.utils.arrayFirst(getRoleOfCurrentUser, function (role) {
                    return role.MenuItemId() == subitem.MenuItemId();
                });

                if (singleUserRole == null) {
                    subitem.checked(false);
                }
                else {
                    subitem.checked(true);
                }
            });

        });


        // Fetch All Module Assign
        self.isProcessing(true);

        var dataForTreeView = {};
        var staffRoleViewModel;
        $.ajax({
            type: "Get",
            url: '/StaffRole/loadStaffModuleactivity',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {

                'staffId': self.selectedStaff().Id()

            },
            success: function (result) {


                var data = JSON.parse(result);

                var jsData = ko.toJS(result);

                var getAllMenuHeaders = ko.utils.arrayFilter(data.modulesList, function (modulelist) {
                    return modulelist.ControllerName == null;
                });

                var MainnewItems = [];

                ko.utils.arrayForEach(getAllMenuHeaders, function (moduleItem) {
                    var newItems = [];

                    var getAllSubItems = ko.utils.arrayFilter(data.modulesList, function (modulesublist) {
                        return modulesublist.ParentMenuItemId == moduleItem.MenuItemId;
                    });

                    ko.utils.arrayForEach(getAllSubItems, function (subItem) {
                        newItems.push({ "name": subItem.MenuText, "MenuItemId": subItem.MenuItemId, "checked": false })
                    });

                    MainnewItems.push({ "name": moduleItem.MenuText, "items": newItems })
                    //item.checked(checked);
                });



                dataForTreeView = {
                    items: MainnewItems,

                    usersList: data.usersList,
                    allStaffRoleList: data.allStaffRoleList,
                    modulesList: data.modulesList,
                    modulesHeadList: data.modulesHeadList,
                    staffModulesActivityList: data.staffModulesActivityList
                }
                //console.log("Main menu extended " + JSON.stringify(MainnewItems));
                var model = new MyApp.TreeViewModel(dataForTreeView);

                self.modulesHeadList(model.modulesHeadList());

                //self.usersList(model.usersList());
                self.allStaffRoleList(model.allStaffRoleList());
                self.modulesList(model.modulesList());
                // //console.log("head List From Data Base " + JSON.stringify(model.modulesHeadList()));
                self.isProcessing(false);



            },
            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });


    };



  

    self.selectUserRole2 = function () {
        if (self.selectedStaff() == null) {
            self.clearTreeView();
            return;
        }



        var StaffId = $("#StaffId").val(); //self.selectedStaff().Id();


        console.log("Staff Id Searched " + StaffId);



        //console.log("Selected Staff  " + self.selectedStaff().Id());
        var getRoleOfCurrentUser = ko.utils.arrayFilter(self.allStaffRoleList(), function (staffRoleList) {
            return staffRoleList.StaffId() == StaffId;
        });

        //console.log("ALl Staff Roles " + self.allStaffRoleList());
        ////console.log("ALl Data Selected" + self.items());
        ko.utils.arrayForEach(self.items(), function (item) {

            /////console.log("Item Checked value " + item.checked());

            ko.utils.arrayForEach(item.items(), function (subitem) {
                //  //console.log("Sub Item Checked value " + subitem.checked());
                ////console.log("Staff menu Item " + subitem.MenuItemId);
                var singleUserRole = ko.utils.arrayFirst(getRoleOfCurrentUser, function (role) {
                    return role.MenuItemId() == subitem.MenuItemId();
                });

                if (singleUserRole == null) {
                    subitem.checked(false);
                } else {
                    subitem.checked(true);
                }


            });

        });


        // Fetch All Module Assign
        self.isProcessing(false);

        var dataForTreeView = {};
        var staffRoleViewModel;
        $.ajax({
            type: "Get",
            url: '/StaffRole/loadStaffModuleactivity',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {

                'staffId': StaffId

            },
            success: function (result) {


                var data = JSON.parse(result);

                var jsData = ko.toJS(result);

                var getAllMenuHeaders = ko.utils.arrayFilter(data.modulesList, function (modulelist) {
                    return modulelist.ControllerName == null;
                });

                var MainnewItems = [];

                ko.utils.arrayForEach(getAllMenuHeaders, function (moduleItem) {
                    var newItems = [];

                    var getAllSubItems = ko.utils.arrayFilter(data.modulesList, function (modulesublist) {
                        return modulesublist.ParentMenuItemId == moduleItem.MenuItemId;
                    });

                    ko.utils.arrayForEach(getAllSubItems, function (subItem) {
                        newItems.push({ "name": subItem.MenuText, "MenuItemId": subItem.MenuItemId, "checked": false })
                    });

                    MainnewItems.push({ "name": moduleItem.MenuText, "items": newItems })
                    //item.checked(checked);
                });



                dataForTreeView = {
                    items: MainnewItems,

                    usersList: data.usersList,
                    allStaffRoleList: data.allStaffRoleList,
                    modulesList: data.modulesList,
                    modulesHeadList: data.modulesHeadList,
                    staffModulesActivityList: data.staffModulesActivityList
                }
                 
                //var model = new MyApp.TreeViewModel(dataForTreeView); 
                //self.modulesHeadList(model.modulesHeadList());

               
                //self.allStaffRoleList(model.allStaffRoleList());
                //self.modulesList(model.modulesList());
                
                self.isProcessing(false);
                 
            },
            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });


    };

    self.clearTreeView = function () {





        ko.utils.arrayForEach(self.items(), function (item) {



            ko.utils.arrayForEach(item.items(), function (subitem) {


                subitem.checked(false);



            });

        });

    };

    self.assignAndUnAssignRole = function () {


        self.assignModuleList([]);

        ////console.log("ALl Data Selected" + self.items());


        ko.utils.arrayForEach(self.items(), function (item) {

            //console.log("Item Checked value " + item.checked());

            ko.utils.arrayForEach(item.items(), function (subitem) {
                ////console.log("Sub Item Checked value " + subitem.checked());

                // subitem.checked(true);
                if (subitem.checked()) {
                    var getSingleModule = ko.utils.arrayFirst(self.modulesList(), function (mod) {
                        return mod.MenuItemId() == subitem.MenuItemId();

                    });
                    if (getSingleModule != null) {
                        self.assignModuleList.push(getSingleModule);
                    }



                }



            });

        });

        //console.log("Module Item Selected For Assignment " + JSON.stringify(self.assignModuleList()));


        self.isProcessing(true);



        var StaffId = $("#StaffId").val();

        var model = ko.mapping.toJS(self.assignModuleList());
        var dataForTreeView = {};
        var staffRoleViewModel;
        $.ajax({
            type: "Post",
            url: '/StaffRole/AssignUnAssignRoles',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'postedData': JSON.stringify(model),
                'staffId': StaffId

            }),
            success: function (result) {
                swal('Role Assigned!', 'The staff role has been assigned successfully', 'success');

                self.isProcessing(false);

                var data = JSON.parse(result);

                var jsData = ko.toJS(result);

                var getAllMenuHeaders = ko.utils.arrayFilter(data.modulesList, function (modulelist) {
                    return modulelist.ControllerName == null;
                });

                var MainnewItems = [];

                ko.utils.arrayForEach(getAllMenuHeaders, function (moduleItem) {
                    var newItems = [];

                    var getAllSubItems = ko.utils.arrayFilter(data.modulesList, function (modulesublist) {
                        return modulesublist.ParentMenuItemId == moduleItem.MenuItemId;
                    });

                    ko.utils.arrayForEach(getAllSubItems, function (subItem) {
                        newItems.push({ "name": subItem.MenuText, "MenuItemId": subItem.MenuItemId, "checked": false })
                    });

                    MainnewItems.push({ "name": moduleItem.MenuText, "items": newItems })
                    //item.checked(checked);
                });

             

                dataForTreeView = {
                    items: MainnewItems,

                    usersList: data.usersList,
                    allStaffRoleList: data.allStaffRoleList,
                    modulesList: data.modulesList,
                    modulesHeadList: data.modulesHeadList,
                    staffModulesActivityList: data.staffModulesActivityList
                }
                //console.log("Main menu extended " + JSON.stringify(MainnewItems));
                //var model = new MyApp.TreeViewModel(dataForTreeView);
                //self.modulesHeadList(model.modulesHeadList());
                ////self.usersList(model.usersList());
                //self.allStaffRoleList(model.allStaffRoleList());
                //self.modulesList(model.modulesList()); 
                self.isProcessing(false); 
            },
            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    };

    self.grantAccess = function () {





        self.isProcessing(true);





        var model = ko.mapping.toJS(self.modulesHeadList());
        var dataForTreeView = {};
        var staffRoleViewModel;
        $.ajax({
            type: "Post",
            url: '/StaffRole/AddStaffModuleAccess',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'postedData': JSON.stringify(model),
                'staffId': self.selectedStaff().Id()

            }),
            success: function (result) {


                var data = JSON.parse(result);

                var jsData = ko.toJS(result);

                var getAllMenuHeaders = ko.utils.arrayFilter(data.modulesList, function (modulelist) {
                    return modulelist.ControllerName == null;
                });




                var MainnewItems = [];

                ko.utils.arrayForEach(getAllMenuHeaders, function (moduleItem) {
                    var newItems = [];

                    var getAllSubItems = ko.utils.arrayFilter(data.modulesList, function (modulesublist) {
                        return modulesublist.ParentMenuItemId == moduleItem.MenuItemId;
                    });

                    ko.utils.arrayForEach(getAllSubItems, function (subItem) {
                        newItems.push({ "name": subItem.MenuText, "MenuItemId": subItem.MenuItemId, "checked": false })
                    });

                    MainnewItems.push({ "name": moduleItem.MenuText, "items": newItems })
                    //item.checked(checked);
                });

                // //console.log("Main menu  " + JSON.stringify(MainnewItems));

                //dataForTreeView = {
                //    items: MainnewItems
                //}

                dataForTreeView = {
                    items: MainnewItems,

                    usersList: data.usersList,
                    allStaffRoleList: data.allStaffRoleList,
                    modulesList: data.modulesList,
                    modulesHeadList: data.modulesHeadList,
                    staffModulesActivityList: data.staffModulesActivityList
                }
                //console.log("Main menu extended " + JSON.stringify(MainnewItems));
                var model = new MyApp.TreeViewModel(dataForTreeView);
                self.modulesHeadList(model.modulesHeadList());
                //self.usersList(model.usersList());
                self.allStaffRoleList(model.allStaffRoleList());
                self.modulesList(model.modulesList());
                //self.usersList(data.usersList);
                //self.allStaffRoleList(data.allStaffRoleList);
                //self.modulesList(data.modulesList);

                self.isProcessing(false);

                //Command: toastr["success"]("Successfuly")

                //toastr.options = {
                //    "closeButton": false,
                //    "debug": false,
                //    "newestOnTop": false,
                //    "progressBar": false,
                //    "positionClass": "toast-top-center",
                //    "preventDuplicates": false,
                //    "onclick": null,
                //    "showDuration": "300",
                //    "hideDuration": "1000",
                //    "timeOut": "5000",
                //    "extendedTimeOut": "1000",
                //    "showEasing": "swing",
                //    "hideEasing": "linear",
                //    "showMethod": "fadeIn",
                //    "hideMethod": "fadeOut"
                //}
                return;

            },
            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    };

};

MyApp.TreeViewModel.prototype = {
    constructor: MyApp.TreeViewModel,
    onChecked: function (checked) {
        ko.utils.arrayForEach(this.items(), function (item) {
            item.checked(checked);
        });
    }
};


function moduleListVW(data) {

    ////console.log("data list in " + data);
    ////console.log("Module list in " + data.ModuleRightList);
    ////console.log("Default Value  " + data.ActivityDefaultRights);

    var self = this;
    var modulemapping = {

        "moduleActivityList": {
            create: function (options) {
                return new moduleActivityListVW(options.data);
            }
        }
    };


    if (data !== null) {
        ko.mapping.fromJS(data, modulemapping, this);
    }



};

function moduleActivityListVW(data) {

    ////console.log("data list in " + data);
    ////console.log("Module list in " + data.ModuleRightList);
    ////console.log("Default Value  " + data.ActivityDefaultRights);

    var self = this;
    var repMapping = {

        'copy': ['ActivityId']

    };


    if (data !== null) {
        ko.mapping.fromJS(data, repMapping, this);
    }


    self.moduleIndexFromServer = ko.observable(0);


    self.moduleIndexFromServer(functiontofindIndexByKeyValue(data.ModuleRightList, "name", data.ActivityDefaultRights));
    self.mainAccessList = ko.observableArray(data.ModuleRightList);
    //self.mainTepTypeListList = ko.observableArray(repTypeList);
    // //console.log("Index Value  " + self.moduleIndexFromServer());

    self.AccessRight = ko.observable(self.mainAccessList()[self.moduleIndexFromServer()]);












};

 

function RoleAssignmentVM(data) {
        var self = this;
        var ReportViewer = "http://map.nepamsonline.com/ReportPage.aspx?";
   
        self.LGAListFiltered = ko.observableArray();
        self.StateListPushed = ko.observableArray();
        self.selectedStaff = ko.observable();
        self.selectUserRole2 = ko.observable();
        self.isProcessing = ko.observable(false);

        self.IMEI = ko.observable(false);
        self.CheckedValue = ko.observable(false);
        self.IMEI1 = ko.observable(); self.AllowBillVerificationChange = ko.observable();
        self.IMEI2 = ko.observable();
        ko.mapping.fromJS(data, {}, this);


        self.ShowIMEI = function () {
            if (self.CheckedValue()) {

                $("#IMEI").slideDown();
               // self.IMEI(true);

            } else {
                $("#IMEI").slideUp();
               // self.IMEI(false);

            }

        }
        self.loadAssignedRoleToStaff = function () {
            //if (self.selectedStaff() == null) {
            //    self.clearTreeView();
            //    return;
            //}


         //   var StaffId = "d6c60368-f73f-411e-860a-1359f184640e";
       var StaffId = $("#StaffId").val();  


            if (StaffId == null || StaffId == "") {
                swal("Search Staff", "Please search for a staff to Assign Roles.Thank you", "info")
                return;
            }
            showloader();

            console.log("Staff Id Searched " + StaffId);
           
            $.ajax({
                type: "Get",
                url: '/StaffRole/loadAssignedRoleToStaff',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {

                    'staffId': StaffId

                },
                success: function (result) {

                    hideloader();
                    var data = JSON.parse(result);

                    console.log(data.AllMenuLists);

                    $(":ui-fancytree").fancytree("destroy");
                    self.AllMenuLists([]);
                    ko.mapping.fromJS(data.AllMenuLists, {}, self.AllMenuLists);


               
                    console.log("Data from Server "+self.AllMenuLists());
                     LoadStaffMenu();
                   
                    self.isProcessing(false); 
                },
                error: function (err) {
                    hideloader();
                    console.log("Fail Details Request " + err.responseText)
                    self.isProcessing(false);
                }
            });


        }


        self.loadAssignedRoleToStaff2 = function () {
       
       var StaffId = $("#StaffId").val();  


            if (StaffId == null || StaffId == "") {
                swal("Search Staff", "Please search for a staff to Assign Roles.Thank you", "info")
                return;
            }
            showloader();

            console.log("Staff Id Searched " + StaffId);
           
            $.ajax({
                type: "Get",
                url: '/StaffRole/loadAssignedRoleToStaff',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {

                    'staffId': StaffId

                },
                success: function (result) {

                    hideloader();
                    var data = JSON.parse(result);

                    console.log(data.AllMenuLists);

                    $(":ui-fancytree").fancytree("destroy");
                    self.AllMenuLists([]);
                    ko.mapping.fromJS(data.AllMenuLists, {}, self.AllMenuLists);


               
                    console.log("Data from Server "+self.AllMenuLists());
                     LoadStaffMenu();
                   
                    self.isProcessing(false); 
                },
                error: function (err) {
                    hideloader();
                    console.log("Fail Details Request " + err.responseText)
                    self.isProcessing(false);
                }
            });


        }




  self.ReassignRolesToStaff = function () {
             

            //get the Roles that were Assigned to the Staff


            var SelectedRoles = $("#StaffSelectedMenu").val();

            var StaffId = $("#StaffId").val();  


            if (StaffId == null || StaffId == "") {
                swal("Search Staff", "Please search for a staff to Assign Roles.Thank you", "info")
                return;
            }

            if (StaffId == null || StaffId == "") {
                swal("Search Staff", "Please search for a staff to Assign Roles.Thank you", "info")
                return;
            }
            showloader();

            console.log("Staff Id Searched " + StaffId);

            $.ajax({
                type: "Get",
                url: '/StaffRole/ReassignRolesToStaff',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {

                    'staffId': StaffId,
                    'SelectedRoles': SelectedRoles,

                },
                success: function (result) {

                    var data = JSON.parse(result);

                  //  showMessage("Successful", "The selected roles have been assigned to the Staff", "success");
                    
                  swal("Successful", "The selected roles have been assigned to the Staff", "success");
                  
                 
                    //self.isProcessing(false);
                  //  hideloader();

                },
                error: function (err) {
                    hideloader();
                    console.log("Fail Details Request " + err.responseText)
                    self.isProcessing(false);
                }
            });


        }


  self.ReassignRolesToStaff2 = function () {
             

            //get the Roles that were Assigned to the Staff


         

            var StaffId = $("#StaffId1").val();
            var FirstName = $("#FirstName").val();
            var LastName = $("#Surname").val();
            var UserName = $("#Username").val();
            var Email = $("#Email").val();
            var Id = $("#Id").val();
            var StaffName = $("#StaffName").val();
            var Status = $("#Status").val();
            var IMEI1 = $("#IMEI1").val();
            var IMEI2 = $("#IMEI2").val();
            var IMEILogin = self.CheckedValue();
            var AllowBillVerificationChange = self.AllowBillVerificationChange();


         
            if (StaffId == null || StaffId == "") {
                swal("Input Staff Id", "Please input the Staff Id continue.Thank you", "info")
                return;
            }

            if (IMEILogin) {
                if ((IMEI1 == null && IMEI2 == null) || (IMEI1 == '' && IMEI2 == '')) {
                    swal("Input IMEI", "Please input the Staff IMEI to continue.Thank you", "info")
                    return;

                }

            }
          
     


            showloader();

            console.log("Staff Id Searched " + StaffId);

            $.ajax({
                type: "Get",
                url: '/StaffRole/CorrectStaffData',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { 
                    'staffId': StaffId, 
                    'FirstName': FirstName,
                    'LastName': LastName,
                    'UserName': UserName,
                    'Email': Email,
                    'Id': Id,
                    'StaffName': StaffName,
                    'Status': Status,
                    'IMEI1': IMEI1,
                    'IMEI2': IMEI2,
                    'IMEILogin': IMEILogin,
                    'AllowBillVerificationChange': AllowBillVerificationChange
                },
                success: function (result) {

                    var data = JSON.parse(result.result);

                    console.log(" Dere ere r e re "+result.error);
                     
                    //showMessage("Successful", "The selected roles have been assigned to the Staff", "success");
                  
                  if (result.error != "") {

                      swal("Error", result.error, "error");
                      return;
                  }

                  else {
                      if (Status = "EDIT") {

                          swal("Successful", "The Staff Details have been corrected Successfully", "success");

                      }
                      else {

                          swal("Successful", "The Staff Details have been added Successfully", "success");
                      }
                      $("#Status").val("");
                  }
            

                },
                error: function (err) {
                    hideloader();
                    console.log("Fail Details Request " + err.responseText)
                    self.isProcessing(false);
                }
            });


        }




    }
 

function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

function functiontofindIndexByKeyValue(arraytosearch, key, valuetosearch) {

    for (var i = 0; i < arraytosearch.length; i++) {

        if (arraytosearch[i][key] == valuetosearch) {
            return i;
        }
    }
    return null;
}

function hideloader() {

    swal.close();
}

function AllLoadStaffMenu() {

    $(".SM").fancytree({
        checkbox: false,
        selectMode: 3,
        activate: function (event, data) {
            // A node was activated: display its title:
            var node = data.node;
            console.log("Node Title" + node.title); console.log("Node Key" + node.key); console.log("Node Id" + node.id);
        },
        beforeSelect: function (event, data) {
            // A node is about to be selected: prevent this, for folder-nodes:
            if (data.node.isFolder()) {
                // alert(data.node.text());
            }
        },
        //select: function (event, data) {
        //     // A node is about to be selected: prevent this, for folder-nodes:
        //     if (data.node.isFolder()) {

        //     } else {
        //         alert("Child");

        //     }
        //},
        //click: function (event, data) {
        //    var node = data.node,
        //        tt = $.ui.fancytree.getEventTargetType(event.originalEvent);
        //    if (tt === "checkbox") {
        //        // alert(data.node.key);
        //    }
        //    //var nodes = $("#tree-checkbox-hierarchical").fancytree("getActiveNode");
        //    //console.log(nodes.data.id);
        //},
        //select: function (event, data) {
        //    // Get a list of all selected nodes, and convert to a key array:
        //    var selKeys = $.map(data.tree.getSelectedNodes(), function (node) {
        //        //return node.id;
        //        return node.key;
        //    });
        //    var StaffMenu = selKeys.join(", ");
        //    console.log(StaffMenu);

        //    $("#StaffSelectedMenu").val(StaffMenu);
        //    //checkbox: function (event, data) {
        //    //    // Hide checkboxes for folders
        //    //    return data.node.isFolder() ? false : true;
        //    //},
        //},
        //checkbox: function (event, data) {
        //    // Hide checkboxes for folders
        //    return data.node.isFolder() ? false : true;
        //},
        tooltip: function (event, data) {
            // Create dynamic tooltips
            //  alert( data.node.title + " (" + data.node.key + ")");
        },
    });
}
function LoadStaffMenu() {

    $(".tree-checkbox-hierarchical").fancytree({
        checkbox: true,
        selectMode: 3,
        activate: function (event, data) {
            // A node was activated: display its title:
            var node = data.node;
         //   console.log("Node Title" + node.title); console.log("Node Key" + node.key); console.log("Node Id" + node.id);
        },
        beforeSelect: function (event, data) {
            // A node is about to be selected: prevent this, for folder-nodes:
            if (data.node.isFolder()) {
                // alert(data.node.text());
            }
        },
        //select: function (event, data) {
        //     // A node is about to be selected: prevent this, for folder-nodes:
        //     if (data.node.isFolder()) {

        //     } else {
        //         alert("Child");

        //     }
        //},
        click: function (event, data) {
            var node = data.node,
                tt = $.ui.fancytree.getEventTargetType(event.originalEvent);
            if (tt === "checkbox") {
                // alert(data.node.key);
            }
            //var nodes = $("#tree-checkbox-hierarchical").fancytree("getActiveNode");
            //console.log(nodes.data.id);
        },
        select: function (event, data) {
            // Get a list of all selected nodes, and convert to a key array:
            var selKeys = $.map(data.tree.getSelectedNodes(), function (node) {
                //return node.id;
                return node.key;
            });
            var StaffMenu = selKeys.join(", ");
            console.log(StaffMenu);

            $("#StaffSelectedMenu").val(StaffMenu);
            //checkbox: function (event, data) {
            //    // Hide checkboxes for folders
            //    return data.node.isFolder() ? false : true;
            //},
        },
        //checkbox: function (event, data) {
        //    // Hide checkboxes for folders
        //    return data.node.isFolder() ? false : true;
        //},
        tooltip: function (event, data) {
            // Create dynamic tooltips
            //  alert( data.node.title + " (" + data.node.key + ")");
        },
    });
}
function showloader() {
    swal({
        title: "Loading...",
        text: "Please wait",
        imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',

        showConfirmButton: false,
        allowOutsideClick: false
    }); 
};
function showMessage(a,b,c) {
    swal( a,b,c); 
};
//var data = {
//    items: [{
//        "name": "MORPHED",
//        "items": [{
//            "name": "5 Day",
//            "items": [{
//                "CategoryId": 20,
//                "name": "30 day countdown"
//            }, {
//                "CategoryId": 19,
//                "name": "Staffing your program"
//            }, {
//                "CategoryId": 22,
//                "name": "Emergency/Medical Information"
//            }, {
//                "CategoryId": 18,
//                "name": "Promoting your program"
//            }, {
//                "CategoryId": 21,
//                "name": "Week of camp"
//            }]
//        }, {
//            "name": "4 Day",
//            "items": []
//        }, {
//            "name": "1/2 Day",
//            "items": []
//        }, {
//            "name": "Age Targeted",
//            "items": []
//        }]
//    }, {
//        "name": "CREATE",
//        "items": [{
//            "name": "5 Day",
//            "items": []
//        }, {
//            "name": "4 Day",
//            "items": []
//        }, {
//            "name": "1/2 Day",
//            "items": []
//        }]
//    }, {
//        "name": "INNOVATE",
//        "items": [{
//            "name": "5 Day",
//            "items": []
//        }, {
//            "name": "4 Day",
//            "items": []
//        }, {
//            "name": "1/2 Day",
//            "items": []
//        }]
//    }, {
//        "name": "ENVISION",
//        "items": [{
//            "name": "5 Day",
//            "items": []
//        }, {
//            "name": "4 Day",
//            "items": []
//        }, {
//            "name": "1/2 Day",
//            "items": []
//        }]
//    }]
//};

