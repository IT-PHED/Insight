
//debuging
var debug = true;
var payment = [
 
        { PaymentStatusName: "PAID" }, 
        { PaymentStatusName: "NOT PAID" },
        { PaymentStatusName: "APPROVED FOR PAYMENT" }
];
var ApprovedBill = [
 
        { PaymentStatusName: "APPROVED" },  
];
var Y = [
 
        { value: "2012" }, 
        { value: "2013" },
        { value: "2014" } ,
        { value: "2015" } ,
        { value: "2016" }, 
        { value: "2017" }, 
        { value: "2018" } ,
        { value: "2019" } ,
        { value: "2020" }, 
        { value: "2021" } ,
        { value: "2022" } ,
        { value: "2023" },
];

var M = [
 
        { value: "January" }, 
        { value: "February" },
        { value: "March" } ,
        { value: "April" } ,
        { value: "May" }, 
        { value: "June" }, 
        { value: "July" } ,
        { value: "August" } ,
        { value: "September" }, 
        { value: "October" } ,
        { value: "November" } ,
        { value: "december" },
];


 

var AuditActivity = [
 
        { PaymentStatusName: "METER CAPTURE" }, 
        { PaymentStatusName: "LOGIN" },
        { PaymentStatusName: "APPROVAL"},
        { PaymentStatusName: "UPDATE" },
        { PaymentStatusName: "INSERT" },
        { PaymentStatusName: "DELETE" },
        { PaymentStatusName: "APPROVAL" }
         
];
var MAPStatus = [
        { StatusName: "APPROVED" }, 
        { StatusName: "NOTAPPROVED" },];

var phase = [ 
  { PhaseName: "ALL" },
  { PhaseName: "SINGLEPHASE" }, 
  { PhaseName: "THREEPHASE" }
];
var payment_mode = [
{ Name: "POS" },
 {Name: "WEB" },

];

