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
    public class APISalesRequest
    {
        public static async Task<ObservableCollection<MstCustomer>> GetCustomers()
        {
            ObservableCollection<MstCustomer> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetCustomers";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstCustomer>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<MstItem>> GetItems()
        {
            ObservableCollection<MstItem> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetItems";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstItem>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<MstUnit>> GetUnits()
        {
            ObservableCollection<MstUnit> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/MstItem/GetUnits";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstUnit>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<MstTax>> GetTaxes()
        {
            ObservableCollection<MstTax> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetTaxes";

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstTax>>(response);
            }

            return result;
        }
    }
}
