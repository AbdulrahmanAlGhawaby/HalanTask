using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Application.Requests.Commands
{
    public record CreateTicketCommand(string PhoneNumber, string Governorate, string City, string District) : IRequest<int>;

}
