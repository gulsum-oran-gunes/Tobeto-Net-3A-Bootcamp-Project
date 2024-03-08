using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class CoreModuleExtensions
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services)
        {
            services.AddTransient<MongoDbLogger>(); 
            services.AddTransient<MssqlLogger>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            return services;
        }
    }
}
