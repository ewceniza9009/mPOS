using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using mPOS.POCO;
using Newtonsoft.Json;

namespace mPOSv2.Services
{
    public class APISalesRequest
    {
        public static async Task<ObservableCollection<MstCustomer>> GetCustomers()
        {
            ObservableCollection<MstCustomer> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetCustomers";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
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

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
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

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
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

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstTax>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<MstTerm>> GetTerms()
        {
            ObservableCollection<MstTerm> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetTerms";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstTerm>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<MstPayType>> GetPaytTypes()
        {
            ObservableCollection<MstPayType> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetPayTypes";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstPayType>>(response);
            }

            return result;
        }

        public static async Task<ObservableCollection<MstDiscount>> GetDiscounts()
        {
            ObservableCollection<MstDiscount> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetDiscounts";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<MstDiscount>>(response);
            }

            return result;
        }
    }
}