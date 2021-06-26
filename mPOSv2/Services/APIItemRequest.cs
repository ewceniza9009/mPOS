using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using mPOS.POCO;
using Newtonsoft.Json;

namespace mPOSv2.Services
{
    public class APIItemRequest
    {
        public static async Task<ObservableCollection<MstUnit>> GetUnits()
        {
            ObservableCollection<MstUnit> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/MstItem/GetUnits";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstUnit>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<string>> GetItemCategories()
        {
            ObservableCollection<string> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/MstItem/GetItemCategories";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<string>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<MstTax>> GetTaxes()
        {
            ObservableCollection<MstTax> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/MstItem/GetTaxes";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstTax>>(response);
            }

            return result;
        }
    }
}