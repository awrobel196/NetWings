using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkMachineWorker.Services;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BenchmarkMachineWorker
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 20 23 * * *")]TimerInfo myTimer, ILogger log)
        {
            List<MachineResponseLog> machineLog = new List<MachineResponseLog>();
            Stopwatch s = new();
            s.Start();
            

            ApplicationDbContext context = InitializeContext.Initialize();
            List<Website> websites = WebsiteService.LoadWebsites(context);
            List<WebsiteResponseScore> benchmarkScoreList = new();
            string apiKey = GTMetrixAPI.GetApiKey(context);

            foreach (var item in websites)
            {
                SendRequest.Send(item,apiKey,benchmarkScoreList,machineLog);
            }

            var machineWebsiteLog = new MachineResponseLog();
            machineWebsiteLog.LogDate = DateTime.Now;
            machineWebsiteLog.Log = $"Reader wykona³ pracê w {s.ElapsedMilliseconds} ms";
            machineLog.Add(machineWebsiteLog);

            context.AddRange(machineLog);
            context.AddRange(benchmarkScoreList);
            context.SaveChanges();
        }
    }
}
