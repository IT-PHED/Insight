function EbillsViewModel(data) {
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


    self.Category = ko.observable();

    self.ModeOfProcess = ko.observable();
    self.SelectedYear = ko.observable();
    self.ModeOfDelivery = ko.observable();

    self.BatchFromNumber = ko.observable();
    self.BatchEndNumber = ko.observable();


    self.DeliverBill = function ()
    {
         


        console.log("This is Going on "+self.BatchEndNumber());


        if ( self.ModeOfProcess() == "--Select Mode--") {

            swal("Select Processing Mode", "Please select the Processing  Mode.", "error");
            return;
        }
        if (parseFloat(self.BatchEndNumber()) < parseFloat(self.BatchFromNumber())) {

            swal("Incorrect Batch Range", "The Batch Range is Incorrect. The Upper Range MUST be greater than the Lower Range Number.", "error");
            return;
        }

 

        //var table = $('#EbillsDatatable').DataTable();
        //table.destroy();
         
        ShowLoader();
        
        $.ajax({
            type: "POST",
            url: '/Ebills/EbillsDeliveryLIVE',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                Category: self.Category(),
                BillMonth: self.SelectedYear(),
                ModeOfDelivery: self.ModeOfDelivery(),
                FromRange: self.BatchFromNumber(),
                EndRange: self.BatchEndNumber()
            }),
            success: function (result) {
                var data = JSON.parse(result); 
                ko.mapping.fromJS(data.DelivererdBillsList, {}, self.DelivererdBillsList);
                hideloader();
                //self.PaymentList(data.PaymentList); 
                PaginateTable('EbillsDatatable'); 
                swal("Approved", "The Selected Bills have been delivered Electronically to the Customers.", "success");
            },

            error: function (err) {
                hideloader();
                console.log(err.responseText);
                dd(err.responseText);
                //alert("Fail Details Request " + err.responseText)
                //self.isProcessing(false);
            }

        });

    }
}

function ShowLoader() {
    swal({
        title: "Processing...",
        text: "Please wait while we deliver the Bills Electronically",
        imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',
        showConfirmButton: false,
        allowOutsideClick: false
    });
};

function hideloader() {

    swal.close();
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