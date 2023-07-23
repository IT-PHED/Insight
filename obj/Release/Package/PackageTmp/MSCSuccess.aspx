<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSCSuccess.aspx.cs" Inherits="PHEDServe.MSCSuccess" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>PHEDMap</title>
    <!-- Bootstrap CSS -->
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome CSS-->
    <link href="assets/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- Linear icons CSS-->
    <link href="assets/linearicons/css/icon-font.min.css" rel="stylesheet">
    <!-- Slick Nav CSS -->
    <link rel="stylesheet" href="assets/css/slicknav.min.css">
    <!-- Owl Carousel -->
    <link href="assets/owl-carousel/css/owl.carousel.css" rel="stylesheet">

    <link href="assets/owl-carousel/css/owl.theme.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="css/style2.css" rel="stylesheet">
    <!-- Animate CSS -->
    <link href="assets/animate/animate.css" rel="stylesheet">
    <!-- Favicon -->
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
</head>
<body>
    <!-- Pre Loader -->
    

    <input type="hidden" id="TicketId45" runat="server" />
    <!-- Header Start -->
    <header class="wow fadeInDown" data-offset-top="197" data-spy="affix">
        <div class="top-wrapper hidden-xs">
            <div class="container">
                <div class="row">
                    <div class="col-lg-7 col-md-6 col-sm-12">
                        <div class="guest"> Welcome to PHEDMAP Website</div>
                    </div>
                    <div class="col-lg-5 col-md-6 hidden-sm">
                        <div class="top-header-add">
                            <div class="phone"><i class="fa fa-phone" aria-hidden="true"></i><span>Call</span> 08139834000</div>
                            <div class="book"><a href="javascript:void(0)"><i class="fa fa-globe" aria-hidden="true"></i><span>Get yours Now.</span></a></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="header-inner">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3 col-md-3">
                        <div class="logo"> <a href="http://map.nepamsonline.com"><img src="images/logo.png" alt="#" data-popupalt-original-title="null" title="#"></a> </div>
                        <div class="mobile-menu"></div>
                    </div>
                    <div class="col-lg-9 col-md-9">
                        <nav class="navbar navbar-default desktop">
                            <div class="navbar-collapse">
                                <!-- Main Menu -->
                                <ul id="nav" class="nav menu navbar-nav">
                                    <li class="active"><a href="http://map.nepamsonline.com">Home</a></li>
                                    
                                </ul>
                                <!-- End Main Menu -->
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <!-- Header End -->
    <div class="breadcromb-wrapper">
        <div class="breadcromb-overlay"></div>
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="breadcromb-left">
                        <h3>MAP Application</h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="breadcromb-text">
        <div class="container">
            <ul>
                <li><a href="@Url.Action("Index","Home")"><i class="fa fa-home"></i>Home</a></li>
                <li><i class="fa fa-angle-right" aria-hidden="true"></i></li>
                <li>MAP</li>
                <li><i class="fa fa-angle-right" aria-hidden="true"></i></li>
                <li>MAP Application</li>
            </ul>
        </div>
    </div>
    <!-- Breadcromb Wrapper End -->
    <!-- Inner Page Wrapper Start -->
   

  



    
