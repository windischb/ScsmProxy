using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reflectensions;
using ScsmClient;
using ScsmClient.JsonConverters;
using ScsmProxy.Service.Implementations;
using ScsmProxy.Shared.Interfaces;
using Serilog;
using SignalARRR.Client;

namespace ScsmProxy.Service
{
    public class MiddlerAgentService : BackgroundService
    {
        private readonly ILogger<MiddlerAgentService> _logger;
        private readonly StartUpConfiguration _startUpConfiguration;

        private HARRRConnection _connection;
        private SCSMClient _scsmClient;

        public MiddlerAgentService(ILogger<MiddlerAgentService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _startUpConfiguration = configuration.Get<StartUpConfiguration>();

            Json.Converter.RegisterJsonConverter<ManagementGroupConverter>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BuildHarrrConnectionAsync(stoppingToken);
        }
        
        private async Task BuildHarrrConnectionAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (true && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {

                        if (!String.IsNullOrWhiteSpace(_startUpConfiguration.ScsmProxy.Username) &&
                            !String.IsNullOrWhiteSpace(_startUpConfiguration.ScsmProxy.Password))
                        {
                            var creds = new NetworkCredential(_startUpConfiguration.ScsmProxy.Username, _startUpConfiguration.ScsmProxy.Password);
                            _scsmClient = new SCSMClient(_startUpConfiguration.ScsmProxy.ScsmServer, creds);
                        }
                        else
                        {
                            _scsmClient = new SCSMClient(_startUpConfiguration.ScsmProxy.ScsmServer);
                        }

                        _connection = HARRRConnection.Create(builder => builder
                                .WithUrl(_startUpConfiguration.MiddlerAgentUrl, options =>
                                {
                                    options.HttpMessageHandlerFactory = (message) =>
                                    {
                                        if (message is HttpClientHandler clientHandler)
                                            // bypass SSL certificate
                                            clientHandler.ServerCertificateCustomValidationCallback +=
                                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                                        return message;
                                    };
                                    options.Headers["#Hostname"] = Environment.MachineName;

                                })
                                //.AddMessagePackProtocol()
                                .AddNewtonsoftJsonProtocol()
                                .ConfigureLogging(log =>
                                {

                                    //log.AddProvider(_logger.AsLoggerProvider());
                                    log.AddSerilog();

                                })
                                .WithAutomaticReconnect(new AlwaysRetryPolicy(TimeSpan.FromSeconds(10)))
                                , builder => builder.UseHttpResponse()
                        );

                        _connection.RegisterInterface<IObjectMethods, ObjectMethods>(_ => new ObjectMethods(_scsmClient));

                        _connection.RegisterInterface<IIncidentMethods, IncidentMethods>(_ => new IncidentMethods(_scsmClient));
                        _connection.RegisterInterface<IServiceRequestMethods, ServiceRequestMethods>(_ => new ServiceRequestMethods(_scsmClient));
                        _connection.RegisterInterface<IChangeRequestMethods, ChangeRequestMethods>(_ => new ChangeRequestMethods(_scsmClient));
                        _connection.RegisterInterface<ICommonMethods, CommonMethods>(_ => new CommonMethods(_scsmClient));
                        _connection.RegisterInterface<IRelationMethods, RelationMethods>(_ => new RelationMethods(_scsmClient));
                        _connection.RegisterInterface<IAttachmentMethods, AttachmentMethods>(_ => new AttachmentMethods(_scsmClient));

                        


                        await _connection.StartAsync(cancellationToken);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                    }
                }
            }
            catch (TaskCanceledException e)
            {

            }
        }
    }
}
