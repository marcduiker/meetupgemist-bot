namespace MeetupGemistBot.App
{
    public static class ProductHeaderValueBuilder
    {
        const string ProductName = "MeetupGemistBot";

        public static Octokit.GraphQL.ProductHeaderValue BuildForOctokitGraphQL()
        {
            return new Octokit.GraphQL.ProductHeaderValue(ProductName);
        }

        public static Octokit.ProductHeaderValue BuildForortokitRest()
        {
            return new Octokit.ProductHeaderValue(ProductName);
        }
    }
}
