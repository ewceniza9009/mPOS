using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace mPOS.Models
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string ServerName { get; set; }
        public DateTime LoginDate { get; set; }
        public bool ContinuesBarcode { get; set; }
    }
}
