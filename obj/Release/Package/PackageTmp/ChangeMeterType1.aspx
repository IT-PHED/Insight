<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeMeterType1.aspx.cs" Inherits="PHEDServe.ChangeMeterType1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                          
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">

                                            <div class="col-md-12">
                                                <div class="field-label">Input your Account No/Meter Number/Ticket Id*</div>
                                             
                                                <asp:TextBox ID="TextBox1" CssClass="selectMAP" runat="server"></asp:TextBox>
                                                
                                                  </div>

                                        </div>
                                   

                                        <div class="row" style="float:right">
                                            <div class="col-md-12 pull-right">

                                                <asp:Button ID="Button1" runat="server" class="btn-one thm-bg-clr" Text="View My Details" OnClick="Button1_Click" />
                                                
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


                             
                                <div class="row" style="display:none">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="field-label">Select Bank*</div>
                                                <select name="AccountpaidFrom" class="selectMAP" id="inputGroupSelect01" data-live-search="true" data-live-search-placeholder="Search" data-actions-box="true" data-bind="options: BankList, selectedOptions: SelectedBankMode, optionsText: 'Name',optionValue: 'Name', value: SelectedBankMode,  valueUpdate:'change',optionsCaption: 'Select Bank' "></select>

                                            </div>
                                            <div class="col-md-6">
                                                <div class="field-label">Ticket ID*</div>
                                                <input name="lname" id="AccountNo" class="selectMAP" data-bind="value:TicketId" placeholder="Please input your Account Number" aria-required="true" type="text">
                                            </div>

                                            <div class="col-md-3">

                                                <div class="field-label">Purpose of Payment*</div>
                                                <select name="AccountpaidFrom" class="selectMAP" id="inputGroupSelect01" data-live-search="true" data-live-search-placeholder="Search" data-actions-box="true" data-bind="options: PaymentPurposeList, selectedOptions: SelectedPaymentMode, optionsText: 'Name',optionValue: 'Name', value: SelectedPaymentMode,  valueUpdate:'change',optionsCaption: 'Select Payment Mode' "></select>

                                            </div>

                                             
                                            <div class="col-md-3">
                                                <div class="field-label">Teller Number*</div>
                                                <input name="lname" id="TellerNoUpload" data-bind="value:TellerNo"  class="selectMAP" value="" placeholder="Please input the Teller Number number" aria-required="true" type="text">
                                            </div>

                                            <div class="col-md-3">
                                                <div class="field-label">Amount Paid</div>

                                                <input name="lname" id="EmailAddress2" class="selectMAP" data-bind="value:TotalAmountPayable" placeholder="Please input the amount paid" aria-required="true" type="text">
                                            </div>

                                            <div class="col-md-3">
                                                <div class="field-label">Date Paid</div>

                                                <input name="lname" id="EmailAddress2" data-bind="value:DatePaid" class="selectMAP" value="" placeholder="Please input your Email Address" aria-required="true" type="date">
                                            </div>

                                            <div class="col-md-12">
                                                <div class="field-label">Select Teller</div>
                                                <input name="lname" id="yourfileID" class="selectMAP" value="" placeholder="Please input your Title, Name Surname and OtherNames" aria-required="true" type="file">
                                            </div>
                                        </div>
                                      

                                        <div class="row" style="float:right">
                                            <div class="col-md-12 pull-right">
                                                <button class="btn-one thm-bg-clr" data-bind="click:UploadAgency" type="button">Upload Details</button>
                                            </div>
                                        </div>
                                                
    <br /><br /><br />

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

    </div>
    </form>
</body>
</html>
