using System;
using System.Collections.Generic;
using ScsmClient;
using ScsmClient.SharedModels.Models;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.Service.Implementations
{


    public class ServiceRequestMethods: IServiceRequestMethods
    {
        private SCSMClient ScsmClient { get; }
        public ServiceRequestMethods(SCSMClient scsmClient)
        {
            ScsmClient = scsmClient;
        }


        public ServiceRequest GetByGenericId(Guid id)
        {
            return ScsmClient.ServiceRequest().GetByGenericId(id);
        }

        public ServiceRequest GetById(string id)
        {
            return ScsmClient.ServiceRequest().GetById(id);
        }

        public List<ServiceRequest> GetByCriteria(string criteria, RetrievalOptions retrievalOptions = null)
        {
            return ScsmClient.ServiceRequest().GetByCriteria(criteria, retrievalOptions);
        }

        public Guid Create(ServiceRequest ServiceRequest)
        {
            return ScsmClient.ServiceRequest().Create(ServiceRequest);
           
        }

        public Guid CreateFromTemplate(string templateName, ServiceRequest ServiceRequest)
        {
            
            return ScsmClient.ServiceRequest().CreateFromTemplate(templateName, ServiceRequest);
        }
    }
}
