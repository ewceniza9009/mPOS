using System;
using System.Linq;
using mPOSv2.Models;
using SQLite;

namespace mPOSv2.Services
{
    public static class SettingsRepository
    {
        private static Models.Settings Settings;

        public static Settings GetSettings()
        {
            Settings result;

            using (var conn = new SQLiteConnection(App.FilePath))
            {
                result = conn.Table<Settings>().FirstOrDefault();
            }

            return result;
        }

        public static void Save(Settings settings)
        {
            using (var conn = new SQLiteConnection(App.FilePath))
            {
                var _settings = conn.Query<Settings>("SELECT * FROM Settings WHERE Id = ?", 1).FirstOrDefault();

                if (_settings != null)
                {
                    _settings.UserId = settings.Id;
                    _settings.UserFullName = settings.UserFullName;
                    _settings.ServerName = settings.ServerName;
                    _settings.ContinuesBarcode = settings.ContinuesBarcode;
                    _settings.IsPrint = settings.IsPrint;
                    _settings.SalesLinePageSize = settings.SalesLinePageSize;                   
                    _settings.StoreName = settings.StoreName;
                    _settings.Address = settings.Address;
                    _settings.OperatedBy = settings.OperatedBy;
                    _settings.TIN = settings.TIN;
                    _settings.AccreditNo = settings.AccreditNo;
                    _settings.SerialNo = settings.SerialNo;
                    _settings.PermitNo = settings.PermitNo;
                    _settings.TerminalNo = settings.TerminalNo;
                    _settings.ReceiptFooter = settings.ReceiptFooter;

                    conn.RunInTransaction(() =>
                    {
                        if (conn != null) conn.Update(_settings);
                    });
                }
            }
        }

        public static string GetUserFullName()
        {
            string result;

            using (var conn = new SQLiteConnection(App.FilePath))
            {
                result = conn.Table<Settings>()
                    .FirstOrDefault()
                    .UserFullName;
            }

            return result;
        }

        public static int GetSalesLinePageSize()
        {
            int result;

            using (var conn = new SQLiteConnection(App.FilePath))
            {
                result = conn.Table<Settings>()
                    .FirstOrDefault()
                    .SalesLinePageSize;
            }

            return result;
        }

        public static void CreateLocalSettingsDB()
        {
            using (var conn = new SQLiteConnection(App.FilePath)) 
            {
                Settings = new Models.Settings
                {
                    UserId = 1,
                    UserFullName = "NotFound",
                    ServerName = GlobalVariables.UriBase,
                    LoginDate = DateTime.Now,
                    ContinuesBarcode = false,
                    IsPrint = true,
                    SalesLinePageSize = Models.Page.Pager.PageSize,                    
                    StoreName = "Acme Grocery",
                    Address = "Mandaue City",
                    OperatedBy = "Erwin Wilson Ceniza",
                    TIN = "00-122-124-7000",
                    PermitNo = "789-123-000",
                    AccreditNo = "234-233-111",
                    SerialNo = "123-456-000",
                    TerminalNo = "001",
                    ReceiptFooter = @"Not to be issued for Non-Vat/Exempt
Sales of goods, properties, services. If
issued, sales shall be subjected to VAT."

                };

                var tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Settings';";
                var result = conn.ExecuteScalar<string>(tableExistsQuery);

                if (string.IsNullOrEmpty(result))
                {
                    conn.CreateTable<Models.Settings>();
                    conn.Insert(Settings);
                }
            }
        }

        public static void UpdateLocalSettingsDB(mPOS.POCO.MstUser login)
        {
            using (var conn = new SQLiteConnection(App.FilePath)) 
            {
                Settings = conn.Query<Models.Settings>("SELECT * FROM Settings WHERE Id = ?", 1)
                    .FirstOrDefault();

                if (Settings != null)
                {
                    Settings.UserId = login.Id;
                    Settings.UserFullName = login.FullName;
                    Settings.LoginDate = DateTime.Now;

                    conn.RunInTransaction(() =>
                    {
                        if (conn != null) conn.Update(Settings);
                    });
                }
            }
        }
    }
}