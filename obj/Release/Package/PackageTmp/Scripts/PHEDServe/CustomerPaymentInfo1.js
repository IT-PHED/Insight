
//debuging
var debug = true;
var payment = [
 
        { PaymentStatusName: "PAID" }, 
        { PaymentStatusName: "NOT PAID" },
        { PaymentStatusName: "APPROVED FOR PAYMENT" }
];

var MAPStatus = [
        { StatusName: "APPROVED" }, 
        { StatusName: "NOTAPPROVED" },];

var phase = [ 
  { PhaseName: "ALL" 
  { PhaseName: "SINGLEPHASE" }, 
  { PhaseName: "THREEPHASE" }
];
var payment_mode = [
{ Name: "POS" },
 {Name: "WEB" },

];
var Bank_mode = [
{ Name: "FirstBank" },
 {Name: "ZenithBank" },
 { Name: "AccessBank" },
 {Name: "GTBank" },
 { Name: "SterlingBank" },
 {Name: "FCMB" },
 { Name: "SCBank" } , 
 { Name: "HeritageBank" } 
];
var approvals = [
{ ApprovalName: "ALL" },
 { ApprovalName: "APPROVED" },
 { ApprovalName: "UNAPPROVED" },
 { ApprovalName: "PENDING" }
];


var titles = [
    { TitleName: "Mr", TitleValue: "Mr" },
    { TitleName: "Mrs", TitleValue: "Mrs" },
    { TitleName: "Dr", TitleValue: "Dr" },
    { TitleName: "Prof", TitleValue: "Prof" },
    { TitleName: "Chief", TitleValue: "Chief" },

];
var AccountType = [
     { AccountTypeName: "PREPAID", AccountTypeValue: "PREPAID" },
     { AccountTypeName: "POSTPAID", AccountTypeValue: "POSTPAID" }, 
];
var type_of_premises = [
    { TypePremisesName: "2BD Room Flat", TypePremisesValue: "2BD_Room_Flat" },
    { TypePremisesName: "3BD Room Flat", TypePremisesValue: "3BD_Room_Flat" },
    { TypePremisesName: "Single Room", TypePremisesValue: "Single_Room" },
    { TypePremisesName: "Boys Qtrs", TypePremisesValue: "Boys_Qtrs" },
    { TypePremisesName: "Shop", TypePremisesValue: "Shop" },
    { TypePremisesName: "Block of Flats", TypePremisesValue: "Block_of_Flats" },
    { TypePremisesName: "Duplex", TypePremisesValue: "Duplex" },
    { TypePremisesName: "Other", TypePremisesValue: "Other" },
];

var uses_of_premises = [
    { UsePremisesName: "Residential", UsePremisesValue: "Residential" },
    { UsePremisesName: "Commercial", UsePremisesValue: "Commercial" },
    { UsePremisesName: "Special", UsePremisesValue: "Special" },
    { UsePremisesName: "Industrial", UsePremisesValue: "Industrial" },
];

var id_cards = [
    { IDcardName: "International Passport", IDcardValue: "International Passport" },
    { IDcardName: "Drivers License", IDcardValue: "Drivers License" },
    { IDcardName: "NAT ID", IDcardValue: "NATIONAL ID" },
    { IDcardName: "PVC", IDcardValue: "PERMANENT VOTERS CARD" },
];

var types_of_meter = [
    { TypeMeterRequestedName: "SINGLE PHASE", TypeMeterRequestedValue: "SINGLE_PHASE" },
    { TypeMeterRequestedName: "THREE PHASE", TypeMeterRequestedValue: "THREE_PHASE" },
       
];
var mode_of_payments = [
    { ModePaymentName: "UPFRONT", ModePaymentValue: "UPFRONT" },
{ ModePaymentName: "METER SERVICE CHARGE", ModePaymentValue: "MSC" },
];

var payment_schedules = [
    { PaymentScheduleName: "1 year",  PaymentScheduleValue: "1 years" },
    { PaymentScheduleName: "2 years", PaymentScheduleValue: "2 years" },
    { PaymentScheduleName: "3 years", PaymentScheduleValue: "3 years" }
          
];
 
function dd(arg) {
    if (debug == true) {
        console.log(arg);
    }
}
//loader
function hideloader() {

    swal.close();
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


function PaginateTable(TableName) {
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

    $(".pagination").addClass('pagination-outline-success');
}
 



function MapPaymentViewModel(data) {
    var self = this;

    ko.mapping.fromJS(data, {}, self);

    self.TransactionId = ko.observable("");

    self.message = ko.observable("");
    self.PaymentModeList = ko.observableArray(payment_mode);
    self.BankModeList = ko.observableArray(Bank_mode);
    self.SelectedPaymentMode  = ko.observable();  self.SelectedBankMode  = ko.observable();
   
    self.ApprovalList = ko.observableArray(approvals);
    //self.CustomerDetails = ko.observable();

    self.IsCustomerDetails = ko.observable(false);
    self.veiwCustomer = function () {

        if(self.TransactionId() == null || self.TransactionId() == ""){
            swal("Error","Transaction ID can not be Balank","error");
            self.IsCustomerDetails(false);
            return;
        }
        //dd(self.TransactionId());
        showloader();
        $.ajax({
            type: "POST",
            url: '/MAP/ViewCustomer',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                TransactionId: self.TransactionId()
            }),
            success: function (result) {
                var data = JSON.parse(result);
                dd(data.CustomerDetails);
                if (data.CustomerDetails == null) {
                    swal("Generate Ticket", 'Kindly ask the customer to generate a Ticket at map.phed.com.ng', "info");
                } else {
                    hideloader();
                    self.IsCustomerDetails(true);
                    ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
                }
                //self.CustomerDetails(data.CustomerDetails);
                 
            },
            error: function (err) {
                //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
                //self.isProcessing(false);
            }
        }); 
         
    }
    self.save = function () {

        console.log(JSON.stringify("ASASASASASASA "+self.CustomerDetails));

        self.MAPPayment.TicketId(self.CustomerDetails.TransactionID());
        self.MAPPayment.IBC(self.CustomerDetails.IBC());
        self.MAPPayment.BSC(self.CustomerDetails.BSC());
        self.MAPPayment.ApprovalStatus("NOTAPPROVED");
        self.MAPPayment.PaymentStatus("PAID");

        self.MAPPayment.CustomerName(self.CustomerDetails.MAPCustomerName());
        self.MAPPayment.Phase(self.CustomerDetails.MeterPhase());
        self.MAPPayment.AccountNo(self.CustomerDetails.CustomerReference());

 
         

        self.MAPPayment.PaymentMode(self.SelectedPaymentMode().Name);
        dd( self.MAPPayment);
        self.insert(self.MAPPayment);

    }
    self.insert = function (data) {
        showloader();
        var PaymentDetails = ko.mapping.toJS(data);
        $.ajax({
            type: "POST",
            url: '/MAP/InsertMAPPayment',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                PaymentData: JSON.stringify(PaymentDetails)
            }),
            success: function (result) {
                var data = JSON.parse(result);
                self.IsCustomerDetails(true);
                dd(data.CustomerDetails);
                //self.CustomerDetails(data.CustomerDetails);
                //ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
                swal('Successful',"Your Data has been successfully Saved",'success');
            },
            error: function (err) {
                //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                alert("Fail Details Request " + err.responseText)
                hideloader();
                //self.isProcessing(false);
            }
        });
    }

}






