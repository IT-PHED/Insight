<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="PINNACLE.Payments" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <script src="Scripts/jquery-1.10.2.min.js"></script>
 <!--notification js -->
<script src="assets/plugins/notifications/js/lobibox.min.js"></script>
<script src="assets/plugins/notifications/js/notifications.min.js"></script>
      
     <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <title></title>


    <style>

         html, body { margin: 0; padding: 0; }

        
#centeredDiv { margin-right: auto; margin-left: auto; width: 60px; height:60px }

        #outPopUp {
  position: absolute;
  width: 300px;
  height: 200px;
  z-index: 15;
  top: 50%;
  left: 50%;
  margin: -100px 0 0 -150px;
  background: red;
}
    </style>
    <script type="hidden/javascript" src="http://sandbox.interswitchng.com/collections/public/webpay.js"></script>

</head>
 <body  style="background-color:white" >
    

<form id="FirstBank1" action="https://sandbox.interswitchng.com/collections/w/pay" method="post">
<input name="product_id" type="hidden" value="1076" />
<input name="pay_item_id" type="hidden" value="101" />
<input name="amount" type="hidden" value="50000" />
<input name="currency" type="hidden" value="566" />
<input name="site_redirect_url" type="hidden" value="<%=ConfigSource.tranx_noti_url%>" />
<input name="txn_ref" type="hidden" value="<%=ConfigSource.tranx_id%>" />
<input name="cust_id" type="hidden" value="<%=ConfigSource.cust_id%>" />
<input name="hash" type="hidden" value="<%=ConfigSource.hash%>" />
</form>








<%--onload="document.form1.submit()"<form name="form1" action='<%=  ConfigSource.DemoMode ? ConfigSource.DemoUrl : ConfigSource.LiveUrl %>' method="post">
<input type="hidden" name="gtpay_mert_id" value='<%=ConfigSource.mert_id  %>' />
<input type="hidden" name="gtpay_tranx_id" value='<%= ConfigSource.tranx_id %>' />
<input type="hidden" name="gtpay_tranx_amt" value='<%= ConfigSource.tranx_amt  %>' />
<input type="hidden" name="gtpay_tranx_curr" value='<%= ConfigSource.tranx_curr %>' />
<input type="hidden" name="gtpay_cust_id" value='<%= ConfigSource.cust_id  %>' />
<input type="hidden" name="gtpay_cust_name" value='<%= ConfigSource.cust_name %>' />
<input type="hidden" name="gtpay_tranx_memo" value='<%= ConfigSource.tranx_memo %>' />
<input type="hidden" name="gtpay_no_show_gtbank" value='<%= ConfigSource.gway_first %>' />
<input type="hidden" name="gtpay_echo_data" value='<%= ConfigSource.echo_data %>' />
<input type="hidden" name="gtpay_gway_name" value='<%= ConfigSource.gway_name%>' />
<input type="hidden" name="gtpay_hash" value='<%=ConfigSource.hash %>' />
<input type="hidden" name="gtpay_tranx_noti_url" value='<%= ConfigSource.tranx_noti_url %>' />--%>

<%--    <div style="border-style: solid none solid none; border-color: #027191; vertical-align: middle; text-align: center; background-color: #D7E7FF; font-weight: bold; font-size: large; font-family: Arial, Helvetica, sans-serif; height: 180px; width: 100%; border-top-width: 2px; border-bottom-width: 2px; padding-top: 120px;">
    Connecting to secured Payment Gateway....
    </div>--%>

 <input type="hidden" id="BankName" value="<%=ConfigSource.BankName%>" /> 


 <form id="GTBank" action="<%=ConfigSource.LiveUrl%>" method="post">
