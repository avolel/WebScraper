using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WebScraper.Models;

namespace WebScraper.Template
{
    public class InvoiceDocument : IDocument
    {
        public ScrapedItemSummary Model { get; }

        public InvoiceDocument(ScrapedItemSummary model) =>
            Model = model;

        public void Compose(IDocumentContainer container)
        {
            //Basic structure
            container.Page(page =>
            {
                page.Margin(50);
            
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);
                column.Item().AlignLeft().Text($"Title: {Model.Title}").FontSize(14);
                column.Item().AlignLeft().Text($"Author: {Model.Author}").FontSize(14);
                column.Item().AlignLeft().Text($"Date: {Model.DatePublished}").FontSize(14);
                column.Item().AlignLeft().Text($"Url: {Model.Url}").FontSize(14);
                column.Item().PaddingTop(25).Element(ComposeSummary);
                });
        }

        void ComposeSummary(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text(Model.Summary);
            });
        }
    }
}