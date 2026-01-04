using WebScraper.PageScraper;
using WebScraper.RSSReader;
using WebScraper.Models;
using Newtonsoft.Json;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using WebScraper.Template;
using WebScraper.Prompts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebScraper.Agentic
{
    public class Agent : IAgent
    {
        private readonly ILogger<Agent> log;
        private readonly IConfiguration config;

        public Agent(ILogger<Agent> log, IConfiguration config)
        {
            this.log = log;
            this.config = config;
        }

        public async Task Run()
        {
            string url = "https://mises.org/rss.xml";
            var scraper = new HTMLScraper();
            ScrapedItem scrapedItem;
            string pdfSavePath = $"{Directory.GetCurrentDirectory()}/pdf/";
            QuestPDF.Settings.License = LicenseType.Community;

            try
            {
                var builder = Kernel.CreateBuilder();
                builder.AddOllamaChatCompletion(
                    modelId: "gpt-oss:20b", 
                    endpoint: new Uri("http://localhost:11434")
                );

                var kernel = builder.Build();
                var chatService = kernel.GetRequiredService<IChatCompletionService>();
                var history = new ChatHistory();
                
                var latestPost = await  SimpleRssReader.GetLatestPostsAsync(url);

                if (latestPost != null)
                {
                    Log.Information($"They are {latestPost.Count} posts for {DateTime.Today.ToShortDateString()}:");

                    foreach(var item in latestPost)
                    {
                        (string author, string article) = await scraper.ScrapeAndReturnAuthorAriticle(item.Link);
                        scrapedItem = new ScrapedItem
                        {
                            Title = item.Title,
                            DatePublished = item.PublishingDateString,
                            Author = author,
                            Article = article,
                            Url = item.Link
                        };

                        string jsonOutput = JsonConvert.SerializeObject(scrapedItem, Formatting.Indented);
                        string userPrompt = BlogPostPrompt.BlogArticleUserPrompt;
                        string userMessage = $"{userPrompt} {jsonOutput}";
                        history.AddUserMessage(userMessage);
                        var response = await chatService.GetChatMessageContentAsync(history);    
                        ScrapedItemSummary summary = JsonConvert.DeserializeObject<ScrapedItemSummary>(response.Content);
                        var document = new InvoiceDocument(summary);
                        document.GeneratePdf($"{pdfSavePath}{summary.Title}.pdf");
                        history.AddAssistantMessage(response.Content);
                    }
                }
                else
                Log.Information("No post was found for today."); 
            }
            catch(Exception e)
            {
                Log.Error(e,"Opps Something went wrong. Check Logs.");                
            }
        }
        
    }
}