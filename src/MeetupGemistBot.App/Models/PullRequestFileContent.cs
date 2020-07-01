using System.Text.RegularExpressions;

namespace MeetupGemistBot.App.Models
{
    public class PullRequestFileContent
    {
        public PullRequestFileContent(string fileContent)
        {
            MatchPattern(fileContent);
        }
        
        public string Title { get; private set; }

        public string YouTubeId { get; private set; }

        public bool IsMatch { get; private set; }

        private void MatchPattern(string fileContent)
        {
            // Regex with two capture groups; one for the title and one for the YouTubeID
            const string TitleCaptureGroup = "TITLE";
            const string YouTubeIdCaptureGroup = "YOUTUBEID";
            var pattern = $"^title:\\s(?<{TitleCaptureGroup}>.*)\\nyoutube_id:\\s(?<{YouTubeIdCaptureGroup}>.*)";
            var regex = new Regex(pattern, RegexOptions.Multiline);
            var match = regex.Match(fileContent);

            if (match.Success)
            {
                Title = match.Groups[TitleCaptureGroup].Value;
                YouTubeId = match.Groups[YouTubeIdCaptureGroup].Value;
            }

            IsMatch = match.Success &&
                !string.IsNullOrWhiteSpace(Title) &&
                !string.IsNullOrWhiteSpace(YouTubeId);
        }
    }
}
