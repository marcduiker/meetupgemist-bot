namespace MeetupGemistBot.App.Models
{
    public class PullRequestFile
    {
        public int Additions { get; set; }

        public int Deletions { get; set; }

        public string FilePath { get; set; }
    }
}
