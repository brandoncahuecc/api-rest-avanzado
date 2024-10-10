using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rest_biblioteca.Dependencias
{
    public static class LoggerDependencia
    {
        public static ILoggingBuilder AgregarLogging(this ILoggingBuilder logging)
        {
            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("./Recursos/serilog-config.json").Build())
                .Enrich.FromLogContext().CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog(loggerConfig);

            return logging;
        }
    }
}
