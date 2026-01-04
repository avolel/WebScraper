using System.Text;
using HtmlAgilityPack;

namespace WebScraper.PageScraper
{
    public class HTMLScraper
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<(string, string)> ScrapeAndReturnAuthorAriticle(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                StringBuilder fullArticle = new StringBuilder();
                response.EnsureSuccessStatusCode();
                string htmlContent = await response.Content.ReadAsStringAsync();

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var authorNode = htmlDocument.DocumentNode.SelectSingleNode("//div//div//div//span[@class='mises-link']//a[starts-with(@href, '/profile')]");
                var article_paragraphNodes = htmlDocument.DocumentNode.SelectNodes("//div[@data-component-id='mises:atom-body-copy']//div//p");

                if(article_paragraphNodes != null)
                {
                    foreach(var paragraphNode in article_paragraphNodes)
                        fullArticle.AppendLine(paragraphNode.InnerText.Trim());
                }

                return (authorNode.InnerText.Trim(), fullArticle.ToString());
            }
            catch(HttpRequestException httpEx)
            {
                throw new Exception("Error scraping web page", httpEx);
            }
        }       
    }
}