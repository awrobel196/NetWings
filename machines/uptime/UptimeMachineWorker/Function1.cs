using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Netwings.UptimeMachine.Services;
using Netwings.UptimeMachine.ViewModel;
using Newtonsoft.Json;
using Domain.Entities;
using Infrastructure.Persistance;
using MongoDB.Driver;
using UptimeMachineWorker.Services;

namespace UptimeMachineWorker
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
           
            UptimeBenchmarkViewModel website = DeserializeRequest.Deserialize(req);
            WebsiteUptimeScore websiteUptimeScore = new();
            ApplicationDbContext context = InitializeContext.Initialize();

            (string statusNumber, int elapsedTime) WebsieTuple = CreateUptimeRequest.Create(website.WebsiteUrl);


            websiteUptimeScore.IdWebsite = new Guid($"{website.IdWebsite}");
            websiteUptimeScore.TestTime = website.UptimeBenchmarkDateTime;
            websiteUptimeScore.ElapsedTime = WebsieTuple.elapsedTime;
            websiteUptimeScore.StatusNumber = WebsieTuple.statusNumber;

            context.Add(websiteUptimeScore);
            context.SaveChanges();

            return new OkObjectResult($"true");
        }
    }
}