var MAP_Vendor = [
{ Name: "HOLLEY", Value: "HOLLEY" },
 {Name: "ARMESE", Value: "ARMESE" },

];
var Bank_mode = [
{ Name: "FirstBank" },
 {Name: "ZenithBank" },
 { Name: "AccessBank" },
 {Name: "GTBank" },
 { Name: "SterlingBank" },
 {Name: "FCMB" },
 { Name: "SCBank" } , 
 { Name: "HeritageBank" } ,
 { Name: "EcoBank" } , 
 { Name: "FidelityBank" },
{ Name: "StanbicIBTC" } , 
{ Name: "UBA" },  
{ Name: "ProvidusBank"} , 
{ Name: "Unity" } 
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
                       { ModePaymentName: "50% UPFRONT", ModePaymentValue: "UPFRONT" },
                       { ModePaymentName: "NERC INSTALLMENT", ModePaymentValue: "MSC" },
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
        title: "Verifying...",
        text: "Please wait while we verify your account details",
        imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif', 
        showConfirmButton: false,
        allowOutsideClick: false}); 
};
function uploader() {
    swal({
        title: "Uploading...",
        text: "Please wait while the details upload",
        imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif', 
        showConfirmButton: false,
        allowOutsideClick: false}); 
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
    
    self.MAP_Vendor = ko.observableArray(MAP_Vendor);
    self.BankModeList = ko.observableArray(Bank_mode);
    self.SelectedPaymentMode  = ko.observable(); 
    self.SelectedVendor  = ko.observable();
    self.ApprovalList = ko.observableArray(approvals);
    //self.CustomerDetails = ko.observable();
     
    self.IsCustomerDetails = ko.observable(false);

    self.SelectedContractor   = ko.observable();
     self.SelectedVendor  = ko.observable();
     self.filteredBSCList  = ko.observableArray([]); 


     self.veiwCustomer = function () {

        if(self.TransactionId() == null || self.TransactionId() == ""){
            swal("Error","Transaction ID can not be Balank","error");
            self.IsCustomerDetails(false);
            return;
        }
       
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
                    swal("Generate Ticket", 'Please Kindly ask the customer to generate a Ticket at map.phed.com.ng', "info");
                } else {
                    hideloader();
                    self.IsCustomerDetails(true);
                    ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
                    
                    self.SelectedApproval(self.CustomerDetails().BRCApprovalCS);
                }
                
            },
            error: function (err) {
                //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        }); 
    }


    self.ResetMAPForm = function () { 
             
        showloader();

        $.ajax({
            type: "POST",
            url: '/MAP/ResetForm',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                TransactionId: self.TransactionId()
            }),
            success: function (result) {
                var data = JSON.parse(result);
                dd(data.CustomerDetails);
                
                // hideloader();
                self.IsCustomerDetails(true);
                ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
               
                swal("Reset", "This Form has been successfully reset", "success"); 
            },
            error: function (err) {
                //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
                //self.isProcessing(false);
            }
        }); 
         
    }

    //////////CAPTURE METERS////////////////////

        self.CaptureMeters = function () {
         
        //  console.log("MAP Vendor "+self.SelectedPaymentMode().Name);
         
        if(self.SelectedPaymentMode() == null)
        {

            swal('Select Vendor',"Please select the Vendor to Proceed with the Meter Captured",'error');

            return;
        }

        if(self.CustomerDetails.MeterSeal1() == null)
        { 
            swal('Input Meter Seal1',"Please input the meter seal Number 1 to proceed with the Meter Capture",'error');
            return;
        }
         
        if(self.CustomerDetails.MeterSeal2() == null)
        { 
            swal('Input Meter Seal2',"Please input the meter seal Number 2 to proceed with the Meter Capture",'error');
            return;
        }

        if(self.CustomerDetails.PoleNo == null)
        { 
            swal('Input Meter Seal',"Please input the meter seal Number 1 to proceed with the Meter Capture",'error');
            return;
        }
         

        if(self.CustomerDetails.InstalledMeterNo == null)
        { 
            swal('Input Meter Seal',"Please input the meter seal Number 1 to proceed with the Meter Capture",'error');
            return;
        }

        self.CustomerDetails.MAPVendor(self.SelectedPaymentMode().Name);
       // self.CustomerDetails.TransactionId(self.TransactionId());
        var StaffId = $("#StaffId").val();

        var MeterCaptureDetails = ko.mapping.toJS(self.CustomerDetails);

        showloader();

        $.ajax({
            type: "POST",
            url: '/MAP/MeterCaptureDetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'PaymentData': JSON.stringify(MeterCaptureDetails),
                'StaffId': StaffId
            }),
            success: function (result) {
                 
                var data = JSON.parse(result.result);
                self.IsCustomerDetails(true);
               // dd(data.CustomerDetails);
                

                if(result.error != "")
                { 
                    swal('Error',result.error,'error');
                    return;
                }

                ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
               hideloader();
               swal('Successful',"This Meter was Captured Successfully, The customer can go ahead and vend. Thank you.",'success');
             
            },
            error: function (err) {
                //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                alert("Fail Details Request " + err.responseText)
                //hideloader();
                //self.isProcessing(false);
            }
        });
          
        }




        self.ResetForm = function () { 
             
            showloader();
            $.ajax({
                type: "POST",
                url: '/MAP/ResetForm',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    TransactionId: self.TransactionId()
                }),
                success: function (result) {
                    var data = JSON.parse(result);
                    dd(data.CustomerDetails);
                
                    // hideloader();
                    self.IsCustomerDetails(true);
                    ko.mapping.fromJS(data.CustomerDetails, {}, self.CustomerDetails);
               
                    swal("Reset", "This Form has been successfully reset", "success"); 
                },
                error: function (err) {
                    //self.message("The Request could not be Completed! ERROR: " + err.responseText);
                    dd("Fail Details Request " + err.responseText)
                    swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
                    //self.isProcessing(false);
                }
            }); 
         
        }

    self.ViewCustomerInstallMeter = function () { 


        if(self.TransactionId() == null || self.TransactionId() == ""){
            swal("Error","Transaction ID can not be Blank","error");
            self.IsCustomerDetails(false);
            return;
        }
         
        showloader();
        $.ajax({
            type: "POST",
            url: '/MAP/ViewCustomerInstallMeter',
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

    ////////////////////////////////

    
    


    self.save = function () {

      //  console.log(JSON.stringify("ASASASASASASA "+self.CustomerDetails));

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
   
     self.ApprovalList = ko.observableArray(approvals);

     self.message = ko.observable("Choose From Date and Start Date to continue");

     self.SelectedApproval = ko.observable();
}
 
 
function ApprovePaymentViewModel(data) {
    var self = this;


    ko.mapping.fromJS(data, {}, self);

    self.ApprovalList = ko.observableArray(approvals);
    self.PaymentStatusList = ko.observableArray(payment);

    self.AuditActivityList = ko.observableArray(AuditActivity);
    self.PhaseList = ko.observableArray(phase);
    self.MAPStatusList = ko.observableArray(MAPStatus);
    self.message = ko.observable("No Data for Preview Available Now");

    self.SelectedPhase = ko.observable();
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

    self.SelectedYear = ko.observable();
    self.SelectedMonth = ko.observable();
    self.YearList = ko.observableArray(Y);
    self.MonthList = ko.observableArray(M);
    self.PaymentStatusList2 = ko.observableArray(ApprovedBill);
    self.BaseURL = ko.observable("http://map.nepamsonline.com/");

    self.SelectedVendor = ko.observable();
    self.SelectedPaymentMode = ko.observable();
    self.InstallationStatus = ko.observable();

    self.SelectedContractor = ko.observable();

    self.MAPVendor = ko.observable();
    self.MarKSelectedPayments = function (d) {

    }

    self.GetContractorByVendor = function (data) {

        var results = [];
        self.filteredBSCList([]);

        var VendorName = self.SelectedPaymentMode().Name;

        console.log("PorPor " + VendorName);

        var filtered_bsc = ko.utils.arrayFilter(self.ContractorList(), function (a) {

            return a.ProviderId() == VendorName;
        });
        ko.utils.arrayForEach(filtered_bsc,

            function (bsc) {
                results.push(bsc);
            });
        self.filteredBSCList(results);
    }


    self.UploadContractors = function (data) {
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
        // data.append("Status", self.InstallationStatus());

        uploader();

        $.ajax({
            type: "POST",
            url: '/MAP/UploadContractors',
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                hideloader();
                if (result.error != "") {
                    swal('File not Saved', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result);



                ko.mapping.fromJS(data.UplodedStatusList, {}, self.UplodedStatusList);

                PaginateTable('customer_payment_info');
                swal('Record Saved!', 'Your entry has been successfully saved', 'success');
            },
            error: function (err) {
                hideloader();
                $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    }









    self.UploadInstallers = function (data) {
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

        if (self.SelectedContractor() == null) {
            swal('Select Contractor', 'Please select a contractor to proceed. Thank you', 'info');
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

        var FileType = ext;
        var DocumentTitle = ext;

        data = new FormData();
        data.append("DocumentFile", files[0]);
        data.append("DocumentName", files[0].name);
        data.append("ContractorId", self.SelectedContractor().ContractorId());
        data.append("ContractorName", self.SelectedContractor().ContractorName());

        data.append("MAPVendor", Vendor);

        uploader();

        $.ajax({
            type: "POST",
            url: '/MAP/UploadInstallers',
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                hideloader();
                if (result.error != "") {
                    swal('File not Saved', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result);

                ko.mapping.fromJS(data.UplodedStatusList, {}, self.UplodedStatusList);
                PaginateTable('customer_payment_info');
                swal('Record Saved!', 'Your entry has been successfully saved', 'success');
            },
            error: function (err) {
                hideloader();
                $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    }





    //////////////////////////////////////


    self.ViewPHEDBills = function (event) {

        var consNo = '832510284901';
        //var consNo = $('#txt_consNo').val();//'832510284901';
        //var billMonth = $('#txt_billMonth').val();//'01-06-2018';
        //var month = billMonth;//'Jun-2018';
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

        //$(document).ready(function(){
        //    $('#txt_billMonth').datepicker({
        //        format: "dd-mm-yyyy",
        //        viewMode: "months",
        //        minViewMode: "months",
        //        autoclose: true,
        //        clearBtn: true,
        //    });
        //});













        //var AccountNo =  $('#AccountNo').val(); 

        //var Year =  self.SelectedYear(); 
        //var Month =  self.SelectedMonth();
        //   console.log("See this "+ Year);
        //   var URL = "https://dlenhance.phed.com.ng/BillPDF/841134043101_feb-2020.pdf?Month="+Month+"&Year="+Year+"&Category=BILL"+"&Account="+AccountNo;

        //window.open(URL, '_blank');
        //$("#TellerModal").modal("show"); 
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

        //var AccountNo =  $('#AccountNo').val(); 

        //var Year =  self.SelectedYear(); 
        //var Month =  self.SelectedMonth();
        //   console.log("See this "+ Year);
        //   var URL = "https://dlenhance.phed.com.ng/CollPDF/150120202736747.pdf?Month="+Month+"&Year="+Year+"&Category=BILL"+"&Account="+AccountNo;

        //window.open(URL, '_blank');
        //$("#TellerModal").modal("show"); 
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
                'FromDate': self.FromDate(),
                'ToDate': self.ToDate()
            }),
            success: function (result) {
                var data = JSON.parse(result);
                hideloader();
                ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                PaginateTable('customer_payment_info');

            },
            error: function (err) {
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });
    }








    self.ViewTeller = function (event) {
        //var name = self.StudentDetail.STUDENT_SURNAME() + " " + self.StudentDetail.STUDENT_NAME() + " " + self.StudentDetail.STUDENT_MIDDLE_NAME()
        //self.StudentName(name);

        console.log("AMAMIoooooooooo" + event.DocumentPath());

        if (event.DocumentPath() == null) {
            swal("Error", "There was no teller attached to this Payment Claim.", "error");
            return;
        }

        var URL = self.BaseURL() + event.DocumentPath();

        window.open(URL, '_blank');
        //$("#TellerModal").modal("show"); 
    }




    self.ApproveMarkedSelectedPayments = function (d) {
        //Stringify the JSON List

        $.ajax({
            type: "POST",
            url: '/MAP/ApproveMAPPayment',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                TicketId: d.TicketId(),
                ReceiptNo: d.ReceiptNo(),
                DatePaid: d.DatePaid(),
                Phase: d.Phase(),
                AccountNo: d.AccountNo(),
                AmountPaid: d.Amount(),
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
                dd(err.responseText);
                //alert("Fail Details Request " + err.responseText)
                //self.isProcessing(false);
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

        console.log("Vendor     " + Vendor); console.log("Status      " + Status);
        uploader();

        $.ajax({
            type: "POST",
            url: '/MAP/ViewApprovedBulkMeters',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Vendor': Vendor,
                'Status': Status,
                'ApprovalStatus': ApprovalStatus

            }),
            success: function (result) {
                //  var data = JSON.parse(result);



                if (result.error != "") {
                    swal('No Meter to View at the Moment', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result);



                ko.mapping.fromJS(data.MeterUploadApprovalList, {}, self.MeterUploadApprovalList);


                hideloader();

                PaginateTable('customer_payment_info');
            },
            error: function (err) {
                dd("Fail Details Request " + err.responseText)
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


        console.log("Vendor     " + Vendor); console.log("Status      " + Status);
        uploader();

        $.ajax({
            type: "POST",
            url: '/MAP/ViewUploadBulkMeters',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'Vendor': Vendor,
                'Status': Status


            }),
            success: function (result) {
                //  var data = JSON.parse(result);



                if (result.error != "") {
                    swal('No Meter to View at the Moment', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result);



                ko.mapping.fromJS(data.MeterUploadApprovalList, {}, self.MeterUploadApprovalList);


                hideloader();

                //PaginateTable('customer_payment_info');  
            },
            error: function (err) {
                dd("Fail Details Request " + err.responseText)
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
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                if (result.error != "") {
                    swal('File not Saved', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result);



                ko.mapping.fromJS(data.UplodedStatusList, {}, self.UplodedStatusList);

                PaginateTable('customer_payment_info');
                swal('Record Saved!', 'Your entry has been successfully saved', 'success');
            },
            error: function (err) {
                $('#pageloader-overlay').fadeOut(100);
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
            type: "post",
            url: '/MAP/UploadApplication',
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
            cache: false,
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                if (result.error != "") {
                    swal('File not Saved', result.error, 'error');
                    return;
                }

                var data = JSON.parse(result.result);


                ko.mapping.fromJS(data.UplodedStatusList, {}, self.UplodedStatusList);

                PaginateTable('customer_payment_info');
                swal('Record Saved!', 'Your entry has been successfully saved', 'success');
            },
            error: function (err) {
                $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

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
        data.append("BankName", self.SelectedBankMode().name);
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

                if (result.error != "") {
                    swal('File not Saved', result.error, 'error');
                    return;
                }
                var data = JSON.parse(result.result);
                swal('Record Saved!', 'Your entry has been successfully saved', 'success');
            },
            error: function (err) {
                $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }

        });

    }



    self.DownloadBulkApplicationExcelTemplate = function (d) {

        var ReportViewers = "http://map.nepamsonline.com/Documents/BulkApplicationExcelTemplate.xlsx";
        var url = ReportViewers;
        window.open(url, '_blank');
    }


    self.DownloadStaffOnboardExcelTemplate = function (d) {

        var ReportViewers = "http://map.nepamsonline.com/Documents/PHEDStaffOnboard.xlsx";
        var url = ReportViewers;
        window.open(url, '_blank');
    }

    self.DownloadMeterExcelTemplate = function (d) {

        var ReportViewers = "http://map.nepamsonline.com/Documents/MeterUploadExcelTemplate.xlsx";
        var url = ReportViewers;
        window.open(url, '_blank');
    }

    self.DownloadContractorExcelTemplate = function (d) {

        var ReportViewers = "http://map.nepamsonline.com/Documents/ContractorExcelTemplate.xlsx";
        var url = ReportViewers;
        window.open(url, '_blank');
    }


    self.DownloadInstallerExcelTemplate = function (d) {

        var ReportViewers = "http://map.nepamsonline.com/Documents/InstallerExcelTemplate.xlsx";
        var url = ReportViewers;
        window.open(url, '_blank');
    }

    self.ViewCustomersUnapprovedData = function (d) {


        showloader();
        console.log("Reaching here ooooooooooo");

        var IBC = self.SelectedIBC().IBCName();
        var BSC = self.SelectedBSC().BSCName();
        var PaymentStatus = self.SelectedPaymentStatus().PaymentStatusName;


        $.ajax({
            type: "POST",
            url: '/MAP/ViewCustomersUnapprovedData',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'IBC': IBC,
                'BSC': BSC,
                'STATUS': PaymentStatus

            }),
            success: function (result) {
                var data = JSON.parse(result);

                hideloader();
                ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
                PaginateTable('customer_payment_info');
            },
            error: function (err) {
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });
    }




    self.ViewCustomersAudittrailReport = function (d) {


        showloader();
        console.log("Reaching here ooooooooooo");

        var PaymentStatus = self.SelectedAuditStatus().PaymentStatusName;

        console.log("sdsdsdsdsdsdsd   " + PaymentStatus);

        $.ajax({
            type: "POST",
            url: '/MAP/ViewCustomersAudittrailReport',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'FromDate': self.FromDate(),
                'ToDate': self.ToDate(),
                'STATUS': PaymentStatus

            }),
            success: function (result) {
                //  var data = JSON.parse(result);



                var data = JSON.parse(result.result);
                //self.IsCustomerDetails(true);
                // dd(data.CustomerDetails);


                if (result.error != "") {
                    swal('Error', result.error, 'error');
                    return;
                }

                hideloader();
                ko.mapping.fromJS(data.AuditTrailList, {}, self.AuditTrailList);
                //  PaginateTable('customer_payment_info');  
            },
            error: function (err) {
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error");
            }
        });
    }











    self.filterBSCFromIBC = function () {
        dd("filtering data...");
        dd("BSCLIST: " + JSON.stringify(self.BSCList()));
        var results = [];
        self.filteredBSCList([]);
        if (self.SelectedIBC() != null) {
            var bsc_id = self.SelectedIBC().IBCId();

            dd("BSC: " + bsc_id);

            var filtered_bsc = ko.utils.arrayFilter(self.BSCList(), function (a) {
                if (a.BSCId() == '080') {

                    return a;
                }
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
        var TicketId = "";
        var DatePaid = "";
        var Phase = "";
        var ReceiptNo = "";
        var AccountNo = "";
        var AmountPaid = "";


        console.log("asasasasasasas" + d.TransactionID());
        $('#TicketIDHidden').val(d.TransactionID());
        $('#ModalTicketId').text(d.TransactionID());

        $('#ModalTicketId').text(d.TransactionID());


        self.CSMApprovalStatus(d.BRCApprovalCSM());
        self.CSMApprovalDate(d.BRCDate());
        self.CSMApprovalAmount(d.BRCApprovalCSMAmount());
        self.BRCApprovalCSMComment(d.BRCApprovalCSMComment());



        $('#largeModal1').appendTo("body").modal('show');

    }


    self.ApproveBRCBillsForPaymentDB = function (d) {


        console.log(" Freedom " + $("#TicketIDHidden").val());

        var data = ko.mapping.toJS({
            PaymentStatus: self.SelectedApproval().ApprovalName,
            IBC: self.SelectedIBC().IBCName,
            BSC: self.SelectedBSC().BSCName,
            BRCApprovalIBCHeadComment: self.BRCApprovalIBCHeadComment(),
            BRCApprovalIBCDate: self.BRCApprovalIBCDate(),
            BRCApprovalIBCAmount: self.BRCApprovalIBCAmount(),
            BRCApprovalIBCHead: self.SelectedPaymentStatus().StatusName

        });

        swal({
            title: "Please Confirm",
            text: "Are you sure you want to approve this BRC?",
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
           showloader();

           $('#largeModal1').modal('hide');
           $('#largeModal1').appendTo("body").modal('hide');


           $.ajax({
               type: "POST",
               url: '/MAP/ApproveBRCBillsForPaymentDB',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               data: JSON.stringify({
                   TicketId: $("#TicketIDHidden").val(),
                   in_data: JSON.stringify(data),
                   StaffID: $("#StaffId").val(),
                   StaffName: $("#StaffName").val()
               }),
               success: function (result) {

                   var table = $('#example2').DataTable();
                   table.destroy();
                   hideloader();

                   var data = JSON.parse(result.result);

                   if (result.error != "") {
                       swal('Error', result.error, 'error');
                       return;
                   }


                   swal("Approved", "The Customer has been approved and can now proceed for payment.", "success");



                   // zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz



                   // ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);

                   //self.PaymentList(data.PaymentList);
                   if (data.PaymentList.length < 1) {
                       dd("no data exist: " + data.PaymentList.length);
                       self.message("No Payment Exists in the Range");
                   }
                   else {

                       $('#largeModal1').modal('hide');
                       $('#largeModal1').appendTo("body").modal('hide');
                       self.PaymentList([]);
                       ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList);
                       PaginateTable('customer_IBC_APPROVALcustomer_IBC_APPROVAL');
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

        var TicketId = "";
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
        console.log(" Freedom " + $("#TicketIDHidden").val());
        var StaffName = $("#StaffName").val();
        var data = ko.mapping.toJS({
            PaymentStatus: self.SelectedPaymentStatus().PaymentStatusName,
            IBC: self.SelectedIBC().IBCName,
            BSC: self.SelectedBSC().BSCName,
            BRCApprovalIBCAmount: self.CustomerDetails.BRCApprovalIBCAmount,
            BRCApprovalCSM: self.SelectedPaymentStatus().PaymentStatusName,
            BRCApprovedBy: StaffName,
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
                   else {
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

    }





    self.ApproveMeter = function (d) {
        var TicketId = "";
        var DatePaid = "";
        var Phase = "";
        var ReceiptNo = "";
        var AccountNo = "";
        var AmountPaid = "";


        var Vendor = self.SelectedPaymentMode().Name;
        var Status = self.InstallationStatus();


        console.log("sssssssssssssssssss" + d.MeterNo());

        swal({
            title: "Please Confirm",
            text: "Are you sure you want to approve this Meter?",
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

           $.ajax({
               type: "POST",
               url: '/MAP/ApproveMeter',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               data: JSON.stringify({
                   'MeterNo': d.MeterNo(),
                   'StaffID': $("#StaffId").val(),
                   'Status': Status,
                   'Vendor': Vendor,
               }),
               success: function (result) {
                   var data = JSON.parse(result.result);

                   if (result.error != "") {
                       swal('Error', result.error, 'error');
                       return;
                   }
                   swal("Approved", "The MAP Meter has been Approved and can now be Installed.", "success");
                   //hideloader();
                   ko.mapping.fromJS(data.MeterUploadApprovalList, {}, self.MeterUploadApprovalList);
                   //PaginateTable('customer_payment_info');  

               },

               error: function (err) {
                   hideloader();
                   self.message("The Request could not be Completed!");
                   dd(err.responseText);

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


    self.ApprovePayment = function (d) {

        var TicketId = "";
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
               showloader();

               var table = $('#customer_payment_info').DataTable();
               table.destroy();
               $.ajax({
                   type: "POST",
                   url: '/MAP/ApproveMAPPayment',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   data: JSON.stringify({
                       'TicketId': d.TicketId(),
                       'ReceiptNo': d.ReceiptNo(),
                       'DatePaid': d.DatePaid(),
                       'Phase': d.Phase(),
                       'AccountNo': d.AccountNo(),
                       'AmountPaid': d.Amount(),
                       'StaffID': $("#StaffId").val(),
                   }),
                   success: function (result) {
                       var data = JSON.parse(result);
                       //dd("count: " + data.PaymentList.length);
                       //dd(data.PaymentList);
                       // hideloader();
                       ko.mapping.fromJS(data.MAPPaymentList, {}, self.MAPPaymentList);
                       swal("Approved", "The Customer's Payment has been approved.", "success");
                       PaginateTable('customer_payment_info');

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
                       swal("Oops", "An error Occured and Customer's Payment was not approved. please try again later", "error");
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
        var TicketId = "";
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
                   else {
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


    self.FetchListOfPaidCustomers = function () {

        dd("fetch data");
        dd(self.SelectedPaymentStatus());

        if (self.SelectedPaymentStatus() == null || self.SelectedPaymentStatus() == "" || self.SelectedPaymentStatus() == "undefined") {
            swal("Error", "Please select Payment Status to proceed", "info");
            return;
        }


        if (self.SelectedIBC() == null || self.SelectedIBC() == "" || self.SelectedIBC() == "undefined") {
            swal("Error", "Please select Zone Status to proceed", "info");

            return;
        }

        if (self.SelectedBSC() == null || self.SelectedBSC() == "" || self.SelectedBSC() == "undefined") {
            swal("Error", "Please select Feeder Status to proceed", "info");
            return;
        }

        console.log("xsxsxsxsxsxs x xs xs xs xsx " + self.SelectedPaymentStatus().StatusName);


        var data = ko.mapping.toJS({
            BRCApprovalCSM: self.SelectedPaymentStatus().StatusName,
            IBC: self.SelectedIBC().IBCName,
            BSC: self.SelectedBSC().BSCName,
            MeterPhase: self.SelectedPhase.PhaseName
        });
        var ApprovalStatus = $("#HiddenStatus").val();
        dd(data);
        showloader();

        $.ajax({
            type: "POST",
            url: '/MAP/FetchListOfPaidCustomers',
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
                PaginateTable('customer_IBC_APPROVAL');



                hideloader();
                //self.PaymentList(data.PaymentList);
                if (data.PaymentList.length < 1) {
                    dd("no data exist: " + data.PaymentList.length);
                    self.message("No Payment Exist in the Range");
                }
                else {
                    self.message("");
                    // ko.mapping.fromJS(data, {}, self);
                    //self.PaymentList(data.PaymentList);


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



    self.fetchCustomerPaymentInfo = function () {

        dd("fetch data");
        dd(self.SelectedPaymentStatus());

        if (self.SelectedPaymentStatus() == null || self.SelectedPaymentStatus() == "" || self.SelectedPaymentStatus() == "undefined") {
            swal("Error", "Please select Payment Status to proceed", "info");
            return;
        }


        if (self.SelectedIBC() == null || self.SelectedIBC() == "" || self.SelectedIBC() == "undefined") {
            swal("Error", "Please select Zone Status to proceed", "info");

            return;
        }

        if (self.SelectedBSC() == null || self.SelectedBSC() == "" || self.SelectedBSC() == "undefined") {
            swal("Error", "Please select Feeder Status to proceed", "info");
            return;
        }

        console.log("xsxsxsxsxsxs x xs xs xs xsx " + self.SelectedPaymentStatus().StatusName);


        var data = ko.mapping.toJS({
            BRCApprovalCSM: self.SelectedPaymentStatus().StatusName,
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
                PaginateTable('customer_IBC_APPROVAL');



                hideloader();
                //self.PaymentList(data.PaymentList);
                if (data.PaymentList.length < 1) {
                    dd("no data exist: " + data.PaymentList.length);
                    self.message("No Payment Exist in the Range");
                }
                else {
                    self.message("");
                    // ko.mapping.fromJS(data, {}, self);
                    //self.PaymentList(data.PaymentList);


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

    self.ViewCustomersData = function (d) { 
        showloader();

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
                }
                 
                else {
                    hideloader();
                    dd(data.CustomerDetailsFromCustomer);
                    //ko.mapping.fromJS(self.CustomerDetailsFromCustomer, data.CustomerDetailsFromCustomer);
                    var CustomerDetails = data.CustomerDetailsFromCustomer;
                    $('#ModalTicketId').text(CustomerDetails.TransactionID);
                   
                    var output = "";
                    var i = 0;
                    for(let [key, value] of Object.entries(CustomerDetails) )
                    {
                        
                       
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
               
            }
    });

         


}
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
                 
                     console.log("Efreborrrrrrrrrrrrrrrr "+data.CustomerDetails.BRCApprovalCS);

                     self.SelectedApproval(data.CustomerDetails.BRCApprovalCS);

                      
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


     self.CaptureMeter = function (data) {
         var StaffID = $("#StaffId").val(); 

         showloader();
         var customerDetails = ko.mapping.toJS(data);
         $.ajax({
             type: "POST",
             url: '/MAP/InsertBRC',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({
                 CustomerData: JSON.stringify(customerDetails),
                 StaffID: StaffID
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



     self.save = function () {
         //self.CustomerDetails.TransactionID(self.CustomerDetails.TransactionID());
         self.CustomerDetails.BRCApprovalCS("APPROVED");


         console.log("See this " + self.CustomerDetails.Amount());
         //self.CustomerDetails.BRCApprovedAmount(self.CustomerDetails.Amount());
         var CustomerDetails = JSON.stringify(self.CustomerDetails);

         dd( self.CustomerDetails);
         self.insertBRC(self.CustomerDetails);

     }


     self.insertBRC = function (data) {
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



     self.saveCSM = function () {
         //self.CustomerDetails.TransactionID(self.CustomerDetails.TransactionID());
         self.CustomerDetails.BRCApprovalCSM(self.SelectedApproval().ApprovalName);


         console.log("See this " + self.CustomerDetails.Amount());
         //self.CustomerDetails.BRCApprovedAmount(self.CustomerDetails.Amount());
         var CustomerDetails = JSON.stringify(self.CustomerDetails);

         dd( self.CustomerDetails);
         self.insertCSM(self.CustomerDetails);

     }


     self.insertCSM = function (data) {

         var StaffID = $("#StaffId").val(); 
         showloader();
         var customerDetails = ko.mapping.toJS(data);
         $.ajax({
             type: "POST",
             url: '/MAP/InsertBRCCSM',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({
                 CustomerData: JSON.stringify(customerDetails),
                 StaffID: StaffID
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
    

