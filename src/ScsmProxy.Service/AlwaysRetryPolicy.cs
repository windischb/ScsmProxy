using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace ScsmProxy.Service
{
    public class AlwaysRetryPolicy : IRetryPolicy
    {
        private readonly TimeSpan _retryWait;

        public AlwaysRetryPolicy(TimeSpan retryWait)
        {
            _retryWait = retryWait;
        }

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return _retryWait;
        }

    }
}
