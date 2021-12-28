using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistance;


namespace UptimeMachineRoot.Services
{
    public static class SaveLogs
    {
        public static void AddLog(ApplicationDbContext context, DateTime date, string log)
        {
            MachineUptimeLog machineUptimeRootLog = new();
            machineUptimeRootLog.LogDate = date;
            machineUptimeRootLog.Log = log;

            context.Add(machineUptimeRootLog);
            context.SaveChanges();
        }
    }
}
