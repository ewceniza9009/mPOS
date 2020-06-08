using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public abstract class BaseFilter
    {
        public FilterMethods filterMethods { get; set; }
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
}
