using HalanTask.Domain.Models;
using HalanTask.Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Application.Requests.Queries
{
    public record GetTicketsQuery(int Page, int PageSize) : IRequest<PaginatedList<Ticket>>;

}
