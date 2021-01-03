using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reflectensions;
using ScsmProxy.Service.Helper;
using Serilog;

namespace ScsmProxy.Service
{
    public class Program
    {
        private static bool IsService { get; set; }

        public static int Main(string[] args)
        {
            IsService = !(Debugger.IsAttached || args.Contains("--console"));
            var webHostArgs = args.Where(arg => arg != "--console").ToArray();

            try
            {
                ConfigureLogging();
                Log.Information("Starting host");
                var hostBuilder = CreateHostBuilder(webHostArgs);
                if (IsService)
                {
                    hostBuilder.UseWindowsService();
                }

                var host = hostBuilder.Build();
                host.Run();
                
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(Log.Logger)
                .ConfigureAppConfiguration(BuildHostConfiguration)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MiddlerAgentService>();
                });

        private static void BuildHostConfiguration(HostBuilderContext context, IConfigurationBuilder config)
        {
            BuildConfiguration(config);
        }

        private static void ConfigureLogging()
        {

            var configBuilder = BuildConfiguration(null);
            StartUpConfiguration startUpConfiguration = configBuilder.Build().Get<StartUpConfiguration>();

            var logConfig = new LoggerConfiguration();

            foreach (var kv in startUpConfiguration.Logging.LogLevels)
            {
                var k = kv.Key;//.Replace('_', '.');
                if (k.Equals("default", StringComparison.OrdinalIgnoreCase) || k.Equals("*", StringComparison.OrdinalIgnoreCase))
                {
                    logConfig.MinimumLevel.Is(kv.Value);
                }
                else
                {
                    logConfig.MinimumLevel.Override(k, kv.Value);
                }

            }

            if (!string.IsNullOrWhiteSpace(startUpConfiguration.Logging.LogPath))
            {
                var path = PathHelper.GetFullPath(startUpConfiguration.Logging.LogPath);
                path = Path.Combine(path, "log.txt");
                logConfig = logConfig.WriteTo.File(path, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31);
            }

            if (!IsService)
            {
                logConfig = logConfig.WriteTo.Console();
            }

            Log.Logger = logConfig.Enrich.FromLogContext()
                .CreateLogger();

        }

        private static IConfigurationBuilder BuildConfiguration(IConfigurationBuilder config)
        {
            if (config == null)
            {
                config = new ConfigurationBuilder();
            }

            if (!Debugger.IsAttached)
            {
                var file = PathHelper.GetFullPath("configuration.json");
                if (!File.Exists(file))
                {
                    var json = Json.Converter.ToJson(new StartUpConfiguration(), true);
                    File.WriteAllText(file, json);
                }

                config.AddJsonFile(file, optional: true);
            }

            config.AddEnvironmentVariables();
            return config;
        }

        
    }
}
