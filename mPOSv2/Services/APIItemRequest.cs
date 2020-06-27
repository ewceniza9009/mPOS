using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using mPOS.POCO;
using mPOSv2.Services;
using Newtonsoft.Json;

namespace mPOSv2.Services
{
    public class APIItemRequest
    {
        private static readonly string UriBase = GlobalVariables.UriBase;

        public static async Task<ObservableCollection<MstUnit>> GetUnits()
        {
            ObservableCollection<MstUnit> result;
            var requestUri = $@"{UriBase}/MstItem/GetUnits";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstUnit>>(response);
            }

            return result;
        }
    }
}
