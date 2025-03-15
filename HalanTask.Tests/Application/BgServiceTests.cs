using FluentAssertions;
using HalanTask.Application.Handlers.Queries;
using HalanTask.Application.Requests.Queries;
using HalanTask.Domain.Models;
using HalanTask.Infrastructure.Context;
using HalanTask.Infrastructure.Repositories;
using HalanTask.Server.BgServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Tests.Application
{
    public class BgServiceTests
    {
        private readonly TicketAutoHandlerService _bgService;
        private readonly ITicketRepository _ticketRepository;
        private readonly IServiceProvider _serviceProvider;

        public BgServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBgDatabase")
                .Options;
            var dbContext = new ApplicationDbContext(options);

            _ticketRepository = new TicketRepository(dbContext);

            var services = new ServiceCollection();
            services.AddScoped(_ => _ticketRepository);
            _serviceProvider = services.BuildServiceProvider();

            _bgService = new TicketAutoHandlerService(_serviceProvider);
        }

        [Fact]
        public async Task Handle_ShouldHandleTicket()
        {
            // Arrange
            await _ticketRepository.AddAsync(new Ticket
            {
                Id = 1,
                PhoneNumber = "1234567890",
                Governorate = "Cairo",
                City = "Maadi",
                District = "Zahraa El Maadi",
                IsHandled = false,
                CreatedAt = DateTime.Now.AddMinutes(-60)
            });

            // Act
            var token = new CancellationToken();
            await _bgService.StartAsync(token);

            // Assert
            var ticket = await _ticketRepository.GetTicket(1);
            ticket?.IsHandled.Should().Be(true);

            await _bgService.StopAsync(token);

        }
    }
}
