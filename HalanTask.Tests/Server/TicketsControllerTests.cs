using HalanTask.Application.Requests.Queries;
using HalanTask.Domain.Models;
using HalanTask.Domain.Utilities;
using HalanTask.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Tests.Server
{
    public class TicketsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TicketController _controller;

        public TicketsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TicketController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetTickets_ShouldReturnOkResult()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTicketsQuery>(), default))
                .ReturnsAsync(new PaginatedList<Ticket>([new Ticket { Id = 1, PhoneNumber = "1234567890" }], 1, 1, 10));

            // Act
            var result = await _controller.GetTickets();

            // Assert
            Assert.IsType<PaginatedList<Ticket>>(result);
        }
    }
}
