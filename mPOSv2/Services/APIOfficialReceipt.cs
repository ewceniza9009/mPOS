using mPOS.POCO.Report;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mPOSv2.Services
{
    public class APIOfficialReceipt
    {
        public static async Task<OfficalReceipt> GetOfficialReceipt(int param)
        {
            OfficalReceipt result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetOfficialReceipt?salesId={param}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<OfficalReceipt>(response);
            }

            return result;
        }

        public static async Task<int> GetCollectionId(int param)
        {
            int result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetCollectionId?salesId={param}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler)) 
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<int>(response);
            }

            return result;
        }
    }
}
