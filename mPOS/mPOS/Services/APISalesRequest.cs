using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mPOS.Services
{
    public class APISalesRequest
    {
        private static readonly string UriBase = GlobalVariables.UriBase;

        public static async Task<ObservableCollection<POCO.MstCustomer>> GetCustomers()
        {
            ObservableCollection<POCO.MstCustomer> result;
            var requestUri = $@"{UriBase}/TrnSales/GetCustomers";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<POCO.MstCustomer>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<POCO.MstItem>> GetItems()
        {
            ObservableCollection<POCO.MstItem> result;
            var requestUri = $@"{UriBase}/TrnSales/GetItems";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<POCO.MstItem>>(response);
            }

            return result;
        }
    }
}
