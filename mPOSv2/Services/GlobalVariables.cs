﻿using mPOSv2.Models;
using SQLite;

namespace mPOSv2.Services
{
    public static class GlobalVariables
    {
        //public static readonly string UriBase = "http://10.0.2.2/posserver";
        public static readonly string UriBase = "http://192.168.1.8/posserver";

        public static string GetUriBase()
        {
            string result;

            using (var conn = new SQLiteConnection(App.FilePath))
            {
                result = conn.Table<Settings>()
                    .FirstOrDefault()
                    .ServerName;
            }

            return result;
        }
    }
}