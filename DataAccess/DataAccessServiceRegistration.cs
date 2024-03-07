﻿using Core.Extensions;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework.Contexts;
using DataAccess.Concretes.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>
                (options => options.UseSqlServer(configuration.
                GetConnectionString("TobetoNet3AConnectionString")));

           
            services.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).
           Where(t => t.ServiceType.Name.EndsWith("Repository"));
            return services;
        }
        //services.AddScoped<IBlacklistRepository, BlacklistRepository>();




    }
}
