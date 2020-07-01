using System;

namespace MeetupGemistBot.App.Application
{
    public class Settings : ISettings
    {
        public string RepositoryOwner => Environment.GetEnvironmentVariable("Repository.Owner");

        public string RepositoryName => Environment.GetEnvironmentVariable("Repository.Name");

        public string WebsiteRoot => Environment.GetEnvironmentVariable("Website.Root");

        public int NumberOfPullRequestsToQuery => 5;

        public int NumberOfFilesToQuery => 10;
    }
}
