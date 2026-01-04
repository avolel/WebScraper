using CodeHollow.FeedReader;

namespace WebScraper.RSSReader
{
    public class SimpleRssReader
    {
        public static async Task<List<FeedItem>> GetLatestPostsAsync(string feedUrl)
        {
            try
            {
                var feed = await FeedReader.ReadAsync(feedUrl);
                var todaysPost = feed.Items.Where(item => item.PublishingDate.HasValue 
                    && item.PublishingDate.Value.Date == DateTime.Today).ToList();

                if(todaysPost == null || todaysPost.Count == 0)
                    return null;
                
                /*if(todaysPost.Count > 1)
                {
                    Random rand = new Random();
                    int randomIndex = rand.Next(0, todaysPost.Count);
                    return todaysPost.Skip(randomIndex).FirstOrDefault();
                }*/               

                return todaysPost;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting latest post", ex);
            }
        }
    }
}