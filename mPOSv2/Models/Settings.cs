using System;
using SQLite;

namespace mPOSv2.Models
{
    public class Settings
    {
        [PrimaryKey] 
        [AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string ServerName { get; set; }
        public DateTime LoginDate { get; set; }
        public bool ContinuesBarcode { get; set; }
        public int SalesLinePageSize { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string OperatedBy { get; set; }
        public string TIN { get; set; }
        public string AccreditNo { get; set; }
        public string SerialNo { get; set; }
        public string PermitNo { get; set; }
        public string TerminalNo { get; set; }
        public string ReceiptFooter { get; set; }
    }
}