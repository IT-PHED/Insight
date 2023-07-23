<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="successPage.aspx.cs" Inherits="PHEDServe.successPage" %>

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
                        <div class="logo"> <a href="index.html"><img src="images/logo.png" alt="#" data-popupalt-original-title="null" title="#"></a> </div>
                        <div class="mobile-menu"></div>
                    </div>
                    <div class="col-lg-9 col-md-9">
                        <nav class="navbar navbar-default desktop">
                            <div class="navbar-collapse">
                                <!-- Main Menu -->
                                <ul id="nav" class="nav menu navbar-nav">
                                    <li class="active"><a href="@Url.Action("Index","Home")">Home</a></li>
                                    <li><a href="@Url.Action("CheckApplicationStatus", "MAPRegister")">Check Application Status</a></li>
                                    <li><a href="@Url.Action("UploadPayment", "MAPRegister")">Upload Payment</a></li>
                                    <li><a href="@Url.Action("Login","Account")">Login</a></li>
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

   

            <div style="margin-top:-30px; " id="MapApplicationPayment" runat="server">
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
                                                                                <div style="font-size: 22px; line-height: 32px; font-weight: 500; margin-left: 20px; margin-right: 20px; margin-bottom: 25px;">Dear <span id="ApplicantsName1" runat="server"></span>,<b> </b> Your Payment <%--<span id="MAPMeterType1" runat="server"></span>--%>  was Successful.</div>

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
                                                                                <strong>Your payment  was Successful.Click on the button to preview and print your receipt as a proof of payment.Kindly Quote the TicketID in all your transactions with PHED. </strong>
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

                         
                                                                    <hr />
                                                                     <p>
 
                                                                    <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="P()"> View Receipt</button>

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

          <div style="margin-top:-30px; " id="ErrorDiv" runat="server">
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
                                                                                <img alt="Coinbase" width="80" src="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRgDbyePfsv6y0qgPVvmfmwGmJKMv_LyOID3NWEgbEbHikxWOQB" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="30" &nbsp;=""></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <div style="font-size: 22px; line-height: 32px; font-weight: 500; margin-left: 20px; margin-right: 20px; margin-bottom: 25px;">Dear <span id="Span1" runat="server"></span>,<b> </b> Your Payment <%--<span id="MAPMeterType1" runat="server"></span>--%>  was NOT Successful.</div>

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

                                                                                            <td style=" text-align:center; padding-bottom: 10px; padding-top: 10px;"><h3><b<span id="Span2" runat="server"></span></b></h3></td>
                                                                                        </tr>
                                                                                        <tr>

                                                                                            <td style=" text-align:center; padding-bottom: 10px; padding-top: 10px;"><h3><b> <span id="Span3" runat="server"></span></b></h3></td>
                                                                                        </tr>
                                                                            </td>
                                                                        </tr>
                                                                         

                                                                        <tr>
                                                                           
                                                                            <td style="text-align:center;  padding-bottom: 10px; padding-top: 10px;">
                                                                                <strong>Your payment  was NOT Successful.Click on the button to Try Again.Kindly Quote the TicketID in all your transactions with PHED. </strong>
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

                         
                                                                    <hr />
                                                                     <p>
 
                                                                    <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="w()"> Try Again</button>

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

        </div>
      
                </div>

     
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
                        var ReportViewer = "http://localhost:14996/ReportPage.aspx?";
                        //Gor the aspx File for the Download of the File fo rthe Application

                        //var url =  ReportViewer+"TicketId="+TicketID+"&TYPE=APPLICATIONFORM";
                        var url = ReportViewer + "TicketId=" + TicketID + "&TYPE=RECEIPT";
                        window.open(url, '_blank');

                    }
                    function w() {
                        //var TicketID = $("#MAPTicketId").val();
                        var TicketID = $("#TicketId45").val();
                        var urls = "http://map.nepamsonline.com/MAPRegister/BRC";
                        //Gor the aspx File for the Download of the File fo rthe Application

                        //var url =  ReportViewer+"TicketId="+TicketID+"&TYPE=APPLICATIONFORM";

                        window.open(urls, '_blank');

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
            <%--    <script type="text/javascript">

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

                </script>--%>



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


