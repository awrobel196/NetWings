using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUptimeBenchmarkRepository, UptimeBenchmarkRepository>();
            services.AddTransient<IWebsiteRepository, WebsiteRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IRegisterRepository, RegisterRepository>();
            services.AddTransient<ILicenseRepository, LicenseRepository>();
            services.AddTransient<IResponseRepository, ResponseRepository>();

            return services;
        }

    }
}
