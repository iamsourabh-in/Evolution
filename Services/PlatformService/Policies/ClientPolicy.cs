using System.Net.Http;
using Polly.Retry;
using Polly;
using System;

namespace PlatformService.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediatePolicy { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearPolicy { get; }

        public AsyncRetryPolicy<HttpResponseMessage> ExponentialPolicy { get; }
        public AsyncRetryPolicy ExceptionPolicy { get; }

        public ClientPolicy()
        {
            ImmediatePolicy = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .RetryAsync(5);

            ExceptionPolicy = Policy.Handle<HttpRequestException>().RetryAsync(5);

            LinearPolicy = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => System.TimeSpan.FromSeconds(3));

            ExponentialPolicy = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(
                    5,
                    retryAttempt => System.TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                );
        }
    }
}
