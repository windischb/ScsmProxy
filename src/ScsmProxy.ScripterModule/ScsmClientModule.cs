using middlerApp.Agents.Shared;
using Scripter.Shared;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.ScripterModule
{
    public class ScsmClientModule : IScripterModule
    {
        private readonly IMiddlerAgentsService _middlerAgentsService;


        public ScsmClientModule(IMiddlerAgentsService middlerAgentsService)
        {
            _middlerAgentsService = middlerAgentsService;
        }


        public IObjectMethods ScsmObject()
        {
            var agent = _middlerAgentsService.GetRandomAgent();
            return new ObjectMethods(agent.GetInterface<IObjectMethods>());
        }

    }
}
