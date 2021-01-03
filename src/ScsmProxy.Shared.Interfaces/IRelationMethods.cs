using System;

namespace ScsmProxy.Shared.Interfaces
{
    public interface IRelationMethods
    {

        Guid CreateRelation(Guid source, Guid target);
    }
}
