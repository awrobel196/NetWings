using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkMachineWorker.Models;
using Domain.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace BenchmarkMachineWorker.Services
{
    public static class SendRequest
    {
        public static void Send
            (Website item, string apiKey, List<WebsiteResponseScore> benchmarkScoreList, List<MachineResponseLog> machineLog)
        {
            var singleWebsiteLog = new MachineResponseLog();
            //Wynik  pojedynczej strony
            WebsiteResponseScore score = new();
            var client = new RestClient($"{item.BenchmarkUrl}");
            client.AddDefaultHeader("Authorization", $"Basic {apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            //client.Authenticator = new HttpBasicAuthenticator("749c0ae4c2cdcde56b020964b6cb712f", "password");

            //request.AddHeader("Authorization", "Basic NzQ5YzBhZTRjMmNkY2RlNTZiMDIwOTY0YjZjYjcxMmY6:");
            IRestResponse response = client.Execute(request);

            Console.WriteLine($"Response status: {response.StatusCode}");
            Console.WriteLine($"Response number: {(int)response.StatusCode}");

            int numericStatusCode = (int)response.StatusCode;
            if (numericStatusCode == 401)
            {
                var redirectedClient = new RestClient(response.ResponseUri.ToString());
                redirectedClient.AddDefaultHeader("Authorization", $"Basic {apiKey}");
                IRestResponse newResponse = redirectedClient.Execute(request);
                Result obj = JsonConvert.DeserializeObject<Result>(newResponse.Content);

                score = obj.data.attributes;

                score.IdWebsite = item.IdWebsite;
                score.TestTime = DateTime.Now;
                benchmarkScoreList.Add(score);
                item.IsTestedByBenchmark = false;
                item.BenchmarkUrl = "";


            }
            //Jeśli nie zwóciło 401 znaczy że nie ma przekierowania więc strona dała 404, wtedy tylko resetujemy jej link do testow
            else
            {
                item.IsTestedByBenchmark = false;
                item.BenchmarkUrl = "";
            }

            singleWebsiteLog.Log = $"Reader wykonał pracę dla strony {item.Name}";
            singleWebsiteLog.LogDate = DateTime.Now;
            machineLog.Add(singleWebsiteLog);
        }
    }
}
