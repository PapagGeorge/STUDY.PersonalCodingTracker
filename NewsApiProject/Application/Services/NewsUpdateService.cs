using Domain.TechnicalKeywords;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;

namespace Application.Services
{
    public class NewsUpdateService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NewsUpdateService> _logger;
        private Timer _timer;

        public NewsUpdateService(IServiceProvider serviceProvider, ILogger<NewsUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("News Update Service is starting");
            _timer = new Timer(UpdateNews, null, TimeSpan.Zero, TimeSpan.FromHours(24));
            return Task.CompletedTask;
        }

        private async void UpdateNews(object state)
        {
            await UpdateNewsAsync(CancellationToken.None);
        }

        public async Task UpdateNewsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("News Update Service is working");
            using(var scope = _serviceProvider.CreateScope())
            {
                var newsService = scope.ServiceProvider.GetRequiredService<INewsService>();
                var keywords = TechnicalKeywords.GetAll();

                foreach(var keyword in keywords)
                {
                    try
                    {
                        _logger.LogInformation($"Updating news for keyword: {keyword}");
                        var newsApiResponse = await newsService.GetNewsApiResponse(keyword, forceRefresh: true);

                        if(newsApiResponse != null)
                        {
                            await newsService.SaveNewsApiResponse(newsApiResponse);
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, $"Error updating news for keyword {keyword}");
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("News update service is stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
