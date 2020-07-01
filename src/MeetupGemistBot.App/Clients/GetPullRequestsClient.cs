using MeetupGemistBot.App.Orchestrators;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeetupGemistBot.App.Clients
{
    public class GetPullRequestsClient
    {
        [FunctionName(nameof(GetPullRequestsClient))]
        public async Task<HttpResponseMessage> Run(
          [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestMessage message,
          [DurableClient] IDurableClient client,
          ILogger logger)
        {
           
            string instanceId = await client.StartNewAsync(
               nameof(GetPullRequestsOrchestrator),
               null);

            return client.CreateCheckStatusResponse(message, instanceId);
        }
    }
}
