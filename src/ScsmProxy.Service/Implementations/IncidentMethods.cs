using System;
using System.Collections.Generic;
using ScsmClient;
using ScsmClient.SharedModels.Models;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.Service.Implementations
{


    public class IncidentMethods: IIncidentMethods
    {
        private SCSMClient ScsmClient { get; }
        public IncidentMethods(SCSMClient scsmClient)
        {
            ScsmClient = scsmClient;
        }


        public Incident GetByGenericId(Guid id)
        {
            return ScsmClient.Incident().GetByGenericId(id);
        }

        public Incident GetById(string id)
        {
            return ScsmClient.Incident().GetById(id);
        }

        public List<Incident> GetByCriteria(string criteria, RetrievalOptions retrievalOptions = null)
        {
            return ScsmClient.Incident().GetByCriteria(criteria, retrievalOptions);
        }

        public Guid Create(Incident incident)
        {
            return ScsmClient.Incident().Create(incident);
           
        }

        public Guid CreateFromTemplate(string templateName, Incident incident)
        {
            
            return ScsmClient.Incident().CreateFromTemplate(templateName, incident);
        }
    }
}
