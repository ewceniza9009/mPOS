using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public abstract class BaseFilter
    {
        public FilterMethods FilterMethods { get; set; }
    }

    public class MstUserFilter : BaseFilter
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string UserCardNumber { get; set; }
        public int? EntryUserId { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool? IsLocked { get; set; }

    }
    public class MstAccountFilter : BaseFilter
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Account { get; set; }
        public bool? IsLocked { get; set; }
        public string AccountType { get; set; }
    }
    public class MstCustomerFilter : BaseFilter
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

    public class MstItemFilter : BaseFilter 
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

    public class TrnSalesFilter : BaseFilter
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
    }
}
