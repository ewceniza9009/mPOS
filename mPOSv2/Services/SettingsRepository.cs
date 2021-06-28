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
                    _settings.SalesLinePageSize = settings.SalesLinePageSize;

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
                    SalesLinePageSize = Models.Page.Pager.PageSize
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