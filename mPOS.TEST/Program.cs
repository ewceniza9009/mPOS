using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using mPOS.POCO;
using Newtonsoft.Json;

namespace mPOS.TEST
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                //    var uri = (@"http://192.168.1.2/posserver/MstCustomer/GetCustomer");
                //    var uri = (@"http://localhost/posserver/MstCustomer/GetCustomers");
                var uri = (@"http://localhost/posserver/MstUser/canLogin");


                //    var filterParam = new MstUserFilter()
                //    {
                //        FullName = "Walk",
                //        filterMethods = new FilterMethods()
                //        {
                //            Operations = new List<FilterOperation>()
                //            {
                //                new FilterOperation("FullName", Operation.Contains),
                //            }
                //        }
                //    };

                //    var paramContent = JsonConvert.SerializeObject(filterParam);
                //    var buffer = System.Text.Encoding.UTF8.GetBytes(paramContent);
                //    var byteContent = new ByteArrayContent(buffer);

                //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //    var responseContent = await client.PostAsync(uri, byteContent);
                //    var response = await responseContent.Content.ReadAsStringAsync();
                //    var customers = JsonConvert.DeserializeObject<List<MstCustomer>>(response);

                //    customers.ForEach(x =>
                //    {
                //        Console.WriteLine(x.Customer);
                //    });
                //}

                //var response = await client.GetStringAsync($"{uri}?id={5520}");
                //var result = JsonConvert.DeserializeObject<MstCustomer>(response.ToString());

                var user = new MstUser()
                {
                    UserName = "admin",
                    Password = "1234"
                };


                var paramContent = JsonConvert.SerializeObject(user);
                var buffer = System.Text.Encoding.UTF8.GetBytes(paramContent);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseContent = await client.PostAsync(uri, byteContent);
                var response = await responseContent.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<bool>(response);

                Console.WriteLine(customers);

                Console.Read();
            }
        }
    }
}
