using middlerApp.Agents.Shared;
using Scripter.Shared;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.ScripterModule
{
    public class ScsmProxyModule : IScripterModule
    {
        private readonly IMiddlerAgentsService _middlerAgentsService;


        public ScsmProxyModule(IMiddlerAgentsService middlerAgentsService)
        {
            _middlerAgentsService = middlerAgentsService;
        }

        public ScsmClient GetRandomClient()
        {
            var agent = _middlerAgentsService.GetRandomAgent();
            return new ScsmClient(agent);
        }

    }
}
