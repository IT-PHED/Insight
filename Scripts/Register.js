function RegViewModel(data) {

    var self = this;


    ko.mapping.fromJS(data, {}, this);

    self.isProcessing = ko.observable(true);
    self.Password = ko.observable();
    self.ConfirmPassword = ko.observable();
    self.AccountNo = ko.observable();
    self.AccountType = ko.observable();

    self.VerifyAccount = function (form) {

        self.isProcessing(true);



        //var InsertModifiedunmapped = ko.mapping.toJS(self.ApplicationUser);

         
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


                if (result.result == "Customer Not Found")
                {

                    swal('No Record!', 'The Account/Meter No does not exist', 'error');
                    return;
                }
    console.log("sssssssssssssssssssssssss "+result);
                //if()
                //{
                     
                //}

                var data = JSON.parse(result.result);

            

                swal('Record Seen!', 'The user data was Found', 'success');
                ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);
                self.isProcessing(false);
               // window.location.href = "/Account/Login"
            },
            error: function (err) {
                
                alert("Fail Details Request " + err.responseText)
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


                var data = JSON.parse(result.result);

                if (result.error != "") {

                    swal('Not Successful!', result.error, 'error');
                    return;

                }
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


                var data = JSON.parse(result.result);

                if (result.error != "") {

                    swal('Not Successful!', result.error, 'error');
                    return;

                }


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

    self.RegisterMD = function (form) {

        var CreatedBy = $("#StaffId").val();

        self.isProcessing(true);
         
        var InsertModifiedunmapped = ko.mapping.toJS(self.ApplicationUser);
         
        if (self.Password() != self.ConfirmPassword()) {

            swal('Password Mismatch!', 'The passwords entered do not Match. Please try again.', 'info');
            return; 
        }

        $.ajax({
            type: "POST",
            url: '/Account/AddNewUserMD',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                'userToBeAdded': JSON.stringify(InsertModifiedunmapped),
                'Password': self.Password(),
                'CreatedBy': CreatedBy

            }),
            success: function (result) { 
                var data = JSON.parse(result.result);

                if (result.error != "") {

                    swal('Not Successful!', result.error, 'error');
                    return;

                }
                swal('Record Saved!', 'The user has been registered successfully', 'success'); 
                self.isProcessing(false);
                ko.mapping.fromJS(data.ApplicationUser, {}, self.ApplicationUser);
                 
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
            cursor: 'wait' },
        css: {
            border: 0,
            padding: 0,
            backgroundColor: 'none',
            color: '#fff'
        }
    });
} 