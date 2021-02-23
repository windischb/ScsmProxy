using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using middlerApp.Agents.Shared;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.ScripterModule
{
    public class ScsmClient
    {
        private readonly IMiddlerAgent _middlerAgent;


        public ScsmClient(IMiddlerAgent middlerAgent)
        {
            _middlerAgent = middlerAgent;
        }


        public IObjectMethods ScsmObjects => Get<IObjectMethods>();

        public IIncidentMethods Incidents => Get<IIncidentMethods>();
        public IServiceRequestMethods ServiceRequests => Get<IServiceRequestMethods>();
        public IChangeRequestMethods ChangeRequests => Get<IChangeRequestMethods>();
        
        public IRelationMethods Relations => Get<IRelationMethods>();
        public IAttachmentMethods Attachments => Get<IAttachmentMethods>();

        public ICommonMethods Common => Get<ICommonMethods>();


        private T Get<T>() where T : class
        {
            return _middlerAgent.GetInterface<T>();
        }
    }
}
