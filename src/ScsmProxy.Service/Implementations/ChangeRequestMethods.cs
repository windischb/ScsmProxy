using System;
using System.Collections.Generic;
using ScsmClient;
using ScsmClient.SharedModels.Models;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.Service.Implementations
{

    public class ChangeRequestMethods: IChangeRequestMethods
    {
        private SCSMClient ScsmClient { get; }
        public ChangeRequestMethods(SCSMClient scsmClient)
        {
            ScsmClient = scsmClient;
        }


        public ChangeRequest GetByGenericId(Guid id, int? levels = null)
        {
            return ScsmClient.ChangeRequest().GetByGenericId(id, levels);
        }

        public ChangeRequest GetById(string id, int? levels = null)
        {
            return ScsmClient.ChangeRequest().GetById(id, levels);
        }

        public List<ChangeRequest> GetByCriteria(string criteria, RetrievalOptions retrievalOptions = null)
        {
            return ScsmClient.ChangeRequest().GetByCriteria(criteria, retrievalOptions);
        }

        public Guid Create(ChangeRequest changeRequest)
        {
            return ScsmClient.ChangeRequest().Create(changeRequest);
           
        }

        public Guid CreateFromTemplate(string templateName, ChangeRequest changeRequest)
        {
            
            return ScsmClient.ChangeRequest().CreateFromTemplate(templateName, changeRequest);
        }

        public void UpdateByGenericId(Guid genericId, Dictionary<string, object> properties)
        {
            ScsmClient.ChangeRequest().UpdateByGenericId(genericId, properties);
        }

        public void UpdateById(string changeId, Dictionary<string, object> properties)
        {
            ScsmClient.ChangeRequest().UpdateById(changeId, properties);
        }
    }
}
