    namespace WebScraper.Models
{
    public class ScrapedItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string DatePublished { get; set; }
        public string Article { get; set; }
        public string Url {get; set; }
    }

    public class ScrapedItemSummary
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string DatePublished { get; set; }
        public string Summary { get; set; }
        public string Url {get; set; }
    }
}