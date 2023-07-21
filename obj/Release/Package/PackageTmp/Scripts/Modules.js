function ReferencesViewModel(data) {

    var self = this;

    ko.mapping.fromJS(data, {}, this);

    self.AllDataList = ko.observableArray([]);
    self.AllDeletedList = ko.observableArray([]);
    self.isProcessing = ko.observable(true);

    self.pageSize = ko.observable(6);
    self.pageIndex = ko.observable(0);
    self.Initial = ko.computed(function () {

        self.AllDataList(self.MenuItemsMainList());

    });

    self.addRecord = function () {

        //   console.log("Reaching");
        var guid = (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
        self.AllDataList.push({
            MenuItemId: guid,
            icon:"",
            ModuleName:"",
            MenuOrder:"",
        });
         
        console.log(self.AllDataList());
    };
     
    self.delete = function (bank) {

        self.AllDeletedList.push(bank);
        self.AllDataList.remove(bank);

        console.log("Deleted Items " + self.AllDeletedList());
    };

    self.save = function (form) {
        self.isProcessing(true);
        //console.log("Reaching");
         
        var InsertModifiedunmapped = ko.mapping.toJS(self.AllDataList);
        var Deletedunmapped = ko.mapping.toJS(self.AllDeletedList);

        //Busy();
        $.ajax({
            type: "POST",
            url: '/Module/InsertUpdatedDelete',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'addModifiedItems': JSON.stringify(InsertModifiedunmapped),
                'deletedItems': JSON.stringify(Deletedunmapped) 
            }),
            success: function (result) { 
                var data = JSON.parse(result); 
                //var jsData = ko.toJS(result); 
                //var AllReferenceCheckViewModel = new AllReferenceViewModel(data);

                self.ModuleList([]);

                ko.mapping.fromJS(data.ModuleList, {}, self.ModuleList);
                //ko.mapping.fromJS(data, {}, AllReferenceViewModel(data));

                //self.BankList(AllReferenceCheckViewModel.BankList());

               // Done();

                swal('Record Saved!', 'Your entry has been successfully saved', 'success');


            },
            error: function (err) {


                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    };

 


}

function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

function Done() {
    var light_2 = $("#Holder");
    $(light_2).unblock();
}

function Busy() {
    var light_2 = $("#Holder");


    alert(light_2);
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