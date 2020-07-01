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
using Octokit;
using System.Text.RegularExpressions;

namespace MarcDuiker.MeetupGemistBot.App
{
    public class GetPullRequestFileHttpTrigger
    {
        [FunctionName(nameof(GetPullRequestFileHttpTrigger))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            var productInformation = new ProductHeaderValue("MeetupGemistBot", "v0.1");
            var client = new GitHubClient(productInformation);
            var credentials = new Credentials(Environment.GetEnvironmentVariable("GitHub_Token"));
            client.Credentials = credentials;
            var path = "_videos/2020-06-09-SDN.md";

            var contentBytes = await client.Repository.Content.GetRawContent("meetupgemist", "meetupgemist.github.io", path);
            var contentString = System.Text.Encoding.UTF8.GetString(contentBytes);
            // Regex with two capture groups; one for the title and one for the YoutTubeID
            var pattern = $"^title:\\s(?<TITLE>.*)\\nyoutube_id:\\s(?<YOUTUBEID>.*)";
            var regex = new Regex(pattern, RegexOptions.Multiline);
            var match = regex.Match(contentString);

            dynamic returnObject=null;
            if (match.Groups.ContainsKey("TITLE") && match.Groups.ContainsKey("YOUTUBEID"))
            {
                returnObject = new { 
                    Title = match.Groups["TITLE"].Value, YouTubeId = match.Groups["YOUTUBEID"].Value};
            }

            return new OkObjectResult(returnObject);
    }
}
}