function CustomerPaymentViewModel(data) {
    var self = this;
    self.PaymentList = ko.observableArray(); 
    self.ApprovalStatus = ko.observableArray();
    ko.mapping.fromJS(data, {}, this);

    self.ApprovalList = ko.observableArray(approvals);

    self.ApprovePaymentComment  = ko.observable(null);
    self.message = ko.observable("Choose From Date and Start Date to contiune");
    self.fromDate = ko.observable(null);
    self.toDate = ko.observable(null);

    self.fetchCustomerPaymentInfo = function () {
       
        dd("fetch data");
        dd(self.PaymentList());
        if (self.fromDate() == null) {
            swal("Error","Select From Date to Procced","error");
            return;
        }else if(self.toDate() == null){
            swal("Error", "Select To Date to Procced", "error");
            return;
        } else {
            showloader();
            $.ajax({
                type: "POST",
                url: '/MAP/GetCustomerByDate',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    fromDate: JSON.stringify(self.fromDate),
                    toDate: JSON.stringify(self.toDate),

                }),
                success: function (result) {
                   
                    var data = JSON.parse(result);
                    dd("count: " + data.PaymentList.length);
                    dd(data.PaymentList);
                    //self.PaymentList([676, 877]);
                    ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                    hideloader();
                    self.PaymentList(data.PaymentList);
                    if (data.PaymentList.length < 1) {
                        dd("no data exist: " + data.PaymentList.length);
                        self.message("No Payment Exist in the Range");
                    } else {
                        self.message("");
                        ko.mapping.fromJS(data, {}, self);
                        //PaginateTable('customer_payment_info');

                        $('#customer_payment_info').DataTable({
                            dom: 'Bfrtip',
                            buttons: ['pageLength']
                        });
                        //$('#customer_payment_info').DataTable();
                        dd(data);
                    }



                },
                error: function (err) {
                    hideloader();
                    self.message("The Request could not be Completed!");
                    dd(err.responseText);
                    //alert("Fail Details Request " + err.responseText)
                    //self.isProcessing(false);
                }
            });
        }
        
        }





}

