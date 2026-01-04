# WebScraper.Agentic

This project is a web scraping and content summarization tool built using C#. It scrapes articles from an RSS feed, processes the content with an AI model, and generates PDF summaries of the articles. The project uses various libraries for web scraping, semantic AI processing, PDF generation, and logging.

## Features

- **RSS Feed Parsing**: Fetches the latest articles from a given RSS feed.
- **Web Scraping**: Scrapes articles from the fetched links to extract the author's name and article content.
- **AI Summarization**: Uses the Ollama Chat API (GPT-based model) to summarize the article content.
- **PDF Generation**: Converts the AI-generated summaries into nicely formatted PDF documents using QuestPDF.
- **Logging**: Logs application activity with Serilog, providing detailed information about the scraping process and any errors.

## Prerequisites

Before you begin, make sure you have the following:

- .NET 6 or higher
- An instance of the Ollama Chat model running locally (required for AI summarization)
- [QuestPDF](https://www.questpdf.com/) library installed for PDF generation
- [Serilog](https://serilog.net/) for logging
- [Newtonsoft.Json](https://www.newtonsoft.com/json) for JSON serialization