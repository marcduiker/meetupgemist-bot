using MeetupGemistBot.App.Application;
using MeetupGemistBot.App.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Octokit;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MeetupGemistBot.App.Activities
{
    public class GetPullRequestFilesActivity
    {

        private readonly IGitHubClient _gitHubClient;
        private readonly ISettings _settings;

        public GetPullRequestFilesActivity(
            IGitHubClient gitHubClient,
            ISettings settings)
        {
            _gitHubClient = gitHubClient;
            _settings = settings;
        }

        [FunctionName(nameof(GetPullRequestFilesActivity))]
        public async Task<PullRequestFileContent> Run(
          [ActivityTrigger] string path,
          ILogger logger)
        {
            var fileBytes = await _gitHubClient.Repository.Content.GetRawContent(
                _settings.RepositoryOwner,
                _settings.RepositoryName,
                path);
            var fileString = Encoding.UTF8.GetString(fileBytes);

            return new PullRequestFileContent(fileString);
            
        }
    }
}
