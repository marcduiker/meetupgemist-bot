using MeetupGemistBot.App.Activities;
using MeetupGemistBot.App.Builders;
using MeetupGemistBot.App.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupGemistBot.App.Orchestrators
{
    public class GetPullRequestsOrchestrator
    {
        [FunctionName(nameof(GetPullRequestsOrchestrator))]
        public async Task Run(
          [OrchestrationTrigger] IDurableOrchestrationContext context,
          ILogger logger)
        {
            var pullRequests = await context.CallActivityWithRetryAsync<IEnumerable<Models.PullRequest>>(
                nameof(GetPullRequestsActivity),
                RetryOptionsBuilder.BuildForGitHub(),
                null);
            const int minNrOfLines = 7;

            var pullRequestContentTaskList = new List<Task<PullRequestFileContent>>();

            foreach (var pullRequest in pullRequests)
            {

                // Check if pull request is new

                foreach (var pullRequestFile in pullRequest.Files.Where(file => file.Additions >= minNrOfLines))
                {
                    // Determine if the PR Number is already known
                    // If not collect the file content
                    var pullRequestFileContentTask = context.CallActivityWithRetryAsync<PullRequestFileContent>(
                        nameof(GetPullRequestFilesActivity),
                        RetryOptionsBuilder.BuildForGitHub(),
                        pullRequestFile);

                    pullRequestContentTaskList.Add(pullRequestFileContentTask);
                }
            }

            var pullRequestFileContentResults = await Task.WhenAll(pullRequestContentTaskList);

            // TODO store pullRequestFileContentResults as entities.

            // TODO generate messages for new pullRequestFileContent

            // TODO post message to twitter

        }
    }
}
