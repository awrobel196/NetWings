using BenchmarkMachineRoot.Models;
using Domain.Entities;
using RestSharp;
using System;
using Newtonsoft.Json;
namespace BenchmarkMachineRoot.Services
{
    public static class SendRequest
    {
        public static void Send(Website item, string apiKey)
        {
            DataViewModel dataViewModel = new();
            dataViewModel.data.attributes.url = $"{item.Url}";
            string serializedBody = JsonConvert.SerializeObject(dataViewModel);


            var client = new RestClient("https://gtmetrix.com/api/2.0/tests");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/vnd.api+json");
            request.AddHeader("Authorization", $"Basic {apiKey}");
            request.AddParameter("application/vnd.api+json", serializedBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            while ((int)response.StatusCode == 429)
            {
                response = client.Execute(request);
            }
            var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
            Console.WriteLine($"{(int)response.StatusCode}");
            item.IsTestedByBenchmark = true;
            item.BenchmarkUrl = obj.links.self;
            item.LastTestedByBenchmark = DateTime.Now;
        }
    }
}
