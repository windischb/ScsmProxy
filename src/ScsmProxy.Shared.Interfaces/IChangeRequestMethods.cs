using System;
using System.Collections.Generic;
using ScsmClient.SharedModels.Models;

namespace ScsmProxy.Shared.Interfaces
{
    public interface IChangeRequestMethods
    {
        
        ChangeRequest GetByGenericId(Guid id, int? levels = null);
        ChangeRequest GetById(string id, int? levels = null);
        List<ChangeRequest> GetByCriteria(string criteria, RetrievalOptions retrievalOptions = null);
        Guid Create(ChangeRequest changeRequest);
        Guid CreateFromTemplate(string templateName, ChangeRequest changeRequest);
        void UpdateByGenericId(Guid genericId, Dictionary<string, object> properties);
        void UpdateById(string changeId, Dictionary<string, object> properties);
    }
}
