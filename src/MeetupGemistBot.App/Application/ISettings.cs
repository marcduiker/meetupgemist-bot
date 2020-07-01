namespace MeetupGemistBot.App.Application
{
    public interface ISettings
    {
        string RepositoryName { get; }
        string RepositoryOwner { get; }
        string WebsiteRoot { get; }
        int NumberOfPullRequestsToQuery { get; }
        int NumberOfFilesToQuery { get; }
    }
}