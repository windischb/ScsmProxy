using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ScsmClient.SharedModels.Models;

namespace ScsmProxy.Shared.Interfaces
{
    public interface IObjectMethods
    {
        ScsmObject GetByGenericId(string id);
        ScsmObject GetByGenericId(Guid id);
        
        ScsmObject[] GetObjectsByTypeName(string typeName, string criteria, RetrievalOptions retrievalOptions = null);
        ScsmObject[] GetObjectsByTypeId(string id, string criteria, RetrievalOptions retrievalOptions = null);
        ScsmObject[] GetObjectsByTypeId(Guid id, string criteria, RetrievalOptions retrievalOptions = null);
        Dictionary<int, Guid> CreateObjects(string className, Stream jsonArrayStream, CreateOptions createOptions, CancellationToken cancellationToken);

        Dictionary<int, Guid> CreateObjectsFromTemplate(string templateName, Stream jsonStream, CreateOptions createOptions, CancellationToken cancellationToken);

        void UpdateObject(Guid id, Dictionary<string, object> properties);
    }

}
