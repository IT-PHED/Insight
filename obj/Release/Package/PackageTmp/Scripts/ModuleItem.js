function ReferencesViewModel(data) {

    var self = this;


    ko.mapping.fromJS(data, {}, this);

    self.AllCountryList = ko.observableArray([]);
    self.filteredFromModuleItemList = ko.observableArray([]);
    self.AllDeletedList = ko.observableArray([]);
    self.selectedModule = ko.observable();

    self.isProcessing = ko.observable(true);
    //self.Initial = ko.computed(function () {

    //    self.filteredFromContinentList(self.CountryList());

    //});

    self.addRecord = function () {



        if (self.selectedModule() == null) {

            swal("Select Module", "Please select a valid Module to Continue", "info");

        }






        console.log("Reaching");
        var guid = (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
        console.log("id" + guid);


        self.filteredFromModuleItemList.push({
            MenuItemId: guid,
            MenuText: "",
            MenuOrder: "",
            ControllerName: "",
            ActionName:"",
            ParentMenuItemId: self.selectedModule().MenuItemId(),

        });
    };

    self.delete = function (country) {

        self.AllDeletedList.push(country);
        self.filteredFromModuleItemList.remove(country);

        console.log("Deleted Items " + self.AllDeletedList());
    };




    self.save = function (form) {

        self.isProcessing(true); 
        //console.log("Reaching");
        var InsertModifiedunmapped = ko.mapping.toJS(self.filteredFromModuleItemList);
        var Deletedunmapped = ko.mapping.toJS(self.AllDeletedList); 
        $.ajax({
            type: "Post",
            url: '/ModuleItem/InsertUpdatedDelete',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'addModifiedItems': JSON.stringify(InsertModifiedunmapped),
                'deletedItems': JSON.stringify(Deletedunmapped)
            }),

            success: function (result) { 
                var data = JSON.parse(result); 
                self.ModuleItemList([]);

                ko.mapping.fromJS(data.MenuItemsMainList, {}, self.MenuItemsMainList); 
                swal('Record Saved!', 'Your entry has been successfully saved', 'success');
                self.isProcessing(false);
            },
            error: function (err) {
                alert("Fail Details Request " + err.responseText)
                self.isProcessing(false);
            }
        });

    };
    self.filteredFromContinent = ko.computed(function () {



        var results = [];
        self.filteredFromModuleItemList([]);

        if (self.selectedModule() != null) {


            var t = self.selectedModule().MenuItemId();

            
            //console.log("Select Continent " + t);

            var ModuleItemBelongstoModuleItem = ko.utils.arrayFilter(self.MenuItemsMainList(), function (ModuleItem) {
                return ModuleItem.ParentMenuItemId() == t;
            });

            console.log("Select Filtered List " + ModuleItemBelongstoModuleItem);
            //console.log(productBelongstoCategory.length);


            ko.utils.arrayForEach(ModuleItemBelongstoModuleItem, function (ModuleItemmain) {



                results.push(ModuleItemmain);


            });

            // console.log(results);



            self.filteredFromModuleItemList(results);
            //return results;

        } else {



            self.filteredFromModuleItemList(results);
            // return results;
        }


    }, self).extend({ throttle: 750 });


}
function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}