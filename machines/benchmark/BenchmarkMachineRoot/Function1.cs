using BenchmarkMachineRoot.Services;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkMachineRoot
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, ILogger log)
        {
            MachineResponseLog machineLog = new MachineResponseLog();
            Stopwatch s = new();
            s.Start();

            ApplicationDbContext context = InitializeContext.Initialize();
            List<Website> websites = WebsiteService.LoadWebsites(context);
            string apiKey = GTMetrixAPI.GetApiKey(context);
            

            foreach (var item in websites)
            {
                SendRequest.Send(item, apiKey);
                Thread.Sleep(1000);
            }

            machineLog.LogDate = DateTime.Now;
            machineLog.Log = $"Caller wykona³ pracê w {s.ElapsedMilliseconds} ms";

            context.Add(machineLog);
            context.SaveChanges();
        }
    }

    

    

    
}
