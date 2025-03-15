using FluentAssertions;
using HalanTask.Application.Handlers.Queries;
using HalanTask.Application.Requests.Queries;
using HalanTask.Domain.Models;
using HalanTask.Infrastructure.Context;
using HalanTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Tests.Application
{
    public class GetTicketsQueryHandlerTests
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly GetTicketsQueryHandler _handler;

        public GetTicketsQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            _ticketRepository = new TicketRepository(dbContext);
            _handler = new GetTicketsQueryHandler(_ticketRepository);
        }

        [Fact]
        public async Task Handle_ShouldReturnTickets()
        {
            // Arrange
            await _ticketRepository.AddAsync(new Ticket
            {
                Id = 1,
                PhoneNumber = "1234567890",
                Governorate = "Cairo",
                City = "Maadi",
                District = "Zahraa El Maadi",
            });

            // Act
            var result = await _handler.Handle(new GetTicketsQuery(1, 10), CancellationToken.None);

            // Assert
            result.Items.Should().NotBeEmpty();
            result.Items.Count.Should().Be(1);
        }
    }
}
