using MeetupGemistBot.App.Application;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Octokit;
using System;

[assembly: FunctionsStartup(typeof(StartUp))]
namespace MeetupGemistBot.App.Application
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var githubToken = Environment.GetEnvironmentVariable("GitHub.Token");
            builder.Services.AddSingleton<ISettings, Settings>();
            builder.Services.AddSingleton<Octokit.GraphQL.IConnection>(
                new Octokit.GraphQL.Connection(
                    ProductHeaderValueBuilder.BuildForOctokitGraphQL(),
                    githubToken));
            builder.Services.AddSingleton<IGitHubClient>(
                new GitHubClient(
                    new Octokit.Connection(ProductHeaderValueBuilder.BuildForortokitRest())
                    {
                        Credentials = new Credentials(githubToken)
                    }));
        }
    }
}
