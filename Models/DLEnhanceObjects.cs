using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHEDMAP.Models
{
    

    public class GetCustomerInfo
    {
        public string STATUS { get; set; }
        public string IBC_NAME { get; set; }
        public string BSC_NAME { get; set; }
        public string CONS_NAME { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string METER_NO { get; set; }
        public string MOB_NO { get; set; }
        public string CONS_TYPE { get; set; }
        public string CURRENT_AMOUNT { get; set; }
        public string ARREAR { get; set; }
        public string TOTAL_BILL { get; set; }
        public string ADDRESS { get; set; }



    }
    public class RequeryResponse
    {
        public string responseCode { get; set; }
        public string responseMsg { get; set; }
        public string transactionID { get; set; }
        public string merchantID { get; set; }
        public string amount { get; set; }
        public string hash { get; set; }

    }




    public class Comments
    {
        public string Value { get; set; }
    }

    public class DirectJSON
    {
        public string STATUS { get; set; }
        public string IBC_NAME { get; set; }
        public string BSC_NAME { get; set; }
        public string CONS_NAME { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string METER_NO { get; set; }
        public string MOB_NO { get; set; }
        public string CONS_TYPE { get; set; }
        public string CURRENT_AMOUNT { get; set; }
        public string ARREAR { get; set; }
        public string TOTAL_BILL { get; set; }
        public string ADDRESS { get; set; }
    }



    //public class TokenJSONRoot
    //{
    //    public List<TokenJSONObject> TokenJSONList { get; set; }
    //}

    public class TokenJSONObject
    {
        public string STATUS { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string METER_NO { get; set; }
        public string RECEIPTNUMBER { get; set; }
        public string PAYMENTDATETIME { get; set; }
        public string AMOUNT { get; set; }
        public string TOKENDESC { get; set; }
        public string UNITSACTUAL { get; set; }
        public string TARIFF { get; set; }
        public List<DEATIL> DEATILS { get; set; }
    }

    public class DEATIL
    {
        public string HEAD { get; set; }
        public string AMOUNT { get; set; }
    }
     
}