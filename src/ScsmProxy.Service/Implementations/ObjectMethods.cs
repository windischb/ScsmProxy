using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reflectensions;
using Reflectensions.ExtensionMethods;
using ScsmClient;
using ScsmClient.SharedModels.Models;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.Service.Implementations
{


    public class ObjectMethods : IObjectMethods
    {
        private SCSMClient ScsmClient { get; }
        public ObjectMethods(SCSMClient scsmClient)
        {
            ScsmClient = scsmClient;
        }

        public ScsmObject GetByGenericId(string id)
        {
            return GetByGenericId(id.ToGuid());
        }

        public ScsmObject GetByGenericId(Guid id)
        {
            return ScsmClient.ScsmObject().GetObjectById(id);
        }

        public ScsmObject[] GetObjectsByTypeName(string typeName, string criteria, int? maxResults = null, int? levels = null)
        {
            var objs = ScsmClient.ScsmObject()
                .GetObjectsByTypeName(typeName, criteria, maxResults, levels).ToArray();
            return objs;
        }

        public ScsmObject[] GetObjectsByTypeId(string id, string criteria, int? maxResults = null, int? levels = null)
        {
            return GetObjectsByTypeId(id.ToGuid(), criteria, maxResults, levels);
        }

        public ScsmObject[] GetObjectsByTypeId(Guid id, string criteria, int? maxResults = null, int? levels = null)
        {
            return ScsmClient.ScsmObject()
                .GetObjectsByTypeId(id, criteria, maxResults, levels).ToArray();
        }

        //public Guid CreateObject(string className, Dictionary<string, object> properties)
        //{
        //    var obj = ScsmClient.Object().CreateObjectByClassName(className, properties);
        //    return obj;

        //}

        //public Guid CreateObjectFromTemplate(string templateName, Dictionary<string, object> properties)
        //{
        //    var obj = ScsmClient.Object().CreateObjectFromTemplateName(templateName, properties);
        //    return obj;
        //}

        public Dictionary<int, Guid> CreateObjectsFromTemplate(string templateName, Stream jsonStream, CancellationToken cancellationToken)
        {
            if (jsonStream == null || jsonStream.CanRead == false)
                throw new InvalidCastException(nameof(jsonStream));

            var objectList = FromStreamToEnumerable(jsonStream, cancellationToken);

            return ScsmClient.Object().CreateObjectsFromTemplateName(templateName, objectList, cancellationToken);
        }

        public void UpdateObject(Guid id, Dictionary<string, object> properties)
        {
            ScsmClient.Object().UpdateObject(id, properties);
        }


        public Dictionary<int, Guid> CreateObjects(string className, Stream jsonStream, CancellationToken cancellationToken)
        {

            if (jsonStream == null || jsonStream.CanRead == false)
                throw new InvalidCastException(nameof(jsonStream));

            var objectList = FromStreamToEnumerable(jsonStream, cancellationToken);

            return ScsmClient.Object().CreateObjectsByClassName(className, objectList, cancellationToken);

        }

        private IEnumerable<Dictionary<string, object>> FromStreamToEnumerable(Stream jsonStream, CancellationToken cancellationToken)
        {

            using (StreamReader sr = new StreamReader(jsonStream))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var jObject = JObject.Load(reader);
                        yield return Json.Converter.ToDictionary(jObject);
                    }
                }
            }

        }
    }
}