function ApproveInstallationViewModel() {
     //ko.mapping.fromJS(data, {}, this);
  
     self.ApprovalList = ko.observableArray(approvals);

     self.message = ko.observable("Choose From Date and Start Date to continue");

     self.SelectedApproval = ko.observable();
}
 
function ApprovePaymentViewModel(data) {
    var self = this;

     ko.mapping.fromJS(data, {}, self); 

     self.ApprovalList = ko.observableArray(approvals); 
     self.PaymentStatusList = ko.observableArray(payment);
     self.PhaseList = ko.observableArray(phase);
     self.MAPStatusList = ko.observableArray(MAPStatus);
     self.message = ko.observable("No Data for Preview Available Now");

     self.SelectedPhase = ko.observable();
     //.SelectedApproval = ko.observable();
     self.SelectedIBC = ko.observable();
     self.SelectedBSC = ko.observable();
     self.SelectedPaymentStatus = ko.observable();
     self.filteredBSCList = ko.observableArray();
     self.PaymentList = ko.observableArray();
     self.ApprovePaymentComment = ko.observableArray();

     self.ViewCustomersUnapprovedData = function (d) { 


         console.log("Reaching here ooooooooooo");

         var IBC =  self.SelectedIBC().IBCName();
         var BSC = self.SelectedBSC().BSCName();
         var PaymentStatus = self.SelectedPaymentStatus().PaymentStatusName;

         console.log("wwwwwwwwwwwwwww "+IBC);

         console.log("qqqqqqqqqqqqqqqqq"+BSC);

         console.log("Payment Status "+PaymentStatus);


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
                 
                 //console.log("view Data requirements "+data);
                 //ko.mapping.fromJS(self.MAPPaymentList,{}, data.MAPPaymentList); 
                  
                 ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
         },
             error: function (err) {
               
                 dd("Fail Details Request " + err.responseText)
                 swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
             }
     });
     }
      
     self.ViewCustomersData = function (d) { 
         $.ajax({
             type: "POST",
             url: '/MAP/GetCustomerDetails',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({
                 TransactionId: d.TransactionID()
             }),
             success: function (result) {
                 var data = JSON.parse(result);
                 dd(data);
                 if (data.CustomerDetailsFromCustomer == null) {
                     swal("No Application", "Meter Application Unavaliable For this Transaction ID " + d.TransactionID(), "info");
                 } else {
                     //hideloader();
                     dd(data.CustomerDetailsFromCustomer);
                     //ko.mapping.fromJS(self.CustomerDetailsFromCustomer, data.CustomerDetailsFromCustomer);
                     var CustomerDetails = data.CustomerDetailsFromCustomer;
                     $('#ModalTicketId').text(CustomerDetails.TransactionID);
                   
                     var output = "";
                     var i = 0;
                     for(let [key, value] of Object.entries(CustomerDetails) ){
                         ///dd(key);
                         //dd("vv"+arg[key]());
                         dd(value);
                         if(i%2 == 0){
                          output += '<hr>';
                         }
                         i += 1;
                         if(key == "CustomerAddress" || key == "MeterInstallationAddress"){
                         output += '<div class="col-md-6 form-group"><label><strong>'+key+'</strong> </label><textarea class="form-control">' + value + '</textarea></div>';
                            continue;
                         }
                         output += '<div class="col-md-6"><strong>'+key+': </strong><span id="phone">' + value + '</span></div>';
                 }

                     $('#customer_details').html(output);
                     //$('#email').text(i.Email);
                     $('#largeModal').appendTo("body").modal('show');
                 }
                 //self.CustomerDetails(data.CustomerDetails);

             },
             error: function (err) {
                 //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                 dd("Fail Details Request " + err.responseText)
                 swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
                 //self.isProcessing(false);
             }
         });

         


     }
     
     self.filterBSCFromIBC = function () {
        dd("filtering data...");
        dd("BSCLIST: "+JSON.stringify(self.BSCList()));
        var results = [];
        self.filteredBSCList([]);
        if (self.SelectedIBC() != null) {
            var bsc_id = self.SelectedIBC().IBCId();

            dd("BSC: "+bsc_id);

            var filtered_bsc = ko.utils.arrayFilter(self.BSCList(), function (a) {
                return a.IBCId() == bsc_id;
            });
            ko.utils.arrayForEach(filtered_bsc, 
                
                function (bsc) {
                results.push(bsc);
            });
            self.filteredBSCList(results);
        }

        else {
            self.filteredBSCList(results);
            // return results;
        }

        dd(results);
     }

     self.ApproveBRCBillsForPayment = function (d) {
   
         $('#TicketIDHidden').val("");
         $('#ModalTicketId').text("");
         var  TicketId = "";
         var DatePaid = "";
         var Phase = "";
         var ReceiptNo = "";
         var AccountNo = "";
         var AmountPaid = "";


         console.log("asasasasasasas" + d.TransactionID());
         $('#TicketIDHidden').val(d.TransactionID());
         $('#ModalTicketId').text(d.TransactionID());

 $('#ModalTicketId').text(d.TransactionID());
 //self.CustomerDetails.BRCApprovalIBCAmount(self.CustomerDetails.BRCApprovalCSAmount());

         $('#largeModal1').appendTo("body").modal('show');
          
     }
     self.ApproveBRCBillsForPaymentDB = function (d) {
          

         console.log(" Freedom "+ $("#TicketIDHidden").val());
         
         var data = ko.mapping.toJS({
             PaymentStatus: self.SelectedPaymentStatus().PaymentStatusName,
             IBC: self.SelectedIBC().IBCName,
             BSC: self.SelectedBSC().BSCName  
         });

         swal({
             title: "Please Confirm",
             text: "Are you sure you want to approve this Adjustment?",
             type: "warning",
             showCancelButton: true,
             confirmButtonColor: "#EF5350",
             confirmButtonText: "Yes, Approve!",
             cancelButtonText: "No, Cancel!",
             closeOnConfirm: false,
             closeOnCancel: false
         },
    function (isConfirm) {
        if (isConfirm) {
            //   showloader();
              
            $.ajax({
                type: "POST",
                url: '/MAP/ApproveBRCBillsForPaymentDB',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    TicketId: $("#TicketIDHidden").val(),
                    in_data: JSON.stringify(data)
                }),
                success: function (result) {
                    var data = JSON.parse(result);
                    dd("count: " + data.PaymentList.length);
                    dd(data.PaymentList);
                    ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                    hideloader();
                    //self.PaymentList(data.PaymentList);
                    if (data.PaymentList.length < 1) {
                        dd("no data exist: " + data.PaymentList.length);
                        self.message("No Payment Exists in the Range");
                    }
                    else
                    {
                        ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                        PaginateTable('customer_payment_info');
                        dd(data);
                    }

                    $('#largeModal1').appendTo("body").modal('hide');
                    swal("Approved", "The Customer has been approved to proceed for payment.", "success");
                },

                error: function (err) {
                    hideloader();
                    self.message("The Request could not be Completed!");
                    dd(err.responseText);
                    //alert("Fail Details Request " + err.responseText)
                    //self.isProcessing(false);
                }

            });



        }
        else {
            swal({
                title: "Cancelled",
                text: "The Payment was not approved. ",
                confirmButtonColor: "#2196F3",
                type: "error"
            });
        }
    });


         // hideloader();

          
     }
     self.ApproveUpfrontPayment = function (d) {
   

         var  TicketId = "";
         var DatePaid = "";
         var Phase = "";
         var ReceiptNo = "";
         var AccountNo = "";
         var AmountPaid = "";


         console.log("asasasasasasas" + d.TransactionID());
         $('#TicketIDHidden').val(d.TransactionID());
         $('#ModalTicketId').text(d.TransactionID());
         $('#largeModal1').appendTo("body").modal('show');
          
    }

     self.ApproveUpfrontPaymentDB = function (d) {
          

         console.log(" Freedom "+ $("#TicketIDHidden").val());
         var StaffName = $("#StaffName").val();
         var data = ko.mapping.toJS({
             PaymentStatus: self.SelectedPaymentStatus().PaymentStatusName,
             IBC: self.SelectedIBC().IBCName,
             BSC: self.SelectedBSC().BSCName  ,
             BRCApprovalIBCAmount: self.CustomerDetails.BRCApprovalIBCAmount,
             BRCApprovedBy : StaffName
         });

         swal({
             title: "Please Confirm",
             text: "Are you sure you want to approve this Payment?",
             type: "warning",
             showCancelButton: true,
             confirmButtonColor: "#EF5350",
             confirmButtonText: "Yes, Approve!",
             cancelButtonText: "No, Cancel!",
             closeOnConfirm: false,
             closeOnCancel: false
         },
    function (isConfirm) {
        if (isConfirm) {
            //   showloader();
              
            $.ajax({
                type: "POST",
                url: '/MAP/ApproveUpfrontPayment',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    TicketId: $("#TicketIDHidden").val(),
                    in_data: JSON.stringify(data)
                }),
                success: function (result) {
                    var data = JSON.parse(result);
                    dd("count: " + data.PaymentList.length);
                    dd(data.PaymentList);
                    ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                    hideloader();
                    //self.PaymentList(data.PaymentList);
                    if (data.PaymentList.length < 1) {
                        dd("no data exist: " + data.PaymentList.length);
                        self.message("No Payment Exists in the Range");
                    }
                    else
                    {
                        ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                        PaginateTable('customer_payment_info');
                        dd(data);
                    }

                    $('#largeModal1').appendTo("body").modal('hide');
                    swal("Approved", "The Customer has been approved to proceed for payment.", "success");
                },

                error: function (err) {
                    hideloader();
                    self.message("The Request could not be Completed!");
                    dd(err.responseText);
                    //alert("Fail Details Request " + err.responseText)
                    //self.isProcessing(false);
                }

            });



        }
        else {
            swal({
                title: "Cancelled",
                text: "The Payment was not approved. ",
                confirmButtonColor: "#2196F3",
                type: "error"
            });
        }
    });


         // hideloader();

          
     }

     self.ApprovePayment = function (d) {
   

         var data = ko.mapping.toJS({
             PaymentStatus: self.SelectedPaymentStatus().PaymentStatusName,
             IBC: self.SelectedIBC().IBCName,
             BSC: self.SelectedBSC().BSCName  
         });
         var  TicketId = "";
         var DatePaid = "";
         var Phase = "";
         var ReceiptNo = "";
         var AccountNo = "";
         var AmountPaid = "";




         swal({
             title: "Please Confirm",
             text: "Are you sure you want to approve this Payment?",
             type: "warning",
             showCancelButton: true,
             confirmButtonColor: "#EF5350",
             confirmButtonText: "Yes, Approve!",
             cancelButtonText: "No, Cancel!",
             closeOnConfirm: false,
             closeOnCancel: false
         },
    function (isConfirm) {
        if (isConfirm) {
       // showloader();
              
          
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
                    //dd("count: " + data.PaymentList.length);
                    //dd(data.PaymentList);
// hideloader();
                    ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
                    swal("Approved", "The Customer's Payment has been approved.", "success");
                   // PaginateTable('customer_payment_info'); 
                    
                    // ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                   
                    //self.PaymentList(data.PaymentList);
                    //if (data.PaymentList.length < 1) {
                    //    dd("no data exist: " + data.PaymentList.length);
                    //    self.message("No Payment Exist in the Range");
                    //}
                    //else
                    //{
                      
                    //    ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                       
                    //    dd(data);
                    //}

                    
                },

                error: function (err) {
                    hideloader();
                    self.message("The Request could not be Completed!");
                    dd(err.responseText);
                    //alert("Fail Details Request " + err.responseText)
                    //self.isProcessing(false);
                }

            });


        }
        else {
            swal({
                title: "Cancelled",
                text: "The Payment was not approved. ",
                confirmButtonColor: "#2196F3",
                type: "error"
            });
        }
    });
          
     }




     self.ApproveInstalledMeter = function (d) {
   

         var data = ko.mapping.toJS({
             PaymentStatus: self.SelectedPaymentStatus().PaymentStatusName,
             IBC: self.SelectedIBC().IBCName,
             BSC: self.SelectedBSC().BSCName  
         });
         var  TicketId = "";
         var DatePaid = "";
         var Phase = "";
         var ReceiptNo = "";
         var AccountNo = "";
         var AmountPaid = "";




         swal({
             title: "Please Confirm",
             text: "Are you sure you want to approve this Meter installation?",
             type: "warning",
             showCancelButton: true,
             confirmButtonColor: "#EF5350",
             confirmButtonText: "Yes, Approve!",
             cancelButtonText: "No, Cancel!",
             closeOnConfirm: false,
             closeOnCancel: false
         },
    function (isConfirm) {
        if (isConfirm) {
            //   showloader();
              
          
            $.ajax({
                type: "POST",
                url: '/MAP/ApproveInstalledMeter',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    TicketId: d.TicketId(),
                    in_data: JSON.stringify(data), 
                }),
                success: function (result) {
                    var data = JSON.parse(result);
                    dd("count: " + data.PaymentList.length);
                    dd(data.PaymentList);
                    ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                    hideloader();
                    //self.PaymentList(data.PaymentList);
                    if (data.PaymentList.length < 1) {
                        dd("no data exist: " + data.PaymentList.length);
                        self.message("No Payment Exist in the Range");
                    }
                    else
                    {
                        ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                        PaginateTable('customer_payment_info');
                        dd(data);
                    }
                    swal("Approved", "The Customer has been approved to proceed for payment.", "success");
                },

                error: function (err) {
                    hideloader();
                    self.message("The Request could not be Completed!");
                    dd(err.responseText);
                    //alert("Fail Details Request " + err.responseText)
                    //self.isProcessing(false);
                }

            });


        }
        else {
            swal({
                title: "Cancelled",
                text: "The Payment was not approved. ",
                confirmButtonColor: "#2196F3",
                type: "error"
            });
        }
    });
          
     }




     self.fetchCustomerPaymentInfo = function () {

         dd("fetch data");
         dd(self.SelectedPaymentStatus());

         if (self.SelectedPaymentStatus() == null || self.SelectedPaymentStatus() == "" || self.SelectedPaymentStatus() == "undefined") {
             swal("Error", "Please select Payment Status to proceed", "info"); 
             return;
         }


         if (self.SelectedIBC() == null || self.SelectedIBC() == "" || self.SelectedIBC() == "undefined") {
             swal("Error", "Please select IBC Status to proceed", "info");

             return;
         }

         if (self.SelectedBSC() == null || self.SelectedBSC() == "" || self.SelectedBSC() == "undefined") {
             swal("Error", "Please select BSC Status to proceed", "info"); 
             return;
         }

         var data = ko.mapping.toJS({
             PaymentStatus: self.SelectedPaymentStatus().PaymentStatusName,
             IBC: self.SelectedIBC().IBCName,
             BSC: self.SelectedBSC().BSCName,
             MeterPhase: self.SelectedPhase.PhaseName
         });
         var ApprovalStatus = $("#HiddenStatus").val();
         dd(data); 
         showloader();

        $.ajax({
            type: "POST",
            url: '/MAP/FetchCustomerDataForApproval',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                in_data: JSON.stringify(data), 
                Status: ApprovalStatus
            }),
            success: function (result) {
                var data = JSON.parse(result);
                dd("count: " + data.PaymentList.length);
                dd(data.PaymentList);
                //self.PaymentList([676, 877]);
                ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                hideloader();
                //self.PaymentList(data.PaymentList);
                if (data.PaymentList.length < 1)
                {
                    dd("no data exist: " + data.PaymentList.length);
                    self.message("No Payment Exist in the Range");
                } 
                else
                {
                    self.message("");
                   // ko.mapping.fromJS(data, {}, self);
                    //self.PaymentList(data.PaymentList);

                    PaginateTable('customer_payment_info');
                    dd(data);
                }

            },
            error: function (err) {
                hideloader();
                self.message("The Request could not be Completed!");
                dd(err.responseText);
                //alert("Fail Details Request " + err.responseText)
                //self.isProcessing(false);
            }
        });
    }
   
