<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MSCSuccessUpfront.aspx.cs" Inherits="PHEDServe.MSCSuccessUpfront" %>

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
                                                                            <div style="font-size: 22px; line-height: 32px; font-weight: 500; margin-left: 20px; margin-right: 20px; margin-bottom: 25px;">
                                                                                 
                                                                                 
                                                                            </div>

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

                                                                     

                                                                    <tr>
                                                                        
                                                                        <td style="text-align:center;  padding-bottom: 10px; padding-top: 10px;">
                                                                            <strong> 
                                                                                <span id="InnerInfo1" runat="server"></span>
                                                                            </strong>
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
         
        <div style="width:100%">

            <div style="margin-left:200px">
                
            <div style="margin-top:-30px;  text-align:center " id="MapApplicationPayment" runat="server" >
              
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
                                                <table width="750px" cellpadding="0" cellspacing="0" border="0" bgcolor="#ffffff"
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
                                                                                <div style="font-size: 22px; line-height: 32px; font-weight: 500; margin-left: 20px; margin-right: 20px; margin-bottom: 25px;"><span runat="server" id="BiggerInfo"></span></div>

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
                                                                            <strong>

                                                                                <span id="InnerInfo" runat="server"></span>

                                                                                 
                                               
                                                                            </strong>
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
 
                                                                    <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="P()"> Download Completed Application Form</button>

                                                                </p>
                                                                    <hr />
                                                                    <p>
                                                                         
                                                                        <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="Pay()">   Pay with your Debit/Credit Card</button>
                                                                         
                                                                    </p>
                                                                    <hr />

                                                                    <p>
                                                                         
                                                                        <button class="MAPButton" style="height:60px; cursor:pointer; width:100%;  color:white; border:none; border-radius:10px" onclick="PayBank()">   Pay At the Bank</button>


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

        
        
        
         </div>
    
                 <div class="form-group">
                        <div class="col-md-8 col-md-offset-2">
                            <form id="XpressPay" action="https://xpresspayonline.com:8000/xp-gateway/v2" method="post">
                                <input type="hidden" style="color:black" value="<%=ConfigSource.tranx_amt %>" name="amount" id="amount" />
                                <input type="hidden" style="color:black" value="map.nepamsonline.com/successPage.aspx" id="callback-url" name="callback-url" />
                                <input type="hidden" style="color:black" value="NGN" name="currency" id="currency" />
                                <input type="hidden" style="color:black" value="<%=ConfigSource.Email %>" name="customer-email" id="customer-email" />
                                <input type="hidden" style="color:black" value="<%=ConfigSource.hash %>" name="hash" id="hash" />
                                <input type="hidden" style="color:black" value="SHA256" name="hash-type" id="hash-type" />
                                <input type="hidden" style="color:black" value="<%=_MerchantID%>" name="merchant-id" id="merchant-id" />
                                <input type="hidden" style="color:black" value="<%=ConfigSource.tranx_memo %>" name="product-desc" id="product-desc" />
                                <input type="hidden" style="color:black" value="<%=ConfigSource.ProductId %>" name="product-id" id="product-id" />
                                <input type="hidden" style="color:black" value="<%=ConfigSource.PublicKey %>" name="public-key" id="public-key" />
                                <input type="hidden" style="color:black" value="<%=ConfigSource.tranx_id %>" name="trans-id" id="trans-id" />
                            </form>
                        </div>
                 </div>  
    
                </div> 
    <br />
    <br />
    <br />
    <div class="clearfix"></div>

 
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

                    function PayBank() {
                        //Set the payment in Motion 
                        var TicketID = $("#TicketId45").val();
                        var ReportViewer = "http://map.nepamsonline.com/ReportPage.aspx?";
                        //Gor the aspx File for the Download of the File fo rthe Application 
                        //var url =  ReportViewer+"TicketId="+TicketID+"&TYPE=APPLICATIONFORM";
                        var url = ReportViewer + "TicketId=" + TicketID + "&TYPE=PAYBANKMAP";
                        window.open(url, '_blank');
                    }


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


