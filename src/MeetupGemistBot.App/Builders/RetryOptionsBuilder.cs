using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;

namespace MeetupGemistBot.App.Builders
{
    public static class RetryOptionsBuilder
    {
        public static RetryOptions BuildForGitHub()
        {
            return new RetryOptions(TimeSpan.FromSeconds(2), 3) { BackoffCoefficient = 2 };
        }
    }
}
