using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using UptimeMachineRoot.Services;
using UptimeMachineRoot.ViewModels;

namespace UptimeMachineRoot
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            Stopwatch s = new();
            s.Start();

            ApplicationDbContext context = InitializeContext.Initialize();
            DateTime date = GetDateTime.GetCurrentDateTime();

            List<WebsiteUptimeMachineLibrary> machines = context.WebsiteUptimeMachineLibrary.ToList();
            List<Website> websites = context.Website.ToList();


            foreach (var item in websites)
            {
                UptimeBenchmarkViewModel model = new();
                model.IdWebsite = item.IdWebsite.ToString();
                model.WebsiteUrl = item.Url;
                model.UptimeBenchmarkDateTime = date;
                model.MachineUrl = machines.Where(x => x.MachineLocation == item.TestLocation)
                    .Select(x => x.MachineAdress)
                    .FirstOrDefault();

                Task task = new Task(() => SendRequest.Send(model));
                task.Start();
            }

            Task.WaitAll();

            SaveLogs.AddLog(context, date, $"Wykonano w czasie {s.ElapsedMilliseconds.ToString()}ms");
        }
    }
}
