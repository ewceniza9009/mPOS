using System;
using SQLite;

namespace mPOSv2.Models
{
    public class Settings
    {
        [PrimaryKey] [AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string ServerName { get; set; }
        public DateTime LoginDate { get; set; }
        public bool ContinuesBarcode { get; set; }
    }
}