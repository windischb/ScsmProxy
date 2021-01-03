using System;
using System.Collections.Generic;
using ScsmClient.SharedModels.Models;

namespace ScsmProxy.Shared.Interfaces
{
    public interface IServiceRequestMethods
    {
        ServiceRequest GetByGenericId(Guid id);
        ServiceRequest GetById(string id);
        List<ServiceRequest> GetByCriteria(string criteria, int? maxResults = null);
        Guid Create(ServiceRequest serviceRequest);
        Guid CreateFromTemplate(string templateName, ServiceRequest serviceRequest);
    }
}
