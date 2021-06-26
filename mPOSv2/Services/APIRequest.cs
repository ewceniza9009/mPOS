using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mPOSv2.Services
{
    public class ApiRequest<T, TResult> where T : class
    {
        public static async Task<TResult> Read(string route, long id)
        {
            TResult result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/{route}?id={id}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<TResult>(response);
            }

            return result;
        }

        public static async Task<TResult> PostRead(string route = null, T arg = null)
        {
            TResult result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/{route}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {

                var paramContent = JsonConvert.SerializeObject(arg);
                var buffer = Encoding.UTF8.GetBytes(paramContent);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseContent = await client.PostAsync(requestUri, byteContent);
                var response = await responseContent.Content.ReadAsStringAsync();
                var typeParameterType = typeof(TResult);

                if (typeParameterType.FullName != null && typeParameterType.FullName.Contains("TrnSales"))
                    result = JsonConvert.DeserializeObject<TResult>(response, new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local,
                        DateParseHandling = DateParseHandling.DateTimeOffset
                    });
                else
                    result = JsonConvert.DeserializeObject<TResult>(response);
            }

            return result;
        }

        public static async Task<long> Save(string route, T arg)
        {
            long result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/{route}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var paramContent = JsonConvert.SerializeObject(arg);
                var buffer = Encoding.UTF8.GetBytes(paramContent);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseContent = await client.PostAsync(requestUri, byteContent);
                var response = await responseContent.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<long>(response);
            }

            return result;
        }

        public static async Task Delete(string route, long id)
        {
            var requestUri = $@"{GlobalVariables.GetUriBase()}/{route}?id={id}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                await client.DeleteAsync(requestUri);
            }
        }
    }
}