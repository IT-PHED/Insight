function PHEDServeEnergyViewModel(data) 
{

    var self = this;
    ko.mapping.fromJS(data, {}, this);
    self.isProcessing = ko.observable(true);
    self.SelectIfManualOrAutomatic = ko.observable(true);
    self.showAutomatic = ko.observable(true);
    self.SelectedSerial = ko.observable();
    self.selectedType = ko.observable();
    self.ModalDescription = ko.observable(); self.ModalDatepaid = ko.observable();
    self.ConfirmPassword = ko.observable();
    self.ToDatePaid = ko.observable();
    self.FromDatePaid = ko.observable();
    self.PaymentId = ko.observable();
    self.SelectIfManualOrAutomatic = ko.observable();

    self.SearchItem = ko.observable();
    self.SearchAccountNumber = ko.observable();


    self.CustomerName = ko.observable();
    self.ModalAccountName = ko.observable();
    self.CustomerSurname = ko.observable();
    self.CustomerMiddleName = ko.observable();

    self.FromDate = ko.observable();
    self.ToDate = ko.observable();
    self.MeterType = ko.observable();


    self.NationalGridEnergy = ko.observable([]);

    self.AllDataList = ko.observableArray([]);
    self.PaymentList2 = ko.observableArray();

    self.SelectedReadingDate = ko.observable();

    self.PaymentList1 = ko.observableArray([]);

    self.selectedAgency = ko.observable();
    self.MeterNo = ko.observable();

    self.ResidenceType = ko.observable();

    self.StatusOptions = ko.observable();

    self.showAutomatic = ko.observable();
    self.showAutomatic2 = ko.observable();
    self.SelectedTransmissionStation = ko.observable();

    self.SelectedTransmissionStations = ko.observable();

    //////////////////////////////////////
    self.AccountType = ko.observable();
    self.PhoneNumber = ko.observable();
    self.DateOfBirth = ko.observable();
    self.Address = ko.observable();
    self.AccountNumber = ko.observable();
    self.DatePaid = ko.observable();
    self.EmailAddress = ko.observable();
    self.Datepaid = ko.observable(); self.Status = ko.observable(); self.Amount = ko.observable();


    self.SelectedTransformers = ko.observable(); self.AllSelectedTransformers = ko.observable();

    self.initial = ko.computed(function bind() {

        ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList2);

    });


    self.VerifyAccountNumber = function () {

        //Post Account Number for the Right person
        if (self.AccountNumber() == "" || self.AccountNumber() == null) {

            swal("Error", "Please input a valid account number", "error");
            return;
        }

        $.ajax({
            type: "post",
            url: '/Payments/VerifyAccountNo',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'AccountNumber': self.AccountNumber()
            }),
            success: function (result) {

                if (result == "Customer Not Found") {

                    swal("Error", "Customer Does not Exist Please cross check and try again", "error");
                    return;
                }

                if (result.Message == "Success") {

                    self.ModalAccountName(result.AccountName);
                    swal("verified", "Account Number has been verified", "success");
                    return;
                }
            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });



    }


    self.Calculate = function () {

        var search = ko.utils.arrayFilter(self.BillList(), function (item) {

            return item;
        });

        var items = 0; var items2 = 0;

        ko.utils.arrayForEach(search, function (s) {
            items = parseFloat(items) + parseFloat(s.TotalAmt);
        });

        self.TotalAmtOwed(items);
    }
    self.RequeryTransaction = function (data) {
        v(data);

        // console.log("Adadadada "+ data);

    }
    self.ViewReportAccount = function () {

        console.log("Abracadabra ");

        $("#ViewReceiptDiv").empty();

        url = '/GridEnergy/GetAllDetails?';


        var displayObject = "<object data='" + url + "'  type='application/pdf' width='100%' height='700px'></object>"

        //console.log("reaching me " + displayObject);
        $("#ViewReceiptDiv").append(displayObject);
        ////   $('#largeModal').modal('show');

        $('#largeModal').appendTo("body").modal('show');

        //$('#largeModal').appendTo("body");
    }
    self.CreateEnergy = function () {



        if (self.SelectedTransmissionStations() == null) {

            swal('Transmission Station!', 'Please select a transmission station to enter the reading for.', 'info');
            return;
        }


        if (self.AllSelectedTransformers() == null) {

            swal('Select Transformer!', 'Please select a Transformer to enter the reading for.', 'info');
            return;
        }

        if (self.SelectedReadingDate() == "undefined" || self.SelectedReadingDate() == 'undefined' || self.SelectedReadingDate() == null) {
            swal('No Date!', 'Please select a valid  Date to input the reading. Please try again later.', 'info');
            return;
        }

        //iterate through the List and Make it 

        var a = ko.utils.arrayFilter(self.AllDataList(), function (b) {

            if (b.ReadingValue() == null || b.ReadingValue() == "" || b.ReadingValue() == " ") {
                swal('No Value', 'Please input a valid reading for ' + b.FeederName() + ' Feeder to be able to proceed.Thank you.', 'info');
                return;
            }
        });

        var EnergyReadings = ko.mapping.toJS(self.AllDataList());

        $.ajax({
            type: "post",
            url: '/GridEnergy/InsertReading',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'DailyReadings': JSON.stringify(EnergyReadings),
                'Date': self.SelectedReadingDate()
            }),

            success: function (result) {

                //  var data = JSON.parse(result); 

                if (result.Message == "Success") {
                    swal('Record Saved!', 'Your entry has been successfully saved', 'success');
                    return;
                }
            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    }


    self.ApplyPaymentDirects = function (item) {



        if (self.AccountNumber() == null || self.AccountNumber() == "") {

            swal("Input Account Number", "Please input an account number to proceed", "info");
            return;
        }
        $.ajax({
            type: "post",
            url: '/Payments/ApplyPaymentDirect',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'AccountNumber': self.AccountNumber(),
                'Amount': self.Amount(),
                'PaymentId': self.PaymentId()
            }),
            success: function (result) {
                if (result.Message == "Success") {
                    swal('Record Saved!', 'The Payment has been claimed successfully', 'success');
                    return;
                }
                else {

                    swal('Error!', 'The Payment was not claimed successfully, Please try again Later', 'error');
                    return;
                }


            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });
        //iterate through the List and add the Item to the List for Insertion into the database

    }
    self.AddToTheList = function (item) {


        //iterate through the List and add the Item to the List for Insertion into the database

    }

    self.CancelEnergy = function () {
    }

    self.bindFeeders = function () {

        //check the Date


        if (self.SelectedReadingDate() == "undefined" || self.SelectedReadingDate() == 'undefined' || self.SelectedReadingDate() == null) {
            swal('No Date!', 'Please select a valid  Date to input the reading. Please try again later.', 'info');
            return;
        }




        var FeederID = self.SelectedTransmissionStations().TransmissionStationID();
        var FeederName = self.SelectedTransmissionStations().TransmissionStationName();
        console.log(FeederName);


        $.ajax({
            type: "post",
            url: '/GridEnergy/GetFeeders33KV',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'FeederID': FeederID,
                'FeederName': FeederName,

            }),

            success: function (result) {

                var data = JSON.parse(result);
                self.AllTransformerList([]);
                console.log(data);

                if (data.AllTransformerList.length == 0) {

                    swal('No Record!', 'No Transformer records were found , please try again later.', 'info');
                    return;
                }

                ko.mapping.fromJS(data.AllTransformerList, {}, self.AllTransformerList);
            },

            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);

            }

        });
    }


    self.bindAllFeeders = function (item) {

        // var FeederID = self.AllSelectedTransformers().TransformerID(); 
        //  console.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa " + self.AllSelectedTransformers().TransformerID()); 

        if (self.SelectedReadingDate() == "undefined" || self.SelectedReadingDate() == 'undefined' || self.SelectedReadingDate() == null) {
            swal('No Date!', 'Please select a valid  Date to input the reading. Please try again later.', 'info');
            self.SelectedTransmissionStations(null)
            self.AllSelectedTransformers(null);
            return;
        }


        if (self.AllSelectedTransformers() == null) {

            return;

        }
        var FeederID = self.AllSelectedTransformers().TransformerID();
        var Date = self.SelectedReadingDate();
        //  console.log(FeederID);

        //var FeederName = self.SelectedTransformers().Description();
        $.ajax({
            type: "post",
            url: '/GridEnergy/Get33KVFeeders',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'FeederID': FeederID,
                'Date': Date
            }),
            success: function (result) {
                var data = JSON.parse(result);
                self.Feeder33List([]); self.AllDataList([]);
                //if (data.AllTransformerList.length == 0) { 
                //    swal('No Record!', 'No Transformer records were found , please try again later.', 'info');
                //    return;
                //}





                ko.mapping.fromJS(data.Feeder33List, {}, self.Feeder33List);
                console.log(self.Feeder33List());
                var a = ko.utils.arrayFilter(self.Feeder33List(), function (b) {

                    return b;

                });

                ko.utils.arrayForEach(a, function (d) {



                    self.AllDataList.push({
                        FeederId: ko.observable(d.FeederID()),
                        FeederName: ko.observable(d.Description()),
                        FeederVoltLevel: ko.observable(d.FeederVoltlevel()),
                        PanelCTR: ko.observable(d.PanelCTR()),
                        MeterNo: ko.observable(d.MeterNo()),
                        ReadingValue: ko.observable(d.Reading()),
                        TransmissionStation: ko.observable(self.SelectedTransmissionStations().TransmissionStationName()),
                        TransmissionStationID: ko.observable(d.TransmissionStationID()),
                        TransformerCapacity: ko.observable(self.AllSelectedTransformers().TransformerName()),
                        Date: ko.observable(self.SelectedReadingDate())
                    });


                });

            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }

        });
    }


    self.InsertNewUser = function () {



        if (self.ConfirmPassword() != self.TaxUser.Password()) {
            swal('Password Mismatch!', 'The Passwords you"ve entered do not match please try again.', 'Info');

            return;
        }

        var unmapped = ko.mapping.toJS(self.TaxUser);
        $.ajax({
            type: "post",
            url: '/PayMaster/InsertTaxPayer',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'GetTaxUserInfo': JSON.stringify(unmapped),
                'TaxCategory': self.SelectedCheckBox()
            }),

            success: function (result) {

                //  var data = JSON.parse(result);


                swal('Record Saved!', 'Your entry has been successfully saved', 'success');


            },

            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);

            }

        });



    }

    self.calculate = ko.computed(function () {
        //   return this.firstName() + " " + this.lastName();

        // self.TotalSubHeadAmt(self.SubheadAmount() * self.SubheadQuantity());

    });

    self.ShowAmountSubhead = function (item) {
        var SubheadName = self.TaxRevenueSubHead().TaxRevenueSubHeadName();
        var SubheadDescription = self.TaxRevenueSubHead().TaxRevenueSubHeadDescription();
        self.SubheadAmount(self.TaxRevenueSubHead().TaxRevenueSubHeadAmount())
    }

    self.SearchItem = ko.observable();
    self.bindAccountHead = function (item) {
        var AgencyID = self.selectedAgency().AgencyID();
        console.log(AgencyID);

        $.ajax({
            type: "post",
            url: '/PayMaster/GetRevenueHead',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'AgencyID': AgencyID,

            }),

            success: function (result) {

                var data = JSON.parse(result);
                self.RevenueHeadList([]);
                console.log(data);

                if (data.RevenueHeadList.length == 0) {

                    swal('No Record!', 'No revenue head records were found , please try again later.', 'info');
                    return;
                }


                ko.mapping.fromJS(data.RevenueHeadList, {}, self.RevenueHeadList);

            },

            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);

            }

        });
    }

    self.click = function () {

        console.log("checking selected " + self.SelectIfManualOrAutomatic());
        var checked = true; var Prefixcode = "SINV-";
        if (self.SelectIfManualOrAutomatic() == "Automatic") {

            //self.numberOfClicks(previousCount + 1 + guid);

            self.showAutomatic(true);
            self.showAutomatic2(false);
        }
        else {
            self.showAutomatic(false);
            self.showAutomatic2(true);
        }
        return checked;
    }

    self.SearchData = function (item) {
        showloader();

        $.ajax({
            type: "post",
            url: '/CustomerCare/SearchData',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'AccountNumber': self.SearchAccountNumber(),
                'AccountType': self.AccountType()
            }),

            success: function (result) {

                console.log(result.Data);
                if (result === "Customer Not Found") {
                    swal('No Record!', 'The Customer Data does not exist on the KYC Database.Please refine the search', 'info');
                    return;
                }
                var data = JSON.parse(result.Data.result);

                if (data.KYC == null || data.KYC == 'null' || data.KYC == '') {
                    swal('Not Updated!', 'The Customer Data does not exist on the KYC Database.', 'info');
                    self.AccountType(null);
                    self.PhoneNumber(null),
                    self.DateOfBirth(null),
                    self.Address(null),
                    self.EmailAddress(null)
                    //return;
                }

                //DLEnhance x[0].CUSTOMER_NO
                $('#AccountType').html(result.Data.AccountType);
                $('#IBC').html(result.Data.IBC);
                $('#BSC').html(result.Data.BSC);

                $('#AccountName').html(result.Data.CustomerName);
                $('#PhoneNo').html(result.Data.PhoneNo);
                $('#AccountNo').html(result.Data.AccountNo);
                //$('#MeterNo').html(result.Data.MeterNo);
              
                if (data.KYC != null || data.KYC != 'null' || data.KYC != '') {
                    $('#DateOfBirth').html(moment(data.KYC.DATE_OF_BIRTH).format('MM/DD/YYYY'));
                    self.Address(result.Data.Address);
                    self.AccountNumber(data.KYC.ACCOUNT_NO);
                    self.EmailAddress(data.KYC.E_MAIL);

                    self.CustomerName(data.KYC.CustomerName);
                    self.CustomerSurname(data.KYC.CustomerSurname);
                    self.CustomerMiddleName(data.KYC.CustomerMiddleName);
                    self.PhoneNumber(data.KYC.PHONE);
                }
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);
                //$('#IBC').html(data.result.IBC);

                $('#Address1').val(result.Data.Address);

                //KYC Application
                self.AccountType(result.Data.AccountType);

              
              
                $('#MeterNo').val(result.Data.MeterNo);

              self.DateOfBirth(),
            

                hideloader();
            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }

        });
    }

    self.ClaimPaymentAction = function (event) {
        //$("#largeModal").modal("show"); 


        self.Status(event.Status());
        self.Amount(event.Amount());
        self.ModalDatepaid(event.Datepaid());
        self.ModalDescription(event.PaymentDescription());
        self.PaymentId(event.PaymentId());


        console.log("ddddddddddddddddddddddddddddddd" + self.PaymentId());


        $('#largeModal').appendTo("body").modal('show');
    }


    self.UploadAgency = function (event) {

        var files = $("#yourfileID").get(0).files;

        //if (self.ToDatePaid() == null) {
        //    swal('Select Date', 'Please when did you make the payment? Select the date to proceed. Thank you', 'info');
        //    return;
        //}

        //if (self.FromDatePaid() == null) {
        //    swal('Select Date', 'Please when did you make the payment? Select the date to proceed. Thank you', 'info');
        //    return;
        //}
        if (files[0] == null) {
            swal('select a document!', 'Please select a document to proceed.', 'info');
            return;
        }

        var ext = files[0].name.split('.').pop();

        var n = files[0].size;

        var m = parseFloat(n) / parseFloat(1024);
        // var Size = m.toFixed(2) + " KB";
        var FileType = ext;
        var DocumentTitle = ext;

        data = new FormData();
        data.append("DocumentFile", files[0]);
        data.append("DocumentName", files[0].name);

        $.ajax({
            type: "post",
            url: '/Payments/UploadAgency',
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
                self.BackBoneMasterScheduleList([]);
                ko.mapping.fromJS(data.BackBoneMasterScheduleList, {}, self.BackBoneMasterScheduleList);
                $('.nav-tabs a[href="#tabe-8"]').tab('show');

            },
            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    };



    self.SaveKYC = function (item) {

        showloader();
        var MeterNo = $("#MeterNo").val();


        console.log("See this here "+ MeterNo);


        var AccountNo1 = $("#AccountNo").val() + $("#AccountNo").next("span").text();
        $.ajax({
            type: "post",
            url: '/CustomerCare/SaveKYC',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'AccountType': self.AccountType(),
                'PhoneNumber': self.PhoneNumber(),
                'DateOfBirth': self.DateOfBirth(),
                'Address': self.Address(),
                'AccountNumber': $('#AccountNo').text(),
                'EmailAddress': self.EmailAddress(),
                'CustomerName': self.CustomerName(),
                'CustomerMiddleName': self.CustomerMiddleName(),
                'CustomerSurname': self.CustomerSurname(),
                'MeterNo': MeterNo
            }),

            success: function (result) {

                var data = JSON.parse(result.Data.result);

                console.log("Successfuly");
                 hideloader(); 
                swal('Successful!', 'The Customers Data has been Saved Successfully.', 'info');
                self.AccountType(null);
                self.PhoneNumber(null);
                self.DateOfBirth(null);
                self.Address(null);
                self.EmailAddress(null);
                 
                $('#AccountType').html(result.Data.AccountType);
                $('#IBC').html(result.Data.IBC);
                $('#BSC').html(result.Data.BSC);

                $('#AccountName').html("");
                $('#PhoneNo').html("");
                $('#AccountNo').html("");

                $('#DateOfBirth').html("");


                $('#Address1').val("");

                //KYC Application
                self.AccountType(null);


                self.PhoneNumber(null);
                $('#MeterNo').val("");

                self.DateOfBirth(null);
                self.Address(null);
                self.AccountNumber(null);
                self.EmailAddress(null);

                self.CustomerName(null);
                self.CustomerSurname(null);
                self.CustomerMiddleName(null);
                

            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }

        });
    }


    self.GetAllPaymentData = function (item) {

        var MeterNumber = self.MeterNo();
        var Status = self.StatusOptions();

        if (MeterNumber == "" || MeterNumber == "undefined" || MeterNumber == 'undefined' || MeterNumber == undefined) {

            swal('Meter Number!', 'Please Input a meter Number, Account Number or a Transaction Id to Proceed.', 'info');
            return;
        }
        if (Status == "-Select--") {

            swal('Select Status!', 'Please select a payment Status to proceed.', 'info');
            return;
        }

        $.ajax({
            type: "post",
            url: '/Payments/GetPaymentHistory',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'MeterNo': RevenueHead,
                'Status': RevenueHead
            }),

            success: function (result) {

                var data = JSON.parse(result);

                var data = JSON.parse(result);
                console.log("Data From Server " + JSON.stringify(data));


                if (data.PaymentList.length == 0) {
                    swal('No Record!', 'No records were found , please try again later.', 'info');
                    return;
                }
                self.PaymentList1([]);


                //self.PaymentList(data.PaymentList)
                ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList1);
                var s = $("#TableGrid").DataTable();


                s.clear().draw();
            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }

        });

    }


    self.GetAllPaymentDataByMeterNo = function (item) {

        var MeterNumber = self.MeterNo();
        var Status = self.StatusOptions();

        if (MeterNumber == "" || MeterNumber == "undefined" || MeterNumber == 'undefined' || MeterNumber == undefined) {

            swal('Meter Number!', 'Please Input a meter Number, Account Number or a Transaction Id to Proceed.', 'info');
            return;
        }
        if (Status == "-Select--") {

            swal('Select Status!', 'Please select a payment Status to proceed.', 'info');
            return;
        }
        console.log("dsdsdsdsds " + Status);
        console.log("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff " + MeterNumber);

        swal({
            title: "Loading Data...",
            text: "Please wait",
            imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',

            showConfirmButton: false,
            allowOutsideClick: false
        });
        $.ajax({
            type: "post",
            url: '/Payments/GetPaymentHistory1',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'MeterNo': MeterNumber,
                'Status': Status
            }),

            success: function (result) {

                var data = JSON.parse(result);

                if (data.PaymentList.length == 0) {
                    swal('No Record!', 'No records were not found , please try again later.', 'info');
                    return;
                }

                self.PaymentList1([]);
                console.log("ssssssssssssssssssss " + JSON.stringify(data.PaymentList));
                ko.mapping.fromJS({}, {}, self.PaymentList);
                ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList1);

                swal.close();

                $("#TableGrid").DataTable();

            },

            error: function (err) {
                swal("Error", "A Fatal Error has occured, Please try again or contact PHED", "error");
                self.isProcessing(false);
            }

        });

    }



    self.GetAllPaymentDataByMeterNo2 = function (item) {

        var FromDate = self.FromDate(); var ToDate = self.ToDate();
        var MeterType = self.MeterType();

        if (FromDate == "" || FromDate == "undefined" || FromDate == 'undefined' || FromDate == undefined) {

            swal('From Date!', 'Please select the From Date to Proceed.', 'info');
            return;
        }

        if (ToDate == "" || ToDate == "undefined" || ToDate == 'undefined' || ToDate == undefined) {


            swal('To Date!', 'Please select the To Date to Proceed.', 'info');
            return;
        }
        console.log("dsdsdsdsds " + Status);
        console.log("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff " + MeterNumber);

        swal({
            title: "Loading Data...",
            text: "Please wait",
            imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',

            showConfirmButton: false,
            allowOutsideClick: false
        });
        $.ajax({
            type: "post",
            url: '/Payments/GetPaymentHistory2',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'FromDate': FromDate,
                'ToDate': ToDate,
                'MeterType': MeterType

            }),

            success: function (result) {

                var data = JSON.parse(result);

                if (data.PaymentList.length == 0) {
                    swal('No Record!', 'No records were not found , please try again later.', 'info');
                    return;
                }

                self.PaymentList1([]);
                console.log("ssssssssssssssssssss " + JSON.stringify(data.PaymentList));
                ko.mapping.fromJS({}, {}, self.PaymentList);
                ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList1);

                swal.close();

                $("#TableGrid").DataTable();

            },

            error: function (err) {
                swal("Error", "A Fatal Error has occured, Please try again or contact PHED", "error");
                self.isProcessing(false);
            }

        });

    }










    self.bindAccountSubHead = function (item) {

        var RevenueHead = self.TaxRevenueHead().TaxRevenueHeadID();
        $.ajax({
            type: "post",
            url: '/PayMaster/GetRevenueSubHead',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'RevenueHead': RevenueHead,

            }),

            success: function (result) {

                var data = JSON.parse(result);
                self.RevenueSubHeadHeadList([]);
                console.log(data);

                if (data.RevenueSubHeadHeadList.length == 0) {
                    swal('No Record!', 'No revenue head records were not found , please try again later.', 'info');
                    return;
                }
                ko.mapping.fromJS(data.RevenueSubHeadHeadList, {}, self.RevenueSubHeadHeadList);
            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }

        });
    }

    self.PopulateTaxPayerInfo = function (item) {
        console.log(item.TaxPayerName());
        self.TaxPayerName(item.TaxPayerName());
        //self.TaxPayerPhone = ko.PhoneNumber();
        self.TaxPayerType(item.TaxPayerType());
        self.TaxPayerNo(item.TaxPayerNo());
        self.Address(item.Address());
        self.EmailAddress(item.EmailAddress());
        self.PhoneNumber(item.PhoneNumber());

        Bill();




    }
    self.SearchNewUser = function () {

        if (self.SearchItem() == "" || self.SearchItem() == null) {
            swal('Empty Search Criteria!', 'Please type a search item in the space provided to search for Name or Tax ID.', 'info');

            return;
        }

        $.ajax({
            type: "post",
            url: '/PayMaster/SearchTaxPayer',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'GetTaxUserInfo': self.SearchItem(),
                'TaxCategory': self.SelectedCheckBox()
            }),

            success: function (result) {

                var data = JSON.parse(result);
                self.TaxPayersList([]);
                console.log(data);

                if (data.TaxPayersList.length == 0) {
                    swal('No Record!', 'Your records were not found , please try modifying the search criteria.', 'info');
                    return;
                }

                else {
                    Search();
                }

                ko.mapping.fromJS(data.TaxPayersList, {}, self.TaxPayersList);
            },

            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    }

}

