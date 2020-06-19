using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public class __MigrationHistory
    {
        public string MigrationId { get; set; }
        public string ContextKey { get; set; }
        public byte[] Model { get; set; }
        public string ProductVersion { get; set; }
    }

    public class AspNetRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class AspNetUserClaims
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }

    public class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
    }

    public class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }

    public class AspNetUsers
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    }

    public class MstAccount
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Account { get; set; }
        public bool IsLocked { get; set; }
        public string AccountType { get; set; }
    }

    public partial class MstCustomer
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public int TermId { get; set; }
        public string TIN { get; set; }
        public bool WithReward { get; set; }
        public string RewardNumber { get; set; }
        public decimal RewardConversion { get; set; }
        public int AccountId { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsLocked { get; set; }
    }

    public class MstDiscount
    {
        public int Id { get; set; }
        public string Discount { get; set; }
        public decimal DiscountRate { get; set; }
        public bool IsVatExempt { get; set; }
        public bool IsDateScheduled { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool IsTimeScheduled { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public bool IsDayScheduled { get; set; }
        public bool DayMon { get; set; }
        public bool DayTue { get; set; }
        public bool DayWed { get; set; }
        public bool DayThu { get; set; }
        public bool DayFri { get; set; }
        public bool DaySat { get; set; }
        public bool DaySun { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsLocked { get; set; }
    }

    public class MstDiscountItem
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int ItemId { get; set; }
    }

    public class MstItem
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string BarCode { get; set; }
        public string ItemDescription { get; set; }
        public string Alias { get; set; }
        public string GenericName { get; set; }
        public string Category { get; set; }
        public int SalesAccountId { get; set; }
        public int AssetAccountId { get; set; }
        public int CostAccountId { get; set; }
        public int InTaxId { get; set; }
        public int OutTaxId { get; set; }
        public int UnitId { get; set; }
        public int DefaultSupplierId { get; set; }
        public decimal Cost { get; set; }
        public decimal MarkUp { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public decimal ReorderQuantity { get; set; }
        public decimal OnhandQuantity { get; set; }
        public bool IsInventory { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string LotNumber { get; set; }
        public string Remarks { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsLocked { get; set; }
        public string DefaultKitchenReport { get; set; }
        public bool IsPackage { get; set; }
    }

    public class MstItemComponent
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ComponentItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Amount { get; set; }
        public bool IsPrinted { get; set; }
    }

    public class MstItemGroup
    {
        public int Id { get; set; }
        public string ItemGroup { get; set; }
        public string ImagePath { get; set; }
        public string KitchenReport { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsLocked { get; set; }
    }

    public class MstItemGroupItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ItemGroupId { get; set; }
    }

    public class MstItemInventory
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime InventoryDate { get; set; }
        public decimal Quantity { get; set; }
    }

    public class MstItemPackage
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int PackageItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public bool IsOptional { get; set; }
    }

    public class MstItemPrice
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string PriceDescription { get; set; }
        public decimal Price { get; set; }
        public decimal TriggerQuantity { get; set; }
    }

    public class MstPayType
    {
        public int Id { get; set; }
        public string PayType { get; set; }
        public int? AccountId { get; set; }
    }

    public class MstPeriod
    {
        public int Id { get; set; }
        public string Period { get; set; }
    }

    public class MstSupplier
    {
        public int Id { get; set; }
        public string Supplier { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string CellphoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public int TermId { get; set; }
        public string TIN { get; set; }
        public int AccountId { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsLocked { get; set; }
    }

    public class MstTable
    {
        public int Id { get; set; }
        public string TableCode { get; set; }
        public int TableGroupId { get; set; }
        public int? TopLocation { get; set; }
        public int? LeftLocation { get; set; }
    }

    public class MstTableGroup
    {
        public int Id { get; set; }
        public string TableGroup { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsLocked { get; set; }
    }

    public class MstTax
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Tax { get; set; }
        public decimal Rate { get; set; }
        public int AccountId { get; set; }
    }

    public class MstTerm
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public decimal NumberOfDays { get; set; }
    }

    public class MstTerminal
    {
        public int Id { get; set; }
        public string Terminal { get; set; }
    }

    public class MstUnit
    {
        public int Id { get; set; }
        public string Unit { get; set; }
    }

    public class MstUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string UserCardNumber { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsLocked { get; set; }
    }

    public class MstUserForm
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int UserId { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAdd { get; set; }
        public bool CanLock { get; set; }
        public bool CanUnlock { get; set; }
        public bool CanPrint { get; set; }
        public bool CanPreview { get; set; }
        public bool CanEdit { get; set; }
        public bool CanTender { get; set; }
        public bool CanDiscount { get; set; }
        public bool CanView { get; set; }
        public bool CanSplit { get; set; }
        public bool CanCancel { get; set; }
        public bool CanReturn { get; set; }
    }

    public class SysAuditTrail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AuditDate { get; set; }
        public string TableInformation { get; set; }
        public string RecordInformation { get; set; }
        public string FormInformation { get; set; }
        public string ActionInformation { get; set; }
    }

    public class sysdiagrams
    {
        public string name { get; set; }
        public int principal_id { get; set; }
        public int diagram_id { get; set; }
        public int? version { get; set; }
        public byte[] definition { get; set; }
    }

    public class SysForm
    {
        public int Id { get; set; }
        public string Form { get; set; }
        public string FormDescription { get; set; }
    }

    public class SysSalesLocked
    {
        public int Id { get; set; }
        public int SalesId { get; set; }
        public int UserId { get; set; }
    }

    public class TrnCollection
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public DateTime CollectionDate { get; set; }
        public string CollectionNumber { get; set; }
        public int TerminalId { get; set; }
        public string ManualORNumber { get; set; }
        public int CustomerId { get; set; }
        public string Remarks { get; set; }
        public int? SalesId { get; set; }
        public decimal SalesBalanceAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal TenderAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsLocked { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class TrnCollectionLine
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public decimal Amount { get; set; }
        public int PayTypeId { get; set; }
        public string CheckNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public string CheckBank { get; set; }
        public string CreditCardVerificationCode { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardBank { get; set; }
        public string GiftCertificateNumber { get; set; }
        public string OtherInformation { get; set; }
        public int? StockInId { get; set; }
        public int AccountId { get; set; }
    }

    public class TrnDebitCreditMemo
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public string DCMemoNumber { get; set; }
        public DateTime DCMemoDate { get; set; }
        public string Particulars { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsLocked { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class TrnDebitCreditMemoLine
    {
        public int Id { get; set; }
        public int DCMemoId { get; set; }
        public int? SalesId { get; set; }
        public int AccountId { get; set; }
        public string Particulars { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }

    public class TrnDisbursement
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public DateTime DisbursementDate { get; set; }
        public string DisbursementNumber { get; set; }
        public string DisbursementType { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int PayTypeId { get; set; }
        public int TerminalId { get; set; }
        public string Remarks { get; set; }
        public bool IsReturn { get; set; }
        public int? StockInId { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsLocked { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public decimal? Amount1000 { get; set; }
        public decimal? Amount500 { get; set; }
        public decimal? Amount200 { get; set; }
        public decimal? Amount100 { get; set; }
        public decimal? Amount50 { get; set; }
        public decimal? Amount20 { get; set; }
        public decimal? Amount10 { get; set; }
        public decimal? Amount5 { get; set; }
        public decimal? Amount1 { get; set; }
        public decimal? Amount025 { get; set; }
        public decimal? Amount010 { get; set; }
        public decimal? Amount005 { get; set; }
        public decimal? Amount001 { get; set; }
    }

    public class TrnJournal
    {
        public int Id { get; set; }
        public DateTime JournalDate { get; set; }
        public string JournalRefDocument { get; set; }
        public int AccountId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public int? SalesId { get; set; }
        public int? StockInId { get; set; }
        public int? StockOutId { get; set; }
        public int? CollectionId { get; set; }
        public int? DCMemoId { get; set; }
        public int? DisbursementId { get; set; }
    }

    public class TrnPurchaseOrder
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public decimal Amount { get; set; }
        public int SupplierId { get; set; }
        public string Remarks { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsLocked { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class TrnPurchaseOrderLine
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Amount { get; set; }
    }

    public partial class TrnSales
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public DateTime SalesDate { get; set; }
        public string SalesNumber { get; set; }
        public string ManualInvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public int? TableId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public int TermId { get; set; }
        public int? DiscountId { get; set; }
        public string SeniorCitizenId { get; set; }
        public string SeniorCitizenName { get; set; }
        public int? SeniorCitizenAge { get; set; }
        public string Remarks { get; set; }
        public int SalesAgent { get; set; }
        public int TerminalId { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsLocked { get; set; }
        public bool IsCancelled { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int? Pax { get; set; }
        public List<TrnSalesLine> TrnSalesLines { get; set; }
    }

    public partial class TrnSalesLine
    {
        public int Id { get; set; }
        public int SalesId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Price { get; set; }
        public int DiscountId { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public int TaxId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public int SalesAccountId { get; set; }
        public int AssetAccountId { get; set; }
        public int CostAccountId { get; set; }
        public int TaxAccountId { get; set; }
        public DateTime SalesLineTimeStamp { get; set; }
        public int? UserId { get; set; }
        public string Preparation { get; set; }
    }

    public class TrnStockCount
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public DateTime StockCountDate { get; set; }
        public string StockCountNumber { get; set; }
        public string Remarks { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public int IsLocked { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class TrnStockCountLine
    {
        public int Id { get; set; }
        public int StockCountId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Amount { get; set; }
    }

    public class TrnStockIn
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public DateTime StockInDate { get; set; }
        public string StockInNumber { get; set; }
        public int SupplierId { get; set; }
        public string Remarks { get; set; }
        public bool IsReturn { get; set; }
        public int? CollectionId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public int IsLocked { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class TrnStockInLine
    {
        public int Id { get; set; }
        public int StockInId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string LotNumber { get; set; }
        public int AssetAccountId { get; set; }
    }

    public class TrnStockOut
    {
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public DateTime StockOutDate { get; set; }
        public string StockOutNumber { get; set; }
        public int AccountId { get; set; }
        public string Remarks { get; set; }
        public int PreparedBy { get; set; }
        public int CheckedBy { get; set; }
        public int ApprovedBy { get; set; }
        public bool IsLocked { get; set; }
        public int EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class TrnStockOutLine
    {
        public int Id { get; set; }
        public int StockOutId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Amount { get; set; }
        public int AssetAccountId { get; set; }
    }
}