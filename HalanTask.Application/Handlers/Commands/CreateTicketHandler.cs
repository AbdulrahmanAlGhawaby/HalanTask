using HalanTask.Application.Requests.Commands;
using HalanTask.Domain.Models;
using HalanTask.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Application.Handlers.Commands
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly ITicketRepository _repository;

        public CreateTicketHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                PhoneNumber = request.PhoneNumber,
                Governorate = request.Governorate,
                City = request.City,
                District = request.District
            };
            await _repository.AddAsync(ticket);
            return ticket.Id;
        }
    }

}
