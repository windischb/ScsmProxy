using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Reflectensions.ExtensionMethods;
using ScsmClient.SharedModels.Models;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.ScripterModule
{


    public class ObjectMethods : IObjectMethods
    {
        private readonly IObjectMethods _methods;

        public ObjectMethods(IObjectMethods methods)
        {
            _methods = methods;
        }

        public ScsmObject GetByGenericId(string id)
        {
            return _methods.GetByGenericId(id);
        }

        public ScsmObject GetByGenericId(Guid id)
        {
            return _methods.GetByGenericId(id);
        }

        public ScsmObject[] GetObjectsByTypeName(string typeName, string criteria, int? maxResults = null, int? levels = null)
        {
            var objs = _methods.GetObjectsByTypeName(typeName, criteria, maxResults, levels);
            return objs;

        }

        public ScsmObject[] GetObjectsByTypeId(string id, string criteria, int? maxResults = null, int? levels = null)
        {
            return _methods.GetObjectsByTypeId(id, criteria, maxResults, levels);
        }
        public ScsmObject[] GetObjectsByTypeId(Guid id, string criteria, int? maxResults = null, int? levels = null)
        {
            return _methods.GetObjectsByTypeId(id, criteria, maxResults, levels);
        }

        public Dictionary<int, Guid> CreateObjectsFromTemplate(string templateName, Stream jsonStream, CancellationToken cancellationToken)
        {
            return _methods.CreateObjectsFromTemplate(templateName, jsonStream, cancellationToken);
        }

        public void UpdateObject(Guid id, Dictionary<string, object> properties)
        {
            _methods.UpdateObject(id, properties);
        }


        public Dictionary<int, Guid> CreateObjects(string className, Stream jsonStream, CancellationToken cancellationToken)
        {

            return _methods.CreateObjects(className, jsonStream, cancellationToken);

        }
        
    }
}
