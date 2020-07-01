using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;

namespace MarcDuiker.MeetupGemistBot.App
{
    public class GetPullRequestsHttpTrigger
    {
        [FunctionName(nameof(GetPullRequestsHttpTrigger))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            var productInformation = new ProductHeaderValue("MeetupGemistBot", "v0.1");
            var connection = new Connection(productInformation, Environment.GetEnvironmentVariable("GitHub_Token"));

            var query = new Query()
                .RepositoryOwner("meetupgemist")
                .Repository("meetupgemist.github.io")
                .PullRequests(
                    first: 3, 
                    states: new[]{ PullRequestState.Merged }, 
                    orderBy: new IssueOrder { 
                        Field = IssueOrderField.CreatedAt, 
                        Direction = OrderDirection.Desc})
                .Nodes
                .Select(pr => new
                {
                    pr.Id,
                    pr.Title,
                    pr.Number,
                    files = pr.Files(1,null, null, null)
                        .Nodes
                        .Select(file => new
                        {
                            file.Additions,
                            file.Deletions,
                            file.Path
                        }).ToList()
                }).Compile();


            var result = await connection.Run(query);

            return new OkObjectResult(result);
        }
    }
}
