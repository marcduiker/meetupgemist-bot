using MeetupGemistBot.App.Application;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetupGemistBot.App.Activities
{
    public class GetPullRequestsActivity
    {
        private readonly IConnection _octokitConnection;
        private readonly ISettings _settings;


        public GetPullRequestsActivity(
            IConnection octokitConnection,
            ISettings settings)
        {
            _octokitConnection = octokitConnection;
            _settings = settings;
        }
        
        [FunctionName(nameof(GetPullRequestsActivity))]
        public async Task<IEnumerable<Models.PullRequest>> Run(
          [ActivityTrigger] string input)
        {
            // This GraphQL query retreives the three most recent 
            // Pull requests from the given repository.
            // It projects the results to PullRequest & PullRequestFile objects.
            var query = new Query()
                .RepositoryOwner(_settings.RepositoryOwner)
                .Repository(_settings.RepositoryName)
                .PullRequests(
                    first: _settings.NumberOfPullRequestsToQuery,
                    states: new[] { PullRequestState.Merged },
                    orderBy: new IssueOrder
                    {
                        Field = IssueOrderField.CreatedAt,
                        Direction = OrderDirection.Desc
                    })
                .Nodes
                .Select(pr => new Models.PullRequest
                {
                    Id = pr.Id.Value,
                    Title = pr.Title,
                    Number= pr.Number,
                    Files = pr.Files(_settings.NumberOfFilesToQuery, null, null, null)
                        .Nodes
                        .Select(file => new Models.PullRequestFile
                        {
                            Additions = file.Additions,
                            Deletions = file.Deletions,
                            FilePath = file.Path
                        }).ToList()
                }).Compile();


            return await _octokitConnection.Run(query);
        }
    }
}