<div id="AccountDetails" class="inner-page-wrapper blog-single-area"> 
    <div class="container">

 
    <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                <div class="blog-post">

                                    <div class="single-blog-post">
                                 
                                        <div class="add-comment-box" id="Verify" style="display:none">
                                          
                                                <div class="title">
                                                    <h3>Your Account Details</h3><hr />
                                                </div>

                                                <form id="add-comment-form" action="#" novalidate>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="field-label">Meter Applicant's Name*</div>
                                                                    <input name="lname" readonly id="MAPplicant" class="selectMAP" value="" placeholder="" aria-required="true" type="text">
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="field-label">Account Type*</div>
                                                                    <input name="lname" readonly id="AccountType2" class="selectMAP" value="" placeholder="" aria-required="true" type="text">
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="field-label">Account No</div>
                                                                    <input name="lname" readonly id="AccountNo2" class="selectMAP" value="" placeholder="" aria-required="true" type="text">
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="field-label">Meter No*</div>
                                                                    <input name="lname" readonly class="selectMAP" id="MeterNo" value="" placeholder="" aria-required="true" type="text">
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="field-label">Arrears</div>
                                                                    <input name="lname" readonly class="selectMAP" id="TotalBill" value="" placeholder="" aria-required="true" type="text">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="field-label">Account Name</div>
                                                                    <input name="form_email" readonly class="selectMAP" id="AccountName2" value="" placeholder="" aria-required="true" type="text">
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <div class="field-label">TicketId</div>
                                                                    <%--<input name="lname" readonly class="selectMAP" id="TicketId45" value="" placeholder="" aria-required="true" type="text">--%>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="field-label">Address</div>

                                                                    <textarea name="form_email" readonly id="HouseAddress2" class="selectMAP2" cols="5" placeholder="" aria-required="true"></textarea>
                                                                </div>
                                                            </div>


                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="field-label">IBC</div>
                                                                    <input name="lname" readonly class="selectMAP" id="IntegratedBusinessCenter" value="" placeholder="" aria-required="true" type="text">
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="field-label">BSC</div>
                                                                    <input name="lname" readonly class="selectMAP" id="BusinessServiceCenter" value="" placeholder="" aria-required="true" type="text">
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-12 pull-right" style="float:right">
                                                                    <button class="btn-one thm-bg-clr" type="button" data-bind="click: ProceedAfterVerification">Proceed!</button>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </form>
                                            </div>


                                        <div class="text-holder" id="ArrearsDiv" style="display:none">
                                            <h3 class="blog-title">You have some arrears that have not yet been paid</h3>
                                            <hr />
                                            <div class="meta-box">
                                              
                                            </div>
                                            <div class="text" style="color:black !important">
                                                <p class="mar-bottom">
                                                    Thank you for your Interest to Purchase a Meter from Meter Asset Providers (MAP). As you Know the MAP Excercise is an initiative of the Federal Government. To get a Meter from the MAP, you need to pay off your arrears or agree to pay a certain percentage of the amount you vend every month.
                                                </p>
                                                <p>If you choose to pay off your arrears now, you may proceed to click on the Payoff Arrears Button, But if you decide to pay a certain Percentage of what you owe, every Month, please proceed to Click on Pay installmentally. Else if you feel the amount accrued to your account is Exhorbitant and you'll want to dispute that amount or want it revised, please click on Attent Bill Revision Camp to Fill a BRC form.</p>
                                            </div>
                                            <!--Start author slogan-->
                                            <div class="author-slogan">
                                                <p>You can either Payoff your arrears now, have it split into Monthly perentages of what you vend or attend a Bill revision Camp to have your Bill Reviewed and Revised. </p>
                                                <div class="border-box"></div>
                                                <div class="author-info">
                                                    <h3>Please Quote your BRC Identification Number in all transactions</h3>
                                                    <span class="thm-clr">PHED</span>
                                                </div>
                                            </div>
                                            <!--End author slogan-->
                                            <!--Start bottom content box-->
                                            <div class="bottom-content-box">

                                                <div class="col-md-12">
                                                    <div class="text-box">



                                                        <div class="row single-sidebar">
                                                            <div class="sec-title">
                                                                <h3>What do you want to do?</h3>
                                                            </div>

                                                            <ul class="mypopular-tag">
                                                                <li><button class="btn-one MAPButton" type="button" data-bind="click: BillRevision"> I need a Bill Revision</button></li>
                                                                <li><button class="btn-one MAPButton" data-bind="click: PayOffArrears" type="button">I want to Pay off my arrears Now</button></li>
                                                                <li><button class="btn-one MAPButton" type="button" data-bind="click: ArrearsForm">I want to pay off my arrears Installmentally Every Month</button></li>
                                                            </ul>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--End bottom content box-->
                                            <!--Start tag box-->
                                            <div class="tag-box">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="left pull-left">
                                                            <ul>
                                                                <li><a href="#"><i class="fa fa-refresh" aria-hidden="true"></i></a></li>
                                                                <li><a href="#">Go Back</a></li>

                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--End tag box-->
                                        </div>



                                        <div class="text-holder" id="Installment1" style="display:none">
                                            <h3 class="blog-title">You have some arrears that have not yet been paid</h3>
                                            <hr />
                                            <div class="meta-box">
                                                 
                                                <ul class="meta-info">
                                                    <li><span class="thm-clr">BRC Id:</span> <span id="BRC_ID"></span></li>
                                                    @*<li><span class="thm-clr">On:</span> <a href="#">@DateTime.Now</a></li>*@
                                                    <li><span class="thm-clr">Ticket Id:</span> <a href="#"><span id="TicketID"></span></a></li>
                                                    <li><span class="thm-clr">Account Name:</span> <a href="#"><span id="AccountName3"></span></a></li>
                                                    <li><span class="thm-clr">Account No:</span> <a href="#"><span id="AccountNo3"></span></a></li>
                                                </ul>
                                            </div>

                                            <div class="text">
                                                <p class="mar-bottom">You can choose to pay off your arrears instalmentally.The percentage selected will be used to determine the amount you'll pay monthly when you vend.  </p>
                                            </div>


                                            <div class="bottom-content-box">

                                                <div class="col-md-12">
                                                    <div class="text-box">

                                                        Select the Mothly Payment Rate <select class="selectMAP" data-bind="value: Installments">

                                                            <option>40%</option>
                                                            <option>50%</option>
                                                            <option>60%</option>
                                                            <option>70%</option>
                                                            <option>80%</option>
                                                            <option>90%</option>
                                                            <option>100%</option>
                                                        </select>
                                                        <hr />



                                                        <br />
                                                        <br />
                                                        <br />


                                                        <div class="row single-sidebar">
                                                            <div class="sec-title">
                                                                <h3>What you can do?</h3>
                                                            </div>

                                                            <ul class="mypopular-tag">
                                                                <li><button class="btn-one MAPButton" data-bind="click: SubmitInstallmental" type="button">Submit my Selection for Installmental Payment</button></li>
                                                                <li><button class="btn-one MAPButton" type="button" data-bind="click: BillRevision">I need my Bill revised for Bill Revision</button></li>
                                                                <li><button class="btn-one MAPButton" data-bind="click: PayOffArrears" type="button">I want to pay off my arrears Now</button></li>
                                                            </ul>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <!--End bottom content box-->
                                            <!--Start tag box-->
                                            <div class="tag-box">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="left pull-left">
                                                            <ul>
                                                                <li><a href="#"><i class="fa fa-heart" aria-hidden="true"></i></a></li>
                                                                <li><a href="#">18</a></li>
                                                                <li><a href="#"><i class="fa fa-comments" aria-hidden="true"></i></a></li>
                                                                <li><a href="#">6</a></li>
                                                            </ul>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <!--End tag box-->
                                        </div>


                                         
                                        <div class="text-holder" id="MapApplication" style="display:none">
                                            @*<h3 class="blog-title">MAP Application Form ~ <span data-bind="text: customer.TransactionID" id="MapTicketId"></span></h3>
                                            <hr />*@

                                            <div class="row">
                                            
                                                <div class="col-md-7">
                                                <h3 class="blog-title">MAP Application Form     </h3>
                                            </div>

                                                <div class="col-md-5" >
                                                    <h3 style="float:right"> Ticket Id: <span data-bind="text: customer.TransactionID" runat="server" id="MapTicketIdApplied"></span></h3>
                                                </div>
                                            </div>
                                            
                                            <hr />



                                            <form enctype="multipart/form-data" id="Customer">


                                                <fieldset>


                                                    <div class="row">

                                                        <div class="form-group col-md-4">
                                                            <label for="title">Title<span>*</span></label>
                                                            <select class="selectMAP m-b" style="width: 100%" data-bind="options: TitleList, optionsText: 'TitleName', optionValue: 'TitleValue', value: selectedTitle, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: '--Select Title--'"></select>





                                                            @*<select class="selectMAP">
                                <option value="">--Select Title--</option>
                                <option value="Mr">Mr</option>
                                <option value="Mrs">Mrs</option>
                                <option value="Dr">Dr</option>
                                <option value="Prof">Prof</option>
                                <option value="Chief">Chief</option>
                            </select>*@
                                                        </div>
                                                        <div class="form-group col-md-8">
                                                            <label for="sn">Customer Name<span>*</span></label>
                                                            <input type="text" id="MapName" data-bind="value: customer.CustomerSurname" class="form selectMAP" placeholder="Enter Customer Name" />
                                                        </div>
                                                        @*<div class="form-group col-md-4">
                            <label for="on">Other Name<span>*</span></label>
                            <input type="text" data-bind="value: customer.CustomerOtherName" class="selectMAP" placeholder="Enter Other Name" />
                        </div>*@
                                                    </div>
                                                    <div class="row">

                                                        <div class="form-group col-md-4">
                                                            <label for="OCCUPATION">Occupation<span>*</span></label>
                                                            <input class="selectMAP" data-bind="value: customer.Occupation" type="text" placeholder="Enter Occupation" />
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label for="ctn">Contact Tel No<span>*</span></label>
                                                            <input class="selectMAP" id="MapPhone" data-bind="value: customer.Phone" type="text" placeholder="Enter Contact Tel Num" />
                                                        </div>
                                                        @*<div class="form-group col-md-3">
                            <label for="address">ALTERNATIVE CONTACT TEL NO</label>
                            <input class="selectMAP" type="text" placeholder="Enter Alternative Contact Tel Num" />
                        </div>*@
                                                        <div class="form-group col-md-4">
                                                            <label for="address">E-mail<span>*</span></label>
                                                            <input class="selectMAP" id="MapEmail" data-bind="value: customer.Email" type="text" placeholder="Enter Email" />
                                                        </div>
                                                    </div>
                                                </fieldset>


                                                <fieldset>
                                                    <legend><h4>Other Information</h4></legend>
                                                    <div class="row">
                                                        <div class="form-group col-md-12">
                                                            <label for="address">Address at Which Meter is Required<span>*</span></label>
                                                            <textarea id="MapAddress1" data-bind="value: customer.MeterInstallationAddress" class="selectMAP" cols="5" placeholder="Enter Address at which Meter is Required"></textarea>

                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-md-4">
                                                            <label for="state">State<span>*</span></label>
                                                            <input data-bind="value: customer.State" class="selectMAP" type="text" placeholder="Enter State" />

                                                            @*<select class="selectMAP m-b" style="width: 100%" data-bind="options: StateList, optionsText: 'name', optionValue: 'id', value: selectedState, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: 'Select State'"></select>*@

                                                            @*<input data-bind="value: customer.State" class="selectMAP" type="text" placeholder="Enter LGA" />*@
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label for="lga">LGA<span>*</span></label>
                                                            <input data-bind="value: customer.LGA" class="selectMAP" type="text" placeholder="Enter LGA" />
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label for="hn">House Number<span>*</span></label>
                                                            <input data-bind="value: customer.HouseNo" class="selectMAP" type="text" placeholder="Enter House Number" />
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="form-group col-md-6">
                                                            <label for="bs">Bus Stop<span>*</span></label>
                                                            <input data-bind="value: customer.BusStop" class="selectMAP" type="text" placeholder="Enter Bus Stop" />
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label for="lm">Landmark<span>*</span></label>
                                                            <input data-bind="value: customer.Landmark" class="selectMAP" type="text" placeholder="Enter Landmark" />
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-md-12">
                                                            <label for="paddress">Present Residential Address<span>*</span></label>
                                                            <textarea id="MapAddress2" data-bind="value: customer.CustomerAddress" class="selectMAP" cols="8" placeholder="Enter present Residential Address at which Meter is Required"></textarea>

                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-md-3">
                                                            <label for="tpremises">Type of the Premises<span>*</span></label>
                                                            <select class="selectMAP m-b" style="width: 100%" data-bind="options: TypePremisesList, optionsText: 'TypePremisesName', optionValue: 'TypePremisesValue', value: selectedTypePremises, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: 'Select Type of Premises'"></select>

                                                            @*<select data-bind="value: customer.TypePremises" class="selectMAP">
                                <option value=""></option>
                                <option value="2BD_Room_Flat">2BD Room Flat</option>
                                <option value="3BD_Room_Flat">3BD Room Flat</option>
                                <option value="Single_Room">Single Room</option>
                                <option value="Boys_Qtrs">Boys Qtrs.</option>
                                <option value="Shop">Shop</option>
                                <option value="Block_of_Flats">Block of Flats</option>
                                <option value="Duplex">Duplex</option>
                                <option value="Other">Others</option>
                            </select>*@
                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label for="upremises">Use of the Premises<span>*</span></label>
                                                            <select class="selectMAP m-b" style="width: 100%" data-bind="options: UsePremisesList, optionsText: 'UsePremisesName', optionValue: 'UsePremisesValue', value: selectedUsePremises, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: 'Select Use of Premises'"></select>

                                                            @*<select data-bind="value: customer.UsePremises" class="selectMAP">
                                <option value="">Select Use of Premises</option>
                                <option value="Residential">Residential</option>
                                <option value="Commercial ">Commercial </option>
                                <option value="Special">Special</option>
                                <option value="Industrial">Industrial</option>
                            </select>*@
                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label for="an">Account Number<span>*</span></label>
                                                            <input data-bind="value: customer.AccountNo" disabled id="MapAccountNo" class="selectMAP" type="text" placeholder="Enter Account Number" />
                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label for="mn">Meter Number<span>*</span></label>
                                                            <input data-bind="value: customer.MeterNo" id="MapMeterNo" class="selectMAP" type="text" placeholder="Enter Meter Number" />
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="form-group col-md-3">
                                                            <label for="vmi">ID Card<span>*</span></label>
                                                            <select class="selectMAP m-b" style="width: 100%" data-bind="options: IDcardList, optionsText: 'IDcardName', optionValue: 'IDcardValue', value: selectedIDcard, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: 'Select Valid Means Of Identification'"></select>
                                                            @*<select data-bind="value: customer.IDcard" class="selectMAP">
                                <option value="">Select Valid Means Of Identification</option>
                                <option value="International_Passport ">International Passport </option>
                                <option value="Drivers_License">Driver’s License</option>
                                <option value="NAT_ID">National ID</option>
                                <option value="PVC">PVC</option>
                            </select>*@
                                                        </div>

                                                        <div class="col-md-3">
                                                            <label for="tmr">IDCard No<span>*</span></label>
                                                            <input data-bind="value: customer.IDCardNo" id="IDCardNo" class="selectMAP" type="text" placeholder="Enter IDCard Number" />

                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label for="tmr">Type of Meter Requested<span>*</span></label>
                                                            <select class="selectMAP m-b" style="width: 100%" data-bind="options: TypeMeterRequestedList, optionsText: 'TypeMeterRequestedName', optionValue: 'TypeMeterRequestedValue', value: selectedTypeMeterRequested, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: 'Select Type Of Meter Requested'"></select>
                                                            @*<select data-bind="value: customer.TypeMeterRequested" class="selectMAP">
                                <option value="">Select Type Of Meter Requested</option>
                                <option value="SINGLE_PHASE">SINGLE PHASE</option>
                                <option value="THREE_PHASE">THREE PHASE</option>
                            </select>*@
                                                        </div>

                                                        <div class="form-group col-md-3">
                                                            <label for="mop">Mode of Payment<span>*</span></label>
                                                            <select class="selectMAP m-b" style="width: 100%" data-bind="options: ModePaymentList, optionsText: 'ModePaymentName', optionValue: 'ModePaymentValue', value: selectedModePayment, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: 'Select Mode of Payment', event: { change: SchedulePaymentEvent }"></select>

                                                        </div>

                                                        <div class="form-group col-md-12" data-bind="visible: IsMSC">
                                                            <label for="mop">Payment Schedule<span>*</span></label>
                                                            <select class="selectMAP m-b" style="width: 100%" data-bind="options: PaymentScheduleList, optionsText: 'PaymentScheduleName', optionValue: 'PaymentScheduleValue', value: selectedPaymentSchedule, enable: !isProcessing(), valueUpdate: 'change', optionsCaption: 'Select Schedule of Payment'"></select>
                                                        </div>
                                                    </div>

                                                </fieldset>


                                                <fieldset>
                                                    <legend>Agreement</legend>

                                                    <label>
                                                        I/WE HEREBY REQUEST THE PORT HARCOURT ELECTRICITY DISTRIBUTION PLC TO SUPPLY US WITH METER AT THE ADDRESS STATED ABOVE FOR THE USE OF PREMISES STATED ABOVE AND
                                                        AGREE TO PAY ALL CHARGES REQUIRED BY PORT HARCOURT ELECTRICITY DISTRIBUTION PLC IN ACCORDANCE WITH THE PREVAILING METER ASSET PROVIDER REGULATIONS, 2018 (MAP) ISSUED BY NIGERIAN ELECTRICITY REGULATORY COMMISSION (NERC).
                                                        I/WE CONFIRM THAT THE INFORMATION GIVEN IN THE FORM ABOVE IS TRUE IN IT'S ENTIRETY AND AGREE THAT IF ANY PART OF IT IS FOUND TO BE UNTRUE THE ELECTRICITY SUPPLY MAY BE DISCONTINUED.

                                                    </label>
                                                    <input type="checkbox" title="SHA" data-bind="checked: IAgree" /><span>Do you Accept Our Term and Condition?</span>
                                                </fieldset>

                                                <hr />

                                                <div class="row single-sidebar">


                                                    <ul class="mypopular-tag">
                                                        <li data-bind="ifnot: IAgree"><button disabled class="btn-one MAPButton-disable" type="button">Submit my Application Form</button></li>
                                                        <li data-bind="if: IAgree"><button class="btn-one MAPButton" type="button" data-bind="    click: save">Submit my Application Form</button></li>
                                                        @*<li><button class="btn-one MAPButton" onclick="PayOffArrears()" type="button">I want to Pay off my arrears Now</button></li>
                        <li><button class="btn-one MAPButton" type="button">I want to pay off my arrears Installmentally Every Month</button></li>*@
                                                    </ul>

                                                </div>
                                            </form>

                                        </div>


                                    

                                        <div class="text-holder" id="BRC" style="display:none">
                                            <h3 class="blog-title">Your Guide to the Bill Revision Camp</h3>
                                            <div class="meta-box">
                                                @*<span class="thm-clr">Post Info</span>*@
                                                <ul class="meta-info">
                                                    <li><span class="thm-clr">BRC Id:</span> <span id="BRC_ID2"></span></li>
                                                    <li><span class="thm-clr">Ticket Id:</span> <a href="#"><span id="TicketID2"></span></a></li>
                                                    <li><span class="thm-clr">Account Name:</span> <a href="#"><span id="AccountName32"></span></a></li>
                                                    <li><span class="thm-clr">Account No:</span> <a href="#"><span id="AccountNo32"></span></a></li>
                                                </ul>
                                            </div>
                                            <div class="text" style="color:black !important">
                                                <p class="mar-bottom">Welcome to the PHED Bill Revision Excercise. This is an excercise geared towards reviewing your estimated Bills and giving you a fairly priced bill that is comeasurate to your Consumption. </p>
                                                <p>During the Bill revision Camp, you'll be asked to list an estimte of all you consume in your apartment. This will guide the BRC-Personnel on what best to bill you and how to review your Bill. Your best judgement coupled with integrity would be needed to make this a success.Please note that you may be visited by PHED to verify the authenticity of your information should need arise.</p>
                                            </div>
                                            <!--Start author slogan-->
                                            <div class="author-slogan">
                                                <p>
                                                    The Bill revision Camp closest to you will be taking place on the soon. You can click <a href="../Reports/BRCSchedule.pdf">here</a> to view the Bill Revision Camp Schedule. Please be there from 8:00am to 4:00pm to have your estimated bill revised.
                                                    Call 0809777432 or send an email to BRC@phed.com.ng for more enquiries. To proceed, Please download and Complete your BRC Form.
                                                </p>
                                                <div class="border-box"></div>
                                                <div class="author-info">
                                                    <h3>Download the BRC Schedule <a href="../Reports/BRCSchedule.pdf">here</a></h3>
                                                    <span class="thm-clr">Or call the BRC Help Line: 0809777432</span>
                                                </div>
                                            </div>
                                            <!--End author slogan-->
                                            <!--Start bottom content box-->
                                            <div class="bottom-content-box">

                                                <div class="col-md-12">
                                                    <div class="text-box">



                                                        <div class="row single-sidebar">
                                                            <div class="sec-title">
                                                                <h3>What you can do?</h3>
                                                            </div>

                                                            <ul class="mypopular-tag">
                                                                <li><button class="btn-one MAPButton" onclick="PayOffArrears()" type="button">I want to pay off my Arrears Now!</button></li>
                                                                <li><button class="btn-one MAPButton" type="button" data-bind="click: BRCForm">Yes! Proceed to Bill Revision application Form</button></li>
                                                                <li><button class="btn-one MAPButton" type="button" data-bind="click: ArrearsForm">I want to pay off my arrears Installmentally Every Month</button></li>
                                                            </ul>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <!--End bottom content box-->
                                            <!--Start tag box-->
                                            <div class="tag-box">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="left pull-left">
                                                            <ul>
                                                                <li><a href="#"><i class="fa fa-heart" aria-hidden="true"></i></a></li>
                                                                <li><a href="#">18</a></li>
                                                                <li><a href="#"><i class="fa fa-comments" aria-hidden="true"></i></a></li>
                                                                <li><a href="#">6</a></li>
                                                            </ul>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <!--End tag box-->
                                        </div>

                                     
                                 

                                    </div>
                                    <!--End single blog post-->

                                </div>
                            </div>

                       
                        </div>

       
        
      
        


        <!-- Large modal -->
 
        <div id="ScheduleMAP" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <div class="col-md-10">
                        
                                <div class="single-blog-post">
                                    <div class="text-holder">
                                        <h3 class="blog-title">Important Information for Your METER Installation</h3>
                                        <div class="meta-box">
                                           
                                            <ul class="meta-info">
                                                <li><span class="thm-clr">IBC:</span> <a href="#">Garden City Industrial</a></li>
                                                <li><span class="thm-clr">BSC:</span> <a href="#">Rumuogba</a></li>
                                                <li><span class="thm-clr">Feeder is:</span> <a href="#">11kv Oyigbo Feeder</a></li>
                                            </ul>
                                        </div>
                                        <div class="text">
                                            <p class="mar-bottom">
                                            
                                            Your area has been scheduled according to the MAP-PHED installation schedule for the month of JUNE 2020.
                                            However we've got you covered. If you need a Meter Installed before that time, Kindly go ahead to Pay for a meter and you will have it installed for you in 10 days.
                                            
                                            To pay for a Meter, please select the Upfront Option in the meter </p>
                                           
                                        </div>
                                        <!--Start author slogan-->
                                        <div class="author-slogan">
                                            <p>Your area will be Metered in JUNE 2020, However if you want a meter installed for you in 10 Days, kindly select the UPFRONT Payment Option to pay for a meter now.
                                            </p>
                                            <div class="border-box"></div>
                                            <div class="author-info">
                                                <h3>Meter Asset Provider Sheme</h3>
                                                <span class="thm-clr">PHED</span>
                                            </div>
                                        </div>


                                        <!--End bottom content box-->
                                        <!--Start tag box-->
                                        <div class="tag-box">
                                            <div class="row">
                                                <div class="col-md-12">
                                                  
                                                    <button class="btn-one MAPButton" type="button" data-dismiss="modal">OK! Proceed to Apply</button>
                                                </div>
                                            </div>
                                        </div>
                                        <!--End tag box-->
                                    </div>
                                   
                                </div> 
                    </div>
                </div>
                    
            </div>
        </div>
         


        <div class="text-holder" id="MapApplicationSuccess" runat="server" style="margin-top:-500px">
             
            <table id="main" width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#F4F7FA">
                <tbody>
                    <tr>
                        <td valign="top">
                            <table class="innermain" cellpadding="0" width="580" cellspacing="0" border="0"
                                   bgcolor="#F4F7FA" align="center" style="margin:0 auto; table-layout: fixed;">
                                <tbody>
                                    <!-- START of MAIL Content -->
                                    <tr>
                                        <td colspan="4">
                                            <!-- Logo start here -->
                                            <table class="logo" width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td colspan="2" height="30"></td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="2" height="30"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!-- Logo end here -->
                                            <!-- Main CONTENT -->
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff"
                                                   style="border-radius: 4px; border-top:4px solid #3C90DF; box-shadow: 0 2px 8px rgba(0,0,0,0.05);">
                                                <tbody>
                                                    <tr>
                                                        <td height="40"></td>
                                                    </tr>
                                                    <tr style="font-family: -apple-system,BlinkMacSystemFont,&#39;Segoe UI&#39;,&#39;Roboto&#39;,&#39;Oxygen&#39;,&#39;Ubuntu&#39;,&#39;Cantarell&#39;,&#39;Fira Sans&#39;,&#39;Droid Sans&#39;,&#39;Helvetica Neue&#39;,sans-serif; color:#4E5C6E; font-size:14px; line-height:20px; margin-top:20px;">
                                                        <td class="content" colspan="2" valign="top" align="center" style="padding-left:90px; padding-right:90px;">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff">
                                                                <tbody>
                                                                    <tr>
                                                                        <td height="30" &nbsp;=""></td>
                                                                    </tr>

                                                                    <tr style="height:100px; width:200px">
                                                                        <td align="center" valign="bottom" colspan="2" cellpadding="3">
                                                                            <img alt="Coinbase" width="80" src="http://phedpayments.nepamsonline.com/EmailTemplate/Images/succeed-green.png" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="30" &nbsp;=""></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <div style="font-size: 22px; line-height: 32px; font-weight: 500; margin-left: 20px; margin-right: 20px; margin-bottom: 25px;">Dear <span runat="server" id="MAPApplicantsName"></span>,<b> </b> Your Application for a <span runat="server" id="MAPMeterPhase"></span> Meter  was successful</div>

                                                                            <p style="font-size: 28px; font-weight: 800;"></p>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="24" &nbsp;=""></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="1" bgcolor="#DAE1E9"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="24" &nbsp;=""></td>
                                                                    </tr>



                                                                    <tr>
                                                                        <td>
                                                                            <table style="width: 100%; border-collapse:collapse;">
                                                                                <tbody style="border: 0; padding: 0; margin-top:20px;">
                                                                                    <tr>
                                                                                        <td style=" text-align:center; padding-bottom: 10px; padding-top: 10px;"><h3><b>Ticket Id: <span runat="server" id="MAPTicketId"></span></b></h3></td>
                                                                                       
                                                                        </td>
                                                                    </tr>




                                                                    <!--<tr>
                                                                    <td style="padding-bottom: 10px; padding-top: 10px;"><b>VAT</b></td>
                                                                    <td style="padding-bottom: 10px; padding-top: 10px;">{{VAT}}</td>
                                                                </tr>-->
                                                                 


                                                                    <tr>
                                                                       

                                                                        <td style="text-align:center;  padding-bottom: 10px; padding-top: 10px;">
                                                                            <strong>You will be contacted by the Meter Asset Provider Installation Team, for your Meter Installation according to the MAP installation Schedule, please quote the TicketID in all your transactions with PHED. </strong>
                                                                            <br />
                                                                        </td>
                                                                    </tr>

                                                                

                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="10">&nbsp;</td>
                                                    </tr>

                                                    <tr>
                                                        <td height="24" &nbsp;=""></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="1" bgcolor="#DAE1E9"></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="24" &nbsp;=""></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="color:#48545d; font-size:14px; line-height:24px; text-align:center;">

                                                                <p>
                                                                     
                                                                    <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="P()"> Download Completed Application Form</button>

                                                                </p>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="60"></td>
                                    </tr>
                                </tbody>
                            </table>

                        </td>
                    </tr>
                </tbody>
            </table>
















        </div>

 


        <div class="text-holder" runat="server" id="MapApplicationDone" style="display:none"  >

            <div class="col-md-10 offset-md-1">

                <div class="single-blog-post">
                    <div class="text-holder">
                        <h3 class="blog-title">Your Map Application was Successful</h3>
                       
                        <div class="text">
                            <p class="mar-bottom"> 
                                Kindly go ahead to Pay for a meter and you will have it installed for you in 10 days.
                                To pay for a Meter, please select the Upfront Option in the meter
                            </p>

                        </div>
                        <!--Start author slogan-->
                        <div class="author-slogan">
                            <p>
                              Remember you need to Pay for the Meter before it's Installed for you.
                            </p>
                            <div class="border-box"></div>
                            <div class="author-info">
                                <h3>Meter Asset Provider Sheme</h3>
                                <span class="thm-clr">PHED</span>
                            </div>
                        </div>
                         
                        <!--End bottom content box-->
                        <!--Start tag box-->
                        <div class="tag-box">
                            <div class="row">
                                <div class="col-md-12">

                                    <button class="btn-one MAPButton" type="button" data-dismiss="modal">Download Completed Application Form</button>
                                </div>
                            </div>
                        </div>
                        <!--End tag box-->
                    </div>

                </div>
            </div>

        </div>

  
            <div style="margin-top:-30px;" id="MapApplicationPayment" runat="server">
              
                  <table id="main" width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#F4F7FA">
                    <tbody>
                        <tr>
                            <td valign="top">
                                <tab class="innermain" cellpadding="0" width="580" cellspacing="0" border="0"
                                       bgcolor="#F4F7FA" align="center" style="margin:0 auto; table-layout: fixed;">
                                    <tbody>
                                        <!-- START of MAIL Content -->
                                        <tr>
                                            <td colspan="4">
                                                <!-- Logo start here -->
                                                <table class="logo" width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="2" height="30"></td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="2" height="30"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <!-- Logo end here -->
                                                <!-- Main CONTENT -->
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff"
                                                       style="border-radius: 4px; border-top:4px solid #3C90DF; box-shadow: 0 2px 8px rgba(0,0,0,0.05);">
                                                    <tbody>
                                                        <tr>
                                                            <td height="40"></td>
                                                        </tr>
                                                        <tr style="font-family: -apple-system,BlinkMacSystemFont,&#39;Segoe UI&#39;,&#39;Roboto&#39;,&#39;Oxygen&#39;,&#39;Ubuntu&#39;,&#39;Cantarell&#39;,&#39;Fira Sans&#39;,&#39;Droid Sans&#39;,&#39;Helvetica Neue&#39;,sans-serif; color:#4E5C6E; font-size:14px; line-height:20px; margin-top:20px;">
                                                            <td class="content" colspan="2" valign="top" align="center" style="padding-left:90px; padding-right:90px;">
                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td height="30" &nbsp;=""></td>
                                                                        </tr>

                                                                        <tr style="height:100px; width:200px">
                                                                            <td align="center" valign="bottom" colspan="2" cellpadding="3">
                                                                                <img alt="Coinbase" width="80" src="http://phedpayments.nepamsonline.com/EmailTemplate/Images/succeed-green.png" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="30" &nbsp;=""></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div style="font-size: 22px; line-height: 32px; font-weight: 500; margin-left: 20px; margin-right: 20px; margin-bottom: 25px;">Dear <span id="ApplicantsName1" runat="server"></span>,<b> </b> Your Application for a <span id="MAPMeterType1" runat="server"></span>  Meter  has been approved for Payment.</div>

                                                                                <p style="font-size: 28px; font-weight: 800;"></p>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="24" &nbsp;=""></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="1" bgcolor="#DAE1E9"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="24" &nbsp;=""></td>
                                                                        </tr>



                                                                        <tr>
                                                                            <td>
                                                                                <table style="width: 100%; border-collapse:collapse;">
                                                                                    <tbody style="border: 0; padding: 0; margin-top:20px;">
                                                                                        <tr>

                                                                                            <td style=" text-align:center; padding-bottom: 10px; padding-top: 10px;"><h3><b>Amount: ₦<span id="MAPAmount" runat="server"></span></b></h3></td>
                                                                                        </tr>
                                                                                        <tr>

                                                                                            <td style=" text-align:center; padding-bottom: 10px; padding-top: 10px;"><h3><b>Trans Ref: <span id="MAPTransRef" runat="server"></span></b></h3></td>
                                                                                        </tr>
                                                                            </td>
                                                                        </tr>
                                                                         

                                                                        <tr>
                                                                           
                                                                            <td style="text-align:center;  padding-bottom: 10px; padding-top: 10px;">
                                                                                <strong>Your Application has been approved for Payment.Please click on the button to pay for the Meter.Quote the TicketID in all your transactions with PHED. </strong>
                                                                                <br />
                                                                            </td>
                                                                        </tr>

                                                                        
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="10">&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td height="24" &nbsp;=""></td>
                                                        </tr>
                                                        <tr>
                                                            <td height="1" bgcolor="#DAE1E9"></td>
                                                        </tr>
                                                        <tr>
                                                            <td height="24" &nbsp;=""></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="color:#48545d; font-size:14px; line-height:24px; text-align:center;">

                                                                    <p>
                                                                         
                                                                        <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="Pay()">   Proceed To Pay</button>


                                                                    </p>
                                                                    <hr />
                                                                     <p>
 
                                                                    <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="P()"> Download Completed Application Form</button>

                                                                </p>
                                                                </div>
                                                            </td>
                                                        </tr>




                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="60">

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                             
            </div>
        </div>
    
                 
    
                </div> 
    <br />
    <br />
    <br />
    <div class="clearfix"></div>


    <!-- Inner Page Wrapper End -->
    <!-- Footer Wrapper Start -->

    <!-- Copyright Wrapper End -->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
 

    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" rel="stylesheet" />
  
                <script src="assets/jquery/jquery-3.1.1.min.js"></script>
                <link href="Scripts/sweetalert2-master/dist/sweetalert.css" rel="stylesheet" />
                <script src="Scripts/sweetalert2-master/dist/sweetalert.js"></script>
                <script src="Scripts/knockout-3.4.2.debug.js"></script>
                <script src="Scripts/knockout-3.4.2.js"></script>
                <script src="Scripts/KnockOutMapping.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.js"></script>
               
                <script src="Scripts/References/RegisterCustomer.js"></script>

                <script>

                    function Pay() {

                        //Set the payment in Motion

                        swal({
                            title: "Loading...",
                            text: "Please wait",
                            imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',

                            showConfirmButton: false,
                            allowOutsideClick: false
                        });

                        document.forms['XpressPay'].submit();

                    }

                    function P() {
                        //var TicketID = $("#MAPTicketId").val();
                        var TicketID = $("#TicketId45").val();
                        var ReportViewer = "http://map.nepamsonline.com/ReportPage.aspx?";
                        //Gor the aspx File for the Download of the File fo rthe Application

                        //var url =  ReportViewer+"TicketId="+TicketID+"&TYPE=APPLICATIONFORM";
                        var url = ReportViewer + "TicketId=" + TicketID + "&TYPE=APPLICATIONFORM";
                        window.open(url, '_blank');

                    }

                    function PayOffArrears() {
                        swal({
                            title: "Loading...",
                            text: "Please wait",
                            imageUrl: 'http://phedpayments.nepamsonline.com/images/loader4.gif',

                            showConfirmButton: false,
                            allowOutsideClick: false
                        });

                        document.forms['XpressPay'].submit();
                    }

                    //function PayOffArrears() {

                    //};

                </script>
                <script type="text/javascript">

                    $(document).ready(function () {

                        //var AllOptionItemViewModel;
                        $.ajax({
                            url: '/MAPRegister/CreateCustomer',
                            type: 'GET',
                            dataType: 'JSON',
                            success: function (result) {
                                var data = JSON.parse(result);
                                console.log("Data From Server " + JSON.stringify(data));
                                //AllOptionItemViewModel = new OptionItemViewModel(data);

                                //AllOptionItemViewModel.isProcessing(false);

                                ko.applyBindings(new RegisterCustomerViewModel(data));

                            },
                            error: function (result) {
                                alert("Failed" + result.responseText);
                            }
                        });


                    });

                </script>



