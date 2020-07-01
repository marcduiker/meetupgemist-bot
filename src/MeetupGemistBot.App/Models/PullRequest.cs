using System.Collections.Generic;

namespace MeetupGemistBot.App.Models
{
    public class PullRequest
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Number { get; set; }

        public IEnumerable<PullRequestFile> Files { get; set; }
    }
}
