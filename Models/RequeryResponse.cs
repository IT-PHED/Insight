using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
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

        public string TARIFFCODE { get; set; }
    }

    public class MDCustomerData
    {
        public string customerName { get; set; }
        public string accountno { get; set; }
        public string ibc { get; set; }
        public string bsc { get; set; }
        public string meterNo { get; set; }
        public string totalOutstanding { get; set; }
        public Monthlybilldata[] monthlyBillData { get; set; }
    }

    public class Monthlybilldata
    {
        public string accountNo { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string amountBilled { get; set; }
        public string dateBilled { get; set; }
        public string amountPaid { get; set; }
        public string datePaid { get; set; }
        public string consumption { get; set; }
        public string totalOutStanding { get; set; }
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
        public List<DETAILS> DEATILS { get; set; }


        public string CONS_NAME { get; set; }

        public string CON_TYPE { get; set; }
        public string ADDRESS { get; set; }
        public string PAYMENT_PURPOSE { get; set; }
        public string MR_NAME { get; set; }
        public string COL_TIME { get; set; }
        public string PYMNT_TYPE { get; set; }
        public string TARIFFCODE { get; set; }
        public string TARIFF_RATE { get; set; }


        public string TARIFF_INDEX { get; set; }
        public string INCIDENT_TYPE { get; set; }

        public string CHEQUEDATE { get; set; }
        public string CHEQUENO { get; set; }
        public string BANKNAME { get; set; }
        public string IBC { get; set; }
        public string BSC { get; set; }


    }

    public class DETAILS
    {
        public string HEAD { get; set; }
        public string AMOUNT { get; set; }
    }

}
