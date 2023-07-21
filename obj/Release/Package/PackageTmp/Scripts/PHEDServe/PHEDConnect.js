function PHEDConnectViewModel(data) {
    var self = this;

    ko.mapping.fromJS(data, {}, self);
     
    //self.ApprovalList = ko.observableArray(approvals); 
    //self.PaymentStatusList = ko.observableArray(payment);
     
    //self.AuditActivityList = ko.observableArray(AuditActivity);
    //self.PhaseList = ko.observableArray(phase);
    //self.MAPStatusList = ko.observableArray(MAPStatus);
    self.message = ko.observable("No Data for Preview Available Now");

    self.SelectedPhase = ko.observable();
    self.Category = ko.observable();
    self.ModeOfDelivery = ko.observable();
    self.SelectedYear = ko.observable();
    self.ModeOfProcess = ko.observable();
    self.BRCApprovalIBCDate = ko.observable();
    self.BRCApprovalIBCHeadComment = ko.observable();
    self.BRCApprovalCSMComment = ko.observable();
    self.BRCApprovalIBCAmount = ko.observable(); 
    self.SelectedIBC = ko.observable();
    self.SelectedAuditStatus = ko.observable();
    self.SelectedBSC = ko.observable();
    self.SelectedPaymentStatus = ko.observable();
    self.filteredBSCList = ko.observableArray();
    self.PaymentList = ko.observableArray(); 
    self.CSMApprovalStatus = ko.observableArray(); 
    self.FromDate = ko.observable();
    self.ToDate = ko.observable();
    self.CSMApprovalDate = ko.observableArray();
    self.CSMApprovalAmount = ko.observableArray();
    self.SelectedApproval = ko.observableArray(); 
    self.ApprovePaymentComment = ko.observableArray();
    self.SelectedPaymentList = ko.observableArray();
    self.someBoolProperty = ko.observable(false); 
    self.SelectedYear  = ko.observable();
    self.SelectedMonth = ko.observable();
    self.AccountType = ko.observable();

    self.Zone = ko.observable();
    self.Feeder = ko.observable();
    self.Address = ko.observable(); 
    self.AccountType = ko.observable(); 
    self.DateOfDiscon = ko.observable();
    self.Settlement_Agreement = ko.observable();
    self.DisconStatus = ko.observable();
    self.CustomerEmail = ko.observable();
    self.CustomerPhone = ko.observable();
    self.Arrears = ko.observable();
     
    self.SelectedCategory = ko.observable();

    self.SelectedGang = ko.observable();

    //------------------------------

    self.RPDApproval = ko.observable();
    self.DisconID = ko.observable();
    self.IADApproval = ko.observable();
    self.IADApprovalDate = ko.observable();
    self.RPDApprovalDate = ko.observable();
    self.IADApprovalComments = ko.observable();
    self.RPDApprovalComments = ko.observable();
    self.RPDCalculatedLoad = ko.observable();
    self.RPDLossOfRevenueAmount = ko.observable();
    self.RPDLossOfRevenueAvailabilty = ko.observable();
    self.RPDLossOfRevenueInfractionDuration = ko.observable();
    self.SubCategory = ko.observable();
    self.filteredSubCategoryList = ko.observable();


    self.iSelectedSubCategory = ko.observable();
    self.iSelectedCategory = ko.observable();
    //-----------------------------

    self.selectedStaff = ko.observable();


    self.isProcessing = ko.observable(false);

    self.deleteGang = function (data) {


        var table = $('#EbillsDatatable').DataTable();

        table.destroy();
        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDConnect/deleteGang',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'GangId': data.Gang_ID() 
            }),
            success: function (result) {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.GangList, {}, self.GangList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err) {
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });

    }

    self.deleteGangMember = function (data) {


        ////var table = $('#EbillsDatatable').DataTable();

        ////table.destroy();
        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDConnect/deleteGang',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'GangId': data.Gang_ID() 
            }),
            success: function (result) {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.GangList, {}, self.GangList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err) {
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });

    }

    self.AddGangMemberToGang = function (data) {

        self.GangMember.StaffID($("#TLGSI").val());
        self.GangMember.StaffEmail($("#TLGE").val());
        self.GangMember.StaffName($("#TLGN").val());

        self.GangMember.Gang_ID(self.SelectedGang().GangID());

        self.GangMember.GangID(self.SelectedGang().GangID());
         
        var Gang = ko.mapping.toJS(self.GangMember);
        //var table = $('#EbillsDatatable').DataTable();
        //table.destroy();
        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDConnect/AddGangMemberToGang',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'GangModel': JSON.stringify(Gang),
                'CreatedBy': $("#StaffId").val()
            }),
            success: function (result) {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.RCDCMemberList, {}, self.RCDCMemberList);
              //  PaginateTable('EbillsDatatable');
            },
            error: function (err) {
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });
         
    }



    self.AddGangToList = function (data) {

        self.GangModel.TeamLeadStaff_ID($("#TLGSI").val());
        self.GangModel.TeamLeadEmail($("#TLGE").val());
        self.GangModel.TeamLeadName($("#TLGN").val()); 
        self.GangModel.Zone(self.SelectedIBC().IBCId()); 
        self.GangModel.FeederName(self.SelectedBSC().BSCName());
        self.GangModel.Feeder(self.SelectedBSC().BSCId());
        self.GangModel.Feeder(self.SelectedBSC().BSCId());
        //.................................................

        var Gang = ko.mapping.toJS(self.GangModel);
        ////  var table = $('#EbillsDatatable').DataTable();
        ////table.destroy();
        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDConnect/AddGangToList',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'GangModel': JSON.stringify(Gang),
                'CreatedBy': $("#StaffId").val()
            }),
            success: function (result) {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.GangList, {}, self.GangList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err) {
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });

    }






    self.AccountNo = ko.observable(); self.AccountName = ko.observable();
 
    
    self.BaseURL = ko.observable("http://map.nepamsonline.com/"); 
    self.SelectedVendor  = ko.observable();
    self.SelectedPaymentMode  = ko.observable();
    self.InstallationStatus = ko.observable();
    self.ActionDate = ko.observable();
    self.AgreementStatus = ko.observable();
 

    self.ProcessAction = function (event) {
         
        console.log("Feeder Id "+self.SelectedBSC().BSCId()); 
        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        console.log("Debt Profile " + self.ModeOfDelivery()); 
        console.log("Action Cateogry " + self.Category());
        console.log("Account Type " + self.AccountType());
        console.log("Account Date " + self.ActionDate());

        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        } 
        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       if (self.AccountType() == null) {
           swal('Select Account Type', 'Please select the Account Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.Category() == null) {
           swal('Select Category', 'Please select the Category Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.ActionDate() == null) {
           swal('Select ActionDate', 'Please select the ActionDate to Proceed.Thank you', 'info');
           return;
       }
       if (self.ModeOfDelivery() == null) {
           swal('Select Debt Profile', 'Please select the Debt Profile to Proceed.Thank you', 'info');
           return;
       }

 

        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDConnect/ProcessAction',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Zone': self.SelectedIBC().IBCId(),
                'Feeder': self.SelectedBSC().BSCId(),
                'ActionDate': self.ActionDate(),
                'DebtProfile': self.ModeOfDelivery(),
                'Category': self.Category(),
                'AccountType': self.AccountType(),
                'CreatedBy': $("#StaffId").val()
            }),
            success: function (result) {
                var data = JSON.parse(result.result);
                hideloader();

                ko.mapping.fromJS(data.DisconnectionList, {}, self.DisconnectionList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err) {
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });

    }



    self.RPDApprovalLOR = function (d) {

        showloader();
        $('#LORModal').appendTo("body").modal('hide');
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/RPDApprovalLOR',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'DisconId': self.DisconID(),
                'Availability': self.RPDLossOfRevenueAvailabilty(),
                'DurationOfInfraction': self.RPDLossOfRevenueInfractionDuration(),
                'Load': self.RPDCalculatedLoad(),
                'AccountNo': self.AccountNo(),
                'ApprovalStatus': self.RPDApproval(),
                'ApprovalComment': self.RPDApprovalComments(),
                'StaffId':  $("#StaffId").val()
            }),
            success: function (result) {

                hideloader();
                var data = JSON.parse(result.result);

               // ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
                swal("Approved", "The Customer's Loss of Revenue has been approved.", "success");
                 
            },
            error: function (err)
            {
                hideloader();
                self.message("The Request could not be Completed!");
                console.log(err.responseText);
            }
        });
    }


    self.IADApprovalLOR = function (d) {

        showloader();
        $('#LORModal').appendTo("body").modal('hide');
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/IADApprovalLOR',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'DisconId': self.DisconID(),
                
                'AccountNo': self.AccountNo(),
                'ApprovalStatus': self.IADApproval(),
                'ApprovalComment': self.IADApprovalComments(),
                'StaffId':  $("#StaffId").val()
            }),
            success: function (result) {

                hideloader();
                var data = JSON.parse(result.result);

               // ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
                swal("Approved", "The Customer's Loss of Revenue has been approved.", "success");
                 
            },
            error: function (err)
            {
                hideloader();
                self.message("The Request could not be Completed!");
                console.log(err.responseText);
            }
        });
    }





    self.DisconnectedCustomers = function (event) {
         
        console.log("Feeder Id "+self.SelectedBSC().BSCId()); 
        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        
        console.log("Action Cateogry " + self.Category());
        console.log("Account Type " + self.AccountType());
       

        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        } 
        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       if (self.AccountType() == null) {
           swal('Select Account Type', 'Please select the Account Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.Category() == null) {
           swal('Select Category', 'Please select the Category Type to Proceed.Thank you', 'info');
           return;
       }
      
       showloader();


        $.ajax({
            type: "POST",
            url: '/PHEDConnect/DisconnectedCustomers',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Zone': self.SelectedIBC().IBCId(),
                'Feeder': self.SelectedBSC().BSCId(), 
                'Category': self.Category(),
                'AccountType': self.AccountType(),
                'CreatedBy': $("#StaffId").val()
            }),
            success: function (result)
            {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.DisconnectionList, {}, self.DisconnectionList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err)
            {
                hideloader();
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        }); 
    }

    self.openDocumentHref = function (a) {
         
        console.log(a.DOCUMENT_PATH());
         
          
        var ReportViewers = a.DOCUMENT_PATH();
        var url = ReportViewers;
        window.open(url, '_blank');
    }


    self.NewConnectionApprovalFeeder = function (event)
    { 
        console.log("Feeder Id " + self.SelectedBSC().BSCId());

        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        
        console.log("Action Cateogry " + self.Category());

        console.log("Account Type " + self.AccountType());
        
        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        }

        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       if (self.AccountType() == null) {
           swal('Select Account Type', 'Please select the Account Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.Category() == null) {
           swal('Select Category', 'Please select the Category Type to Proceed.Thank you', 'info');
           return;
       }

       var FromDate = self.FromDate();
       var ToDate = self.ToDate();;
     
       //var table = $('#EbillsDatatable2').DataTable();
        
       //     table.destroy();

            showloader();
    
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/NewConnectionApprovalFeeder',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Zone': self.SelectedIBC().IBCId(),
                'Feeder': self.SelectedBSC().BSCId(), 
                'Category': self.Category(),
                'AccountType': self.AccountType(),
                'CreatedBy': $("#StaffId").val(),
                'Status': $("#ApprovalStatus").val(),
                'FromDate': FromDate,
                'ToDate': ToDate,
            }),
            success: function (result)
            {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.NewCustomerOnboardList, {}, self.NewCustomerOnboardList);
                PaginateTable('EbillsDatatable2'); PaginateTable('EbillsDatatable');
            },
            error: function (err)
            {
                hideloader();
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        }); 
    }

 self.IreportReports = function (event) {
         
        console.log("Feeder Id " + self.SelectedBSC().BSCId());

        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        
        console.log("Action Cateogry " + self.Category());

        console.log("Account Type " + self.AccountType());
        
        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        }

        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       
        if (self.iSelectedCategory() == null) {
            swal('Select Category', 'Please select the category to Proceed.Thank you', 'info');
            return;
        }

        if (self.iSelectedSubCategory() == null) {
            swal('Select Feeder', 'Please select the sub Category to Proceed.Thank you', 'info');
            return;
        }
       var FromDate = self.FromDate();
       var ToDate = self.ToDate();;
     
       //var table = $('#EbillsDatatable2').DataTable();
        
       //     table.destroy();

            showloader();
    
            console.log("alagbada Ina " + self.iSelectedSubCategory().ReportSubCategoryId());

           
    
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/IreportReports',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Zone': self.SelectedIBC().IBCId(),
                'Feeder': self.SelectedBSC().BSCId(), 
                'Category': self.iSelectedCategory().ReportCategoryId(),
                'CreatedBy': $("#StaffId").val(),
                'Status': $("#ApprovalStatus").val(),
                'FromDate': FromDate,
                'ToDate': ToDate,
                'Subcategory': self.iSelectedSubCategory().ReportSubCategoryId(),
                'SubcategoryName': self.iSelectedSubCategory().ReportSubCategoryName(),
            }),
            success: function (result)
            {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.IReportList, {}, self.IReportList);
                PaginateTable('EbillsDatatable2');
            },
            error: function (err)
            {
                hideloader();
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        }); 
    }


    self.ApproveAccountForOpening = function (a) {
         
        console.log("Feeder Id "+self.SelectedBSC().BSCId()); 
        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        
        console.log("Action Cateogry " + self.Category());
        console.log("Account Type " + self.AccountType());
       

        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        } 
        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       if (self.AccountType() == null) {
           swal('Select Account Type', 'Please select the Account Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.Category() == null) {
           swal('Select Category', 'Please select the Category Type to Proceed.Thank you', 'info');
           return;
       }
       var TicketId = a.TicketNo();
       showloader(); 
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/ApproveAccountForOpening',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Zone': self.SelectedIBC().IBCId(),
                'Feeder': self.SelectedBSC().BSCId(), 
                'Category': self.Category(),
                'AccountType': self.AccountType(),
                'CreatedBy': $("#StaffId").val(),
                'TicketId': TicketId,
                'Status': $("#ApprovalStatus").val()

            }),
            success: function (result)
            {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.NewCustomerOnboardList, {}, self.NewCustomerOnboardList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err)
            {
                hideloader();
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        }); 
    }



    self.ViewBillDistributionDetails = function (a) {
         
        console.log("Feeder Id "+self.SelectedBSC().BSCId()); 
        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        
        console.log("Action Cateogry " + self.Category());
        console.log("Account Type " + self.AccountType());
       

        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        } 
        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       if (self.AccountType() == null) {
           swal('Select Account Type', 'Please select the Account Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.Category() == null) {
           swal('Select Category', 'Please select the Category Type to Proceed.Thank you', 'info');
           return;
       }
       var TicketId = a.TicketNo();

       var FromDate = self.FromDate();
       var ToDate = self.ToDate();;
       showloader(); 
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/ViewBillDistributionDetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Zone': self.SelectedIBC().IBCId(),
                'Feeder': self.SelectedBSC().BSCId(), 
                'Category': self.Category(),
                'AccountType': self.AccountType(),
                'CreatedBy': $("#StaffId").val(),
                'TicketId': TicketId,
                'Status': $("#ApprovalStatus").val(),
                'FromDate': FromDate,
                'ToDate': ToDate,

            }),
            success: function (result)
            {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.BillDistributionList, {}, self.BillDistributionList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err)
            {
                hideloader();
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        }); 
    }



    self.IreportersReport = function (a) {
         
        console.log("Feeder Id "+self.SelectedBSC().BSCId()); 
        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        
        console.log("Action Cateogry " + self.Category());
        console.log("Account Type " + self.AccountType());
       

        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        } 
        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       if (self.AccountType() == null) {
           swal('Select Account Type', 'Please select the Account Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.Category() == null) {
           swal('Select Category', 'Please select the Category Type to Proceed.Thank you', 'info');
           return;
       }
       var TicketId = a.TicketNo();

       var FromDate = self.FromDate();
       var ToDate = self.ToDate();;
       showloader(); 
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/IreportersReport',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Zone': self.SelectedIBC().IBCId(),
                'Feeder': self.SelectedBSC().BSCId(), 
                'Category': self.Category(),
                'AccountType': self.AccountType(),
                'CreatedBy': $("#StaffId").val(),
                'TicketId': TicketId,
                'Status': $("#ApprovalStatus").val(),
                'FromDate': FromDate,
                'ToDate': ToDate,
            }),
            success: function (result)
            {
                var data = JSON.parse(result.result);
                hideloader();
                ko.mapping.fromJS(data.IreportersList, {}, self.IreportersList);
                PaginateTable('EbillsDatatable');
            },
            error: function (err)
            {
                hideloader();
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        }); 
    }


    self.Viewstaffperformancereport = function (a) {
         
        console.log("Feeder Id "+self.SelectedBSC().BSCId()); 
        console.log("Zone Id " + self.SelectedIBC().IBCId()); 
        
        console.log("Action Cateogry " + self.Category());
        console.log("Account Type " + self.AccountType());
       

        if (self.SelectedIBC() == null) {
            swal('Select Zone', 'Please select the Zone to Proceed.Thank you', 'info');
            return;
        } 
        if (self.SelectedBSC() == null) {
            swal('Select Feeder', 'Please select the feeder to Proceed.Thank you', 'info');
            return;
        } 
       if (self.AccountType() == null) {
           swal('Select Account Type', 'Please select the Account Type to Proceed.Thank you', 'info');
           return;
       }
       if (self.Category() == null) {
           swal('Select Category', 'Please select the Category Type to Proceed.Thank you', 'info');
           return;
       }
     
       
       var FromDate = self.FromDate();
       var ToDate = self.ToDate();;

        //open in another ta for the report to be shown
 var URL = "https://insight.phed.com.ng/ReportPage.aspx?Zone=" + self.SelectedIBC().IBCId() + "&Feeder=" + self.SelectedBSC().BSCId() + "&Category=" + self.Category() + "&PhaseType=" + self.AccountType() + "&TYPE=STAFFPERFORMANCE" + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "";

      // var URL = "localhost:14996/ReportPage.aspx?Zone=" + self.SelectedIBC().IBCId() + "&Feeder=" + self.SelectedBSC().BSCId() + "&Category=" + self.Category() + "&PhaseType=" + self.AccountType() + "&TYPE=STAFFPERFORMANCE" + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "";

        window.open(URL, '_blank');
        
    }



    self.ViewPHEDBills = function (event) {

        var consNo = '832510284901';
        var billMonth = '01-06-2019';
        var month = 'Jun-2019';
        var obj = {};
        obj.fileName = consNo + '_' + month;
        obj.consNo = " and t2.cons_acc ='" + consNo + "'";
        obj.billMonth = billMonth;
        $.ajax({
            type: "POST",
            url: "https://dlenhance.phed.com.ng/RptPages/Billing/ExportBill.aspx/SetBillSessionVariables",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                value = msg.d;
                var filePath = 'https://dlenhance.phed.com.ng//BillPDF//' + obj.fileName + '.pdf';
                window.open(filePath, '_blank');
            },
            async: false,
            error: function (msg) {
                value = msg.d;
            }
        });
    }



    self.ViewPHEDReceipt = function (event) {
        
        var receipt = $('#txt_receiptNo').val();;
        var obj = {};
        obj.fileName = receipt;
        obj.receiptNo = receipt;
        $.ajax({
            type: "POST",
            url: "https://dlenhance.phed.com.ng/RptPages/Collection/ExportReceipt.aspx/SetCollectionSessionVariables",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                value = msg.d;
                var filePath = 'https://dlenhance.phed.com.ng//CollPDF//' + obj.fileName + '.pdf';
                window.open(filePath, '_blank');
            },
            async: false,
            error: function (msg) {
                value = msg.d;
            }
        }); 
    }


    self.ViewInstalledMeters = function (d) {  
        showloader();  

        var Vendor = self.SelectedPaymentMode().Name;
          
        $.ajax({
            type: "POST",
            url: '/MAP/ViewInstalledMeters',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Vendor': Vendor,
                'FromDate':self.FromDate(),
                'ToDate': self.ToDate() 
            }),
            success: function (result) {
                var data = JSON.parse(result); 
                hideloader();
                ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                PaginateTable('customer_payment_info');  
                  
            },
            error: function (err) { 
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error"); 
            }
        });
    }







     
    self.ViewTeller = function (event) { 
        console.log("AMAMIoooooooooo" + event.DocumentPath());
        
        if (event.DocumentPath() == null)
        {
            swal("Error", "There was no teller attached to this Payment Claim.", "error");
            return;
        }

        var URL = self.BaseURL() + event.DocumentPath();
        
        window.open(URL, '_blank');
        
    }

    self.GetImageDocumentsView = function (a) {
       

      //  console.log("Habamana "+a.DisconID());


        var TicketId = a.TicketNo();
      

        console.log("APAMA " + TicketId);

        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDConnect/GetImageDocumentsView',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ 
                TicketId: TicketId 
            }),
            success: function (result) {
                var data = JSON.parse(result.result); 
                ko.mapping.fromJS(data.UploadedDocumentList, {}, self.UploadedDocumentList);
                
                hideloader();

               // swal("Approved", "The Customer's Payment has been approved.", "success");
                $('#largeModal1').appendTo("body").modal('show');
            }, 
            error: function (err) {
                hideloader();
                self.message("The Request could not be Completed!");
                console.log(err.responseText);

            }
        });
    }
      self.GetImageDocumentsView2 = function (a) {
       

      //  console.log("Habamana "+a.DisconID());


        var TicketId = a.RCDC_Ireport_Id();
      
        var FilePaths = a.filePaths();
   


        console.log("  FilePaths FilePaths " + FilePaths);

        self.UploadedDocumentList([]);
        var array = FilePaths.split(',');

        var path = "";


        for (let i = 0; i < array.length; i++) {
            console.log("Changers Lounge "+i);

            path = array[i].replace('["', '');
            path  = path.replace('"]', ''); path = path.replace('PHEDConnect/', '');
            path = path.replace('localhost:14996/', '');
            path = path.replace('localhost:14996/PHEDConnect/', '');
            path = path.replace('https://insight.phed.com.ng/', '');
            path = path.replace('"', '');
            path = path.replace('"', '');




            self.UploadedDocumentList.push({

                DOCUMENT_EXTENSION: ko.observable("JPG"),
                DOCUMENT_PATH:  ko.observable(path),
                DOCUMENT_NAME: ko.observable("I-Report Image " + parseFloat(i) + 1),
                Size: ko.observable("325"),
                DocumentDescription: ko.observable("I-Report Documented Image " + parseFloat(i) + 1),
            })


        }

        //ko.mapping.fromJS(data.UploadedDocumentList, {}, self.UploadedDocumentList);
     
        showloader();

        $('#largeModal1').appendTo("body").modal('show');
        hideloader();
 


        console.log("Freedom "+self.UploadedDocumentList());
    }
   
    
    self.SettlementModal = function (a) {
        self.AccountNo(a.AccountNo());
        self.AccountName(a.AccountName());
     self.Address(a.Address());
        self.Zone(a.Zone());
        self.Feeder(a.FeederName());

        $('#SettlementModal').appendTo("body").modal('show');
    }


    self.GetLossOfRevenueData = function (a) {
        self.AccountNo(a.AccountNo());
        self.AccountName(a.AccountName());
       self.Address(a.Address());
        self.Zone(a.Zone());
       self.Feeder(a.FeederName());
       self.AccountType(a.AccountType());
       self.Arrears(a.Arrears());

       self.DateOfDiscon(a.DateOfDiscon());
       self.Settlement_Agreement(a.Settlement_Agreement()); 
       self.DisconStatus(a.DisconStatus());
       self.CustomerEmail(a.CustomerEmail());
       self.CustomerPhone(a.CustomerPhone());

        //----------------------------
       self.DisconID(a.DisconID());
       self.RPDApproval(a.RPDApproval());
       self.IADApproval(a.IADApproval());
       self.IADApprovalDate(a.IADApprovalDate());
       self.RPDApprovalDate(a.RPDApprovalDate());
       self.IADApprovalComments(a.IADApprovalComments());
       self.RPDApprovalComments(a.RPDApprovalComments());
       self.RPDCalculatedLoad(a.RPDCalculatedLoad());
       self.RPDLossOfRevenueAmount(a.RPDLossOfRevenueAmount());
       self.RPDLossOfRevenueAvailabilty(a.RPDLossOfRevenueAvailabilty());
       self.RPDLossOfRevenueInfractionDuration(a.RPDLossOfRevenueInfractionDuration());


        //---------------------------

        console.log("Habamana "+a.DisconID());

        showloader();
        $.ajax({
            type: "POST",
            url: '/PHEDConnect/GetLossOfRevenueData',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ 
                DisconId: a.DisconID(),
                AccountNo: a.AccountNo() 
            }),
            success: function (result) {
                var data = JSON.parse(result.result); 
                ko.mapping.fromJS(data.UploadedDocumentList, {}, self.UploadedDocumentList);
                ko.mapping.fromJS(data.LossOfRevenueList, {}, self.LossOfRevenueList);
                hideloader();

               // swal("Approved", "The Customer's Payment has been approved.", "success");
                $('#LORModal').appendTo("body").modal('show');
            }, 
            error: function (err) {
                hideloader();
                self.message("The Request could not be Completed!");
                console.log(err.responseText);

            }
        });
          
    }







    self.AccountStatusModal = function () {
        $('#AccountStatusModal').appendTo("body").modal('show');
    }




    self.filterSubFromCategory = function () {
       
        var resultss = [];
        // 
        self.filteredSubCategoryList([]);

        if (self.iSelectedCategory() != null) {
            var ReportCategoryId = self.iSelectedCategory().ReportCategoryId();

            console.log("BSC: " + ReportCategoryId);

            var dd = ko.utils.arrayFilter(self.RCDC_ReportSubCategory(), function (a) {
               
                return a.ReportCategoryId() == ReportCategoryId;
            });

            resultss.push({
                ReportCategoryId: ReportCategoryId,
                ReportSubCategoryId: ko.observable("ALL"),
                ReportSubCategoryName: ko.observable("ALL")
            })

              
            ko.utils.arrayForEach(dd, function (bsc) {
                    resultss.push(bsc);
                });

            self.filteredSubCategoryList(resultss);
        }
        else {
            self.filteredSubCategoryList(resultss);
        }
        console.log(resultss);
    }
     

    self.filterBSCFromIBC = function () {
        console.log("filtering data...");
        console.log("BSCLIST: " + JSON.stringify(self.BSCList()));

        var results = [];

        self.filteredBSCList([]);

        if (self.SelectedIBC() != null) {
            var bsc_id = self.SelectedIBC().IBCId();

            console.log("BSC: " + bsc_id);

            var filtered_bsc = ko.utils.arrayFilter(self.BSCList(), function (a) {
                if (a.BSCId() == '080') {

                    return a;
                }
                return a.IBCId() == bsc_id;
            });

            results.push({
                IBCId: self.SelectedIBC().IBCId(),
                BSCName: ko.observable("ALL FEEDERS"),
                BSCId: ko.observable("ALL")
            })


            ko.utils.arrayForEach(filtered_bsc,

                function (bsc) {
                    results.push(bsc);
                });

            self.filteredBSCList(results);
        }
        else {
            self.filteredBSCList(results);
        }
        console.log(results);
    }
     

    self.ApproveMarkedSelectedPayments = function (d){ 
     
        $.ajax({
            type: "POST",
            url: '/MAP/ApproveMAPPayment',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                TicketId: d.TicketId(),
                ReceiptNo: d.ReceiptNo(), 
                DatePaid : d.DatePaid(),
                Phase : d.Phase(), 
                AccountNo : d.AccountNo(),
                AmountPaid : d.Amount(),
            }),
            success: function (result) {
                var data = JSON.parse(result);
            
                ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
                swal("Approved", "The Customer's Payment has been approved.", "success");
                PaginateTable('customer_payment_info');  
            }, 
            error: function (err) {
                hideloader();
                self.message("The Request could not be Completed!");
                console.log(err.responseText);
            }
        }); 
    }
     
     
    self.ApprovalStatus = ko.observable();

    self.ViewApprovedBulkMeters = function (d) { 

        var Vendor = self.SelectedPaymentMode().Name;
        var Status = self.InstallationStatus();
        var ApprovalStatus = self.ApprovalStatus()();
        

        if (self.SelectedPaymentMode() == null) {
            swal('MAP Vendor', 'Please select the MAP Vendor to Proceed.Thank you', 'info');
            return;
        }

        if (self.InstallationStatus() == "--Select Installation Status--") {
            swal('Select Status', 'Please select the Installation Status to proceed. Thank you', 'info');
            return;
        }
        if (self.ApprovalStatus() == "--Select Approval Status--") {
            swal('Select Status', 'Please select the Approval Status to proceed. Thank you', 'info');
            return;
        }

        console.log("Vendor     "+Vendor);  console.log("Status      "+Status);
        uploader();
          
        $.ajax({
            type: "POST",
            url: '/MAP/ViewApprovedBulkMeters',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Vendor': Vendor,
                'Status':Status,
                'ApprovalStatus':ApprovalStatus

            }),
            success: function (result) {
                //  var data = JSON.parse(result);
                  


                if (result.error != "") 
                {
                    swal('No Meter to View at the Moment', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result); 



                ko.mapping.fromJS(data.MeterUploadApprovalList, {}, self.MeterUploadApprovalList);

                  
                hideloader();
                 
                PaginateTable('customer_payment_info');  
            },
            error: function (err) { 
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error"); 
            }
        });
    }






















    self.ViewUploadBulkMeters = function (d) { 

        var Vendor = self.SelectedPaymentMode().Name;
        var Status = self.InstallationStatus();
        

        if (self.SelectedPaymentMode() == null) {
            swal('MAP Vendor', 'Please select the MAP Vendor to Proceed.Thank you', 'info');
            return;
        }

        if (self.InstallationStatus() == null) {
            swal('Select Status', 'Please select the Installation Status to proceed. Thank you', 'info');
            return;
        }


        console.log("Vendor     "+Vendor);  console.log("Status      "+Status);
        uploader();
          
        $.ajax({
            type: "POST",
            url: '/MAP/ViewUploadBulkMeters',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Vendor': Vendor,
                'Status':Status
                

            }),
            success: function (result) {
                //  var data = JSON.parse(result);
                  


                if (result.error != "") 
                {
                    swal('No Meter to View at the Moment', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result); 



                ko.mapping.fromJS(data.MeterUploadApprovalList, {}, self.MeterUploadApprovalList);

                  
                hideloader();
                 
                //PaginateTable('customer_payment_info');  
            },
            error: function (err) { 
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error"); 
            }
        });
    }
     

    ////////////////////////////////////////////////

    

    ////////////////////////////////////////////////

    self.UploadBulkMeters = function (d) { 

        var Vendor = self.SelectedPaymentMode().Name;

        var files = $("#yourfileID").get(0).files;

         

        if (self.SelectedPaymentMode() == null) {
            swal('MAP Vendor', 'Please select the MAP Vendor.Thank you', 'info');
            return;
        }

        if (self.InstallationStatus() == null) {
            swal('Select Status', 'Please select the Installation Status to proceed. Thank you', 'info');
            return;
        }

        

        if (files[0] == null) {
            swal('select a File!', 'Please select a document to proceed.', 'info');
            return;
        }

        var ext = files[0].name.split('.').pop();

        console.log("dsdsd dsdsd ds ds " + ext);

        //if (ext !== 'jpg' || ext !== 'jpeg' || ext !== 'doc' || ext !== 'docx' ||ext !== 'pdf' ) {
        //    swal('Wrong File Type', 'File must be a word document, an Image, or a PDF file to be uploaded, Please try again. Thank you.', 'info');
        //    return;
        //}

        // var LoanApplicationId = self.PaymentRequest.PaymentRequestId();

         
        var n = files[0].size;

        if (n > 1500000) {
            swal('File Size Exceeded!', 'File size must not exceed 150kb, Please compress and try again.', 'info');
            return;
        }
         
        var m = parseFloat(n) / parseFloat(1024);
        // var Size = m.toFixed(2) + " KB";
        var FileType = ext;
        var DocumentTitle = ext;

        data = new FormData();
        data.append("DocumentFile", files[0]);
        data.append("DocumentName", files[0].name);
        //data.append("StudentCode", self.StudentCode());
        
      
        data.append("MAPVendor", Vendor);
        data.append("Status", self.InstallationStatus());
         

        //Upload the Files
        uploader();

        $.ajax({
            type: "POST", 
            url: '/MAP/UploadMeter', 
            cache: false,
            contentType: false,
            processData: false,
            data: data, 
            success: function (result) { 
                
                if (result.error != "") 
                {
                    swal('File not Saved', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result); 



                ko.mapping.fromJS(data.UplodedStatusList, {}, self.UplodedStatusList);

                PaginateTable('customer_payment_info'); 
                swal('Record Saved!', 'Your entry has been successfully saved', 'success'); 
            },
            error: function (err) { $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

        ////


          
         
    }




    self.UploadApplication = function (d) { 

        var Vendor = self.SelectedPaymentMode().Name;

        var files = $("#yourfileID").get(0).files;

         

        if (self.SelectedPaymentMode() == null) {
            swal('MAP Vendor', 'Please select the MAP Vendor.Thank you', 'info');
            return;
        }

        if (self.InstallationStatus() == null) {
            swal('Select Status', 'Please select the Installation Status to proceed. Thank you', 'info');
            return;
        }

        

        if (files[0] == null) {
            swal('select a File!', 'Please select a document to proceed.', 'info');
            return;
        }

        var ext = files[0].name.split('.').pop();

        console.log("dsdsd dsdsd ds ds " + ext);
          
        var n = files[0].size;

        if (n > 1500000) {
            swal('File Size Exceeded!', 'File size must not exceed 150kb, Please compress and try again.', 'info');
            return;
        }
         
        var m = parseFloat(n) / parseFloat(1024);
        // var Size = m.toFixed(2) + " KB";
        var FileType = ext;
        var DocumentTitle = ext;

        data = new FormData();
        data.append("DocumentFile", files[0]);
        data.append("DocumentName", files[0].name);
        //data.append("StudentCode", self.StudentCode());
        
      
        data.append("MAPVendor", Vendor);
        data.append("Status", self.InstallationStatus());
         

        //Upload the Files
        

        uploader();

        $.ajax({
            type: "post",
            url: '/MAP/UploadApplication',
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
            cache: false,
            contentType: false,
            processData: false,
            data: data, 
            success: function (result) { 
                
                if (result.error != "") 
                {
                    swal('File not Saved', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result); 

                 
                ko.mapping.fromJS(data.UplodedStatusList, {}, self.UplodedStatusList);

                PaginateTable('customer_payment_info'); 
                swal('Record Saved!', 'Your entry has been successfully saved', 'success'); 
            },
            error: function (err) { $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

        ////


          
         
    }



    self.UploadAgency = function (event) {

         
        var files = $("#yourfileID").get(0).files;
           
        if (self.TellerNo() == null) {
            swal('Input Teller No', 'Please input the teller Number to proceed. Thank you', 'info');
            return;
        }

        if (self.SelectedBankMode() == null) {
            swal('Select Bank', 'Please select the receiving Bank to proceed. Thank you', 'info');
            return;
        }

        if (self.DatePaid() == null) {
            swal('Select Date', 'Please whe you make the payment? Select the date to proceed. Thank you', 'info');
            return;
        }
        if (files[0] == null) {
            swal('select a document!', 'Please select a document to proceed.', 'info');
            return;}
         

        var ext = files[0].name.split('.').pop();

        console.log("dsdsd dsdsd ds ds " + ext);

        //if (ext !== 'jpg' || ext !== 'jpeg' || ext !== 'doc' || ext !== 'docx' ||ext !== 'pdf' ) {
        //    swal('Wrong File Type', 'File must be a word document, an Image, or a PDF file to be uploaded, Please try again. Thank you.', 'info');
        //    return;
        //}

        // var LoanApplicationId = self.PaymentRequest.PaymentRequestId();

         
        var n = files[0].size;

        if (n > 990000) {
            swal('File Size Exceeded!', 'File size must not exceed 150kb, Please compress and try again.', 'info');
            return;
        }
         
        var m = parseFloat(n) / parseFloat(1024);
        // var Size = m.toFixed(2) + " KB";
        var FileType = ext;
        var DocumentTitle = ext;

        data = new FormData();
        data.append("DocumentFile", files[0]);
        data.append("DocumentName", files[0].name);
        //data.append("StudentCode", self.StudentCode());
        data.append("DatePaid", self.DatePaid());
        data.append("BankName",self.SelectedBankMode().name);
        data.append("TellerNo", self.TellerNo());
        data.append("AmountPaid", self.TotalAmountPayable());
        data.append("TicketId", self.TicketId());

        showloader();
        $.ajax({
            type: "POST",
            url: '/MAPRegister/UploadAgency',
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
            cache: false,
            contentType: false,
            processData: false,
            data: data, 
            success: function (result) { 
                
                if (result.error != "") 
                {
                    swal('File not Saved', result.error, 'error');
                    return;
                } 
                var data = JSON.parse(result.result); 
                swal('Record Saved!', 'Your entry has been successfully saved', 'success'); 
            },
            error: function (err) { $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }

        });

    };


     
    self.DownloadBulkApplicationExcelTemplate = function (d) { 

        var ReportViewers = "http://map.nepamsonline.com/Documents/BulkApplicationExcelTemplate.xlsx";
        var url =  ReportViewers;
        window.open(url, '_blank'); 
    }


    self.DownloadStaffOnboardExcelTemplate = function (d) { 

        var ReportViewers = "http://map.nepamsonline.com/Documents/PHEDStaffOnboard.xlsx";
        var url =  ReportViewers;
        window.open(url, '_blank'); 
    }

    self.DownloadMeterExcelTemplate = function (d) { 

        var ReportViewers = "http://map.nepamsonline.com/Documents/MeterUploadExcelTemplate.xlsx";
        var url =  ReportViewers;
        window.open(url, '_blank'); 
    }

    self.DownloadContractorExcelTemplate = function (d) { 

        var ReportViewers = "http://map.nepamsonline.com/Documents/ContractorExcelTemplate.xlsx";
        var url =  ReportViewers;
        window.open(url, '_blank'); 
    }
    self.ViewCustomersUnapprovedData = function (d) { 


        showloader();
        console.log("Reaching here ooooooooooo");

        var IBC =  self.SelectedIBC().IBCName();
        var BSC = self.SelectedBSC().BSCName();
        var PaymentStatus = self.SelectedPaymentStatus().PaymentStatusName;
          

        $.ajax({
            type: "POST",
            url: '/MAP/ViewCustomersUnapprovedData',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'IBC': IBC,
                'BSC':BSC,
                'STATUS': PaymentStatus

            }),
            success: function (result) {
                var data = JSON.parse(result);
                  
                hideloader();
                ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
                PaginateTable('customer_payment_info');  
            },
            error: function (err) { 
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error"); 
            }
        });
    }
      
    
      

    self.ViewCustomersAudittrailReport = function (d) { 


        showloader();
        console.log("Reaching here ooooooooooo");
 
        var PaymentStatus = self.SelectedAuditStatus().PaymentStatusName;
          
        console.log("sdsdsdsdsdsdsd   "+PaymentStatus);

        $.ajax({
            type: "POST",
            url: '/MAP/ViewCustomersAudittrailReport',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'FromDate': self.FromDate(),
                'ToDate':self.ToDate(),
                'STATUS': PaymentStatus

            }),
            success: function (result) {
                //  var data = JSON.parse(result);
                  


                var data = JSON.parse(result.result);
                //self.IsCustomerDetails(true);
                // console.log(data.CustomerDetails);
                

                if(result.error != "")
                { 
                    swal('Error',result.error,'error');
                    return;
                }
                  
                hideloader();
                ko.mapping.fromJS(data.AuditTrailList, {}, self.AuditTrailList);
                //  PaginateTable('customer_payment_info');  
            },
            error: function (err) { 
                console.log("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error"); 
            }
        });
    }
     


}

function hideloader() {

    swal.close();
}
function showloader() {
    swal({
        title: "Processing...",
        text: "Please wait while we process your request",
        imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',
        showConfirmButton: false,
        allowOutsideClick: false
    });
};


function PaginateTable(TableName) {



    //var table = $('#EbillsDatatable').DataTable();

    //    table.destroy();




    var table_id = '#' + TableName
    var data_table_object = {
        dom: 'Bfrtip',
        order: [[0, "asc"]],
        lengthMenu: [
            [5, 15, 30, -1],
            ['5 rows', '15 rows', '30 rows', 'Show all']
        ],
        buttons: [
            'pageLength', 'pdf', {
                text: 'Excel',
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            }, 'colvis'
        ]
    };
    if ($.fn.dataTable.isDataTable(table_id)) {
        //add the destroy key to the data_table_object
        data_table_object.destroy = true;

        //recreate Table
        $(table_id).DataTable(data_table_object);
    }
    else {
        $(table_id).DataTable(data_table_object);
    }

   // $(".pagination").addClass('pagination-outline-success');
}