function FeederReading(FeederId, FeederName, FeederVoltLevel, PanelCTR, MeterNo, Reading, TransmissionStation, TransmissionStationID, TransformerCapacity, Date) {

    this.FeederId = ko.observable(FeederId);
    this.FeederName = ko.observable(FeederName);
    this.FeederVoltLevel = ko.observable(FeederVoltLevel);
    this.PanelCTR = ko.observable(PanelCTR);
    this.MeterNo = ko.observable(MeterNo);
    this.Reading = ko.observable(Reading);
    this.TransmissionStation = ko.observable(TransmissionStation);
    this.TransmissionStationID = ko.observable(TransmissionStationID);
    this.TransformerCapacity = ko.observable(TransformerCapacity);
    this.Date = ko.observable(self.SelectedReadingDate());
}


function CreateBillinvoice() {

    $("#TaxPanel").slideUp();
    $("#InfoPanel").slideUp();
}

function Search() {

    $("#SearchPanel").slideUp();
    $("#InfoPanel").slideDown();
}
function showgrid() {
    $("#BilGridPanel").slideDown();
}
function Hidegrid() {
    $("#BilGridPanel").slideUp();
}
function showgrid2() {
    //$("#BilGridPanel").slideUp();
    $("#BilGridPanel").slideDown();
}
function Bill() {

    $("#TaxPanel").slideDown();
}
function verify1() {
    return false;
}

