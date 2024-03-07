﻿using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MssqlLogger :LoggerServiceBase
    {
        public MssqlLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
            var logConfig = configuration.GetSection("SerilogConfigurations:MssqlConfiguration").Get<MssqlConfiguration>() ?? throw new Exception(SerilogMessages.NullOptionsMessage);
            MSSqlServerSinkOptions sinkOptions = new()
            { TableName = logConfig.TableName, AutoCreateSqlTable = logConfig.AutoCreateSqlTable };

            ColumnOptions columnOptions = new();
            global::Serilog.Core.Logger serilogConfig = new LoggerConfiguration().WriteTo
                .MSSqlServer(connectionString: logConfig.ConnectionString, sinkOptions: sinkOptions, columnOptions: columnOptions).CreateLogger();
            Logger = serilogConfig;


        }

    }
}
