using System;
using System.Collections.Generic;
using ScsmClient.SharedModels.Models;

namespace ScsmProxy.Shared.Interfaces
{
    public interface IIncidentMethods
    {
        Incident GetByGenericId(Guid id);
        Incident GetById(string id);
        List<Incident> GetByCriteria(string criteria, int? maxResults = null);
        Guid Create(Incident incident);
        Guid CreateFromTemplate(string templateName, Incident incident);
    }
}
