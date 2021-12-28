using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistance;

namespace BenchmarkMachineRoot.Services
{
    public static class GTMetrixAPI
    {
        public static string GetApiKey(ApplicationDbContext context)
        {
            string apiKey = context.EnvVariable.Where(x => x.Name == "GTMetrixApiKey")
                .Select(x=>x.Value).FirstOrDefault();
            return apiKey;
        }
    }
}
