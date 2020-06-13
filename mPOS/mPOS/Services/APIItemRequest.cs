using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mPOS.Services
{
    public class APIItemRequest
    {
        private static readonly string UriBase = GlobalVariables.UriBase;

        public static async Task<ObservableCollection<POCO.MstUnit>> GetUnits()
        {
            ObservableCollection<POCO.MstUnit> result;
            var requestUri = $@"{UriBase}/MstItem/GetUnits";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<POCO.MstUnit>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<string>> GetCategories()
        {
            return null;
        }
    }
}
