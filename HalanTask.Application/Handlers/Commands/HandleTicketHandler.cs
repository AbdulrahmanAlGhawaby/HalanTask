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
    public class HandleTicketHandler : IRequestHandler<HandleTicketCommand, int>
    {
        private readonly ITicketRepository _repository;

        public HandleTicketHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(HandleTicketCommand request, CancellationToken cancellationToken)
        {
            await _repository.HandleTicket(request.id);
            return 1;
        }
    }

}
