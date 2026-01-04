namespace WebScraper.Prompts
{
    public static class BlogPostPrompt
    {
        const string blogArticlePrompt = @"
            You are given a JSON object of the latest article published on a blog.
            The JSON object contains the following fields:

            1) Title
            2) Author
            3) DatePublished
            4) Article
            5) Url

            Analyze the article content and rewrite it in plain language so that an average reader with no specialized background can easily understand it.

            Instructions:

            1) Read the article content carefully.
            2) Write a clear, neutral, and informative rewrite of the article that explains the main ideas and key points in a way that is easy for the average person to read and understand.
            3) Do not copy sentences verbatim from the article.
            4) Return a JSON object that consist of the Author, Title, DatePublished, Summary, and Url.
            5) In your summary always refer the reader to the article Url so they can read the full article for themselves.

            Output:

            {
                'Title': 'Sample Title',
                'Author': 'John Doe',
                'DatePublished': '12/30/2025',
                'Summary': 'Summary goes here',
                'Url': 'https//someurl.com'
            }
        ";

        public static string BlogArticleUserPrompt { get{ return blogArticlePrompt; }}
    }
}