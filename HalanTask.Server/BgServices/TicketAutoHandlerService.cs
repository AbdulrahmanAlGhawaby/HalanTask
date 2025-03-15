using HalanTask.Infrastructure.Context;
using HalanTask.Infrastructure.Repositories;

namespace HalanTask.Server.BgServices
{
    public class TicketAutoHandlerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TicketAutoHandlerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var _ticketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();
                var tickets = await _ticketRepository.GetUnHandledTicketsOlderThan(60);

                await _ticketRepository.MarkAsHandledAsync(tickets);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Run every minute
            }
        }
    }

}
