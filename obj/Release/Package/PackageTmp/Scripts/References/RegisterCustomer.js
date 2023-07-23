//import state with lga data

//debuging
var debug = true;
function dd(arg) {
    if (debug == true) {
        console.log(arg);
    }
}

 
var titles = [
    { TitleName: "Mr", TitleValue: "Mr" },
    { TitleName: "Miss", TitleValue: "Miss" },
    { TitleName: "Mrs", TitleValue: "Mrs" },
    { TitleName: "Dr", TitleValue: "Dr" },
    { TitleName: "Prof", TitleValue: "Prof" },
    { TitleName: "Chief", TitleValue: "Chief" },
];

var Occupant = [
    { TitleName: "LANDLORD", TitleValue: "LANDLORD" },
    { TitleName: "TENANT", TitleValue: "TENANT" } 
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
//var mode_of_payments = [
//    { ModePaymentName: "UPFRONT", ModePaymentValue: "UPFRONT" },
//    { ModePaymentName: "100% UPFRONT", ModePaymentValue: "100UPFRONT" },
//    { ModePaymentName: "75% UPFRONT", ModePaymentValue: "75UPFRONT" },
//    { ModePaymentName: "50% UPFRONT", ModePaymentValue: "50UPFRONT" },
//    { ModePaymentName: "25% UPFRONT", ModePaymentValue: "25UPFRONT" },
//{ ModePaymentName: "NERC INSTALLMENT", ModePaymentValue: "MSC" },
//];
var mode_of_payments = [ { ModePaymentName: "100% UPFRONT", ModePaymentValue: "100UPFRONT" },
    { ModePaymentName: "75% UPFRONT", ModePaymentValue: "75UPFRONT" },
    { ModePaymentName: "50% UPFRONT", ModePaymentValue: "50UPFRONT" },
    { ModePaymentName: "25% UPFRONT", ModePaymentValue: "25UPFRONT" },
{ ModePaymentName: "0% UPFRONT", ModePaymentValue: "0UPFRONT" }];


//var payment_schedules = [
//    { PaymentScheduleName: "1 year (₦6,704.06/Month)",  PaymentScheduleValue: "1 years" },
//    { PaymentScheduleName: "2 years (₦3,651.96/Month)", PaymentScheduleValue: "2 years" },
//    { PaymentScheduleName: "3 years (₦2,649.13/Month)", PaymentScheduleValue: "3 years" },
//    { PaymentScheduleName: "4 years (₦2,158.39/Month)",  PaymentScheduleValue: "4 years"},
//    { PaymentScheduleName: "5 years (₦1,872.23/Month)", PaymentScheduleValue: "5 years" },
//    { PaymentScheduleName: "6 years (₦1,688.11/Month)", PaymentScheduleValue: "6 years" },
//    { PaymentScheduleName: "7 years (₦1,562.03/Month)",  PaymentScheduleValue: "7 years"},
//    { PaymentScheduleName: "8 years (₦1,471.97/Month)", PaymentScheduleValue: "8 years" },
//    { PaymentScheduleName: "9 years (₦1,405.70/Month)", PaymentScheduleValue: "9 years" },  
//    { PaymentScheduleName: "10 years (₦1,355.83/Month)", PaymentScheduleValue: "10 years" }, 
//];






//var payment_schedules = [
//    { PaymentScheduleName: "1 year (₦6,704.06/Month)",  PaymentScheduleValue: "1 year", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "2 years (₦3,651.96/Month)", PaymentScheduleValue: "2 years", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "3 years (₦2,649.13/Month)", PaymentScheduleValue: "3 years", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "4 years (₦2,158.39/Month)",  PaymentScheduleValue: "4 years", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "5 years (₦1,872.23/Month)", PaymentScheduleValue: "5 years", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "6 years (₦1,688.11/Month)", PaymentScheduleValue: "6 years", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "7 years (₦1,562.03/Month)",  PaymentScheduleValue: "7 years", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "8 years (₦1,471.97/Month)", PaymentScheduleValue: "8 years", MeterPhase : "ThreePhase" },
//    { PaymentScheduleName: "9 years (₦1,405.70/Month)", PaymentScheduleValue: "9 years", MeterPhase : "ThreePhase" },  
//    { PaymentScheduleName: "10 years (₦1,355.83/Month)", PaymentScheduleValue: "10 years", MeterPhase : "ThreePhase" }, 
//    { PaymentScheduleName: "1 year (₦3,698.31/Month)",  PaymentScheduleValue: "1 year", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "2 years (₦2,014.61/Month)", PaymentScheduleValue: "2 years", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "3 years (₦1,461.40/Month)", PaymentScheduleValue: "3 years", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "4 years (₦1,190.68/Month)",  PaymentScheduleValue: "4 years", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "5 years (₦1,032.82/Month)", PaymentScheduleValue: "5 years", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "6 years (₦931.25/Month)", PaymentScheduleValue: "6 years", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "7 years (₦861.70/Month)",  PaymentScheduleValue: "7 years", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "8 years (₦812.02/Month)", PaymentScheduleValue: "8 years", MeterPhase : "SinglePhase" },
//    { PaymentScheduleName: "9 years (₦775.45/Month)", PaymentScheduleValue: "9 years", MeterPhase : "SinglePhase" },  
//    { PaymentScheduleName: "10 years (₦747.95/Month)", PaymentScheduleValue: "10 years", MeterPhase : "SinglePhase" },
//];
 

















var Payment_mode = [
{ Name: "METER PAYMENT" },
 {Name: "UPGRADE TO 3PHASE" },
 { Name: "ARREARS PAYMENT" }
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
 { Name: "FidelityBank" } , 
 { Name: "WEMABank" } 
];
function RegisterCustomerViewModel(data) {
    var self = this;

   var ReportViewer = "http://map.nepamsonline.com/ReportPage.aspx?";
    //var ReportViewer = "localhost:14996/ReportPage.aspx?";
    self.LGAListFiltered = ko.observableArray();  
    self.StateListPushed = ko.observableArray(); 
self.LGAListPushed = ko.observableArray(); 
    ko.mapping.fromJS(data, {}, self);
    //dd(states);

    ko.mapping.fromJS(data.StateList, {}, self.StateList);
    ko.mapping.fromJS(data.LGAList, {}, self.LGAList);

    var b = ko.utils.arrayFilter(self.StateList(), function (a) {
            
        return a;
        });

        
        ko.utils.arrayForEach(b, function (d)
        {
            self.StateListPushed.push(d); 
            
        });

        var s = ko.utils.arrayFilter(self.LGAList(), function (a) {
            
        return a;
        });

        
        ko.utils.arrayForEach(s, function (d)
        {
            self.LGAListPushed.push(d); 
            
        });


    self.isProcessing = ko.observable(false);

    self.submitButton = ko.observable('<i class="fa fa-bell"></i> Apply Now');

    //seleted Option
    self.selectedTitle = ko.observable(); 
    
    
    self.SelectedMAPVendor = ko.observable();
self.SelectedMAPContractor = ko.observable();
self.SelectedMAPInstaller = ko.observable();
self.ContractorList1 = ko.observableArray();
self.InstallersList1 = ko.observableArray();




self.SelectedPaymentMode = ko.observable();
    self.selectedTypePremises = ko.observable();
    self.selectedUsePremises = ko.observable();
    self.selectedIDcard = ko.observable();
    self.Arrears = ko.observable();
    self.selectedTypeMeterRequested = ko.observable();
    self.selectedModePayment = ko.observable();
    self.selectedPaymentSchedule = ko.observable();
    self.selectedState = ko.observable(); 
    self.selectedLGA = ko.observable();
    self.selectedAccountType = ko.observable(); 
    self.Ticket = ko.observable(); 
    self.AccountNo = ko.observable();self.SelectedBankMode = ko.observable();
    self.IsMSC = ko.observable(false);
    self.IAgree = ko.observable(false); 
    self.IsVisible = ko.observable(false);
    self.BankList = ko.observableArray(Bank_mode);self.PaymentPurposeList = ko.observableArray(Payment_mode);
    self.AccountTypeList = ko.observableArray(AccountType);
    self.TitleList = ko.observableArray(titles);
    self.selectedOccupant = ko.observable(); 
    self.OccupantList = ko.observableArray(Occupant);
    self.TypePremisesList = ko.observableArray(type_of_premises);
    self.UsePremisesList = ko.observableArray(uses_of_premises);
    self.IDcardList = ko.observableArray(id_cards);
    self.TypeMeterRequestedList = ko.observableArray(types_of_meter);
    self.ModePaymentList = ko.observableArray(mode_of_payments);
   // self.PaymentScheduleList = ko.observableArray(payment_schedules);
    self.StateList = ko.observableArray("");
    self.Installments = ko.observableArray(); 
    self.Installments = ko.observableArray(); 
    self.customer.MeterNo("NOMETER"); 
    self.TellerNo = ko.observableArray();  
    
    
   
    

    self.DatePaid = ko.observableArray(); 
    self.TotalAmountPayable = ko.observableArray();
    self.RepaymentPlan = ko.observableArray(); 
    self.TicketId = ko.observableArray(); 
      
    self.SchedulePaymentEvent = function(){

        self.selectedPaymentSchedule("0_year");

        
        if(self.selectedModePayment().ModePaymentValue == "MSC")
        {   
            $("#ScheduleMAP").modal("show"); 
            self.IsMSC(true);
        }
        else if(self.selectedModePayment().ModePaymentValue == "75UPFRONT" || self.selectedModePayment().ModePaymentValue == "50UPFRONT" || self.selectedModePayment().ModePaymentValue == "25UPFRONT" || self.selectedModePayment().ModePaymentValue == "0UPFRONT")
        {
            //Filter the List and use it to get the Corresponding 


            console.log(self.MeterRepaymentList());


            if(self.selectedTypeMeterRequested() == "Select Type of Meter Requested")
            {


                swal("Select Phase","Please select the Type of Meter you're applying for to proceed. Thank you","");
                return;
            }
            //;;;;;;;;;;;;;;;;;;;;;;
            var MAPPaymentPlan = self.selectedModePayment().ModePaymentValue ;
            
            console.log("MAPPaymentPlan "+MAPPaymentPlan);

            var MAPPaymentPhase = self.selectedTypeMeterRequested().TypeMeterRequestedValue ;
             
            console.log("MAPPaymentPhase "+MAPPaymentPhase);
            showloader();
            $.ajax({
                type: "GET",
                url: '/MAPRegister/SchedulePaymentEvent',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {
                    MAPPaymentPlan: MAPPaymentPlan,
                    MAPPaymentPhase: MAPPaymentPhase
                },
                success: function (result) {
                    var data = JSON.parse(result);
                    self.MeterRepaymentList([]);
                    ko.mapping.fromJS(data.MeterRepaymentList, {}, self.MeterRepaymentList); 
                    $("#ScheduleMAPUpfront").modal("show"); 
                    self.IsMSC(false); 
                    hideloader();
                },
                error: function (err) {
                
                    alert("Fail Details Request " + err.responseText)
                    // self.isProcessing(false);
                }
            });
             
        }
        else  
        {
            self.IsMSC(false);
        }
    }
     
    self.BillRevision = function () {
        UpdateWithApplicationStatus("GOBRC");
        BRC();
    }



    self.LoadLGAFromState = function ()
    {

        console.log("Reaching here from Ebuka");

        self.LGAListFiltered([]);

        var StateCode = self.selectedState().STATE_CODE();
        
        console.log("StateCode "+ StateCode);

        console.log("Checking the LGA List Length "+self.LGAList().length);

        var b = ko.utils.arrayFilter(self.LGAListPushed(), function (a) {

            return a;
             
        });

         
        ko.utils.arrayForEach(b, function (d)
        {
           // console.log("Checking if the State Code will reach "+d.STATE_CODE());

            if(d.STATE_CODE() == StateCode)
            {  
                console.log("Checking StateCode"+ d.STATE_CODE());
                
                self.LGAListFiltered.push(d); 
            }
        }); 
         
    }


    self.ArrearsForm = function ()
    {
        UpdateWithApplicationStatus("INSTALLMENTAL");

        Installmental();
    }

    self.SubmitInstallmental = function () {
        
        var InstalmentalValue  = self.Installments();
    
       
        var TicketId =  $("#TicketId45").val();
        
        //document.getElementById("").innerText;
  
        console.log("Instalmental Value "+InstalmentalValue);
        console.log("Ticket ID "+TicketId); console.log("Ticket ID 222222222 "+ TicketID);

      


        $.ajax({
            type: "GET",
            url: '/MAPRegister/UpdatePercentage',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                Percentage: InstalmentalValue,
                TicketId: TicketId
            },
            success: function (result) {
                var data = JSON.parse(result);
               // ko.mapping.fromJS(data.customer, {}, self.customer);
               // self.isProcessing(false);
                //self.Sucessful(data); 
                swal("Saved","Your account has been Updated with the Selected Monthly Percentage","success"); 
            },
            error: function (err) {
                
                alert("Fail Details Request " + err.responseText)
               // self.isProcessing(false);
            }
        }); 
        Apply(); 
    }

    self.BRCForm = function () {
        var ReportViewer = "http://map.nepamsonline.com/ReportPage.aspx?";
        var TicketId =  $("#TicketId45").val();
        var url =  ReportViewer+"TicketId="+TicketId+"&TYPE=BRCFORM";
        window.open(url, '_blank');
    }

 function PayOffArrears() {
         
    // 
     $("#PayArrearsBank").modal("show");

 };

 
     self.PayOffArrearsBank = function () {
           
          var ReportViewer = "http://map.nepamsonline.com/ReportPage.aspx?";

          var TicketID = $("#MAPTicketId1").val(); 
          //Gor the aspx File for the Download of the File fo rthe Application 
          //var url =  ReportViewer+"TicketId="+TicketID+"&TYPE=APPLICATIONFORM";
          var url = ReportViewer + "TicketId=" + TicketID + "&TYPE=PAYBANKARREARS";
          window.open(url, '_blank'); 
 };


 self.PayOffArrears1 = function () {
    
         
        UpdateWithApplicationStatus("PAYARREARS");
        swal({
            title: "Loading...",
            text: "Please wait",
            imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',

            showConfirmButton: false,
            allowOutsideClick: false
        });

        document.forms['XpressPay'].submit();
    };

    self.ProceedAfterVerification = function () {
         
        if(parseFloat(self.Arrears()) > 0)
        {
            //the Guy  has Arrears. Please show him the Arrears Page
            //Installmental()
            //BRC()
            //Welcome()
            //Verify()

            UpdateWithApplicationStatus("HASARREARS");
            Arrears();
        }
        else
        {
            UpdateWithApplicationStatus("ABOUTTOAPPLY");
            Apply();
        }
  
    }

    self.DownloadRegistrationForm = function () {
        
        var TicketID = $("#MAPTicketId").val();


        //Gor the aspx File for the Download of the File fo rthe Application 
      
        //var url =  ReportViewer+"TicketId="+TicketID+"&TYPE=APPLICATIONFORM";
        var url =  ReportViewer+"TicketId="+TicketID+"&TYPE=APPLICATIONFORM";
        window.open(url, '_blank'); 
    }


    self.VerifyMAPData2 = function () 
    {

        var AccountNo = $("#AccountNo").val();
        var AmountPaid = '1';
        var PhoneNumber = "";
        var EmailAddress = "";
        var AccountType = ""; 
        var MAPApplicantName = "";
         
        if (AccountNo == "" || AccountNo == '' || AccountNo == 'undefined') {
            swal("Account No", "Please enter a value for your account Number", "info");
            return;
        }

         
        swal({
            title: "Verifying...",
            text: "Please wait while we verify your account details",
            imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif', 
            showConfirmButton: false,
            allowOutsideClick: false});
        $.ajax({
            type: "GET",
            url: '/MAPRegister/w',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                AccountNo: AccountNo, 
                PhoneNumber: "00000000000",
                EmailAddress: "a@a.a",
                AccountType: "PREPAID",
                AmountPaid: "1", 
                MAPApplicantName: "N/A"
            },
            success: function (result) { 

                if (result.result == "Customer Not Found") {
                    swal("Error", "Customer Not Found", "error"); 
                    return; 
                }

                var x = JSON.parse(result.result);
   
                  
 
                $('#amount').val(result.amount);
                $('#customer-email').val(result.customer_email);
                $('#hash').val(result.HashCode); 
                $('#product-id').val(result.ProductId);
                $('#trans-id').val(result.trans_id2);
                 
                //$('#trans-id2').val(result.trans_id2); 
                $('#product-desc').val(result.ProductDescription);
                $('#public-key').val(result.PublicKey); 


                //$('#amount').val(result.amount);
                //$('#customer-email').val(result.customer_email);
                //$('#hash').val(result.HashCode);
                //$('#product-id').val(result.ProductId);
                //$('#trans-id').val(result.trans_id);
                //$('#product-desc').val(result.ProductDescription);
                //$('#public-key').val(result.PublicKey);
                //$('#CustNames').html(result.MAPplicant);



                $("#AccountNo2").val(x[0].CUSTOMER_NO);
                $("#AccountNo3").html(x[0].CUSTOMER_NO);
                $("#MeterNo").val(x[0].METER_NO);
                $("#AccountName2").val(x[0].CONS_NAME);
                $("#AccountName3").html(x[0].CONS_NAME);  
                $("#HouseAddress2").val(x[0].ADDRESS);
                $("#BusinessServiceCenter").val(x[0].BSC_NAME);
                $("#IntegratedBusinessCenter").val(x[0].IBC_NAME); 
                $("#TotalBill").val(x[0].ARREAR);
                $("#AccountType2").val(x[0].CONS_TYPE);
                $("#BRC_ID").html(result.BRC_ID);
                $("#TicketID").html(result.trans_id);
                $("#BRC_ID2").html(result.BRC_ID);
                $("#TicketID2").html(result.trans_id);
                $("#Transformer").val(result.DTR); 
                
                
                $("#AccountNo32").html(x[0].CUSTOMER_NO);
                $("#AccountName32").html(x[0].CONS_NAME);
                $("#Arrears").val(x[0].ARREAR);
                $("#BRC_ID233").html(result.BRC_ID);
                $("#TicketID233").html(result.trans_id);
                $("#AccountNo323").html(x[0].CUSTOMER_NO);
                $("#AccountName323").html(x[0].CONS_NAME);
                $("#Arrears").html(x[0].ARREAR);
                
                $("#Arrears7").html(x[0].ARREAR);  
                $("#Arrears99").html(x[0].ARREAR);
                $("#TicketId4").val(result.trans_id);
                $("#TicketId45").val(result.trans_id); 
                $('#CustNames').html(result.CustomerName); 
                $('#MAPplicant').val(result.MAPplicant); 



                $("#BRC_ID44").html(result.BRC_ID);
                $('#CustNames').html(result.CustomerName); 
                $("#TicketID44").val(result.trans_id);
                $("#AccountName344").html(x[0].CONS_NAME);
                $("#AccountNo344").html(x[0].CUSTOMER_NO);



                ///$('#MapAddress2').val(x[0].ADDRESS); 
                //$('#MapAddress1').val(x[0].ADDRESS); 
                //New Fields
                //$('#MapName').val(result.MAPplicant); 
                //$('#MapPhone').val(result.CustomerPhoneNumber);  
                //$('#MapEmail').val(result.CustomerEmail); 

                //  self.customer.Phone(result.CustomerPhoneNumber);
                //$('#MapMeterNo').val(result.AlternateCustReference); 
                //$('#MapAccountNo').val(x[0].CUSTOMER_NO); 
                //$('#MapTicketId').html(result.trans_id); 



               console.log(result); 
               //bind Customer Details
               self.customer.CustomerSurname(result.MAPplicant);

               console.log("Reaching Here For the Selman "+result.CustomerPhone);


               self.customer.Phone(result.CustomerPhone);
               self.customer.Email(result.CustEmail);

               self.customer.MeterNo(result.AlternateCustReference);
                self.customer.AccountNo(x[0].CUSTOMER_NO);
                self.customer.TransactionID(result.trans_id);
               // self.customer.MeterInstallationAddress(x[0].ADDRESS);
                self.customer.CustomerAddress(x[0].ADDRESS);
              
                 
                self.Arrears(x[0].ARREAR);
                //self.Ticket(x[0].trans_id);
                self.Ticket(result.trans_id);
                 
                // alert(" DEPOSTICT "+result.Status);
                 
                console.log("hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh "+result.Status);
                 
                if(result.Status == "HASARREARS")
                { 
                    Arrears();  
                }

                if(result.Status == "GOBRC")
                {

                    BRC();
                  
                }
                if(result.Status == "INSTALLMENTAL")
                { 

                    //   alert("Chukwuma Obioma");

                    Installmental();
                  
                }
                if(result.Status == "ABOUTTOAPPLY")
                {

                    Apply();
                  
                }

                if(result.Status == "PAYARREARS")
                {

                    Installmental();
                  
                }

                if(result.Status == "ARREARSPERCENT")
                {
                    Installmental();
                }



                if(result.Status == "PROCEEDTOMAPPAY")
                {

                    var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=MSC";
                    window.open(url, '_blank');
                }

                if(result.Status == "MAPAPPLIED")
                {
                     
                    $("#ApplicantsName").html(result.MAPplicant);
                    $("#MAPTicketId").html(result.trans_id); 
                    MessageBoard();
                     
                }

                if(result.Status == "APPROVED FOR PAYMENT")
                { 
                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    $("#MapPayment").delay(1500).slideDown();  
                }

                if(result.Status == "PAID FOR METER")
                {
                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=PAID FOR METER";
                    window.open(url, '_blank');


                }

                if(result.Status == "INSTALLATION DONE")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=INSTALLATION DONE";
                    window.open(url, '_blank');  
                  
                }
                if(result.Status == "APPROVED FOR INSTALLATION")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=APPROVED FOR INSTALLATION";
                    window.open(url, '_blank');  
                }


                if(result.Status == "MAPPAID")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=MAPPAID";
                    window.open(url, '_blank');  
                }

                if(result.Status == "GOBRC")
                {
                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").delay(1500).slideDown(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp();  
                }

                if(result.Status == "NEW")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").delay(1500).slideDown(); 
                    $("#ArrearsDiv").slideUp(); 
                    return ;
                }

                if(result.Status == "VERIFY")
                { 
                    Verify(); 
                    //  swal("TicketID", "An Email has been sent to your mail box. Kindly use the Ticket Number.", "info");
                }

                //$("#PhoneNumber2").html(x[0].STATUS);
                //$("#EmailAddress2").html(x[0].STATUS);
                //$("#Arrears").html("₦ " + x[0].ARREAR);
                //$("#AccountType2").html(x[0].CONS_TYPE); 
                //$("#Tariff").html(x[0].TARIFFCODE);
                //$("#TotalBill").html("₦ " + x[0].TOTAL_BILL);
                //$("#AmountPaid").html("₦ " + result.amount);
                //$("#EmailAccount1").html(result.customer_email);
                //$("#Min").html("₦ " + x[0].CURRENT_AMOUNT);
                //$("#TransID").html(result.trans_id); 
                //console.log("sssssssssssssss  " + result.CustomerName);
                /// $('#f').val(result.hashString);
                //if (x[0].CONS_TYPE == "POSTPAID") {
                //    if (parseFloat(result.amount) <= parseFloat(x[0].CURRENT_AMOUNT)) { 
                //        $("#Message").html('Note: You are paying less than the minimum '); 
                //    }
                //    else {
                //        $("#Message").html('Note: You can proceed to pay'); 
                //    }
                //}
                //if (x[0].CONS_TYPE == "PREPAID") {
                //    if (parseFloat(result.amount) <= parseFloat(x[0].CURRENT_AMOUNT)) {
                //        $("#Message").html('Note: Payment will be used to Offset Debt');
                //    }
                //    else 
                //{
                //    $("#Message").html('Note: You can proceed to pay');
                //    }
                //} 
                //document.getElementById('tab-2').classList.add('active');
                //document.getElementById('tab-1').classList.remove('active'); 
                //document.getElementById('tab2').classList.add('active');
                //document.getElementById('tab1').classList.remove('active');
                  
                swal.close();  
               
            },
            error: function (err) {
                console.log("errrrrrrrrrrrrrrrrrrrrrrp " + err.responseText);
                if (err.responseText == "Customer Not Found" || err.responseText == "Consumer Not Found") { 
                    swal("Error", err.responseText, "error");
                    return;
                }
                else
                    if (err.responseText != null || err.responseText != "") {

                        swal("Oops!", "An error occured, we're trying to fix it. Please try again. Thank you.", "error");


                        console.log(  err.responseText ); 
                        
                        
                        return;
                    }

                    else
                        if (err.responseText != null || err.responseText != "") {
                            
                            swal("Oops!", "An error occured, we're trying to fix it. Please try again. Thank you.", "error");

                            console.log("There was an error verifying your account details, Please ensure your Account/Meter Number is valid and your network connection is active and try again.");
                            return;
                        }
            }
        }); 
    }
    self.VerifyMAPData = function () 
    {

        var AccountNo = $("#AccountNo").val();
        var AmountPaid = '1';
        var PhoneNumber = $("#PhoneNumber").val();
        var EmailAddress = $("#EmailAddress2").val();
        var AccountType = $("#AccountType").val(); 
        var MAPApplicantName = $("#MAPApplicantName").val();

         
        if (AccountType === 'undefined' || AccountType === '' || AccountType === "undefined" || AccountType == undefined || AccountType === undefined) 
        {
            swal("Account Type", "Please select an account type", "info");
            return;
        }

        if (AccountNo == "" || AccountNo == '' || AccountNo == 'undefined') {
            swal("Account No", "Please enter a value for your account Number", "info");
            return;
        }

        if (MAPApplicantName == "" || MAPApplicantName == '' || MAPApplicantName == 'undefined') {
            swal("Your Name please", "Please enter a your Name in Full including titles", "info");
            return;
        }

        if (PhoneNumber === 'undefined' || PhoneNumber === '' || PhoneNumber === "undefined" || PhoneNumber == undefined || PhoneNumber === undefined) {
             
            swal("Phone Number", "Please input a valid  Phone number", "info");

            return;
        }

        var value = $.trim(PhoneNumber).replace(/\D/g, '');

        if (value.length != 11) { 
            swal("Phone Number", "Please input your Phone number", "info"); 
            return;
        }

        //if (EmailAddress === 'undefined' || EmailAddress === '' || EmailAddress === "undefined" || EmailAddress == undefined || EmailAddress === undefined) {
                 
        //    swal("Email Address", "Please input a valid  Email Address", "info"); 
        //    return;
        //}

        //var filter = /^([\w-]+(?:\.[\w-]+)*)@@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
        //if (!filter.test(EmailAddress)) {
        //    swal("Email Address", "Please input a valid email address to proceed", "info");

        //    return;
        //}
        //if (AmountPaid == "" || AmountPaid == '' || AmountPaid == 'undefined') {


        //    swal("Amount", "Please enter a value for the amount you're buying", "info");

        //    return;
        //}

        //$('#cover').fadeOut(1000);
        swal({
            title: "Verifying...",
            text: "Please wait while we verify your account details",
            imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif', 
            showConfirmButton: false,
            allowOutsideClick: false});
        $.ajax({
            type: "GET",
            url: '/MAPRegister/v',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                AccountNo: AccountNo, 
                PhoneNumber: PhoneNumber,
                EmailAddress: EmailAddress,
                AccountType: AccountType,
                AmountPaid: AmountPaid, 
                MAPApplicantName: MAPApplicantName
            },
            success: function (result) { 

                if (result.result == "Customer Not Found") {
                    swal("Error", "Customer Not Found", "error"); 
                    return; 
                }

                var x = JSON.parse(result.result);
   
                  
 
                $('#amount').val(result.amount);
                $('#customer-email').val(result.customer_email);
                $('#hash').val(result.HashCode); 
                $('#product-id').val(result.ProductId);
                $('#trans-id').val(result.trans_id2);
                $('#MAPTicketId1').val(result.trans_id);
                //$('#trans-id2').val(result.trans_id2); 
                $('#product-desc').val(result.ProductDescription);
                $('#public-key').val(result.PublicKey); 
                 

                $("#AccountNo2").val(x[0].CUSTOMER_NO);
                $("#AccountNo3").html(x[0].CUSTOMER_NO);
                $("#MeterNo").val(x[0].METER_NO);
                $("#AccountName2").val(x[0].CONS_NAME);
                $("#AccountName3").html(x[0].CONS_NAME);  
                $("#HouseAddress2").val(x[0].ADDRESS);
                $("#BusinessServiceCenter").val(result.Feeder);
                $("#IntegratedBusinessCenter").val(result.Zone);  
                 
                console.log("Sacrifice thou the men "+result.DTR);

                $("#Transformer").val(result.DTR); 
                 
                $("#TotalBill").val(x[0].ARREAR);
                $("#AccountType2").val(x[0].CONS_TYPE);
                $("#BRC_ID").html(result.BRC_ID);
                $("#TicketID").html(result.trans_id);
                $("#BRC_ID2").html(result.BRC_ID);
                $("#TicketID2").html(result.trans_id);
                 
                $("#AccountNo32").html(x[0].CUSTOMER_NO);
                $("#AccountName32").html(x[0].CONS_NAME);
                $("#Arrears").val(x[0].ARREAR);
                $("#BRC_ID233").html(result.BRC_ID);
                $("#TicketID233").html(result.trans_id);
                $("#AccountNo323").html(x[0].CUSTOMER_NO);
                $("#AccountName323").html(x[0].CONS_NAME);
                $("#Arrears").html(x[0].ARREAR);
                
                $("#Arrears7").html(x[0].ARREAR);  
                $("#Arrears99").html(x[0].ARREAR);
                $("#TicketId4").val(result.trans_id);
                $("#TicketId45").val(result.trans_id); 
                $('#CustNames').html(result.CustomerName); 
                $('#MAPplicant').val(result.MAPplicant); 


                $("#BRC_ID44").html(result.BRC_ID);
                $('#CustNames').html(result.CustomerName); 
                $("#TicketID44").val(result.trans_id);
                $("#AccountName344").html(x[0].CONS_NAME);
                $("#AccountNo344").html(x[0].CUSTOMER_NO);




                $("#BRC_IDzz").html(result.BRC_ID);
                $("#TicketIDzz").html(result.trans_id);
                 $("#AccountName3zz").html(x[0].CONS_NAME);
                 $("#AccountNo3zz").html(x[0].CUSTOMER_NO);
            
                 $("#Arrearsxxxxxz").html(x[0].ARREAR);
               console.log(result); 
               //bind Customer Details
               self.customer.CustomerSurname(result.MAPplicant);

               console.log("Reaching Here For the Selman "+ result.CustomerPhone);


               self.customer.Phone(result.CustomerPhone);
               self.customer.Email(result.CustEmail);

               self.customer.MeterNo(result.AlternateCustReference);
                self.customer.AccountNo(x[0].CUSTOMER_NO);
                self.customer.TransactionID(result.trans_id);
               // self.customer.MeterInstallationAddress(x[0].ADDRESS);
                self.customer.CustomerAddress(x[0].ADDRESS);
              
                 
                self.Arrears(x[0].ARREAR);
                //self.Ticket(x[0].trans_id);
                self.Ticket(result.trans_id);
                 
                if(result.Status == "HASARREARS")
                { 
                    Arrears();  
                }
                if(result.Status == "GOBRC")
                { 
                    BRC(); 
                }
                if(result.Status == "INSTALLMENTAL")
                {  
                    Installmental(); 
                }
                if(result.Status == "ABOUTTOAPPLY")
                {

                    Apply();
                  
                }

                if(result.Status == "PAYARREARS")
                {

                    Installmental();
                  
                }

                if(result.Status == "ARREARSPERCENT")
                {
                    Installmental();
                }



                if(result.Status == "PROCEEDTOMAPPAY")
                {

                    var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=MSC";
                    window.open(url, '_blank');
              
                }

                if(result.Status == "MAPAPPLIED")
                {
                     
                    $("#ApplicantsName").html(result.MAPplicant);
                    $("#MAPTicketId").html(result.trans_id); 
                    MessageBoard();
                     
                }

                if(result.Status == "APPROVED FOR PAYMENT")
                { 
                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    $("#MapPayment").delay(1500).slideDown();  
                }

                if(result.Status == "PAID FOR METER")
                {
                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=PAID FOR METER";
                    window.open(url, '_blank');


                }

                if(result.Status == "INSTALLATION DONE")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=INSTALLATION DONE";
                    window.open(url, '_blank');  
                  
                }
                if(result.Status == "APPROVED FOR INSTALLATION")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=APPROVED FOR INSTALLATION";
                    window.open(url, '_blank');  
                }


                if(result.Status == "MAPPAID")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp(); 
                    var ReportViewer = "http://map.nepamsonline.com/MAPInformation.aspx?";
                    var TicketId =  $("#TicketId45").val();
                    var url =  ReportViewer+"TicketId="+TicketId+"&Type=MAPPAID";
                    window.open(url, '_blank');  
                }

                if(result.Status == "GOBRC")
                {
                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").delay(1500).slideDown(); 
                    $("#Installment1").slideUp();
                    $("#Verify").slideUp(); 
                    $("#ArrearsDiv").slideUp();  
                    
                }


                if(result.Status == "NEW")
                {

                    $("#1").slideUp();   
                    $("#MapApplication").slideUp(); 
                    $("#WelcomeDiv").slideUp();
                    $("#BRC").slideUp(); 
                    $("#Installment1").slideUp();
                    $("#Verify").delay(1500).slideDown(); 
                    $("#ArrearsDiv").slideUp(); 
                    return ;
                }

                if(result.Status == "VERIFY")
                { 
                    Verify(); 
                    //  swal("TicketID", "An Email has been sent to your mail box. Kindly use the Ticket Number.", "info");
                }

                //$("#PhoneNumber2").html(x[0].STATUS);
                //$("#EmailAddress2").html(x[0].STATUS);
                //$("#Arrears").html("₦ " + x[0].ARREAR);
                //$("#AccountType2").html(x[0].CONS_TYPE); 
                //$("#Tariff").html(x[0].TARIFFCODE);
                //$("#TotalBill").html("₦ " + x[0].TOTAL_BILL);
                //$("#AmountPaid").html("₦ " + result.amount);
                //$("#EmailAccount1").html(result.customer_email);
                //$("#Min").html("₦ " + x[0].CURRENT_AMOUNT);
                //$("#TransID").html(result.trans_id); 
                //console.log("sssssssssssssss  " + result.CustomerName);
                /// $('#f').val(result.hashString);
                //if (x[0].CONS_TYPE == "POSTPAID") {
                //    if (parseFloat(result.amount) <= parseFloat(x[0].CURRENT_AMOUNT)) { 
                //        $("#Message").html('Note: You are paying less than the minimum '); 
                //    }
                //    else {
                //        $("#Message").html('Note: You can proceed to pay'); 
                //    }
                //}
                //if (x[0].CONS_TYPE == "PREPAID") {
                //    if (parseFloat(result.amount) <= parseFloat(x[0].CURRENT_AMOUNT)) {
                //        $("#Message").html('Note: Payment will be used to Offset Debt');
                //    }
                //    else 
                //{
                //    $("#Message").html('Note: You can proceed to pay');
                //    }
                //} 
                //document.getElementById('tab-2').classList.add('active');
                //document.getElementById('tab-1').classList.remove('active'); 
                //document.getElementById('tab2').classList.add('active');
                //document.getElementById('tab1').classList.remove('active');
                  
                swal.close();  
               
            },
            error: function (err) {
                console.log("errrrrrrrrrrrrrrrrrrrrrrp " + err.responseText);
                if (err.responseText == "Customer Not Found" || err.responseText == "Consumer Not Found") { 
                    swal("Oops!", "An error occured, we're trying to fix it. Please try again. Thank you.", "error");

                    console.log("Error"+ err.responseText );
                    return;
                }
                else
                    if (err.responseText != null || err.responseText != "") {
                        swal("Oops!", "An error occured, we're trying to fix it. Please try again. Thank you.", "error");

                        console.log("Error"+ err.responseText); return;
                    }

                    else
                        if (err.responseText != null || err.responseText != "") {
                            swal("Oops!", "An error occured, we're trying to fix it. Please try again. Thank you.", "error");

                            console.log("Error"+"There was an error verifying your account details, Please ensure your Account/Meter Number is valid and your network connection is active and try again.");
                            return;
                        }
            }
        }); 
    }


       
         


    self.save = function () {
        //alert(self.IAgree());
        //if(self.IAgree() == true){
        self.customer.CustomerTitle(self.selectedTitle()?self.selectedTitle().TitleName:'');
        self.customer.MeterType(self.selectedTypeMeterRequested()?self.selectedTypeMeterRequested().TypeMeterRequestedName:'');
        self.customer.TypePremises(self.selectedTypePremises()?self.selectedTypePremises().TypePremisesValue:'');
        self.customer.PremiseUse(self.selectedUsePremises()?self.selectedUsePremises().UsePremisesValue:'');
        self.customer.IDcard(self.selectedIDcard()?self.selectedIDcard().IDcardValue:'');
        self.customer.ModeOfPayment(self.selectedModePayment()?self.selectedModePayment().ModePaymentValue:'');
         
                                                   


        self.customer.LGA(self.selectedLGA().LGA_NAME()); 
        self.customer.State(self.selectedState().STATE_NAME());

        self.customer.Occupant(self.selectedOccupant().TitleName);
        //cccccccccc
        self.customer.MAPAmount("N/A");
        if(self.selectedModePayment().ModePaymentValue == "MSC")
        {   
            self.customer.PaymentPlan(self.selectedPaymentSchedule()?self.selectedPaymentSchedule().PaymentScheduleValue:'');  
        }

        else if(self.selectedModePayment().ModePaymentValue == "50UPFRONT")
        {   
            self.customer.PaymentPlan("50UPFRONT");  
        }
        else
        {
            self.customer.PaymentPlan("UPFRONT");  
        }

        
        //PaymentPlan
        self.customer.RegistrationDate(getDates());
        self.customer.MAPAmount("1");
        self.customer.MeterAmount("2");
        let Noerror = self.Validate(self.customer);
        console.log("Submission Status:"+ Noerror);
        
         
        console.log("Submission Status:    "+ self.selectedPaymentSchedule().PaymentScheduleValue());
        
        console.log("Submission 2:  "+ self.selectedPaymentSchedule().PaymentScheduleName());
         
        console.log("Submission 3:  "+ self.selectedPaymentSchedule().TOTAL_AMOUNT());

         
 
        if(Noerror == true)
        {
                self.InsertCustomer(self.customer); 
        }
        else
        {
            swal('Can not Proceed',Noerror+' cannot be Blank','info');
        }
        //TODO: add other Select Value
        //self.customer.CustomerTitle(self.selectedTitle().TitleName); 
    }

     
    self.UploadAgency = function (event) 
    {
         
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
        ////////////////////////////////////////////////////


        var s =  self.SelectedPaymentMode().Name;

        data.append("PurposeOfPayment", self.SelectedPaymentMode().Name);
       
        ////////////////////////////////////////////////////

        $.ajax({
            type: "post",
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
                
                
                $("#yourfileID").val('');


                //Clear Fields
                self.TellerNo("");  
                self.TicketId(""); 
                self.TotalAmountPayable("");
                self.SelectedBankMode("");

                self.DatePaid("");




                //return to the Home Page
                //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx



  





                swal({
                    title: "Your Teller was Uploaded.",
                    text: "Do you want to redirect to the MAP HomePage?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#EF5350",
                    confirmButtonText: "Yes, Go Ahead!",
                    cancelButtonText: "No, Cancel!",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
   function (isConfirm) {
       if (isConfirm) {
 
                          
           var url =  "http://map.nepamsonline.com";
           window.open(url, '_parent');

       }
       else {
           swal({
               title: "Uploaded",
               text: "The Payment Teller was uploaded Successfully. ",
               confirmButtonColor: "#2196F3",
               type: "success"
           });
       }
   })


                 
              //  swal('Record Saved!', 'Your entry has been successfully saved', 'success'); 
                 

            },
            error: function (err) { $('#pageloader-overlay').fadeOut(100);
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    };







    self.InsertCustomer = function(arg){
        var CustomerDetails = ko.mapping.toJS(arg);
        self.submitButton("Loading...");
        var TicketId =  $("#TicketId45").val();
        
        showloader();
        $.ajax({
            type: "POST",
            url: '/MAPRegister/ApplyForMeter',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                CustomerDetails: JSON.stringify(CustomerDetails),
                TicketId : TicketId,
                PaymentScheduleValue : self.selectedPaymentSchedule().PaymentScheduleValue(), 
                PaymentScheduleName : self.selectedPaymentSchedule().PaymentScheduleName(),
                TOTAL_AMOUNT : self.selectedPaymentSchedule().TOTAL_AMOUNT(),
            }),
            success: function (result) {

            var data = JSON.parse(result.result);


            hideloader();
            if(result.ModeofPayment == "MSC" )
            {
                var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
                var TicketId =  $("#TicketId45").val();
                var url =  ReportViewer+"TicketId="+TicketId+"&Type=MSC";
                window.open(url, '_blank');

               
            }

            if(result.ModeofPayment == "UPFRONT" )
            {
                var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
                var TicketId =  $("#TicketId45").val();
                var url =  ReportViewer+"TicketId="+TicketId+"&Type=UPFRONT";
                window.open(url, '_blank');

            }
            if(result.ModeofPayment == "50UPFRONT" )
            {
                var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
                var TicketId =  $("#TicketId45").val();
                var url =  ReportViewer+"TicketId="+TicketId+"&Type=50UPFRONT";
                window.open(url, '_blank');

            }
            if(result.ModeofPayment == "100UPFRONT" )
            {
                var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
                var TicketId =  $("#TicketId45").val();
                var url =  ReportViewer+"TicketId="+TicketId+"&Type=100UPFRONT";
                window.open(url, '_blank');

            }

  if(result.ModeofPayment == "75UPFRONT" )
  {
      var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
      var TicketId =  $("#TicketId45").val();
      var url =  ReportViewer+"TicketId="+TicketId+"&Type=75UPFRONT";
      window.open(url, '_blank'); 
  }

  if(result.ModeofPayment == "25UPFRONT" )
  {
      var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
      var TicketId =  $("#TicketId45").val();
      var url =  ReportViewer+"TicketId="+TicketId+"&Type=25UPFRONT";
      window.open(url, '_blank'); 
  }

  if(result.ModeofPayment == "0UPFRONT" )
  {
      var ReportViewer = "http://map.nepamsonline.com/MSCSuccessUpfront.aspx?";
      var TicketId =  $("#TicketId45").val();
      var url =  ReportViewer+"TicketId="+TicketId+"&Type=0UPFRONT";
      window.open(url, '_blank');

  }




          $("#MapApplicationSuccess").delay(1500).slideDown(); 
          $("#ApplicantsName").html(data.customer.CustomerSurname);
                $("#MAPTicketId").html(data.customer.TransactionID); 
                self.IsVisible(true);
                

                $("#AccountDetails").hide();
               // self.Sucessful(data);
                //dd(data);
            },
            error: function (err) { 
                alert("Fail Details Request " + err.responseText);
               // self.isProcessing(false);
            }
        });
    }

    self.Sucessful = function (data) {
        
        dd(data);
        //var output = "<h1>Congratulations " + data.customer.CustomerTitle + " " + data.customer.CustomerSurname + " " + data.customer.CustomerOtherName + "</h2>";
        var output = `<h1>Congratulations ${data.customer.CustomerTitle} ${data.customer.CustomerSurname} ${data.customer.CustomerOtherName}</h2>`;

        output += "<p>Your Application Has been Successfully Recieved and A Copy Has Been Sent to " + data.customer.Email + "</p>";
        output += `<p>Note: Your Ref ID is ${data.customer.CustomerId}</p>`;
        $('#meter-application').html(output);
    }

    /*****************************************************************************************
     Author:      Olaboye David Tobi
     Email:       dtobi59@gmail.com
     name:        validator
     Description: This function combines the power of the SWEETALERT.JS and KnockOut.js
                  to give user feed back in regards to info Entered by the User
     Date:        12/08/2019
     Param:       (Object)
    *******************************************************************************************/
    self.Validate = function(arg){
        dd("======================================");
        dd(Object.entries(arg));
        var validate = "";
        dd("data: "+JSON.stringify(arg));
        dd("==========new===================");
        dd(arg.ModeOfPayment());
        if(arg.ModeOfPayment() != "MSC"){
            delete arg.PaymentPlan();
        }
        var datum = Object.entries(arg);
        for(let [key, value] of datum ){
            validate = true;
           // dd("vv"+arg[key]());
        dd(key+": "+arg[key]());
        if(arg[key]() == null || arg[key]() == ''){
            //swal('Can not Proceed',key+' can not be Blank','info');
            validate = key;
            break;
        }
    }
    return validate;
   }
}

  
function Apply()
{
   // alert("ddddddddddddddddddddddddddddddddddddddddilip");


    $("#1").slideUp();   
    $("#MapApplication").delay(1500).slideDown(); 
    $("#WelcomeDiv").slideUp();
    $("#BRC").slideUp(); 
    $("#Installment1").slideUp();
    $("#Verify").slideUp(); 
    $("#ArrearsDiv").slideUp(); 
}
 