</body>
</html>





















































 
<style>
    .MAPButton {
        background: #383b74 none repeat scroll 0 0;
        border: 1px solid #383b74 !important;
        display: block;
        font-size: 17px;
        font-weight: 400;
        height: 50px;
        padding: 0 15px;
        width: 100%;
        margin-bottom: 14px;
        transition: all 500ms ease;
        color:white;
    }
    .MAPButton-disable {
        background: #5b5c6b none repeat scroll 0 0;
        border: 1px solid #585961 !important;
        display: block;
        font-size: 17px;
        font-weight: 400;
        height: 50px;
        padding: 0 15px;
        width: 100%;
        margin-bottom: 14px;
        transition: all 500ms ease;
        color: white;
    }
     .MapButton:hover {
            background: #fff none repeat scroll 0 0;
            border: 1px solid #383b74 !important;
            display: block;
            font-size: 17px;
            font-weight: 400;
            height: 50px;
            padding: 0 15px;
            width: 100%;
            margin-bottom: 14px;
            transition: all 500ms ease;
            color: #383b74;
        }
    .selectMAP {
        background: #fff none repeat scroll 0 0;
        border: 1px solid #2b3363 !important;
        display: block;
        font-size: 18px;
        font-weight: 400;
        height: 50px;
        padding: 0 15px;
        width: 100%;
        margin-bottom: 14px;
        transition: all 500ms ease;
    }
    .selectMAP2 {
        background: #fff none repeat scroll 0 0;
        border: 1px solid #2b3363 !important;
        
        transition: all 500ms ease;
    }
</style>


