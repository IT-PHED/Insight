<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="ChangeMeterType.aspx.cs" Inherits="PHEDServe.Views.Home.ChangeMeterType" %>
 
<!DOCTYPE html>

 
<head runat="server">
    <title></title>

    <link href="css/style2.css" rel="stylesheet" />
     <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- Font Awesome CSS-->
    <link href="assets/font-awesome/css/font-awesome.min.css" rel="stylesheet"/>
    <!-- Linear icons CSS-->
    <link href="assets/linearicons/css/icon-font.min.css" rel="stylesheet"/>
    <!-- Slick Nav CSS -->
    <link rel="stylesheet" href="assets/css/slicknav.min.css"/>
    <!-- Owl Carousel -->
    <link href="assets/owl-carousel/css/owl.carousel.css" rel="stylesheet"/>

    <link href="assets/owl-carousel/css/owl.theme.css" rel="stylesheet"/>

    <link href="css/style.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="css/style2.css" rel="stylesheet"/>
    <!-- Animate CSS -->
    <link href="assets/animate/animate.css" rel="stylesheet"/>
    <!-- Favicon -->
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico"/>
   
    
<link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-sweetalert/1.0.1/sweetalert.css" rel="stylesheet" />
 

     <style>
    /*.MAPButton {
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
        }*/
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



</head>




    <body>

