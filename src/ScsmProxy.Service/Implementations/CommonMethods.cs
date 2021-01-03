using ScsmClient;
using ScsmProxy.Shared.Interfaces;

namespace ScsmProxy.Service.Implementations
{


    public class CommonMethods: ICommonMethods
    {
        private SCSMClient ScsmClient { get; }
        public CommonMethods(SCSMClient scsmClient)
        {
            ScsmClient = scsmClient;
        }

        public string WhoAmI()
        {
            return ScsmClient.ManagementGroup.GetUserName();
        }
    }
}
