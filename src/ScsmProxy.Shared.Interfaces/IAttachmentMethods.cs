using System;
using System.IO;

namespace ScsmProxy.Shared.Interfaces
{
    public interface IAttachmentMethods
    {

        Guid AddAttachment(Guid sourceId, string displayName, Stream content, string description);
    }
}
