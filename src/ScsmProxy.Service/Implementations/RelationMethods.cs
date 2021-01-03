using System;
using ScsmClient;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.Service.Implementations
{


    public class RelationMethods: IRelationMethods
    {
        private SCSMClient ScsmClient { get; }
        public RelationMethods(SCSMClient scsmClient)
        {
            ScsmClient = scsmClient;
        }


        public Guid CreateRelation(Guid source, Guid target)
        {
            return ScsmClient.Relations().CreateRelation(source, target);
        }
    }
}