<form id="form001" runat="server">
      
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
                            <div class="book"><a href="@Url.Action("BRC", "MAPRegister")"><i class="fa fa-globe" aria-hidden="true"></i><span>Get yours Now.</span></a></div>
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
                                    <li class="active"><a href="Home/index">Home</a></li>
                                    <li><a href="MAPRegister/CheckApplicationStatus">Check Application Status</a></li>
                                    <li><a href="MAPRegister/UploadPayment">Upload Payment</a></li>
                                    <li><a href="MAPRegister/ChangeMeter">Change Meter Type</a></li>
                                    <li><a href="Account/Login">Login</a></li>
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
                <li><a href="/Home/Index"><i class="fa fa-home"></i>Home</a></li>
                <li><i class="fa fa-angle-right" aria-hidden="true"></i></li>
                <li>MAP</li>
                <li><i class="fa fa-angle-right" aria-hidden="true"></i></li>
                <li>MAP Application</li>
            </ul>
        </div>
    </div>
  
 
 
          <div>
            <div class="col-md-10 col-md-offset-1">
                <div class="blog-post">

                    <!--Start single blog post-->
                    <div class="single-blog-post">
                        <!--Start add comment box-->
                        <div class="add-comment-box" id="1">
                            <br />
                            <br />
                            <div class="title">
                                <h3>Upgrade Meter From 1 Phase to 3Phase</h3>
                                <hr />
                            </div>
                            
        <div class="row">
            <div class="col-md-10 col-md-offset-1">
                <div class="blog-post">
                    <!--Start single blog post-->
                    <div class="single-blog-post">
                        <!--Start add comment box-->
                        <div class="add-comment-box" id="1">
                            
                                <div class="row" runat="server" id="TicketInput">
                                    <div class="col-md-12">
                                        <div class="row">

                                            <div class="col-md-12">
                                                <div class="field-label">Input your Account No/Meter Number/Ticket Id*</div>
                                             
                                                <asp:TextBox ID="TextBox1"  CssClass="selectMAP" runat="server"></asp:TextBox>
                                                
                                                  </div>

                                        </div>
                                   

                                        <div class="row" style="float:right">
                                            <div class="col-md-12 pull-right">

                                             
                                                

                                                  <asp:Button ID="Button1" style="height:50px;width:245px; border:none; color:white; background-color:#13325c" Class="btn-one thm-bg-clr" runat="server" Text="View My Details"  OnClick="Button1_Click"/>
                                              
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            
                        </div>
                        <!--End add comment box-->
                    </div>
                    <!--End single blog post-->
                </div>
            </div>

            <!--End Sidebar Wrapper-->
        </div>


                             
                                <div class="row" id="TicketDetails" runat="server"  >
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="field-label">Ticket ID*</div>
                                                <asp:TextBox ID="TicketIDTextBox2" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>
                                           
                                             <div class="col-md-3">
                                                <div class="field-label">Account No</div>
                                                <asp:TextBox ID="AccountNoTextBox3" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>

                                             <div class="col-md-3">
                                                <div class="field-label">Account Name</div>
                                                <asp:TextBox ID="AccountNameTextBox3" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>
                                             <div class="col-md-3">
                                                <div class="field-label">Applicant Name</div>
                                                <asp:TextBox ID="ApplicantNameTextBox3" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>


                                              <div class="col-md-3">
                                                <div class="field-label">Zone</div>
                                                <asp:TextBox ID="ZoneTextBox3" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>
                                            
                                            <div class="col-md-3">
                                                <div class="field-label">Feeder</div>
                                                <asp:TextBox ID="FeederTextBox3" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>



                                              <div class="col-md-3">
                                                <div class="field-label">Transformer*</div>
                                                <asp:TextBox ID="TransformerTextBox2" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="field-label">PaymentPlan*</div>
                                                <asp:TextBox ID="MAPPlanTextBox2" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                            </div>

                                          

 
                                           
                                             
                                           
                                            
                                            <div class="col-md-3">
                                                <div class="field-label">Phase*</div>

                                                   <asp:TextBox ID="MAPPhaseTextBox2" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                               </div>

                                             <div class="col-md-3">
                                                <div class="field-label">Amount Payable*</div>

                                                  <asp:TextBox ID="MAPAmountPhaseNoUpload" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                              </div>

                                            <div class="col-md-3">
                                                <div class="field-label">Upfront Amount*</div>

                                                     <asp:TextBox ID="UpfrontAmountPhaseNoUpload" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                           
                                               
                                            </div>

                                            <div class="col-md-3">
                                                <div class="field-label">Previous Balance to be Paid</div>

                                              <asp:TextBox ID="BalanceTobePaidAddress2" disabled="disabled" CssClass="selectMAP" runat="server"></asp:TextBox>
                                           
                                            </div>
                                             <div class="col-md-3">
                                                <div class="field-label"> Application Status</div>

                                              <asp:TextBox ID="StatusPaidAddress2" disabled="disabled"  CssClass="selectMAP" runat="server"></asp:TextBox>
                                           
                                            </div>

                                             <div class="col-md-3">
                                                <div class="field-label"> Payment Status</div>

                                              <asp:TextBox ID="PaymentStatusMeterPhaseNoUpload" disabled="disabled"  CssClass="selectMAP" runat="server"></asp:TextBox>
                                           
                                            </div> 



                                            <div class="col-md-3">
                                                <div class="field-label">New Meter Amount</div>

                                              <asp:TextBox ID="NewMeterAmount" CssClass="selectMAP" disabled="disabled"  runat="server"></asp:TextBox>
                                           
                                            </div>

                                            
                                            <div class="col-md-3">
                                                <div class="field-label"> New Amount To Pay</div>

                                              <asp:TextBox ID="NewAmountUpgradeAmount"   CssClass="selectMAP" runat="server"></asp:TextBox>
                                           
                                            </div>
                                             
                                        </div>
                                       
                                        <div class="row" style="float:right">
                                            <div class="col-md-12 pull-right">
                                                <asp:Button ID="Button2" style="height:50px;width:245px; border:none; color:white; background-color:#13325c" Class="btn-one thm-bg-clr" runat="server" Text="Proceed with Meter Upgrade" OnClick="Button2_Click" />
                                              
                                            </div>
                                        </div>
                                                
    <br />
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        <br /><br />

                                    </div>
                                </div>
                            


                             
        <div class="card-body" id="InfoPanel" runat="server"  >
            <div class="col-lg-12 ">
                <div class="panel registration-form">

                    <div class="panel-body">

                        <div class="row">


                            <div class="col-md-12 text-center">
                             
                                 
                                <img src="../images/info.png" style="height:100px;width:100px" />
                             
                                <br />
                                <br />
                                <h3>Information! </h3>
                                <h4>
                                    <asp:Label ID="InfoLabel1" runat="server" Text="Label"></asp:Label>
                                 
                                </h4>
                                <br /><br />
                                <h4>
                                     
                                     <asp:Button ID="Button4" style="height:50px;width:245px; border:none; color:white; background-color:#13325c" Class="btn-one thm-bg-clr" runat="server" Text="OK!" OnClick="Button4_Click" />
                                              
                                    <br />
                                 
                                </h4>
                            </div>

                            <br />

                        </div>
                    </div>

                </div>

            </div>

        </div>

     
       






                        </div>
                        <!--End add comment box-->


                         
                    </div>
                    <!--End single blog post-->

                </div>
            </div>

            <!--Start sidebar Wrapper-->
           
        </div>




        
    <br /><br /><br />
    <div class="clearfix"></div>
         
    <script src="assets/jquery/jquery-3.1.1.min.js"></script>
    <script src="assets/jquery/plugins.js"></script>
    <script src="assets/jquery/slicknav.min.js"></script>
    <script src="assets/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/number-animation/jquery.animateNumber.min.js"></script>
    <script src="assets/owl-carousel/js/owl.carousel.js"></script>
    <script src="assets/jquery/jquery.countdown.min.js"></script>
    <script src="assets/wow/wow.min.js"></script>
    <script src="assets/jquery/slider.js"></script>
    <script src="js/custom.js"></script>
        </form>  
</body>









 


  <script src="assets/jquery/jquery-3.1.1.min.js"></script>
                <link href="Scripts/sweetalert2-master/dist/sweetalert.css" rel="stylesheet" />
                <script src="Scripts/sweetalert2-master/dist/sweetalert.js"></script>