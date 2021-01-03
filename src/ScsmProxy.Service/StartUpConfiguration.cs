using System;
using System.Collections.Generic;
using Serilog.Events;

namespace ScsmProxy.Service
{
    public class StartUpConfiguration
    {
        public string MiddlerAgentUrl { get; set; } = "https://localhost:4444/signalr/ra";

        public Logging Logging { get; set; } = new Logging();

        public ScsmProxy ScsmProxy { get; set; } = new ScsmProxy();
    }

    public class Logging
    {
        public string LogPath { get; set; } = "logs";

        private Dictionary<string, LogEventLevel> _loglevels;

        public Dictionary<string, LogEventLevel> LogLevels
        {
            get
            {
                if (_loglevels == null)
                {
                    _loglevels = GetDefaultLoggings();
                }

                return _loglevels;
            }
            set => _loglevels = value;
        }



        internal static Dictionary<string, LogEventLevel> GetDefaultLoggings()
        {
            return new Dictionary<string, LogEventLevel>(StringComparer.OrdinalIgnoreCase)
            {
                ["Default"] = LogEventLevel.Information,
                ["Microsoft.Hosting.Lifetime"] = LogEventLevel.Information
            };
        }
    }

    public class ScsmProxy
    {
        public string ScsmServer { get; set; } = "10.0.0.98";
        public string Username { get; set; } = "BWLAB\\admin";
        public string Password { get; set; } = "ABC12abc";

    }
}
