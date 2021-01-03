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
        
        ScsmObject[] GetObjectsByTypeName(string typeName, string criteria, int? maxResults = null, int? levels = null);
        ScsmObject[] GetObjectsByTypeId(string id, string criteria, int? maxResults = null, int? levels = null);
        ScsmObject[] GetObjectsByTypeId(Guid id, string criteria, int? maxResults = null, int? levels = null);
        Dictionary<int, Guid> CreateObjects(string className, Stream jsonArrayStream, CancellationToken cancellationToken);

        Dictionary<int, Guid> CreateObjectsFromTemplate(string templateName, Stream jsonStream,
            CancellationToken cancellationToken);

        void UpdateObject(Guid id, Dictionary<string, object> properties);
    }

}
