using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PHEDServe.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string SubscriberId { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string UserType { get; set; }

        public string StaffId { get; set; }

        public string SubscriberName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string StaffName { get; set; }

        public byte[] Logo { get; set; }

        public string VerifyStatus { get; set; }
        public string IsConfirmed { get; set; }
        public string UIN { get; set; }
        public string NewUser { get; set; }
        public string StaffRole_RoleId { get; set; }
        public string SpouseName { get; set; }
        public int? NoOfChildren { get; set; }
        public string Address { get; set; }
        public string DepartmentId { get; set; }
        public string TitleId { get; set; }
        public string SexId { get; set; }
        public string MaritalStatusId { get; set; }

        public string Ocupation { get; set; }

        public string TariffCode { get; set; }

        public string BSC { get; set; }

        public string IBC { get; set; }

        public string AccountNumber { get; set; }

        public string PHEDKeyAccountsPhone { get; set; }

        public string PHEDKeyAccountsEmail { get; set; }

        public string ContactPersonEmail { get; set; }

        public string ContactPersonPhone { get; set; }

        public string UserCategory { get; set; }

        public string LoginSequence { get; set; }

        public string AccountNo { get; set; }

        public string MeterNo { get; set; }

        public string CreatedBy { get; set; }

        public string Arrears { get; set; }

        public string OfficeLocation { get; set; }

        public string MeterType { get; set; }

        public string PeriodToClearDebt { get; set; }

        public string JobRole { get; set; }

        public string Installment { get; set; }

        public string DepartmentName { get; set; }

        public string Remarks { get; set; }

        public string Designation { get; set; }

        public string ResolvedBalance { get; set; }

        public string BillReflection { get; set; }

        public string CUGLine { get; set; }

        public System.DateTime? DateRegistered { get; set; }

        public string AccountName { get; set; }

        public string Submission { get; set; }

        public string ContactPersonName { get; set; }

        public string PHEDKeyAccountsName { get; set; }

        public string Zone { get; set; }

        public string Feeder { get; set; }

        public System.DateTime? LastDatePaid { get; set; }

        public string LastAmount { get; set; }

        public string CIN { get; set; }

        public string DTR_Name { get; set; }

        public string MeterMake { get; set; }

        public string IsBalanceCorrect { get; set; }

        public string ActiveStatus { get; set; }

        public string AccountType { get; set; }

        public string IMEI1 { get; set; }
        public string IMEI2 { get; set; }
        public string IMEILogin { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNo { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            string test = "";
        }


  


        //public DbSet<BusinessServiceCenter> BSC { get; set; }
        //public DbSet<IntegratedServiceCenter> IBC { get; set; }
        //public DbSet<Marketer> Marketers { get; set; }
        //public DbSet<TransmissionStations> TransmissionStationss { get; set; }
        //public DbSet<Feeders33> Feeders33s { get; set; }
        //public DbSet<NationalGridEnergy> NationalGridEnergys { get; set; }
        public DbSet<CustomerPaymentInfo> CustomerPaymentInfos { get; set; }

        public DbSet<Menu> Menus { get; set; } public DbSet<MeterList> MeterLists { get; set; }
        public DbSet<CUSTOMER> CUSTOMERS { get; set; }
        public DbSet<MAPPayment> MAPPayments { get; set; } 
        public DbSet<RCDCCustomerIncidences> RCDCCustomerIncidencess { get; set; }
        public DbSet<BRCApprovals> BRCApprovalss { get; set; }
        public DbSet<RCDC_Incidence> RCDC_Incidences { get; set; } 
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<RCDC_Reasons_For_Disconnection> RCDC_Reasons_For_Disconnections { get; set; }
        public DbSet<RCDC_Incidence_For_Reasons> RCDC_Incidence_For_Reasonss { get; set; } 
        public DbSet<StaffSuggestions> StaffSuggestionss { get; set; }
        public DbSet<MenuItemsMain> MenuItemsMains { get; set; }
        public DbSet<StaffBillPaymentData> StaffBillPaymentDatas { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<PaymentSchedule> PaymentSchedules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleItem> ModuleItems { get; set; }  
        public DbSet<StaffBasicData> StaffBasicDatas { get; set; }
        public DbSet<MaritalStatusTbl> MaritalStatusTbls { get; set; } 
        public DbSet<MAP_METER_UPGRADE> MAP_METER_UPGRADEs { get; set; }
        public DbSet<TitleTbl> TitleTbls { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<MassMeterCapture> MassMeterCaptures { get; set; }
        public DbSet<MAP_VENDOR> MAP_VENDORS { get; set; }
        public DbSet<DeliveredEbills> DeliveredEbillss { get; set; }
        public DbSet<RCDC_Spot_Billing> RCDC_Spot_Billings { get; set; } 
        public DbSet<RCDC_Disconnection_Incidence_History> RCDC_Disconnection_Incidence_Historys { get; set; }
        public DbSet<MAP_INSTALLER> MAP_INSTALLERS { get; set; }
        public DbSet<Customerfeedbacks> Customerfeedbackss { get; set; }
        public DbSet<CustomerfeedbackModel> CustomerfeedbackModels { get; set; }
        public DbSet<Customercomplaints> Customercomplaintss { get; set; }
        public DbSet<CustomercomplaintsModel> CustomercomplaintsModels { get; set; }
        public DbSet<RCDC_Ireport> RCDC_Ireports { get; set; } 
        public DbSet<METER_REPAYMENT_PLAN> METER_REPAYMENT_PLANs { get; set; }
        public DbSet<MAP_CONTRACTOR> MAP_CONTRACTORS { get; set; } 
        public DbSet<DirectPayments> DirectPaymentss { get; set; }
        public DbSet<PaymentDetails> PaymentDetailss { get; set; }
        public DbSet<LGA> LGAs { get; set; } 
        public DbSet<RCDC_LoadApplicances> RCDCLoadApplicancess { get; set; }
        public DbSet<STATE> States { get; set; }
        public DbSet<ComplaintCategory> ComplaintCategorys { get; set; } 
        public DbSet<ComplaintsSubCategory> ComplaintsSubCategorys { get; set; } 
        public DbSet<CustomerAccounts> CustomerAccountss { get; set; }
        public DbSet<Sex> Sexs { get; set; }
        public DbSet<RCDC_Gang> RCDC_Gangs { get; set; }
        public DbSet<RCDC_DisconnectionList> RCDCDisconnectionLists { get; set; }
        public DbSet<RCDCDTR_Exec> RCDCDTR_Execs { get; set; }
        public DbSet<RCDC_DTR> RCDC_DTRs { get; set; }

        public DbSet<RCDC_Faulty_Transformer> RCDC_FaultyTransformers { get; set; }
        public DbSet<RCDC_OnboardCustomers> RCDC_OnboardCustomerss { get; set; }public DbSet<RCDC_BillDistribution>  RCDC_BillDistributions { get; set; }
        public DbSet<DOCUMENTS> DOCUMENTSs { get; set; }
        public DbSet<RCDC_Settlement_Duration> RCDC_Settlement_Durations { get; set; }
        
        public DbSet<RCDC_SettlementPlan_Settings> RCDC_SettlementPlan_Settingss { get; set; }
        public DbSet<RCDC_ReportCategory> RCDC_ReportCategorys { get; set; }

        public DbSet<RCDC_ReportSubCategory> RCDC_ReportSubCategorys { get; set; }

        public DbSet<RCDCMember> RCDCMembers { get; set; } 
        public DbSet<RCDC_AppVersion> RCDC_AppVersions { get; set; }
        public DbSet<RCDCPaymentHistory> RCDCPaymentHistorys { get; set; }
        public DbSet<RCDCPaymentMaster> RCDCPaymentMasters { get; set; }

        public DbSet<RCDCReconnectionFee> RCDCReconnectionFee { get; set; }
          
        public DbSet<CustomerTickets> CustomerTicketss { get; set; }
        public DbSet<SMSSent> SMSSents { get; set; }  
        
        
        public DbSet<tbl_map_accountseparation_secoderyaccounts> tbl_map_accountseparation_secoderyaccountss { get; set; } 
        public DbSet<ModuleActivity> ModuleActivitys { get; set; }
        public DbSet<StaffModuleActivity> StaffModuleActivitys { get; set; }
        public DbSet<KYC> KYCs { get; set; } 
        public DbSet<tbl_bd_update_new_dtrdetails> tbl_bd_update_new_dtrdetailss { get; set; } 
        public DbSet<KYCReport> KYCReports { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<IBC> IBCs { get; set; }
        public DbSet<BSC> BSCs { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

           
            //modelBuilder.Entity<tbl_bd_update_new_dtrdetails>().ToTable("tbl_bd_update_new_dtrdetails", "ebuka");
            modelBuilder.Entity<tbl_map_accountseparation_secoderyaccounts>().ToTable("tbl_map_accountseparation_secoderyaccounts", "ebuka");

            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Configure Asp Net Identity Tables
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }

    public class ApplicationDbContextUAT : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContextUAT()
            : base("OracleConnectionUAT")
        { }


        //public DbSet<BusinessServiceCenter> BSC { get; set; }
        //public DbSet<IntegratedServiceCenter> IBC { get; set; }
        //public DbSet<Marketer> Marketers { get; set; }
        //public DbSet<TransmissionStations> TransmissionStationss { get; set; }
        //public DbSet<Feeders33> Feeders33s { get; set; }
        //public DbSet<NationalGridEnergy> NationalGridEnergys { get; set; }
        public DbSet<CustomerPaymentInfo> CustomerPaymentInfos { get; set; }

        public DbSet<Menu> Menus { get; set; } public DbSet<MeterList> MeterLists { get; set; }
        public DbSet<CUSTOMER> CUSTOMERS { get; set; }
        public DbSet<MAPPayment> MAPPayments { get; set; } 
        public DbSet<RCDCCustomerIncidences> RCDCCustomerIncidencess { get; set; }
        public DbSet<BRCApprovals> BRCApprovalss { get; set; }
        public DbSet<RCDC_Incidence> RCDC_Incidences { get; set; } 
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<RCDC_Reasons_For_Disconnection> RCDC_Reasons_For_Disconnections { get; set; }

        public DbSet<RCDC_Incidence_For_Reasons> RCDC_Incidence_For_Reasonss { get; set; } 
        public DbSet<StaffSuggestions> StaffSuggestionss { get; set; }
        public DbSet<MenuItemsMain> MenuItemsMains { get; set; }
        public DbSet<StaffBillPaymentData> StaffBillPaymentDatas { get; set; }

        public DbSet<StaffRole> StaffRoles { get; set; } 
        public DbSet<PaymentSchedule> PaymentSchedules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleItem> ModuleItems { get; set; }  
        public DbSet<StaffBasicData> StaffBasicDatas { get; set; }
        public DbSet<MaritalStatusTbl> MaritalStatusTbls { get; set; } public DbSet<MAP_METER_UPGRADE> MAP_METER_UPGRADEs { get; set; }
        public DbSet<TitleTbl> TitleTbls { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; } 
        public DbSet<MassMeterCapture> MassMeterCaptures { get; set; }
        public DbSet<MAP_VENDOR> MAP_VENDORS { get; set; }
        public DbSet<DeliveredEbills> DeliveredEbillss { get; set; }
        public DbSet<RCDC_Spot_Billing> RCDC_Spot_Billings { get; set; } 
        public DbSet<RCDC_Disconnection_Incidence_History> RCDC_Disconnection_Incidence_Historys { get; set; }
        public DbSet<MAP_INSTALLER> MAP_INSTALLERS { get; set; }
        public DbSet<Customerfeedbacks> Customerfeedbackss { get; set; }
        public DbSet<CustomerfeedbackModel> CustomerfeedbackModels { get; set; }
        public DbSet<Customercomplaints> Customercomplaintss { get; set; }
        public DbSet<CustomercomplaintsModel> CustomercomplaintsModels { get; set; }

        public DbSet<RCDC_Ireport> RCDC_Ireports { get; set; }

        public DbSet<RCDC_Faulty_Transformer> RCDC_FaultyTransformers { get; set; }
        public DbSet<METER_REPAYMENT_PLAN> METER_REPAYMENT_PLANs { get; set; }
        public DbSet<MAP_CONTRACTOR> MAP_CONTRACTORS { get; set; } 
        public DbSet<DirectPayments> DirectPaymentss { get; set; }
        public DbSet<PaymentDetails> PaymentDetailss { get; set; }
        public DbSet<LGA> LGAs { get; set; } 
        public DbSet<RCDC_LoadApplicances> RCDCLoadApplicancess { get; set; }
        public DbSet<STATE> States { get; set; }
        public DbSet<ComplaintCategory> ComplaintCategorys { get; set; } 
        public DbSet<ComplaintsSubCategory> ComplaintsSubCategorys { get; set; } 
        public DbSet<CustomerAccounts> CustomerAccountss { get; set; }
        public DbSet<Sex> Sexs { get; set; }
        public DbSet<RCDC_Gang> RCDC_Gangs { get; set; }
        public DbSet<RCDC_DisconnectionList> RCDCDisconnectionLists { get; set; }
        public DbSet<RCDCDTR_Exec> RCDCDTR_Execs { get; set; }
        public DbSet<RCDC_DTR> RCDC_DTRs { get; set; }
        public DbSet<RCDC_OnboardCustomers> RCDC_OnboardCustomerss { get; set; }public DbSet<RCDC_BillDistribution>  RCDC_BillDistributions { get; set; }
        public DbSet<DOCUMENTS> DOCUMENTSs { get; set; }
        public DbSet<RCDC_Settlement_Duration> RCDC_Settlement_Durations { get; set; }
        
        public DbSet<RCDC_SettlementPlan_Settings> RCDC_SettlementPlan_Settingss { get; set; }
        public DbSet<RCDC_ReportCategory> RCDC_ReportCategorys { get; set; }

        public DbSet<RCDC_ReportSubCategory> RCDC_ReportSubCategorys { get; set; }

        public DbSet<RCDCMember> RCDCMembers { get; set; } 
        public DbSet<RCDC_AppVersion> RCDC_AppVersions { get; set; }
        public DbSet<RCDCPaymentHistory> RCDCPaymentHistorys { get; set; }
        public DbSet<RCDCPaymentMaster> RCDCPaymentMasters { get; set; }

        public DbSet<RCDCReconnectionFee> RCDCReconnectionFee { get; set; }
          
        public DbSet<CustomerTickets> CustomerTicketss { get; set; }
        public DbSet<SMSSent> SMSSents { get; set; }  
        
        
        public DbSet<tbl_map_accountseparation_secoderyaccounts> tbl_map_accountseparation_secoderyaccountss { get; set; } 
        public DbSet<ModuleActivity> ModuleActivitys { get; set; }
        public DbSet<StaffModuleActivity> StaffModuleActivitys { get; set; }
        public DbSet<KYC> KYCs { get; set; } 
        public DbSet<tbl_bd_update_new_dtrdetails> tbl_bd_update_new_dtrdetailss { get; set; } 
        public DbSet<KYCReport> KYCReports { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<IBC> IBCs { get; set; }
        public DbSet<BSC> BSCs { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

           
            //modelBuilder.Entity<tbl_bd_update_new_dtrdetails>().ToTable("tbl_bd_update_new_dtrdetails", "ebuka");
            modelBuilder.Entity<tbl_map_accountseparation_secoderyaccounts>().ToTable("tbl_map_accountseparation_secoderyaccounts", "ebuka");

            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Configure Asp Net Identity Tables
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }

  
}