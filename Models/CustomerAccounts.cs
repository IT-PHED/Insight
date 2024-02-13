using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHEDServe.Models
{

    public class DTRExecutives
    {

        public string DTRExecutiveName { get; set; }
        public string DTRExecutivePhone { get; set; } 
        public bool Status { get; set; }

        public string DTRExecutiveEmail { get; set; }
    }

    public class DTRName
    {
        public string dtrId { get; set; }
        public string dtrName { get; set;}
    }

    public class StreetNameId
    {
        public string SI { get; set;}
        public string StreetName { get; set;}
        public string EnrollmentArea { get; set; }
    }

    public class EnrollmentArea
    {
        public string EnrollmentAreaID { get; set; }
        public string EnrollmentAreaName { get; set; }
       
    }

    public class BIInsightData
    {
        public string tariff_band { get; set; }
        public string product { get; set; }
        public string total_population { get; set; }
        public string total_active_population { get; set; }
        public string total_billed_population { get; set; }
        public string total_kwh_billed { get; set; }
        public string previous_total_billed { get; set; }
        public string current_total_billed { get; set; }
        public string current_total_payment { get; set; }
        public string previous_total_payment { get; set; }
        public string current_total_payment_today { get; set; }
        public string total_response { get; set; }
        public string total_ARREARSPMT { get; set; }
        public string payment_variance { get; set; }
        public string response_variance { get; set; }
        public string previous_collectionefficiency { get; set; }
        public string current_collectionefficiency { get; set; }
        public string current_collectionefficiency_percentage { get; set; }
        public string response_variance_percentage { get; set; }
        public string payment_variance_percentage { get; set; }
        public string billDate { get; set; }
    }

    public class BillData
    {
        public string ID { get; set; }
        public string AccountNo { get; set; }
        public string BookCode { get; set; }
        public string IBC { get; set; }
        public string NAMES { get; set; }
        public string ADDRESS { get; set; }
        public string METERNO { get; set; }
        public string NETARREARS { get; set; }
        public string BILLDATE { get; set; }
        public string TOTALDUE { get; set; }
        public string CURRTCHARGES { get; set; }
        public string LARDATE { get; set; }
        public string Feeder33ID { get; set; }
        public string Feeder11ID { get; set; }
        public string DTRID { get; set; }
        public string AccountStatus { get; set; }
        public string DTR_Name { get; set; }
        public string CurrentTarriffClass { get; set; }
        public string GSM {  get; set; }
        public string CIN { get; set; }
        public string AccountType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Upriser { get; set; }
        public string Feeder33Name { get; set; }
        public string Service_LT_Pole { get; set; }
        public string ZONE { get;set; }
        public string Phase { get; set; }

    }

    public class customerOfflineSn
    {
        public int dtrId { get; set; }
        public int upriserNo { get; set; }
        public string acctNo { get; set; }
        public int sno { get; set; }
        public int poleno2 { get; set; }
        public int userId { get; set; }
    }

    public class CustomerOfflineData
    {
        public string tsid { get; set; }
        public string tsName { get; set; }
        public string Feeder33ID { get; set; }
        public string Feeder33Name { get; set; }
        public string InjId { get; set; }
        public string InjName { get; set; }
        public string Feeder11ID { get; set; }
        public string Feeder11Name { get; set; }
        public string DTRID { get; set; }
        public string DistrictName { get; set; }
        public string DistrictID { get; set; }
        public string ServiceCenterName { get; set; }
        public string ServiceCenterID { get; set; }
        public string ACCOUNTNO { get; set; }
        public string Name { get; set; }
        public string Addr1 { get; set; }
        public string IBC { get; set; }
        public string BSC { get; set; }
        public string CurrentMeterSerialNo { get; set; }
        public string CONS_TYPE { get; set; }
        public string CurrentTarriffClass { get; set; }
        public string GSM { get; set; }
        public string AccountStatus { get; set; }
        public string TransformerID { get; set; }
        public string cin { get; set; }
        public string DTR_Name { get; set; }
        public string ZONE { get; set; }
        public string FeederType { get; set; }
        public string Region_Id { get; set; }
        public string Reginal_Names { get; set; }


    }


    public class CustomerAccounts
    {
        [Key]
        public int SerialNo { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAPIKey { get; set; }
        public string IBC { get; set; }
        public string BSC { get; set; }
        public string MeterNo { get; set; }
        public string StatusType { get; set; }

    }

}