function  UpdateWithApplicationStatus(Status)
{   
    showloader();
    var TicketId =  $("#MAPTicketId1").val(); 
    $.ajax({
        type: "GET",
        url: '/MAPRegister/UpdateApplicationStatus',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { 
            TicketId: TicketId,
            Status: Status
        },

        success: function (result) {
            var data = JSON.parse(result);
            // ko.mapping.fromJS(data.customer, {}, self.customer);
           // self.isProcessing(false);
            //self.Sucessful(data); 

            console.log("Dooooooooone");
            //swal("Saved","Your account has been Updated with the Selected Monthly Percentage","success"); 
        },

        error: function (err) {
                
            //alert("Fail Details Request " + err.responseText)
           // self.isProcessing(false);
        }
    }); 

    hideloader();
}




function Arrears()
{ 

    $("#1").slideUp();   
    $("#MapApplication").slideUp(); 
    $("#WelcomeDiv").slideUp();
    $("#BRC").slideUp(); 
    $("#Installment1").slideUp();
    $("#Verify").slideUp(); 
    $("#ArrearsDiv").delay(1500).slideDown(); 
}
 
function Verify()
{   
    
    $("#Verify").delay(1500).slideDown(); 
    $("#1").slideUp();   
    $("#MapApplication").slideUp(); 
    $("#WelcomeDiv").slideUp();
    $("#BRC").slideUp(); 
    $("#Installment1").slideUp();
   
    $("#ArrearsDiv").slideUp();
}

 
function Welcome()
{ 
    $("#1").slideUp();   
    $("#MapApplication").slideUp(); 
    $("#WelcomeDiv").delay(1500).slideDown();
    $("#BRC").slideUp(); 
    $("#Installment1").slideUp();
    $("#Verify").slideUp(); 
    $("#ArrearsDiv").slideUp();
}
 
