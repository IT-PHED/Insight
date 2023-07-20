//import state with lga data

//debuging
var debug = true;
function dd(arg) {
    if (debug == true) {
        console.log(arg);
    }
}

var x = [];
var y = [];
var Month = [];
var AmountBilled = [];
var AmountPaid = [];
var Consumption = [];
 
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
var mode_of_payments = [
    { ModePaymentName: "UPFRONT", ModePaymentValue: "UPFRONT" },
{ ModePaymentName: "METER SERVICE CHARGE", ModePaymentValue: "MSC" },
];

var payment_schedules = [
    { PaymentScheduleName: "1 year (₦209/Month)",  PaymentScheduleValue: "1 years" },
    { PaymentScheduleName: "2 years (₦309/Month)", PaymentScheduleValue: "2 years" },
    { PaymentScheduleName: "3 years (₦409/Month)", PaymentScheduleValue: "3 years" },
    { PaymentScheduleName: "4 years (₦509/Month)",  PaymentScheduleValue: "4 years" },
    { PaymentScheduleName: "5 years (₦609/Month)", PaymentScheduleValue: "5 years" },
    { PaymentScheduleName: "6 years (₦709/Month)", PaymentScheduleValue: "6 years" },
    { PaymentScheduleName: "7 years (₦209/Month)",  PaymentScheduleValue: "7 years" },
    { PaymentScheduleName: "8 years (₦298/Month)", PaymentScheduleValue: "8 years" },
    { PaymentScheduleName: "9 years (₦303/Month)", PaymentScheduleValue: "9 years" },  
    { PaymentScheduleName: "10 years (₦893/Month)", PaymentScheduleValue: "10 years" }, 
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





function AMRReport(data) {
    var self = this;

    //console.log("Emmanuel Ossai " + JSON.stringify(data.MDCustomerDataLists));

    var ReportViewer = "http://map.nepamsonline.com/ReportPage.aspx?";
    //var ReportViewer = "localhost:14996/ReportPage.aspx?";
    self.LGAListFiltered = ko.observableArray();
    self.StateListPushed = ko.observableArray();
    self.LGAListPushed = ko.observableArray();
    self.Percentage = ko.observable();
    self.TotalToday = ko.observable();
    self.TotalAmountPaid = ko.observable();
    self.ArrearsUnpaid = ko.observable(); self.TotalMonth = ko.observable();
    self.PaymentList = ko.observableArray();
    self.ApprovalStatus = ko.observableArray();
    
    self.SelectedAccountNo = ko.observableArray();
    ko.mapping.fromJS(data, {}, this);

    // self.ApprovalList = ko.observableArray(approvals);
    self.isProcessing = ko.observable(true);
    self.ApprovePaymentComment = ko.observable(null);
    self.message = ko.observable("Choose From Date and Start Date to contiune");
    self.fromDate = ko.observable(null);
    self.toDate = ko.observable(null);
    self.ParentAccountNo = ko.observable();
    self.SelectedMeterNo = ko.observable();
    self.SelectedYear = ko.observable();
    self.SelectedMonth = ko.observable();
    self.AccountType = ko.observable();
    self.ToDate = ko.observable();
    self.FromDate = ko.observable();
    self.SelectedComplaint = ko.observable();
    self.SelectedLinkedAccount = ko.observable();
    self.SelectedLinkedAccount1 = ko.observable();
    self.TicketDescription = ko.observable();
    self.SelectedCategory = ko.observable();
    self.SelectedSubCategory = ko.observable();
    self.TicketStatus = ko.observable();
    self.AccountNo = ko.observable();
    self.LinkedAccountDesc = ko.observable(); 
    self.ComplaintTitle = ko.observable();

    self.AverageConsumptionP = ko.observable();
    self.SelectedSubCategory = ko.observable();
    self.DateSpan = ko.observable();
    self.ComplaintSubCategoryList1 = ko.observableArray();

    ko.mapping.fromJS(data, {}, self);
     
    self.filterSubCategoryFromCategory = function (form) {



        var b = ko.utils.arrayFilter(self.ComplaintSubCategoryList(), function (a) {

            console.log("Complaint Category" + a.ComplaintCategoryID());


            if (self.SelectedCategory() != null) {

                if (a.ComplaintCategoryID() == self.SelectedCategory().ComplaintCategoryID()) {

                    return a;

                }
            } 
        });
        self.ComplaintSubCategoryList1([]);
        ko.utils.arrayForEach(b, function (d) {
            self.ComplaintSubCategoryList1.push(d);
        });
    }


    self.SubmitTicket = function (form) {

        self.isProcessing(true);
         

        var ComplaintCategory = self.SelectedCategory().ComplaintCategoryName();
        var ComplaintSubCategory = self.SelectedSubCategory().ComplaintSubCategoryName();
        var ComplaintAccount = self.SelectedLinkedAccount1().AccountNumber(); 
        var ComplaintStatus = self.TicketStatus(); 
        var ComplaintTitle = self.ComplaintTitle(); 
        var ParentAccountId = $("#StaffId").val();

        $.ajax({
            type: "POST",
            url: '/MDPortal/SubmitTicket',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'ComplaintCategory': ComplaintCategory,
                'ComplaintSubCategory': ComplaintSubCategory,
                'ComplaintAccount': ComplaintAccount,
                'ComplaintStatus': ComplaintStatus,
                'ComplaintTitle': ComplaintTitle,
                'ComplaintDescription': self.TicketDescription(), 
                'LoginCompanyId': ParentAccountId,
                'LoadType': "LinkLoad"
            }),
            success: function (result) {

              //  hideloader();
                //close the Modal POPOUP

                //$('#modal').modal('toggle');
                $('#ShotTicketModal').modal('toggle');

               // $('#ShotTicketModal').appendTo("body").modal('hide');
                var data = JSON.parse(result.result); 
                 
                swal('Account Added!', 'The account has been added', 'success');


                ko.mapping.fromJS(data.TicketList, {}, self.TicketList);
                self.loadUI(data);
                self.isProcessing(false); 
            },
            error: function (err) {

                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });


    }


     
    self.LoadLinkedAccountData = function (form)
    {
        self.isProcessing(true);
        ShowLoader();
        self.AccountNo();
        var AccountNo = self.SelectedLinkedAccount().AccountNumber();

        self.AccountNo(AccountNo);
        var MeterNo = self.SelectedLinkedAccount().MeterNo();
        var ParentAccountId = $("#StaffId").val();
         
        $.ajax({
            type: "GET",
            url: '/Dashboard/LoadMeterReadingValues',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                MeterNo: MeterNo,
                AccountNo: AccountNo,
                LoginCompanyId: ParentAccountId,
                LoadType: "LinkLoad"
            },
            success: function (result) {
                hideloader();

                // self.DailyReadingData([]);
                // self.MDCustomerDataLists([]);
                var data = JSON.parse(result);

                //ko.mapping.fromJS(data.DailyReadingData, {}, self.DailyReadingData);
                //ko.mapping.fromJS(data.MDCustomerDataLists, {}, self.MDCustomerDataLists); 
                //var  AllCustomerPaymentViewModel = new AMRReport(data);

                self.loadUI(data);
                hideloader();

                swal('Account Added!', 'The account has been added', 'success');
                self.isProcessing(false);
                // window.location.href = "/Account/Login"
            },
            error: function (err) {

                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    }


    self.ViewPHEDBills = function (event) {

        if (self.SelectedAccountNo() == null || self.SelectedAccountNo() == "") {

            swal('Select Account!', 'Please select an Account from the list on your left to Proceed.', 'info');
            return;
        }



        var billMonth = moment(self.SelectedYear(), 'YYYY-MM-DD').format('DD-MM-YYYY');
        var month = moment(self.SelectedYear(), 'YYYY-MM-DD').format('MMM-YYYY');

        if (self.SelectedYear() == null || self.SelectedYear() == "") {

            swal('Select Bill Year!', 'Please select a Bill Year to Proceed.', 'info');
            return;
        }
        //if (self.SelectedMonth() == null || self.SelectedMonth() == "") {

        //    swal('Select Bill Month!', 'Please select a Bill Month to Proceed.', 'info');
        //    return;
        //}
        var consNo = self.SelectedAccountNo();
        //var consNo = $('#txt_consNo').val();//'832510284901';
        //var billMonth = $('#txt_billMonth').val();//'01-06-2018';
        //var month = billMonth;//'Jun-2018';


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



    self.ShowTicketModal = function (form) {
        console.log("Please go Ahead and Implement");

        $('#ShotTicketModal').appendTo("body").modal('show');
         
    }
    self.AddLinkedAccount = function (form) {
        self.isProcessing(true);
        ShowLoader();

        var ParentAccountId = self.SelectedAccountNo().Id();
        var AccountNo = self.AccountNo();
        var LinkedAccountDesc = self.LinkedAccountDesc();
        var AccountType = self.AccountType();
        var AccountName = self.LinkedAccountDesc();

        var StatusType = "MDPortal";
        var IBC = self.ApplicationUser.IBC();
        var BSC = self.ApplicationUser.BSC();
        var MeterNo = self.ApplicationUser.MeterNo();

        $.ajax({
            type: "POST",
            url: '/MDPortal/AddAccount',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'MeterNo': MeterNo,
                'BSC': BSC,
                'IBC': IBC,
                'StatusType': StatusType,
                'AccountName': AccountName,
                'AccountType': AccountType,
                'LinkedAccountDesc': LinkedAccountDesc,
                'AccountNo': AccountNo,
                'ParentAccountId': ParentAccountId,

            }),
            success: function (result) {
                hideloader();
                if (result.result == "Customer Not Found") {
                    swal('No Record!', 'The Account/Meter No does not exist', 'error');
                    return;
                }
                console.log("sssssssssssssssssssssssss " + result);


                var data = JSON.parse(result.result);
                ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);

                swal('Account Added!', 'The account has been added', 'success');
                self.isProcessing(false);
                // window.location.href = "/Account/Login"
            },
            error: function (err) {

                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    }

    self.VerifyAccount = function (form) {
        self.isProcessing(true);
         
        ShowLoader();
       
        $.ajax({
            type: "POST",
            url: '/Account/VerifyAccount',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ 
                'AccountNo': self.AccountNo(),
                'AccountType': self.AccountType()
            }),
            success: function (result) {


                if (result.result == "Customer Not Found") {

                    swal('No Record!', 'The Account/Meter No does not exist', 'error');
                    return;
                }

                console.log("sssssssssssssssssssssssss " + result);
                  
                var data = JSON.parse(result.result);

                swal('Record Seen!', 'The user data was Found', 'success');
                ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);

                hideloader();

                self.isProcessing(false);
                // window.location.href = "/Account/Login"
            },
            error: function (err) {

                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
    };

    self.ShowAccountFromSelectedParent = function () {


        var a = self.SelectedAccountNo().AccountNo();
        self.ParentAccountNo(a);

    }

    x = [];
    y = [];

    self.loadUI = function (data) {

        if (data.APIStatus == "LINKLOAD") {

            var AccountNo = self.SelectedLinkedAccount().AccountNumber();
            self.SelectedAccountNo(AccountNo);
            var MeterNo = self.SelectedLinkedAccount().MeterNo();

            console.log("Leeeeeeeeeeeeeeekwe " + data.MDCustomerDataLists.totalOutstanding);

            self.SelectedMeterNo(MeterNo);
            self.ArrearsUnpaid(numeral(data.MDCustomerDataLists.totalOutstanding).format('₦0.00a'));

            var TotalAmountPaid = 0;

            
            var AverageConsumptions = 0;
            var v = ko.utils.arrayFilter(data.MDCustomerDataLists.monthlyBillData, function (a) {

                Month.push(a.month.substring(0, 3) + " " + a.year);
                Consumption.push(a.consumption);
                AverageConsumptions = parseFloat(AverageConsumptions) + parseFloat(a.consumption);

                if (a.amountBilled == "") {
                    AmountBilled.push("0");
                }
                else {
                    AmountBilled.push(a.amountBilled);
                }


                console.log("NDAM " + a.amountPaid);

                if (a.amountPaid == "" || a.amountPaid == '' || a.amountPaid == "undefined") {

                    //alert("Okokokokokokokokoko ");
                    AmountPaid.push(parseFloat("0"));
                }
                else {
                    console.log("Kabosh " + a.amountPaid);
                    TotalAmountPaid = parseFloat(TotalAmountPaid) + parseFloat(a.amountPaid);
                    AmountPaid.push(a.amountPaid);
                }

                return a;
            });

            self.AverageConsumptionP(numeral(AverageConsumptions).format('0.00a'));
            self.TotalAmountPaid(numeral(TotalAmountPaid).format('₦0.00a'));


            let f = Month[0];
            // storing the last item  
            let l = Month[Month.length - 1];


            var Span = l + " - " + f;

            console.log("Kalagbor " + Span);

            self.DateSpan(Span);

            BillsAndPayments(AmountBilled, AmountPaid, Month);
            AverageConsumption(Month, Consumption);

            var Light = 0;
            var NoLight = 0;
            var Total = 0; var TotalToday = 0;
            var TotalMonth = 0;
            x = [];
            y = [];

            ko.utils.arrayForEach(data.MonthlyUsage, function (e) {

                TotalMonth = parseFloat(TotalMonth) + e.QtyValue;
                 
                x.push(e.Days);
                y.push(e.QtyValue);
            });

            self.TotalMonth(numeral(TotalMonth).format('₦0.00a'));

            ShowMonthlyUsageReport(x, y);

            x = [];
            y = [];

            ko.utils.arrayForEach(data.DailyReadingData, function (e) {

                if (parseFloat(e.QtyValue) == 0 || e.QtyValue == "") {

                    //Total = parseFloat(Total) + 1;
                    NoLight = parseFloat(NoLight) + 1;
                }
                else (e.QtyValue > 0)
                {
                    //alert("Light Dey");
                    Total = parseFloat(Total) + 1;
                    // Light = parseFloat(Light) + 1;
                }

                // moment().format('hh')
                x.push(moment(e.Date_M).format('h:mm a'));
                //x.push('2:30PM');
                y.push(e.QtyValue);

                TotalToday = parseFloat(TotalToday) + e.QtyValue;
            });

            Total = parseFloat(data.DailyReadingData.length);
            Light = Total - parseFloat(NoLight);
            var PercentageAvailability = (Light / Total) * 100;
            self.Percentage(PercentageAvailability.toFixed(1) + "%");
            self.TotalToday(TotalToday.toFixed(1));
            ShowReport(x, y);
        }
    }



    if (self.APIStatus() == "PAGELOAD") {


        

        console.log("Challenger Series  " + self.MDCustomerDataLists.monthlyBillData());

        self.ArrearsUnpaid(numeral(self.MDCustomerDataLists.totalOutstanding()).format('₦0.00a'));

        var TotalAmountPaid = 0;
        var AverageConsumptions = 0;
        var v = ko.utils.arrayFilter(self.MDCustomerDataLists.monthlyBillData(), function (a) {


            

            Month.push(a.month().substring(0, 3) + " " + a.year());

            Consumption.push(a.consumption());

            AverageConsumptions = parseFloat(AverageConsumptions) + parseFloat(a.consumption());

            if (a.amountBilled() == "") {
                AmountBilled.push("0");
            }
            else {
                AmountBilled.push(a.amountBilled());
            }



            if (a.amountPaid() == "" || a.amountPaid() == '' || a.amountPaid() == "undefined") {

                //alert("Okokokokokokokokoko ");
                AmountPaid.push(parseFloat("0"));
            }
            else {

                console.log("Kabosh " + a.amountPaid());
                TotalAmountPaid = parseFloat(TotalAmountPaid) + parseFloat(a.amountPaid());
                AmountPaid.push(a.amountPaid());
            }
        });
         
        //get the Monthddddddddddddddddddddddd
          
        let f = Month[0];
        // storing the last item  
        let l = Month[Month.length - 1];


        var Span = l + " - " + f;

        console.log("Kalagbor "+Span);

        self.DateSpan(Span);

        self.AverageConsumptionP(numeral(AverageConsumptions).format('0.00a'));
        self.TotalAmountPaid(numeral(TotalAmountPaid).format('₦0.00a'));
        BillsAndPayments(AmountBilled, AmountPaid, Month);
        AverageConsumption(Month, Consumption);
        


        ///////

        var Light = 0;
        var NoLight = 0;
        var Total = 0; var TotalToday = 0;
        var TotalMonth = 0;
        x = [];
        y = [];

        ko.utils.arrayForEach(data.MonthlyUsage, function (e) {

            TotalMonth = parseFloat(TotalMonth) + e.QtyValue;

            x.push(e.Days);
            y.push(e.QtyValue);
        });

        self.TotalMonth(numeral(TotalMonth).format('₦0.00a'));
        ShowMonthlyUsageReport(x, y);

        /////////////////////



        //x = [];
        //y = [];

        //ko.utils.arrayForEach(self.MonthlyUsage(), function (e) {
        //    x.push(e.Days());
        //    y.push(e.QtyValue());
        //});
        //ShowMonthlyUsageReport(x, y);


        x = [];
        y = [];

        ko.utils.arrayForEach(self.DailyReadingData(), function (e) {

            if (parseFloat(e.QtyValue()) == 0 || e.QtyValue() == "")
            {
                //Total = parseFloat(Total) + 1;
                NoLight = parseFloat(NoLight) + 1;
            }
            else (e.QtyValue() > 0)
            {
                //alert("Light Dey");
                Total = parseFloat(Total) + 1; 
            } 
            x.push(moment(e.Date_M()).format('h:mm a')); 
            y.push(e.QtyValue()); 
            TotalToday = parseFloat(TotalToday) + e.QtyValue();
        });

        Total = parseFloat(self.DailyReadingData().length);
        Light = Total - parseFloat(NoLight);
        var PercentageAvailability = (Light / Total) * 100;
        self.Percentage(PercentageAvailability.toFixed(1) + "%");
        self.TotalToday(TotalToday.toFixed(1));
        ShowReport(x, y);
    }
}


function AverageConsumption(Month, Consumption) 
{
    if (myChart)
    {
        myChart.destroy();
    }


    var myChart =
    new Chart($("#members-chartAvgCons")[0].getContext('2d'), {
           type: 'bar',
           data: {
               labels: Month,
               datasets: [{
                   label: 'Consumption',
                   data: Consumption,
                   borderWidth: 5,
                   borderColor: colors.color_success,
                   backgroundColor: colors.color_success,
               }
               //},
               //    {
               //        label: 'AmountPaid',
               //        data: [5120, 4012, 4200, 3452, 9140, 5178, 5697, 4987, 9955, 8342, 2328, 5433],
               //        borderWidth: 5,
               //        borderColor: colors.color_success,
               //        backgroundColor: colors.color_success,
               //    }
               ]
           },
           options: {
               maintainAspectRatio: false,
               legend: {
                   display: false,
                   labels: {
                       display: false
                   }
               },
               scales: {
                   yAxes: [{
                       stacked: true,
                       gridLines: {
                           color: colors.border_color,
                           zeroLineColor: colors.border_color,
                       },
                       ticks: {
                           callback: function (value, index, values) {
                               if (parseInt(value) >= 1000) {
                                   return  value.toString().replace(/\B(?=(\d{3})+(?!\d))/g + '_KWh', ",");
                               } else {
                                   return  value +'KWh';
                               }
                           }
                       }
                   }],
                   xAxes: [{
                       gridLines: {
                           display: false
                       }
                   }]
               }
           }
       });

}
 
function BillsAndPayments(AmountBilled, AmountPaid, Month) {

    //$('#members-chart').remove(); // this is my <canvas> element
    //$('#members-chart-holder').append('<canvas id="members-chart"><canvas>');

     
 

   var myChart = new Chart($("#members-chart")[0].getContext('2d'), {
        type: 'bar',
        data: {
            labels: Month,
            datasets: [{
                label: 'AmountBilled',
                data: AmountBilled,
                borderWidth: 5,
                borderColor: colors.color_primary,
                backgroundColor: colors.color_primary,
            },
                {
                    label: 'AmountPaid',
                    data: AmountPaid,
                    borderWidth: 5,
                    borderColor: colors.color_success,
                    backgroundColor: colors.color_success,
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            legend: {
                display: false,
                labels: {
                    display: false
                }
            },
            scales: {
                yAxes: [{
                    stacked: false,
                    gridLines: {
                        color: colors.border_color,
                        zeroLineColor: colors.border_color,
                    },
                    ticks: {
                        callback: function (value, index, values) {
                            if (parseInt(value) >= 1000) {
                                return '₦' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            } else {
                                return '₦' + value;
                            }
                        }
                    }
                }],
                xAxes: [{
                    gridLines: {
                        display: false
                    }
                }]
            }
        }
    });











   // members - chart - holder

 
}
 
function FormatNumber(n, d) {
    x = ('' + n).length, p = Math.pow, d = p(10, d)
    x -= x % 3
    return Math.round(n * d / p(10, x)) / d + " kMGTPE"[x / 3]
}
 
function ShowMonthlyUsageReport(x, y)
{
    $('#results-graph').remove();

    $('#graph-container').append('<canvas id="results-graph"><canvas>');


    new Chart($("#dashboard-charts")[0].getContext("2d"), {
        type: "bar", data: {
            labels: x,
            datasets: [{
                label: "Sales",
                data:  y, borderWidth: 2, borderColor: colors.color_primary, backgroundColor: colors.color_bg, pointBackgroundColor: colors.color_primary
            }]
        }, options: { maintainAspectRatio: !1, legend: { display: !1, labels: { display: !1 } }, tooltips: { mode: "index", callbacks: { footer: function (o, r) { var e = 0; return o.forEach(function (o) { e += r.datasets[o.datasetIndex].data[o.index] }), "Sum: " + e } }, footerFontStyle: "normal" }, scales: { yAxes: [{ stacked: !0, gridLines: { color: colors.border_color, zeroLineColor: colors.border_color }, ticks: { callback: function (o, r, e) { return parseInt(o) >= 1e3 ? "Kwh" + o.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : "Kwh" + o } } }], xAxes: [{ gridLines: { display: !1 }, border: { display: !0, color: colors.border_color }, ticks: { beginAtZero: !0 } }] } }
    }) 
}
 
function ShowReport(x, y)
{
    
    new Chart($("#members-charts")[0].getContext('2d'), {
        type: 'line',
        data: {
            labels: x,
            datasets: [{
                label: 'Consumption',
                data: y,
                borderWidth: 1.5,
                borderColor: colors.color_primary,

            }
            ]
        },
        options: {
            maintainAspectRatio: false,
            legend: {
                display: false,
                labels: {
                    display: true
                }
            },
            scales: {
                yAxes: [{
                    stacked: false,
                    gridLines: {
                        color: colors.border_color,
                        zeroLineColor: colors.border_color,
                    },
                    ticks: {
                        callback: function (value, index, values) {
                            if (parseInt(value) >= 1000) {
                                return '$' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            } else {
                                return  value+'KWh';
                            }
                        }
                    }
                }],
                xAxes: [{
                    gridLines: {
                        display: false
                    }
                }]
            }
        }
    }); 
}

function ShowLoader() {
    swal({
        title: "loading...",
        text: "Please wait while the details load",
        imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',
        showConfirmButton: false,
        allowOutsideClick: false
    });
};

function hideloader() {

    swal.close();
};