function verify2() {
    return false;
}
function printout() { $("#printrec").print()(); }
function print() {
    $("#PaymentDetails").slideUp(); $("#Receipt").slideDown(); $("#stepss").slideUp(); $("#showrec").slideUp(); $("#printrec").slideDown();
}
function p() {


    var AmountPay = $("#AmountPay").val();
    var EmailPay = $("#EmailPay").val();
    var PhonePay = $("#PhonePay").val();


    if (AmountPay == "" || AmountPay == '' || AmountPay == 'undefined') {


        swal("Required", "Please enter a value for the purchase Amount", "info");

        return;
    }


    if (PhonePay == "" || PhonePay == '' || PhonePay == 'undefined') {


        swal("Required", "Please enter a valid Phone Number", "info");

        return;
    }

    if (EmailPay == "" || EmailPay == '' || EmailPay == 'undefined') {


        swal("Required", "Please enter a value for your Email Address", "info");

        return;
    }

    document.getElementById('tab-3').classList.add('active');
    document.getElementById('tab-1').classList.remove('active');
    document.getElementById('tab-2').classList.remove('active');

    document.getElementById('tab3').classList.add('active');
    document.getElementById('tab1').classList.remove('active');
    document.getElementById('tab2').classList.remove('active');
}


function v(transaction_id) {
    //var transaction_id = $("#AccountNo1").val();


    if (transaction_id == "" || transaction_id == '' || transaction_id == 'undefined') {

        swal("Account No", "Please enter a value for your transaction ID Number", "info");

        return;
    }

    swal({
        title: "Confirming Status...",
        text: "Please wait",
        imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',

        showConfirmButton: false,
        allowOutsideClick: false
    });

    $.ajax({
        type: "GET",
        url: '/Payments/VerifyPayment',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {
            transaction_id: transaction_id,
        },

        success: function (result) {
            //  var x = JSON.parse(result.result); 
            console.log(result);
            console.log("adssdadasdsdasdsadas" + result[0].STATUS);

            if (result[0].STATUS == "SUCCESS") {
                swal("Success", "Transaction was successful. Token is " + result[0].TOKENDESC, "success");




                var data = JSON.parse(result[0].LIST);
                console.log("Despicable Throat " + JSON.stringify(data));


                if (data.PaymentList.length == 0) {
                    swal('No Record!', 'No records were not found', 'info');
                    return;
                }
                //  self.PaymentList1([]);


                //self.PaymentList(data.PaymentList)
                ko.mapping.fromJS(data.PaymentList, {}, self.PaymentList1);
                var s = $("#TableGrid").DataTable();


                console.log("Done");
                return;
            }
            else {

                swal("Unsuccessful", "The Payment was not Successful. Please advice the Customer to reach the bank for a refund if he was debited.", "info");
                //  swal("Failure", "The Requery was not successful. If the Customer was debited, Please advice to approach bank for a refund/Reversal. All inconveniences are regretted by PHED", "info");
                return;
            }

        },
        error: function (err) {


            console.log("errrrrrrrrrrrrrrrrrrrrrrp " + err.responseText);
            if (err.responseText == "Customer Not Found" || err.responseText == "Consumer Not Found") {

                console.log("Wahala oooooooooooooooooo " + err.responseText);

                swal("Error", err.responseText, "error");
                return;
            }
            else
                if (err.responseText != null || err.responseText != "") {

                    swal("Error", err.responseText, "error"); return;
                }
                else
                    if (err.responseText != null || err.responseText != "") {

                        swal("Error", "There was an error verifying your account details, Please ensure your Account/Meter Number is valid and your network connection is active and try again.", "error");
                        return;
                    }
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
