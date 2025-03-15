using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Application.Requests.Commands
{
    public record HandleTicketCommand(int id) : IRequest<int>;

}
