using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistance;

namespace BenchmarkMachineRoot.Services
{
    public static class WebsiteService
    {
        public static List<Website> LoadWebsites(ApplicationDbContext context)
        {
            List<Website> websites = context.Website.Where(x => x.IsTestedByBenchmark == false).ToList();
            return websites;
        }
    }
}