<input type="hidden" name="gtpay_mert_id" value='<%=ConfigSource.mert_id  %>' />
<input type="hidden" name="gtpay_tranx_id" value='<%= ConfigSource.tranx_id %>' />
<input type="hidden" name="gtpay_tranx_amt" value='<%= ConfigSource.tranx_amt  %>' />
<input type="hidden" name="gtpay_tranx_curr" value='<%= ConfigSource.tranx_curr %>' />
<input type="hidden" name="gtpay_cust_id" value='<%= ConfigSource.cust_id  %>' />
<input type="hidden" name="gtpay_cust_name" value='<%= ConfigSource.cust_name %>' />
<input type="hidden" name="gtpay_tranx_memo" value='<%= ConfigSource.tranx_memo %>' />
<input type="hidden" name="gtpay_no_show_gtbank" value='<%= ConfigSource.gway_first %>' />
<input type="hidden" name="gtpay_echo_data" value='<%= ConfigSource.tranx_id %>' />
<input type="hidden" name="gtpay_gway_name" value='webpay' />
<input type="hidden" name="gtpay_hash" value='<%=ConfigSource.hash %>' />
<input type="hidden" name="gtpay_tranx_noti_url" value='<%= ConfigSource.tranx_noti_url %>' />
</form>  

      <%--<form id="ISW" action="http://sandbox.interswitchng.com/collections/public/webpay.js" method="post">
    <script>
        $(document).ready(function () {
            var iswPay = new IswPay({
                postUrl: "https://sandbox.interswitchng.com/collections/w/pay",
                amount: "<%= ConfigSource.tranx_amt%>",
                productId: "<%= ConfigSource.ProductId%>",
                transRef: "<%= ConfigSource.tranx_id%>",
                siteName: "<%= ConfigSource.BankName%>",
                itemId: "<%= ConfigSource.ItemId%>",
                customerId: "<%= ConfigSource.cust_id%>",
                siteRedirectUrl: "<%= ConfigSource.tranx_noti_url%>",
                currency: "<%= ConfigSource.tranx_curr%>",
                hash: "<%= ConfigSource.hash%>",
                onComplete: function (paymentResponse) {
                    console.log(paymentResponse);
                }
            });
        });
      </script>
    </form>--%>  



    





     

<%--<form id="FirstBank122" action="https://sandbox.interswitchng.com/webpay/pay" method="post"> 
<input name="product_id"type="hidden"value="<%= ConfigSource.ProductId %>"/>
<input name="pay_item_id"type="hidden"value="<%= ConfigSource.ItemId %>"/>
<input name="amount"type="hidden"value="<%= ConfigSource.tranx_amt %>"/>
<input name="currency"type="hidden"value="<%= ConfigSource.tranx_curr %>"/>
<input name="site_redirect_url"type="hidden"value="<%= ConfigSource.tranx_noti_url %>"/>
<input name="txn_ref"type="hidden"value="<%= ConfigSource.tranx_id %>"/>
<input name="cust_id" type="hidden" value="<%= ConfigSource.cust_id %>" />
<input name="cust_name" type="hidden" value="<%=ConfigSource.cust_name%>" />
<input name="hash"type="hidden"value="<%= ConfigSource.hash %>"/>
</form>--%>


<%--<form   id="FirstBank22222" action="https://sandbox.interswitchng.com/collections/w/pay" method="post">
<input name="product_id" type="hidden" value="1076" />
<input name="pay_item_id" type="hidden" value="101" />
<input name="amount" type="hidden" value="50000" />
<input name="currency" type="hidden" value="566" />
<input name="site_redirect_url" type="hidden" value="http://www.mycompany.com/response/" />
<input name="txn_ref" type="hidden" value="AB-12385_TT" />
<input name="cust_id" type="hidden" value="AD99" />
<input name="hash" type="hidden" value="62D36BDC4B7C805844E3E8C813166BD8B42F9D3E768F349EC4FB174084BC9C2027338DA875A460E843A68FA85C15FB1E0195F2B98ECC6F40D0408D719F9D7E5D" />
</form>--%>



            
          <div style="margin-left:40%; margin-right:0%; margin-bottom:10%; margin-top:10%"  class="centeredDiv">
                  <img src="assets/images/payment-icons/1.gif" />
            <h3 style="margin-left:10%; height: 28px; width: 170px; text-align: center;">Please Wait</h3>
             </div>
    
  <%-- </form>--%>
</body>
</html>


<script>

    $(document).ready(function () {
        //document.forms['form1'].submit();



        var BankName = $("#BankName").val();

        if (BankName == "GTBank") {

            document.forms['GTBank'].submit(); 
        }

        if (BankName == "FirstBank") {
         //   document.forms['GTBank'].submit();
            document.forms['FirstBank1'].submit();


        }


       
    });





</script>




 