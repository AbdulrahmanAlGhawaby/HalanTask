using HalanTask.Application.Requests.Commands;
using HalanTask.Application.Requests.Queries;
using HalanTask.Domain.Models;
using HalanTask.Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HalanTask.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketCommand command)
        {
            var ticketId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTickets), new { id = ticketId }, ticketId);
        }

        [HttpGet]
        public async Task<PaginatedList<Ticket>> GetTickets(int page = 0, int pageSize = 10)
        {
            var tickets = await _mediator.Send(new GetTicketsQuery(page, pageSize));
            return tickets;
        }

        [HttpPost]
        public async Task HandleTicket(int Id)
        {
            await _mediator.Send(new HandleTicketCommand(Id));
        }
    }

}
