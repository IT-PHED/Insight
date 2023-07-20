using ERDBManager;
using PHEDServe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace PHEDServe
{
    public class GlobalMethodsLib
    {

        ApplicationDbContext db = new ApplicationDbContext();

        public void AuditTrail(string StaffId, string ActivityName, DateTime DateTime, string StatusId, string InvoiceID, string ActivityType)
        {
            //write the Transation into the Audit trail of the Application 
            AuditTrail Trail = new AuditTrail();
            Trail.StaffId = StaffId;
            Trail.ActivityName = ActivityName;
            Trail.DateTime = DateTime.Now;


            string Name = "";

            var py = db.Users.FirstOrDefault(p => p.Id == StaffId);


            if (py != null)
            {

                Trail.StaffName = py.StaffName.ToString();

                Trail.StatusId = StatusId;
                Trail.InvoiceID = InvoiceID;
                Trail.ActivityType = ActivityType;
                db.AuditTrails.Add(Trail);
                db.SaveChanges();
            }




        }
         
        public bool HasHePaid(string IncidenceId, string IncidenceAmount, string Percentpayment, DateTime DateOfDisconnection, string AccountNo, string AccountType)
        {
            DBManager dBManager = new DBManager(DataProvider.Oracle)
               {
                   ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
               };
            dBManager.Open();







            if (AccountType == "PREPAID")
            {

                string str = "select Consumer_No, Amount, ReceiptNo, createddatetime from ENSERV.tbl_paymentdetails where consumer_no = '" + AccountNo + "' and purpose = '" + IncidenceId + "' and createddatetime >= '" + DateOfDisconnection + "' order by createddatetime desc";

                dBManager.Open();

                try
                {
                    DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                    dBManager.Close();
                    if (dataSet1.Tables[0].Rows.Count <= 0)
                    {
                        //Check the Amount of Money that was paid by the Guy 
                        return false;

                    }
                    else
                    {  //Let see if the amount of Money the Guy paid is Equal to the Amount of Percentage that he should have paid


                        if (CalculatePercentageOfAmount(Percentpayment, IncidenceAmount) >= Convert.ToDouble(dataSet1.Tables[0].Rows[0]["Amount"]))
                        {

                            return true;
                        }
                        else
                        {

                            return false;
                        }
                    }
                }
                catch (Exception exception1)
                {
                    return false;
                }
            }
            if (AccountType == "POSTPAID")
            {

                string str = "";
                //Energy


                str = "select CONSUMER_NO as  AccountNo, receiptnumber as ReceiptNo,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment where    paymentpurpose= '" + IncidenceId + "' and  consumer_no =  '" + AccountNo + "' and cancel_status = '0' and  rownum <= 6 order by paymentdatetime desc";


                dBManager.Open();
                try
                {
                    DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                    dBManager.Close();
                    if (dataSet1.Tables[0].Rows.Count <= 0)
                    {
                        //Check the Amount of Money that was paid by the Guy

                        return false;

                    }
                    else
                    {  //Let see if the amount of Money the Guy paid is Equal to the Amount of Percentage that he should have paid

                        if (CalculatePercentageOfAmount(Percentpayment, IncidenceAmount) <= Convert.ToDouble(dataSet1.Tables[0].Rows[0]["Amount"]))
                        {

                            return true;
                        }
                        else
                        {

                            return false;
                        }
                    }
                }
                catch (Exception exception1)
                {
                    return false;
                }

            }

            return false;
        }
         
        private double GenerateLossOfRevenueAmount(string Availability, string Load, string AverageConsumption, int Duration, string AccountType, string AccountNo, string LoadCalculationType)
        {
            //select the Last 6 Payments of the Customr and Average it adn Give him the Loss of Revenue Amount to be Paid

            DataSet dataSet = new DataSet();
            DBManager dBManager = new DBManager(DataProvider.Oracle)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString.ToString()
            };
            dBManager.Open();
            // string str = string.Concat("SELECT ID AS ID,PURPOSE AS VAL FROM TBL_PAYMENTPURPOSE where id not in (select purpose from tbl_incident where consumerno='", consno, "')");

            int Count = 6;
            // string str = "select CONSUMER_NO AccountNo, receiptnumber,  Amount, paymentdatetime, paymentpurpose, channelname from ENSERV.tbl_allpayment where consumer_no = '" + AccountNo + "' and cancel_status = '0' and  rownum <= '" + Count + "' order by paymentdatetime desc";


            //  string str = "select TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0)) As CurrentCharge,ARR_EC_DF + T1.ARR_ED_DF AS ARREAR,TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))+ARR_EC_DF + T1.ARR_ED_DF AS TOTAL_OUTSTANDING from tbl_billinfo where billmonth = (SELECT MAX(billmonth) FROM tbl_billinfo where consumerno = '" + AccountNo + "') and consumerno = '" + AccountNo + "' and rownum = 6 order by billmonth desc ";
            double LORAmount = 0;


            if (LoadCalculationType == "1")
            {



                if (AccountType == "POSTPAID")
                {

                    string str = @"select TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))) As CurrentCharge,
                ARR_EC_DF + ARR_ED_DF AS ARREAR,
                TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))+ARR_EC_DF + ARR_ED_DF) AS TOTALOUTSTANDING
                from tbl_billinfo
                where consumerno = '" + AccountNo + "' and rownum <= 6 order by billmonth desc ";

                    dBManager.Open();



                    try
                    {
                        DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                        dBManager.Close();
                        int divideby = dataSet1.Tables[0].Rows.Count;


                        if (dataSet1.Tables[0].Rows.Count <= 0)
                        {

                            return 0;
                        }
                        else
                        {
                            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                            {
                                LORAmount = LORAmount + Convert.ToDouble(dataSet1.Tables[0].Rows[i]["CurrentCharge"].ToString());

                            }
                        }
                        if (divideby < Count)
                        {
                            divideby = Count;

                        }

                        LORAmount = LORAmount / divideby;

                        return LORAmount;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }


                if (AccountType == "PREPAID")
                {
                    //Get the Last 6 Vending the guy has done and divide it by 6 and send the amount to the console for the LOR Amount
                    string str = @"select TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))) As CurrentCharge,
                ARR_EC_DF + ARR_ED_DF AS ARREAR,
                TO_CHAR(to_number(nvl(slabec1,0) ) + to_number(nvl(ed,0))+ARR_EC_DF + ARR_ED_DF) AS TOTALOUTSTANDING
                from tbl_billinfo
                where consumerno = '" + AccountNo + "' and rownum <= 6 order by billmonth desc ";

                    dBManager.Open();



                    try
                    {
                        DataSet dataSet1 = dBManager.ExecuteDataSet(CommandType.Text, str);
                        dBManager.Close();
                        int divideby = dataSet1.Tables[0].Rows.Count;


                        if (dataSet1.Tables[0].Rows.Count <= 0)
                        {

                            return 0;
                        }
                        else
                        {
                            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                            {
                                LORAmount = LORAmount + Convert.ToDouble(dataSet1.Tables[0].Rows[i]["CurrentCharge"].ToString());

                            }
                        }
                        if (divideby < Count)
                        {
                            divideby = Count;

                        }



                        return LORAmount;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }

            if (LoadCalculationType == "2" || LoadCalculationType == "3")
            {
                LORAmount = (Convert.ToDouble(Load) * Convert.ToDouble(Duration)) / 1000;
            }

            return LORAmount;

        }
         
        private double CalculatePercentageOfAmount(string Percentpayment, string IncidenceAmount)
        {
            //we Will Calculate the Percentage of the Amount of Money that the Guy Should Pay before he is Reconnected.

            double AmountPaid = 0;

            AmountPaid = Convert.ToDouble(IncidenceAmount) * (Convert.ToDouble(Percentpayment) / 100);


            return AmountPaid;
        }

        public List<RCDC_DisconnectionList> GetListOfDisconnectedCustomers()
        { 
            return db.RCDCDisconnectionLists.Where(p => p.DisconStatus == "DISCONNECTED").ToList(); 
        }

        public DTRExecutives GetDTRExecutiveDetails(string DTRID)
        {
            DTRExecutives Exec = new DTRExecutives();

            //get the FeederId from the 

            try
            {
                var DTRDetails = db.tbl_bd_update_new_dtrdetailss.FirstOrDefault(p => p.dtrid == DTRID);

                if (DTRDetails != null)
                {
                    //use the Staff ID to get the DTR Executive Details
                    //db.Entry.Database<>(StaffBasicData).TableName

                    StaffBasicData basic = db.StaffBasicDatas.FirstOrDefault(p => p.Staff_Id == DTRDetails.dtrexec);

                    ApplicationUser user = db.Users.FirstOrDefault(p => p.StaffId == DTRDetails.dtrexec);

                    if (user != null)
                    {
                        Exec.DTRExecutivePhone = user.PhoneNo;
                        Exec.DTRExecutiveEmail = user.UserName;

                        Exec.DTRExecutiveName = user.StaffName;
                    }
                    else
                    {
                        Exec.DTRExecutiveEmail = basic.Email;
                        Exec.DTRExecutivePhone = basic.Phone;
                        Exec.DTRExecutiveName = basic.OtherNames;

                    }
                    



                  
                    Exec.Status = true;
                }
                else
                {
                    Exec.Status = false;
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

            }

            return Exec;
        }
    }
}