function BRCCustomerPaymentViewModel(data) {
     var self = this;

     ko.mapping.fromJS(data, {}, self);

     self.TransactionId = ko.observable("");

     self.message = ko.observable("");

     self.SelectedApproval = ko.observable();
     self.SelectedComplaint = ko.observable();
     self.SelectedComplaint = ko.observable();
   
   
     self.ApprovalList = ko.observableArray(approvals);
     //self.CustomerDetails = ko.observable();

     self.IsCustomerDetails = ko.observable(false);
 






       /////////////////////////////EBUKA EGONU






     self.veiwCustomerBRCComplaint = function () { 
         if(self.TransactionId() == null || self.TransactionId() == ""){
             swal("Error","Transaction ID can not be Balank","error");
             self.IsCustomerDetails(false);
             return;
         }
         //dd(self.TransactionId());
         showloader();
         $.ajax({
             type: "POST",
             url: '/MAP/ViewCustomerBRC',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({
                 TransactionId: self.TransactionId()
             }),
             success: function (result) {
                 var data = JSON.parse(result);
                 dd(data.CustomerDetails);
                 if (data.CustomerDetails == null) {
                     swal("Error", "Invalid Transaction ID", "error");
                 } else {
                     hideloader();
                     self.IsCustomerDetails(true);
                     ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
                 }
                 //self.CustomerDetails(data.CustomerDetails);
                 
             },
             error: function (err) {
                 //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                 dd("Fail Details Request " + err.responseText)
                 swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
                 //self.isProcessing(false);
             }
         }); 
         
     }






    ///////////////////////////////////////

















     self.veiwCustomer = function () { 
         if(self.TransactionId() == null || self.TransactionId() == ""){
             swal("Error","Transaction ID can not be Balank","error");
             self.IsCustomerDetails(false);
             return;
         }
         //dd(self.TransactionId());
         showloader();
         $.ajax({
             type: "POST",
             url: '/MAP/ViewCustomer',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({
                 TransactionId: self.TransactionId()
             }),
             success: function (result) {
                 var data = JSON.parse(result);
                 dd(data.CustomerDetails);
                 if (data.CustomerDetails == null) {
                     swal("Error", "Invalid Transaction ID", "error");
                 } else {
                     hideloader();
                     self.IsCustomerDetails(true);
                     ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
                 }
                 //self.CustomerDetails(data.CustomerDetails);
                 
             },
             error: function (err) {
                 //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                 dd("Fail Details Request " + err.responseText)
                 swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
                 //self.isProcessing(false);
             }
         }); 
         
     }


     self.save = function () {
         //self.CustomerDetails.TransactionID(self.CustomerDetails.TransactionID());
         self.CustomerDetails.BRCApprovalCS(self.SelectedApproval().ApprovalName);
         //self.CustomerDetails.Complaints(self.SelectedComplaint().ComplaintName);
         var CustomerDetails = JSON.stringify(self.CustomerDetails);

         dd( self.CustomerDetails);
         self.insert(self.CustomerDetails);

     }
     self.insert = function (data) {
         showloader();
         var customerDetails = ko.mapping.toJS(data);
         $.ajax({
             type: "POST",
             url: '/MAP/InsertBRC',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({
                 CustomerData: JSON.stringify(customerDetails)
             }),
             success: function (result) {
                 var data = JSON.parse(result);
                 self.IsCustomerDetails(true);
                 dd(data.CustomerDetails);
                 //self.CustomerDetails(data.CustomerDetails);
                 ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
                 swal('Successful',"Your Data has been successfully Saved",'success');
             },
             error: function (err) {
                 //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                 alert("Fail Details Request " + err.responseText)
                 //self.isProcessing(false);
             }
         });
     }
 }

