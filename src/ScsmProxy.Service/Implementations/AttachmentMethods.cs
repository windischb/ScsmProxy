using System;
using System.IO;
using ScsmClient;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.Service.Implementations
{

    public class AttachmentMethods: IAttachmentMethods
    {
        private SCSMClient ScsmClient { get; }
        public AttachmentMethods(SCSMClient scsmClient)
        {
            ScsmClient = scsmClient;
        }


        public Guid AddAttachment(Guid sourceId, string displayName, Stream content, string description)
        {
            return ScsmClient.Attachment().AddAttachment(sourceId, displayName, content, description);
        }
    }
}
