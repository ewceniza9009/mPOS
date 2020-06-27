using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace mPOSv2.Services
{
    public static class SettingsRepository
    {
        public static Models.Settings GetSettings()
        {
            Models.Settings result;

            using (var conn = new SQLiteConnection(App.FilePath))
            {

                result = conn.Table<Models.Settings>().FirstOrDefault();
            }

            return result;
        }

        public static void Save(Models.Settings settings)
        {
            using (var conn = new SQLiteConnection(App.FilePath))
            {
                var _settings = conn.Query<Models.Settings>("SELECT * FROM Settings WHERE Id = ?", 1).FirstOrDefault();

                if (_settings != null)
                {
                    _settings.UserId = settings.Id;
                    _settings.UserFullName = settings.UserFullName;
                    _settings.ServerName = settings.ServerName;
                    _settings.ContinuesBarcode = settings.ContinuesBarcode;

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

                result = conn.Table<Models.Settings>()
                    .FirstOrDefault()
                    .UserFullName;
            }

            return result;
        }
    }
}