function BRC()
{

    var TicketId =  $("#TicketId45").val();
        
     
    $("#1").slideUp();   
    $("#MapApplication").slideUp(); 
    $("#WelcomeDiv").slideUp();    

    $("#Installment1").slideUp();
    $("#Verify").slideUp(); 
    $("#ArrearsDiv").slideUp(); 
    $("#BRC").delay(1500).slideDown(); 
}

function Installmental()
{
    
        
     
    $("#1").slideUp();   
    $("#MapApplication").slideUp(); 
    $("#WelcomeDiv").slideUp();    
    $("#Verify").slideUp(); 
    $("#ArrearsDiv").slideUp(); 
    $("#BRC").delay(1500).slideUp();  
    $("#Installment1").slideDown();
}

function MessageBoard()
{
    $("#1").slideUp();   
    $("#MapApplication").slideUp(); 
    $("#WelcomeDiv").slideUp();
    $("#BRC").slideUp(); 
    $("#Installment1").slideUp();  
    $("#Verify").slideUp(); 
    $("#ArrearsDiv").slideUp();
    $("#MapApplicationSuccess").delay(1500).slideDown();
}

function PayForMap()
{    
    $("#1").slideUp();   
    $("#MapApplication").slideUp(); 
    $("#WelcomeDiv").slideUp();
    $("#BRC").slideUp(); 
    $("#Installment1").slideUp(); 

    $("#MapApplicationSuccess").slideUp();
    $("#Verify").slideUp(); 
    $("#ArrearsDiv").slideUp();
 $("#MapApplicationPayment").slideDown();
}
function getDates() {
 
    var today = new Date();
    var date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();

    return date;
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