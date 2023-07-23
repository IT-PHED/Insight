function RegViewModel(data) {

    var self = this; 
    ko.mapping.fromJS(data, {}, this);

    self.isProcessing = ko.observable(true); self.IAgree = ko.observable(false);
    self.Password = ko.observable();
    self.ConfirmPassword = ko.observable();
    self.AccountNo = ko.observable();
    self.AccountType = ko.observable();
    self.SelectedBSC = ko.observable();
    self.SelectedIBC = ko.observable();
    self.SubmissionStatus = ko.observable();
    self.ReportStatus = ko.observable();
    self.ReportDate = ko.observable();
    self.message = ko.observable("There is no Data to display Now. Please select a criteria to view Data");

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

                    // PaginateTable('customer_payment_info');
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


    self.GenerateBillPaymentList = function (form) {


        console.log("StaffID " + $("#PHEDStaffId").val());
        self.isProcessing(true);
       
         
        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDServe/GenerateBillPaymentList',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({

                'SubmissionStatus': self.SubmissionStatus(),
                'ReportStatus': self.ReportStatus(),
                'ReportDate': self.ReportDate() 
                 
            }),
            success: function (result) {

                hideloader();
                if (result.result == "Customer Not Found") {

                    swal('Wrong Number!', 'It seems the AccountNo or Meter Number you Typed in is Wrong. Please type AccountNo for POSTPAID Accounts or MeterNo for PREPAID Accounts. Thank You.', 'error');
                    return;
                }
                
                var data = JSON.parse(result.result);
                 
                //swal('Record Seen!', 'The user data was Found', 'success');
                //ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);


                self.StaffBillPaymentDataList([]);
                console.log(JSON.stringify(data.StaffBillPaymentDataList));

                ko.mapping.fromJS(data.StaffBillPaymentDataList, {}, self.StaffBillPaymentDataList);
                self.isProcessing(false);


                PaginateTable('StaffPaymentList');

                
            },
            error: function (err) {
   hideloader();
                 swal('Something went wrong', 'It seems DL-Enhance is not reachable or your internet connection is Unstable. Please check and try again. Thank You.', 'error');
         

                console.log("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    };

    self.DownloadStaffOnboardExcelTemplate = function (d) {

        var ReportViewers = "http://map.nepamsonline.com/Documents/PHEDStaffOnboard.xlsx";
        var url = ReportViewers;
        window.open(url, '_blank');
    }


    self.UploadStaffAndOnboard = function (d) {

       
        var files = $("#yourfileID").get(0).files;



        //if (self.SelectedPaymentMode() == null) {
        //    swal('MAP Vendor', 'Please select the MAP Vendor.Thank you', 'info');
        //    return;
        //}

        //if (self.InstallationStatus() == null) {
        //    swal('Select Status', 'Please select the Installation Status to proceed. Thank you', 'info');
        //    return;
        //}



        if (files[0] == null) {
            swal('select a File!', 'Please select a document to proceed.', 'info');
            return;
        }

        var ext = files[0].name.split('.').pop();


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


        
      


        //Upload the Files
        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDServe/UploadStaffAndOnboard',
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
                 
                ko.mapping.fromJS(data.UplodedStaffList, {}, self.UplodedStaffList);

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

    self.VerifyAccount = function (form) {


        console.log("StaffID " + $("#PHEDStaffId").val());
        self.isProcessing(true);
       


        //var InsertModifiedunmapped = ko.mapping.toJS(self.ApplicationUser);


        if (self.AccountNo() == "" || self.AccountNo() == null)
        {

            swal('Input a Number!', 'It seems the AccountNo or Meter Number you Typed in is Wrong. Please type AccountNo for POSTPAID Accounts or MeterNo for PREPAID Accounts. Thank You.', 'error');
            return;

        }

        showloader();

        $.ajax({
            type: "POST",
            url: '/PHEDServe/VerifyAccount',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({

                'AccountNo': self.AccountNo(),
                'AccountType': self.AccountType(),
                'staffId': $("#PHEDStaffId").val()
            }),
            success: function (result)
            {

                if (result.result == "Customer Not Found") { 
                    swal('Wrong Number!', 'It seems the AccountNo or Meter Number you Typed in is Wrong. Please type AccountNo for POSTPAID Accounts or MeterNo for PREPAID Accounts. Thank You.', 'error');
                    return;
                }


                if(result.error != "")
                {
                    hideloader();
                    $("#StaffBillVerification").slideUp();
                    $("#DuplicatePane").slideDown();
                    $("#VerifyPane").slideUp();
                    return;
                }
                
                var data = JSON.parse(result.result);
                  
                //swal('Record Seen!', 'The user data was Found', 'success');
                ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);
                self.isProcessing(false);

                $("#StaffBillVerification").slideDown(); 
                hideloader(); 
            },
            error: function (err) {
   hideloader();
                 swal('Something went wrong', 'It seems DL-Enhance is not reachable or your internet connection is Unstable. Please check and try again. Thank You.', 'error');
         

                console.log("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    };



    self.register = function (form) {

        self.isProcessing(true);

        var InsertModifiedunmapped = ko.mapping.toJS(self.ApplicationUser);


        $.ajax({
            type: "POST",
            url: '/Account/AddNewUser',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'userToBeAdded': JSON.stringify(InsertModifiedunmapped),
                'Password': self.Password()
            }),
            success: function (result) {


                var data = JSON.parse(result);
                swal('Record Saved!', 'The user has been registered successfully', 'success');

                self.isProcessing(false);
                window.location.href = "/Account/Login"

            },
            error: function (err) {

                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    };

    self.registerAdmin = function (form) {

        self.isProcessing(true);



        var InsertModifiedunmapped = ko.mapping.toJS(self.ApplicationUser);

        $.ajax({
            type: "POST",
            url: '/Account/AddNewUserAdmin',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'userToBeAdded': JSON.stringify(InsertModifiedunmapped),
                'Password': self.Password()

            }),
            success: function (result) {


                var data = JSON.parse(result);
                swal('Record Saved!', 'The user has been registered successfully', 'success');

                self.isProcessing(false);
                //window.location.href = "/Account/Login"
                ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);
            },
            error: function (err) {

                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    };

    self.StaffBillVerification = function (form) {

        var CreatedBy = $("#StaffId").val();

        self.isProcessing(true);


        if (self.ApplicationUser.OfficeLocation() == null || self.ApplicationUser.OfficeLocation() == "") {

            swal('Office Location', 'Please type in your current Office Location to continue', 'info');
            return;
        }

        if (self.ApplicationUser.PhoneNo() == null || self.ApplicationUser.PhoneNo() == "") {

            swal('Phone No', 'Please type in your current PhoneNo to continue', 'info');
            return;
        }


        if (self.ApplicationUser.MeterMake() == null || self.ApplicationUser.MeterMake() == "") {

            swal('MeterType', 'Please type in your current MeterType to continue', 'info');
            return;
        }



        if (self.ApplicationUser.JobRole() == null || self.ApplicationUser.JobRole() == "") {

            swal('JobRole', 'Please type in your current JobRole to continue', 'info');
            return;
        }


        //if (self.ApplicationUser.Designation() == null || self.ApplicationUser.Designation() == "") {

        //    swal('Designation', 'Please type in your current Designation to continue', 'info');
        //    return;
        //}

        if (self.ApplicationUser.CUGLine() == null || self.ApplicationUser.CUGLine() == "") {

            swal('CUGLine', 'Please type in your current CUG Line to continue', 'info');
            return;
        }
        if (self.ApplicationUser.ResolvedBalance() == null || self.ApplicationUser.ResolvedBalance() == "") {

            swal('Resolved Balance', 'Please type in the resolved balance if you went for Bill Reconciliation, enter N/A if not applicable', 'info');
            return;
        }
        if (self.ApplicationUser.PeriodToClearDebt() == null || self.ApplicationUser.PeriodToClearDebt() == "" || self.ApplicationUser.PeriodToClearDebt() == "--Select Period--") {

            swal('Period to Clear Debt', 'Please select the rperiod to Clear your Debt. If you are not owing, select I AM NOT OWING. Kindly note that this will be verified.', 'info');
            return;
        }
        if (self.IAgree() != true || !self.IAgree()) {

            swal('Agree to the Disclaimer', 'Please select the check box to agree to the disclaimer that the information you have supplied is correct and verifiable', 'info');
            return;
        }
        

        self.ApplicationUser.MeterType(self.AccountType());

        var InsertModifiedunmapped = ko.mapping.toJS(self.ApplicationUser);
        showloader();
        $.ajax({
            type: "POST",
            url: '/Account/StaffBillVerification',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'userToBeAdded': JSON.stringify(InsertModifiedunmapped),
                'userID': CreatedBy
            }),
            success: function (result) {
                hideloader();
                var data = JSON.parse(result);
                //swal('Record Saved!', 'The Bill Verification Data has been Submitted successfully', 'success');
                self.isProcessing(false);
                ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);

                $("#VerifyPane").slideUp();
                $("#SuccessPane").slideDown();

            },
            error: function (err) {

                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    };
}


function Done() {
    var light_2 = $("#Holder");
    $(light_2).unblock();
}

function Busy() {
    var light_2 = $("#Holder");
    $(light_2).block({
        message: '<i class="icon-spinner2 spinner"></i>',
        overlayCSS: {
            backgroundColor: '#1B2024',
            opacity: 0.8,
            cursor: 'wait'
        },
        css: {
            border: 0,
            padding: 0,
            backgroundColor: 'none',
            color: '#fff'
        }
    });
}

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
