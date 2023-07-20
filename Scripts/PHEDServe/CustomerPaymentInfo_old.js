
function ApprovePaymentViewModel (data) 
{
    var self = this; 
     
    ko.mapping.fromJS(data, {}, self);

}



function ApprovePaymentViewModel1 (data) 
{
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
      
    self.SelectedYear  = ko.observable();
    self.SelectedMonth  = ko.observable();
    self.YearList = ko.observableArray(Y); 
    self.MonthList = ko.observableArray(M);
    self.PaymentStatusList2 = ko.observableArray(ApprovedBill);
    self.BaseURL = ko.observable("http://map.nepamsonline.com/");

    self.SelectedVendor  = ko.observable();  
    self.SelectedPaymentMode  = ko.observable();
    self.InstallationStatus  = ko.observable();

    self.SelectedContractor   = ko.observable();
     
    self.MAPVendor = ko.observable();  
    self.MarKSelectedPayments = function (d){
          
    }
    /////////////////////////////////////////

     
    //self.GetContractorByVendor = function(data) {
    //    var self = this; 


    //    var Vendor = self.SelectedPaymentMode().Name;
    //    console.log("Hallelujsh Hosanna "+ Vendor );
    //}

    
    self.GetContractorByVendor = function(data) {
         
        var results = [];
        self.filteredBSCList([]);
         
        var VendorName = self.SelectedPaymentMode().Name;

        console.log("PorPor "+ VendorName);

        var filtered_bsc = ko.utils.arrayFilter(self.ContractorList(), function (a) {
                
            return a.ProviderId() == VendorName;
        });
        ko.utils.arrayForEach(filtered_bsc, 
                
            function (bsc) {
                results.push(bsc);
            }); 
        self.filteredBSCList(results); 
    }
 
 
    self.UploadContractors = function(data) {
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
            error: function (err) {
                hideloader();
                $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    }









    self.UploadInstallers = function(data) {
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
        // var Size = m.toFixed(2) + " KB";
        var FileType = ext;
        var DocumentTitle = ext;

        data = new FormData();
        data.append("DocumentFile", files[0]);
        data.append("DocumentName", files[0].name);
        data.append("ContractorId", self.SelectedContractor().ContractorId());
        data.append("ContractorName", self.SelectedContractor().ContractorName());
        //data.append("StudentCode", self.StudentCode());
        
      
        data.append("MAPVendor", Vendor);
        // data.append("Status", self.InstallationStatus());
       
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
        var billMonth =  '01-06-2019';
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
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error"); 
            }
        });
    }







     
    self.ViewTeller = function (event) {
        //var name = self.StudentDetail.STUDENT_SURNAME() + " " + self.StudentDetail.STUDENT_NAME() + " " + self.StudentDetail.STUDENT_MIDDLE_NAME()
        //self.StudentName(name);
          
        console.log("AMAMIoooooooooo" + event.DocumentPath());
        
        if (event.DocumentPath() == null)
        {
            swal("Error", "There was no teller attached to this Payment Claim.", "error");
            return;
        }

        var URL = self.BaseURL() + event.DocumentPath();
        
        window.open(URL, '_blank');
        //$("#TellerModal").modal("show"); 
    }


     

    self.ApproveMarkedSelectedPayments = function (d){ 
        //Stringify the JSON List
          
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


    self.DownloadInstallerExcelTemplate = function (d) { 

        var ReportViewers = "http://map.nepamsonline.com/Documents/InstallerExcelTemplate.xlsx";
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
                dd("Fail Details Request " + err.responseText)
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
                // dd(data.CustomerDetails);
                

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
                dd("Fail Details Request " + err.responseText)
                swal("Error", "This Request Could not be Completed, if it Continues Please Contact IT", "error"); 
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
               
            }
    });

         


}