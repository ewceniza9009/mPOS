﻿using mPOS.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mPOSv2.Services
{
    public class APISalesReportRequest
    {
        public static async Task<ObservableCollection<TrnSales>> GetSalesReport(string param)
        {
            ObservableCollection<TrnSales> result;
            var requestUri = $@"{GlobalVariables.GetUriBase()}/TrnSales/GetSalesReport?param={param}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<ObservableCollection<TrnSales>>(response);
            }

            return result;
        }
    }
}
