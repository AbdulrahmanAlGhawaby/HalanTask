using HalanTask.Application.Requests.Queries;
using HalanTask.Domain.Models;
using HalanTask.Domain.Utilities;
using HalanTask.Infrastructure.Context;
using HalanTask.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Application.Handlers.Queries
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, PaginatedList<Ticket>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<PaginatedList<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            return await _ticketRepository.GetPaginatedTickets(request.Page, request.PageSize);
        }
    }
}
